using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static System.Windows.Forms.Design.AxImporter;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void txtPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is not a digit and is not a control key (like backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // If it's not, suppress the key press event
                e.Handled = true;
            }
        }

        private async void btnDownload_ClickAsync(object sender, EventArgs e)
        {


            progressBar1.Value = 0;
            progressBar1.Maximum = 100;
            backgroundWorker1.RunWorkerAsync();
        }

        private async void StartDownloadImg(string url, int pageNumber)
        {

            List<string> imageUrls = new List<string>();

            // Replace with your image URLs

            // string url = "https://drive.google.com/viewer2/prod-01/img?ck=drive&ds=APznzaYZP6erlpM7wCFIYiFJATvEiN560edVHU0l2B5ce2v7-_Dr9X9e7PBH8yLG1OL1DLrIBf1yRMka7U3aGyQqQ2Mof_Z_9kfrhKBBv1c-p-E30joRXNWb2_5VWbx1H-7QLoR3fX-gGr8kgF92MOB7xawrThgyl3WOJgbR6gLz5UWnMcsbRAsiJK45VXJ-Pi44whlYGLFUf-9F4FrNCU1GHydU6ch8yVwW4zdfdjX3uIbsjWa-3Z1cBX2Z7XjqwnOWYStAlZIUpJFk12zW9S_5nKHFGOpRsSILQB6WJuk9p1M_YkaNLHubE4PDH-XjErniLaJNVxGKlJZ2q_uNHt8iSWQnPBG0ETBB6hzEkyQWklymOSwYelCmrsq-EY_zbbIlHFbJTD_fVt0AwV_svcXhyWLWufoFCYbBcureR-gsUiUjZezXdI0%3D&authuser=0&page=0&skiphighlight=true&w=1600&webp=true";

            // Create a DataTable
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("String Data", typeof(string)); // Assuming you want a single column named "String Data"


            // Parse the URL
            UriBuilder uriBuilder = new UriBuilder(url);
            NameValueCollection queryParameters = HttpUtility.ParseQueryString(uriBuilder.Query);

            for (int i = 0; i < pageNumber; i++)
            {
                // Modify the page parameter
                queryParameters["page"] = i.ToString(); // Change to whatever page number you need
                // Update the query string in the UriBuilder
                uriBuilder.Query = queryParameters.ToString();


                imageUrls.Add(uriBuilder.ToString());

                dataTable.Rows.Add(uriBuilder.ToString());
            }


            // Bind the DataTable to the DataGridView
            dataGridView1.DataSource = dataTable;

        }

        private bool IsValidUrl(string url)
        {
            string pattern = @"^(http|https|ftp)://[^\s/$.?#].[^\s]*$";
            Regex regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return regex.IsMatch(url);
        }

        private bool IsValidPageNumber(string pageNumber)
        {
            return int.TryParse(pageNumber, out _);
        }
        private async Task<System.Drawing.Image> DownloadImageAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    {
                        return System.Drawing.Image.FromStream(stream);
                    }
                }
                catch (HttpRequestException e)
                {
                    MessageBox.Show($"Request error: {e.Message}");
                    return null;
                }
                catch (IOException e)
                {
                    MessageBox.Show($"IO error: {e.Message}");
                    return null;
                }
            }
        }

        private void RunSelenium(string url, int pageNumber, string name, System.ComponentModel.DoWorkEventArgs e)
        {

            // var isFindViewer = false;
            var options = new ChromeOptions();
            //   options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            //   options.AddArgument("start-maximized"); // Maximize browser window on startup
            //   options.AddArgument("disable-infobars"); // Disable info bars that cover the browser window
            options.AddExcludedArgument("disable-popup-blocking");

            //  progressBar1.Maximum = pageNumber;

            using (var driver = new ChromeDriver(options))
            {

                // _ =   GetReponseFromByNetworkAsync(driver);
                GetReponseFromByNetworkAsync(driver, pageNumber, e);


                driver.Navigate().GoToUrl(url);


                // Find the div element by its text content using XPath

                // Execute JavaScript using IJavaScriptExecutor
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                // Your JavaScript code
                string script = $@"
                    var aTags = document.getElementsByTagName('div');
                    var searchText = '{name}';
                    var found;

                    for (var i = 0; i < aTags.length; i++) {{
                        if (aTags[i].textContent.trim() === searchText) {{
                            found = aTags[i];
                            found.click();
                            break;
                        }}
                    }}
            ";
                // Execute the script
                js.ExecuteScript(script);


                System.Threading.Thread.Sleep(15000); // Adjust as needed

                //   driver.Quit();
            }


        }

        private async void GetReponseFromByNetworkAsync(ChromeDriver driver, int pageNumber, System.ComponentModel.DoWorkEventArgs e)
        {
            var isFindViewer = false;
            var session = ((IDevTools)driver).GetDevToolsSession();
            var domains = session.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V126.DevToolsSessionDomains>();


            var getHotelRoomListRequestId = "";
            var url = "";
            // Request ID get Data
            domains.Network.ResponseReceived += (_, response) =>
            {
                e.Result = response.Response.Url;
                //  Console.WriteLine($"ResponseReceived {response.RequestId} => {response.Response.Url} ");
                if (response.Response.Url.Contains("https://drive.google.com/viewer2/prod-01/img?ck=drive"))
                {
                    if (!isFindViewer)
                    {
                        getHotelRoomListRequestId = response.RequestId;
                        isFindViewer = true;

                        StartDownloadImg(response.Response.Url, pageNumber);
                    }

                }
            };

            domains.Network.LoadingFinished += async (_, response) =>
            {
                var requestID = response.RequestId;
                e.Result = $"LoadingFinished => {requestID} ";
                if (getHotelRoomListRequestId == requestID)
                {
                    try
                    {
                        var responseBody = await domains.Network.GetResponseBody(new OpenQA.Selenium.DevTools.V126.Network.GetResponseBodyCommandSettings
                        {
                            RequestId = requestID
                        });
                        e.Result = $"Loading Finished Body => {requestID} ";
                        //   e.Result  = $"Loading Finished Body => {responseBody.Body} ";
                    }
                    catch (Exception ex)
                    {
                        e.Result = $"Loading Fail: {requestID}";
                        //   Console.WriteLine($"Loading Finished({requestID}) fail: {ex.Message}");
                    }
                }

            };
            _ = await domains.Network.Enable(new OpenQA.Selenium.DevTools.V126.Network.EnableCommandSettings() { });

        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string url = txtURL.Text;
            string pageNumber = txtPage.Text;
            string name = txtName.Text;
            btnExportPDF.Enabled = false;

            if (IsValidUrl(url) && IsValidPageNumber(pageNumber) && !string.IsNullOrEmpty(name))
            {
                RunSelenium(url, int.Parse(pageNumber), name, e);

            }
            else
            {
                MessageBox.Show("Vui lòng nhập Link và Page, Name đúng, không được để trống.");
            }

        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            // Update UI with the result from background operation
            progressBar1.Value = e.ProgressPercentage;
        }

        private async void btnExportPDF_ClickAsync(object sender, EventArgs e)
        {
            //flowLayoutPanel1.Controls.Clear();


            //foreach (string imgUrl in imageUrls)
            //{
            //    // lblLoading.Text = $"Show: " + imgUrl;

            //    PictureBox pb = new PictureBox();
            //    pb.SizeMode = PictureBoxSizeMode.StretchImage;
            //    pb.Width = 160;
            //    pb.Height = 227;

            //    Image image = await DownloadImageAsync(imgUrl);
            //    if (image != null)
            //    {
            //        pb.Image = image;
            //        flowLayoutPanel1.Controls.Add(pb);
            //    }
            //    else
            //    {
            //        MessageBox.Show($"Download image from {imgUrl}");
            //    }

            //    progressBar1.Value = imageUrls.IndexOf(imgUrl);
            //    if (progressBar1.Value == progressBar1.Maximum)
            //    {
            //        btnExportPDF.Enabled = true;
            //    }
            //}
        }
    }
}
