
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

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

        private void btnDownload_Click(object sender, EventArgs e)
        {

            if (!backgroundWorker1.IsBusy)
            {
                this.btnDownload.Enabled = false;
                this.btnDownload.Text = "Downloading...";
               // this.TopMost = true;
                progressBar1.Value = 0;
                flowLayoutPanel1.Controls.Clear();

                backgroundWorker1.RunWorkerAsync();
            }

        }

        private void StartDownloadImg(string url, int pageNumber)
        {
            dataGridView1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
                progressBar1.Maximum = pageNumber;
                // Create a DataTable
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Page", typeof(int)); // Assuming you want a single column named "String Data"
                dataTable.Columns.Add("URL Image", typeof(string)); // Assuming you want a single column named "String Data"


                // Parse the URL
                UriBuilder uriBuilder = new UriBuilder(url);
                NameValueCollection queryParameters = HttpUtility.ParseQueryString(uriBuilder.Query);

                for (int i = 0; i < pageNumber; i++)
                {
                    // Modify the page parameter
                    queryParameters["page"] = i.ToString(); // Change to whatever page number you need
                    queryParameters["w"] = "1600"; // Change to whatever page number you need
                                                   // Update the query string in the UriBuilder
                    uriBuilder.Query = queryParameters.ToString();

                    DataRow row = dataTable.NewRow();
                    row["Page"] = i + 1;
                    row["URL Image"] = uriBuilder.ToString();
                    dataTable.Rows.Add(row);

                    this.progressBar1.Value = (i + 1);

                    if (pageNumber == i + 1)
                    {
                        //this.TopMost = false;
                        this.btnDownload.Text = "Downloaded";
                    }
                }

                this.dataGridView1.DataSource = dataTable;

            }));



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
        private async Task<System.Drawing.Image > DownloadImageAsync(string url)
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

        private void updateLable(string text)
        {
            lblLoading.Invoke(new Action(() =>
            {
                var _txt = text;

                if (text.Length > 100)
                {
                    _txt = text.Substring(0, 100) + "....";
                }
                lblLoading.Text = _txt;
            }));
        }

        private void RunSelenium(string url, int pageNumber, string name, System.ComponentModel.DoWorkEventArgs e)
        {

            // var isFindViewer = false;
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            // options.AddArgument("--disable-gpu");
            options.AddArgument("--start-maximized"); // Start maximized
            options.AddArgument("--window-size=1920,1080"); // Set specific window size
            options.AddExcludedArgument("disable-popup-blocking");

            //  progressBar1.Maximum = pageNumber;

            using (var driver = new ChromeDriver(options))
            {

                _ = driver.Manage().Timeouts().ImplicitWait;

                driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);


                // _ =   GetReponseFromByNetworkAsync(driver);
                GetReponseFromByNetworkAsync(driver, pageNumber, e);


                driver.Navigate().GoToUrl(url);


                // Wait until the page is fully loaded
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(driver =>
                {
                    return ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").ToString().Equals("complete");
                });

                // Perform actions after the page is fully loaded
                Console.WriteLine("Page is fully loaded");


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


                // Execute JavaScript using IJavaScriptExecutor
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                // Execute the script
                js.ExecuteScript(script);

                System.Threading.Thread.Sleep(5000);

                // Locate an element by its inner text using XPath
                IWebElement element = driver.FindElement(By.XPath($"//*[text()='{name}']"));

                // Perform actions on the element
                Console.WriteLine("Element found: " + element.Text);


                new Actions(driver).Click(element).Perform();

                System.Threading.Thread.Sleep(3000);

                new Actions(driver).ScrollByAmount(0, 300).Perform();

                // System.Threading.Thread.Sleep(5000);

                driver.Quit();
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

                updateLable($"ResponseReceived {response.RequestId} => {response.Response.Url} ");
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
                updateLable($"LoadingFinished => {requestID} ");
                if (getHotelRoomListRequestId == requestID)
                {
                    try
                    {
                        var responseBody = await domains.Network.GetResponseBody(new OpenQA.Selenium.DevTools.V126.Network.GetResponseBodyCommandSettings
                        {
                            RequestId = requestID
                        });

                    }
                    catch (Exception ex)
                    {

                        updateLable($"Loading Fail: {requestID}");
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
            btnDownload.Enabled = false;

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

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            // Start the background operation
            if (!bgwDownloadImage.IsBusy)
            {
                this.btnDownload.Enabled = false;
               // this.TopMost = true;
                this.btnDownload.Text = "Exporting...";

                bgwDownloadImage.RunWorkerAsync();
            }
        }

        private void bgwDownloadImage_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            _ = flowLayoutPanel1.Invoke(new Action(async () =>
            {
                progressBar1.Value = 0;
                flowLayoutPanel1.Controls.Clear();
                var pdfImgs = new List<System.Drawing.Image>();

                if (this.dataGridView1.Rows != null && this.dataGridView1.Rows.Count > 0)
                {
                    int index = 1;
                    foreach (DataGridViewRow row in this.dataGridView1.Rows)
                    {
                        // lblLoading.Text = $"Show: " + imgUrl;
                        var imgUrl = row.Cells["URL Image"].Value?.ToString();

                        if (!string.IsNullOrEmpty(imgUrl))
                        {

                            PictureBox pb = new PictureBox();
                            pb.SizeMode = PictureBoxSizeMode.StretchImage;
                            pb.Width = 80;
                            pb.Height = 113;
                            pb.Click += this.pictureBox1_Click;

                            var image = await DownloadImageAsync(imgUrl);

                            if (image != null)
                            {
                                pb.Image = image;
                                flowLayoutPanel1.Controls.Add(pb);

                                pdfImgs.Add(image);
                            }
                            else
                            {
                                MessageBox.Show($"Download image from {imgUrl}");
                            }

                            progressBar1.Value = index;
                            dataGridView1.Rows[index-1].Selected = true;
                            index++;

                           // image.Dispose();
                        }
                    }

                    // save PDF
                    // Create a SaveFileDialog and set properties
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                    saveFileDialog1.FilterIndex = 1;
                    saveFileDialog1.RestoreDirectory = true;
                    saveFileDialog1.FileName = this.txtName.Text;

                    // Show the dialog and process the result
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string pdfFilePath = saveFileDialog1.FileName;
                        ConvertImagesToPdf(pdfImgs, pdfFilePath);

                    }
                }

            }));
        }

        public async void ConvertImagesToPdf(List<System.Drawing.Image> Images, string outputPath)
        {
            await Task.Run(() =>
            {

                this.btnDownload.Invoke(new Action(() =>
                {
                    this.progressBar1.Value = 0;
                    this.progressBar1.Maximum = Images.Count;

                    using (PdfDocument pdf = new PdfDocument())
                    {
                        int index = 1;
                        foreach (System.Drawing.Image img in Images)
                        {

                            int imgWidth = img.Width;
                            int imgHeight = img.Height;

                            // Calculate the aspect ratio of the image
                            double aspectRatio = (double)imgWidth / imgHeight;

                            // Set the desired page size (A4: 210mm x 297mm)
                            XSize pageSize = new XSize(XUnit.FromMillimeter((double)210), XUnit.FromMillimeter((double)297));

                            // Determine the size of the image when fitted to the page
                            double pageWidth = pageSize.Width;
                            double pageHeight = pageSize.Height;
                            double imgWidthOnPage = pageWidth;
                            double imgHeightOnPage = imgWidthOnPage / aspectRatio;

                            // Check if the image height fits the page height
                            if (imgHeightOnPage > pageHeight)
                            {
                                imgHeightOnPage = pageHeight;
                                imgWidthOnPage = imgHeightOnPage * aspectRatio;
                            }

                            // Create a PDF page with the calculated dimensions
                            PdfPage page = pdf.AddPage();
                            page.Width = pageSize.Width;
                            page.Height = pageSize.Height;

                            XGraphics gfx = XGraphics.FromPdfPage(page);

                            // Convert System.Drawing.Image to XImage
                            XImage image = ConvertImageToXImage(img);

                            // Calculate position to center the image on the page
                            double x = (pageWidth - imgWidthOnPage) / 2;
                            double y = (pageHeight - imgHeightOnPage) / 2;

                            // Draw the image on the PDF page
                            gfx.DrawImage(image, x, y, imgWidthOnPage, imgHeightOnPage);

                            this.progressBar1.Value = index;
                            

                          //  img.Dispose();
                          //  image.Dispose();
                            index++;
                        }

                        pdf.Save(outputPath);

                    }

                    this.btnDownload.Enabled = true;
                    this.btnDownload.Text = "Start";
                    //this.TopMost = false;

                    // Clear the list
                    Images.Clear();

                    MessageBox.Show($"Export PDF completed!");
                }));
            });
        }

        private XImage ConvertImageToXImage(System.Drawing.Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // Save image to memory stream as PNG (or any other format supported by PdfSharp)
                return XImage.FromStream(ms); // Create XImage from memory stream
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Create a new form for zoomed view
            Form zoomForm = new Form();
            zoomForm.TopMost = true;
            zoomForm.Text = "Zoomed View";
            zoomForm.Size = new Size(800, 600); // Set your desired size
            zoomForm.StartPosition = FormStartPosition.CenterParent; // or set as desired

            // Add a PictureBox to the zoomForm to display the image
            PictureBox pictureBoxZoom = new PictureBox();
            pictureBoxZoom.Dock = DockStyle.Fill;
            pictureBoxZoom.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxZoom.Image = ((PictureBox)sender).Image; // Assign the same image as the original PictureBox

            // Add PictureBox to zoomForm
            zoomForm.Controls.Add(pictureBoxZoom);

            // Show the zoomForm as a dialog (optional)
            zoomForm.ShowDialog();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            btnExportPDF_Click(sender, e);
        }

        private void bgwDownloadImage_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {


        }
    }
}
