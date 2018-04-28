using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadWriteFile
{
    class CreateFile
    {
        public static ResponseVal ConvertHmlToPDF(string QuoteISN, string StaffISN)
        {
            Globals.WriteLog("[ConvertHmlToPDF_CreateFile]: ----------Begin----------");
            Globals.WriteLog("[ConvertHmlToPDF_CreateFile]: QuoteISN: " + QuoteISN);
            Globals.WriteLog("[ConvertHmlToPDF_CreateFile]: StaffISN: " + StaffISN);
            ResponseVal resVal = new ResponseVal();
            var result = -1;

            //bool rsCheck = XSettings.InstallLicense("X/VKS08wmMtAun4hGNvFONzmS/QQY7hZ9Z2488LHIg8X5nu5Qx7dYsZhez00hWZRXd5Xim0uoXp3ifxwDtAusQ0lPTnPXR1401Y=");

            //bool rsCheck = XSettings.InstallLicense("XeJREBodo/8B4nxZb63WaYOgeuQZPdtypqn27rLhKmkRz3CDGnvCaco3Dn5c5nQFBw==");
            //bool rsCheck = XSettings.InstallLicense("XeJREBodo/8B7XFQaf2CPdzyKuccPdtypszj/8HiXH0n+nieP1jmdZIuAHpU7kIFBw==");
            //bool rsCheck = XSettings.InstallLicense("X/VKS0cNn5FipytaG9r2LN6gO9YNAb8f0JfhndPDLmJ14X+2ABmFVcU9cz81hwBrU4M7olk+wz9WgdFFKeN5mjkhfR3iZgJkr1Y=");

            bool rsCheck = XSettings.InstallLicense(Functions.GetConfig_CS("AbcPdfLicense"));

            if (!rsCheck)
            {
                resVal.Code = -1;
                resVal.Msg = "Invalid Websupergoo license.";
                return resVal;
            }

            var ds = Globals.DB.ExecuteQuery(string.Format("select MemberISN from Vw_SolarQuote where QuoteISN={0}", Functions.ConvertObjectToInt(QuoteISN)));

            if (Functions.IsEmpty(ds))
            {
                resVal.Code = -1;
                resVal.Data = null;
                resVal.Msg = "QuoteISN is missing";
                Globals.WriteLog("[ConvertHmlToPDF_CreateFile]: Msg: " + resVal.Msg);
                Globals.WriteLog("[ConvertHmlToPDF_CreateFile]: ----------End----------");
                return resVal;
            }

            var row = ds.Tables[0].Rows[0];
            var MemberISN = row["MemberISN"].ToString();
            string url = string.Format("{0}/Proposal.aspx?isn={1}&staffisn={2}",
                Functions.GetConfig_CS("AdminURL").TrimEnd('/'), QuoteISN, StaffISN);

            Globals.WriteLog("[ConvertHmlToPDF_CreateFile] -- url: " + url);

            string Temp = Functions.GetConfig_CS("TemporaryDir");
            string outputPath = HttpContext.Current.Server.MapPath("~/OutputFiles/Pdf");
            string sFileName = "";
            string sFullPathTemp = "";

            #region Create PDF
            try
            {
                Doc pages = new Doc();
                try
                {
                    Doc doc = new Doc();
                    //doc.Rect.Inset(30, 10);
                    //doc.HtmlOptions.Engine = EngineType.MSHtml;
                    //doc.HtmlOptions.Engine = EngineType.Chrome;
                    //doc.HtmlOptions.Engine = EngineType.MSHtml;
                    //doc.SetInfo(0, "RenderDelay", "500");
                    //doc.SetInfo(0, "OneStageRender", 0);

                    doc.HtmlOptions.FontEmbed = true;
                    doc.HtmlOptions.FontSubstitute = false;
                    doc.HtmlOptions.FontProtection = false;
                    doc.HtmlOptions.BrowserWidth = 1200;

                    doc.HtmlOptions.PageCacheClear();
                    doc.HtmlOptions.UseScript = true;
                    //doc.HtmlOptions.OnLoadScript = "(function(){window.ABCpdf_go = false; setTimeout(function(){window.ABCpdf_go = true;}, 3000);})();";


                    // Render after 3 seconds
                    //doc.HtmlOptions.OnLoadScript = " (function(){"
                    //  + " window.external.ABCpdf_RenderWait();"
                    //  + " setTimeout(function(){ "
                    //  + " window.external.ABCpdf_RenderComplete(); }, 5000);"
                    //  + "})();";
                    //doc.SetInfo(0, "RenderDelay", "5000");
                    //doc.SetInfo(0, "OneStageRender", 0);

                    //Render after 3 seconds
                    //doc.HtmlOptions.OnLoadScript = "(function(){"
                    //  + " window.ABCpdf_go = false;"
                    //  + " setTimeout(function(){ window.ABCpdf_go = true; }, 10000);"
                    //  + "})();";

                    doc.Page = doc.AddPage();
                    int theID;

                    theID = doc.AddImageUrl(url);

                    while (true)
                    {
                        doc.FrameRect(); // add a black border
                        if (!doc.Chainable(theID))
                            break;
                        doc.Page = doc.AddPage();
                        theID = doc.AddImageToChain(theID);

                        System.Threading.Thread.Sleep(500);
                    }

                    //doc.Rect.String = "100 50 500 150";                    

                    //for (int i = 1; i <= doc.PageCount; i++)
                    //{
                    //    doc.PageNumber = i;
                    //    doc.AddText("Page " + i.ToString());
                    //    //doc.FrameRect();
                    //}

                    pages.Append(doc);
                }
                catch (Exception ex2)
                {
                    resVal.Code = -1;
                    resVal.Data = null;
                    resVal.Msg = ex2.Message;
                    Globals.WriteLog("[ConvertHmlToPDF_CreateFile] - Exception: " + ex2.Message);
                    Globals.WriteLog("[ConvertHmlToPDF_CreateFile]: ----------End----------");
                    return resVal;
                }

                if (!System.IO.File.Exists(Temp))
                {
                    System.IO.Directory.CreateDirectory(Temp);
                }

                Globals.WriteLog("[ConvertHmlToPDF_CreateFile] Temp: " + Temp);

                sFileName = "Proposal_" + DateTime.Now.Ticks.ToString() + ".pdf";
                sFullPathTemp = Path.Combine(Temp, sFileName);

                pages.Save(sFullPathTemp);
                pages.Clear();

                string sDocName = "Proposal_" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Year + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;

                result = (int)Globals.DB.ExecuteStoredProc("xp_debtext_documenttask_insupd",
                    new string[] { "DocumentISN", "docFileName", "docName", "docType", "docPublic", "MemberISN" },
                    new object[] { 0, sFileName, sDocName, "Proposal", 1, MemberISN });

                var sPathFileDoc = Path.Combine(Functions.GetConfig_CS("FilesDir"), GetDocumentsPath(result, null));

                if (!System.IO.File.Exists(sPathFileDoc))
                {
                    System.IO.Directory.CreateDirectory(sPathFileDoc);
                }

                var sFileSave = Path.Combine(sPathFileDoc, sFileName);
                File.Copy(sFullPathTemp, sFileSave);
                //File.Delete(sFullPathTemp);                
                resVal.Code = 1;
                resVal.Data = result;
                resVal.Msg = "Success";

                Globals.WriteLog("[ConvertHmlToPDF_CreateFile] sFullPathTemp: " + sFullPathTemp);
                Globals.WriteLog("[ConvertHmlToPDF_CreateFile] sPathFileDoc: " + sFileSave);
                Globals.WriteLog("[ConvertHmlToPDF_CreateFile] sFileName: " + sFileName);
                Globals.WriteLog("[ConvertHmlToPDF_CreateFile] DocumentISN: " + result);

            }
            catch (Exception ex)
            {
                resVal.Code = -1;
                resVal.Data = null;
                resVal.Msg = ex.Message;
                Globals.WriteLog("[ConvertHmlToPDF_CreateFile] -- Exception: " + ex.Message);
            }
            Globals.WriteLog("[ConvertHmlToPDF_CreateFile]: ----------End----------");
            #endregion

            #region old
            // string fileName = DateTime.Now.Ticks + ".pdf";
            // string pathFileName = Path.Combine(Functions.GetConfig_CS("TemporaryDir"), fileName);
            // string ContentHtml = ReadFromFile(pathFileHtml);

            // //StringBuilder sb = new StringBuilder();
            // //sb.Append(ContentHtml);

            // //var css = ContentHtml.Substring(ContentHtml.IndexOf("<style>") + 7, ContentHtml.IndexOf("</style>") - 7 - ContentHtml.IndexOf("<style>"));
            //// var js = ContentHtml.Substring(ContentHtml.IndexOf("<script>") + 8, ContentHtml.IndexOf("</script>") - 8 - ContentHtml.IndexOf("<script>"));

            // StringReader sr = new StringReader(ContentHtml);

            // //StringWriter sw = new StringWriter();

            // //HtmlTextWriter hw = new HtmlTextWriter(sw);
            // //hw.Write(ContentHtml);
            // Document pdfDoc = new Document(PageSize.A4);
            // //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);


            // using (MemoryStream memoryStream = new MemoryStream())
            // {
            //     PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);                
            //     pdfDoc.Open();

            //     //htmlparser.Parse(new StringReader(sw.ToString()));
            //     ////HTMLWorker.ParseToList(sr, new StyleSheet());
            //     //pdfDoc.Close();

            //     //StyleSheet ss = new StyleSheet();

            //     //ArrayList list = HTMLWorker.ParseToList(new StringReader(ContentHtml), ss);
            //     //foreach (IElement e in list)
            //     //{
            //     //    pdfDoc.Add(e);
            //     //          
            //     //using (TextReader sReader = new StringReader(ContentHtml.ToString()))
            //     //{
            //     //    ArrayList list = HTMLWorker.ParseToList(sReader, new StyleSheet());
            //     //    foreach (IElement elm in list)
            //     //    {
            //     //        pdfDoc.Add(elm);
            //     //    }
            //     //}
            //     //pdfDoc.HtmlStyleClass = ContentHtml;
            //     //pdfDoc.JavaScript_onLoad = ContentHtml;
            //     //htmlparser.Parse(sr);
            //     // step 5               
            //     //Image img = Image.GetInstance(ContentHtml);
            //     //pdfDoc.Add(img);
            //     //writer.AddJavaScript(ContentHtml);
            //     iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);

            //     //using (var srHtml = new StringReader(ContentHtml))
            //     //{

            //     //    //Parse the HTML
            //     //    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, srHtml);
            //     //}

            //     //using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(example_css)))
            //     //{
            //     //    using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(ContentHtml)))
            //     //    {

            //     //        Parse the HTML
            //     //        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, msHtml, msCss);
            //     //    }
            //     //}

            //     pdfDoc.Close();

            //     byte[] bytes = memoryStream.ToArray();

            //     //BinaryFormatter bf = new BinaryFormatter();
            //     //MemoryStream ms = new MemoryStream();
            //     //bf.Serialize(ms, ContentHtml);
            //     //bytes = ms.ToArray();

            //     pathFileName = pathFileName + ".pdf";
            //     System.IO.File.WriteAllBytes(pathFileName, bytes);
            //     memoryStream.Close();
            //     File.Copy(Path.Combine(Functions.GetConfig_CS("TemporaryDir"), fileName + ".pdf"), Path.Combine(Functions.GetConfig_CS("FilesConvertPdf"), fileName + ".pdf"), true);
            //     resVal.Code = 1;
            //     resVal.Data = pathFileName;
            //     resVal.Msg = "Success";
            // }
            #endregion

            return resVal;

        }
    }
}
