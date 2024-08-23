using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace extracting_inline_styles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string html = @"<!DOCTYPE html>
<html>
<head>
    <meta http-equiv=""Content-Type"" content=""text/html charset=UTF-8"" />
</head>
<body style=""margin: 38px;"">
    <div style=""font-family: Arial, sans-serif; font-size: 13px; color: #000; margin: 0 auto;width: 100%; position: relative;"">
        <table border=""0"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""width: 100%; margin: 0 auto; border-spacing: 0; background-color: white;"">
            <tr>
                <td style=""padding: 25px 0; border-radius: 2px;"">
                    <table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""width: 100%; border-spacing: 0;"">
                        <tr>
                            <td colspan=""2"" style=""padding: 0px 30px 10px;"">
                                <a href=""https://ohmyhotel.com/"" target=""_blank"">
                                    <img src=""https://photos.ohmytrip.com/logo/ico-email-logo@2x.png"" alt=""OHMYHOTEL&CO"" height=""40"" />
                                </a>
                            </td>
                        </tr>
                        <tr>
                            <td align=""left"" valign=""bottom"" style=""padding: 0 0 0 30px;"">
                                <strong style=""font-family: Arial, sans-serif; font-size: 20px; line-height: 23px;"">Invoice</strong>
                            </td>
                            <td align=""right"" style=""padding-right: 30px;"">
                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border-spacing: 0; border: 1px solid #DADADA; border-radius: 2px; "">
                                    <tr>
                                        <td style=""padding: 10px 15px 10px 20px; text-align: right;"">
                                            <strong style=""font-family: Arial, sans-serif; font-size: 13px; line-height: 15px; font-weight: bold;"">Invoice No.</strong><br />
                                            <strong style=""font-family: Arial, sans-serif; font-size: 17px; line-height: 20px; font-weight: bold;"">{{SellerInvoiceNo}}</strong>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            
                        </tr>
                        <tr>
                            <td align=""left"" valign=""top"" style=""padding-left: 30px; padding-bottom: 28px; padding-top: 57px;"">
                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border-spacing: 0;"">
                                    <tr>
                                        <td colspan=""2"" style=""padding-bottom: 20px;"">
                                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border-spacing: 0;"">
                                                <tr>
                                                    <td style=""padding-bottom: 3px;"">
                                                        <strong style=""font-family: Arial, sans-serif; font-size: 13px; line-height: 15px; font-weight: bold;"">Bill To</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span style=""font-family: Arial, sans-serif; font-size: 13px; line-height: 15px;"">
                                                            {{SellerNameForInvoice}} <br />
                                                        </span>
															{{SellerEmail}}
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style=""padding-bottom: 20px; padding-right: 50px;"">
                                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border-spacing: 0;"">
                                                <tr>
                                                    <td style=""padding-bottom: 3px;"">
                                                        <strong style=""font-family: Arial, sans-serif; font-size: 13px; line-height: 15px; font-weight: bold;"">Currency</strong> <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span style=""font-family: Arial, sans-serif; font-size: 13px; line-height: 15px;"">{{CurrencyCode}}</span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style=""padding-bottom: 20px;"">
                                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border-spacing: 0;"">
                                                <tr>
                                                    <td style=""padding-bottom: 3px;"">
                                                        <strong style=""font-family: Arial, sans-serif; font-size: 13px; line-height: 15px; font-weight: bold;"">Total Amount </strong> <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span style=""font-family: Arial, sans-serif; font-size: 13px; line-height: 15px;"">{{CurrencySymbol}}{{SellerInvoiceTotalAmount}}</span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan=""2"">
                                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border-spacing: 0;"">
                                                <tr>
                                                    <td style=""padding-bottom: 3px;"">
                                                        <strong style=""font-family: Arial, sans-serif; font-size: 13px; line-height: 15px; font-weight: bold;"">Check Out </strong> <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span style=""font-family: Arial, sans-serif; font-size: 13px; line-height: 15px;"">{{SellerInvoiceCheckOut}}</span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align=""right"" width=""230"" valign=""top"" style=""padding-right: 30px; padding-bottom: 28px; padding-top: 5px;"">
                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border-spacing: 0; width: 100%;"">
                                    <tr>
                                        <td style="" text-align: right;"">
                                            <strong style=""font-family: Arial, sans-serif; font-size: 12px; line-height: 14px; font-weight: bold;"">Invoice Date: </strong>
                                            <span style=""font-family: Arial, sans-serif; font-size: 12px; line-height: 14px;"">{{SellerInvoiceDate}}</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="" text-align: right; padding-top: 2px;"">
                                            <strong style=""font-family: Arial, sans-serif; font-size: 12px; line-height: 14px; font-weight: bold;"">Due Date: </strong>
                                            <span style=""font-family: Arial, sans-serif; font-size: 12px; line-height: 14px;"">{{SellerInvoiceDueDate}}</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="" padding-top: 18px;"">
                                            <table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border-spacing: 0;"">
                                                <tr>
                                                    <td style=""padding-bottom: 3px;"">
                                                        <strong style=""font-family: Arial, sans-serif; font-size: 13px; line-height: 15px; font-weight: bold;"">From</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span style=""font-family: Arial, sans-serif; font-size: 13px; line-height: 15px;"">
                                                            {{BranchOfficeCompanyName}}
                                                        </span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style=""position: relative;"">
                                            <div style=""font-family: Arial, sans-serif; font-size: 13px; line-height: 15px; padding-bottom: 10px;position: absolute; bottom: 10px; width: 100%;"">
                                                Approved by
                                            </div>
                                            <div style=""text-align: right;"">
                                                <img src=""https://photos.ohmytrip.com/sign/sign-kr.png"" style=""display: inline-block; width: 70%; margin-left: 10px; margin-bottom: -20px;"" alt=""sign"">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan=""2"">
                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""width: 100%; border-spacing: 0; border-collapse: collapse;"">
                                    <thead>
                                        <tr>
                                            <th width=""30"" height=""46""></th>
                                            <th width=""14"" height=""46"" align=""left"" style=""border-bottom: 1px solid #606060; padding-right: 10px; font-family: Arial, sans-serif; font-size: 14px; line-height: 23px; font-weight: bold;"">#</th>
                                            <th align=""left"" height=""46"" style=""border-bottom: 1px solid #606060;padding: 0 10px; font-family: Arial, sans-serif; font-size: 14px; line-height: 23px; font-weight: bold;"">Description</th>
                                            <th width=""20"" height=""46"" align=""center"" style=""border-bottom: 1px solid #606060; padding: 0 10px; font-family: Arial, sans-serif; font-size: 14px; line-height: 23px; font-weight: bold;"">Qty</th>
                                            <th width=""70"" height=""46"" align=""right"" style=""border-bottom: 1px solid #606060; padding-left: 10px; font-family: Arial, sans-serif; font-size: 14px; line-height: 23px; font-weight: bold; text-align: right;"">Price</th>
                                            <th width=""30"" height=""46""></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                       <tr style=""{{ReservationDisplay}}"">
                                            <td height=""46""></td>
                                            <td height=""46"" align=""left"" style=""padding-right: 10px; border-top: 1px solid #F0F0F0; font-family: Arial, sans-serif; font-size: 13px; line-height: 15px;"">{{ReservationOrder}}</td>
                                            <td height=""46"" align=""left"" style=""padding: 0 10px; border-top: 1px solid #F0F0F0; font-family: Arial, sans-serif; font-size: 13px; line-height: 15px;"">Reservation</td>
                                            <td height=""46"" align=""center"" style=""padding: 0 10px; border-top: 1px solid #F0F0F0; font-family: Arial, sans-serif; font-size: 13px; line-height: 15px;"">{{SellerInvoiceDetailQuantityReservation}}</td>
                                            <td height=""46"" align=""right"" style=""padding-left: 10px; border-top: 1px solid #F0F0F0; font-family: Arial, sans-serif; font-size: 13px; line-height: 15px; white-space: nowrap;"">{{CurrencySymbol}}{{SellerInvoiceDetailPriceReservation}}</td>
                                            <td height=""46""></td>
                                        </tr>
                                        <tr style=""{{CancellationDisplay}}"">
                                            <td height=""46""></td>
                                            <td height=""46"" align=""left"" style=""padding-right: 10px; border-top: 1px solid #F0F0F0; font-family: Arial, sans-serif; font-size: 13px; line-height: 15px;"">{{CancellationOrder}}</td>
                                            <td height=""46"" align=""left"" style=""padding: 0 10px; border-top: 1px solid #F0F0F0; font-family: Arial, sans-serif; font-size: 13px; line-height: 15px;"">Cancellation Fee</td>
                                            <td height=""46"" align=""center"" style=""padding: 0 10px; border-top: 1px solid #F0F0F0; font-family: Arial, sans-serif; font-size: 13px; line-height: 15px;"">{{SellerInvoiceDetailQuantityCancellation}}</td>
                                            <td height=""46"" align=""right"" style=""padding-left: 10px; border-top: 1px solid #F0F0F0; font-family: Arial, sans-serif; font-size: 13px; line-height: 15px; white-space: nowrap;"">{{CurrencySymbol}}{{SellerInvoiceDetailPriceCancellation}}</td>
                                            <td height=""46""></td>
                                        </tr>
                                    </tbody>
                                    <tfoot>
                                       <tr>
                                            <td style=""background-color: #FAFAFA; padding-top: 14px; padding-bottom: 14px;""></td>
                                            <td colspan=""3"" style=""text-align: right; background-color: #FAFAFA; padding: 14px 10px; font-family: Arial, sans-serif; font-size: 13px; line-height: 15px;"">Total Amount</td>
                                            <td style=""text-align: right;background-color: #FAFAFA; padding-top: 14px; padding-bottom: 14px; padding-left: 10px;  font-family: Arial, sans-serif; font-size: 16px; line-height: 18px; font-weight: bold; white-space: nowrap;"">{{CurrencySymbol}}{{SellerInvoiceTotalAmount}}</td>
                                            <td style=""background-color: #FAFAFA; padding-top: 14px; padding-bottom: 14px;""></td>
                                        </tr>
                                        <tr>
                                            <td style=""background-color: #FAFAFA; padding-bottom: 14px;""></td>
                                            <td colspan=""3"" style=""text-align: right; background-color: #FAFAFA; padding: 0 10px 14px; font-family: Arial, sans-serif; font-size: 13px; line-height: 15px;"">Total Paid</td>
                                            <td style=""text-align: right; background-color: #FAFAFA; padding-bottom: 14px; padding-left: 10px; font-family: Arial, sans-serif; font-size: 16px; line-height: 18px; font-weight: bold; white-space: nowrap;"">- {{CurrencySymbol}}{{SellerInvoiceTotalPaid}}</td>
                                            <td style=""background-color: #FAFAFA; padding-bottom: 14px;""></td>
                                        </tr>
                                        <tr>
                                            <td style=""background-color: #FAFAFA; padding-bottom: 14px;""></td>
                                            <td colspan=""3"" style=""text-align: right; background-color: #FAFAFA; color: #F20000; padding: 0 10px 14px; font-family: Arial, sans-serif; font-size: 13px; line-height: 15px; font-weight: bold;"">Balance</td>
                                            <td style=""text-align: right; background-color: #FAFAFA; color: #F20000; padding-bottom: 14px; padding-left: 10px; font-family: Arial, sans-serif; font-size: 16px; line-height: 18px; font-weight: bold; white-space: nowrap;"">{{CurrencySymbol}}{{SellerInvoiceBalance}}</td>
                                            <td style=""background-color: #FAFAFA; padding-bottom: 14px;""></td>
                                        </tr>
                                    </tfoot>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan=""2"">
                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""width: 100%; border-spacing: 0;"">
                                    <tr>
                                        <td width=""30""></td>
                                        <td align=""center"" style=""border-bottom: 1px solid #606060; padding: 30px 0; font-family: Arial, sans-serif; font-size: 20px; line-height: 23px;"">Thank you for your business</td>
                                        <td width=""30""></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan=""2"" style=""padding: 10px 30px 0;"" align=""center"">
                                <span style=""color: #000000; font-family: Arial; font-size: 12px; line-height: 20px;"">
                                     {{BranchOfficeAddress}} <br />
                                    Tel :  {{BranchOfficePhoneNumber}}  /  Fax :  {{BranchOfficeFaxNumber}}
                                </span> <br />
                                        <span style=""line-height: 34px; font-size: 12px; font-family: Arial; color: #555555;"">
                                            Copyright © Ohmyhotel&Co. Ltd. All rights reserved.
                                        </span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>";

            // Load the HTML document
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            // Dictionary to store the style and corresponding class name
            Dictionary<string, string> styleToClassMap = new Dictionary<string, string>();
            int classCounter = 1;

            // Process each node with a style attribute
            foreach (var node in document.DocumentNode.SelectNodes("//*[@style]"))
            {
                string style = node.GetAttributeValue("style", "");

                // Check if the style is already in the dictionary
                if (!styleToClassMap.ContainsKey(style))
                {
                    string className = "c-" + classCounter++;
                    styleToClassMap.Add(style, className);
                    node.SetAttributeValue("class", className);
                    node.Attributes.Remove("style");
                }
                else
                {
                    node.SetAttributeValue("class", styleToClassMap[style]);
                    node.Attributes.Remove("style");
                }
            }

            // Generate CSS classes
            StringBuilder cssBuilder = new StringBuilder();
            foreach (var style in styleToClassMap)
            {
                cssBuilder.AppendLine($".{style.Value} {{ {style.Key} }}");
            }

            // Output the transformed HTML
            string transformedHtml = document.DocumentNode.OuterHtml;

            // Output the generated CSS
            string generatedCss = cssBuilder.ToString();

            Console.WriteLine("Transformed HTML:");
            Console.WriteLine(transformedHtml);
            Console.WriteLine();
            Console.WriteLine("Generated CSS:");
            Console.WriteLine(generatedCss);
        }
    }
}
