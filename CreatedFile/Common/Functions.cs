using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using WebSupergoo.ABCpdf9;

namespace CreatedFile.Common
{
    class Functions
    {
        public static decimal ConvertObjectToDecimal(object obj, decimal defaultValue)
        {
            if (obj == null || obj is DBNull)
                return defaultValue;
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch { }
            return defaultValue;
        }

        public static int ConvertObjectToInt(object obj, int defaultValue)
        {
            if (obj == null || obj is DBNull)
                return defaultValue;
            try
            {
                return Convert.ToInt32(obj);
            }
            catch { }
            return defaultValue;
        }

        public static DateTime? ConvertToDate(object date)
        {
            try
            {
                if (date == null) return null;
                return Convert.ToDateTime(date);
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public static string ConvertToDateString(object date, string format)
        {
            var dDate = ConvertToDate(date);
            if (dDate == null)
            {
                return string.Empty;
            }
            else
            {
                return dDate.Value.ToString(format);
            }
        }

        public static bool IsEmptyDataSet(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) return false;
            return true;
        }

        /// <summary>
		/// Kiem tra mot object co rong,null or DBNull hay ko?.		
		/// Create By HTML
		/// </summary>
		/// <param name="obj">object can kiem tra. Object type: dataset, string, ... </param>
		/// <returns></returns>
		public static bool IsEmpty(object obj)
        {
            if (obj == null || obj is DBNull) return true;
            if (obj is DataSet)
            {
                DataSet ds = (DataSet)obj;
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    return true;
                return false;
            }
            if (obj is string && obj.ToString().Trim() == "")
                return true;
            return false;
        }

        /// <summary>
        /// Convert object(normal or get from DB) to String				
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ConvertString(object obj, string format)
        {
            if (obj == null || obj is DBNull)
                return String.Empty;
            try
            {
                if (obj is decimal)
                    return ((decimal)obj).ToString(format);
                if (obj is double)
                    return ((double)obj).ToString(format);
                if (obj is DateTime)
                    return ((DateTime)obj).ToString(format);

                if ((format != string.Empty))
                {
                    return Convert.ToDecimal(obj).ToString(format);
                }
            }
            catch
            {
            }
            return obj.ToString();
        }

        public static DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                // Load the XmlTextReader from the stream
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }

        public static StringBuilder ReadHtmlFile(string htmlFileNameWithPath)
        {
            StringBuilder htmlContent = new StringBuilder();
            string line;
            try
            {
                using (StreamReader htmlReader = new StreamReader(htmlFileNameWithPath))
                {
                    while ((line = htmlReader.ReadLine()) != null)
                    {
                        htmlContent.Append(line);
                    }
                }
            }
            catch (Exception objError)
            {
                throw objError;
            }

            return htmlContent;
        }

        public static bool WriteFileHtml(string dataHtml, string pathSave)
        {
            try
            {
                using (FileStream fs = new FileStream(pathSave, FileMode.Create))
                {
                    using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                    {
                        w.WriteLine(dataHtml);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return false;
        }

        #region DownloadFileExcel
        public static void DownloadFileExcel(DataTable dt)
        {

            try
            {
                DownloadFileExcel(dt, false);
            }
            catch (Exception ex)
            {
                WriteLog("DownloadFileExcel - Msg: " + ex.Message);
            }
        }

        public static void DownloadFileExcelColumnIndex(DataTable dt)
        {

            try
            {
                DownloadFileExcel(dt, true);
            }
            catch (Exception ex)
            {
                WriteLog("DownloadFileExcel - Msg: " + ex.Message);
            }
        }

        public static void DownloadFileExcel(DataTable dt, bool firstRow)
        {

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
            }
            if (firstRow)
            {
                dt.Columns.Add("No.", typeof(Int16)).SetOrdinal(0);
                int RowIndex = 0;
                foreach (DataRow row in dt.Rows)
                {
                    ++RowIndex;
                    row["No."] = RowIndex;
                }
                dt.AcceptChanges();
            }
            string filename = "RecordFailed_" + DateTime.Now.Ticks + ".xls";
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            GridView dgGrid = new GridView();
            dgGrid.DataSource = dt;
            dgGrid.DataBind();

            //(start): require for date format issue
            HtmlTextWriter hw2 = new HtmlTextWriter(tw);

            foreach (GridViewRow r in dgGrid.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    for (int columnIndex = 0; columnIndex < r.Cells.Count; columnIndex++)
                    {
                        if (columnIndex == 1) // column Client ID
                            r.Cells[columnIndex].Attributes.Add("class", "text");
                    }
                }
            }
            //(end): require for date format issue

            //Get the HTML for the control.
            dgGrid.RenderControl(hw);

            //(start): require for date format issue
            System.Text.StringBuilder style = new System.Text.StringBuilder();
            style.Append("<style>");
            style.Append("." + "text" + " { mso-number-format:" + "\\@;" + " }");
            style.Append("</style>");
            HttpContext.Current.Response.Clear();

            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            HttpContext.Current.Response.Write(style.ToString());
            HttpContext.Current.Response.Output.Write(tw.ToString());
            HttpContext.Current.Response.Flush();
            try
            {
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {

            }
        }

        public static void DownloadFileExcel(string wordSheetName, string fileName, DataSet dSet)
        {

            try
            {
                DownloadFileExcel(wordSheetName, fileName, dSet, false);
            }
            catch (Exception ex)
            {
                WriteLog("DownloadFileExcel - Msg: " + ex.Message);
            }
        }

        public static void DownloadFileExcelColumnIndex(string wordSheetName, string fileName, DataSet dSet)
        {

            try
            {
                DownloadFileExcel(wordSheetName, fileName, dSet, true);
            }
            catch (Exception ex)
            {
                WriteLog("DownloadFileExcel - Msg: " + ex.Message);
            }
        }
        public static void DownloadFileExcel(string wordSheetName, string fileName, DataSet dSet, bool firstRow)
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet excelWorkSheet = excelPackage.Workbook.Worksheets.Add(wordSheetName);
                var listFieldName = new List<string>();
                var countRow = 1;
                //DataTable dt = dSet.Tables[0];
                if (firstRow)
                {
                    dSet.Tables[0].Columns.Add("No.", typeof(Int16)).SetOrdinal(0);
                    int RowIndex = 0;
                    foreach (DataRow row in dSet.Tables[0].Rows)
                    {
                        ++RowIndex;
                        row["No."] = RowIndex;
                    }
                    dSet.Tables[0].AcceptChanges();

                }
                for (int i = 0; i < dSet.Tables[0].Columns.Count; i++)
                {
                    var sName = dSet.Tables[0].Columns[i].ColumnName.ToUpper();
                    listFieldName.Add(dSet.Tables[0].Columns[i].ColumnName);
                    if (!string.IsNullOrEmpty(sName))
                    {
                        excelWorkSheet.Cells[countRow, i + 1].Value = sName;
                        excelWorkSheet.Cells[countRow, i + 1].AutoFitColumns();
                    }
                    using (ExcelRange col = excelWorkSheet.Cells[countRow, i + 1])
                    {
                        col.Style.Font.Bold = true;
                    }
                }
                int itemIndex = 1;

                if (!Functions.IsEmptyDataSet(dSet))
                {
                    foreach (DataRow iRow in dSet.Tables[0].Rows)
                    {
                        countRow++;
                        var iColumn = 0;
                        foreach (var fName in listFieldName)
                        {
                            iColumn++;

                            //Common.EPPlusHelper.FormatExcelNumber(excelWorkSheet, countRow, iColumn, iRow[fName]);
                            excelWorkSheet.Cells[countRow, iColumn].Value = iRow[fName];
                            //FormatExcelAlign(ws, iRow, iColumn, ExcelAlignment.);
                        }

                        itemIndex++;
                    }
                }

                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + ".xlsx");
                HttpContext.Current.Response.BinaryWrite(excelPackage.GetAsByteArray());
                HttpContext.Current.Response.End();
            }
        }
        #endregion


        #region Create File Excel from DataSet
        /// <summary>
        /// Create file Excel from DataSet
        /// </summary>        
        /// <param name="pathFile">Path to save file</param>  
        /// <param name="fileName">fileName to save file (has not extenstion .xlsx only file name)</param>  
        /// <param name="dSet">dSet to created file</param>  
        /// <returns></returns>
        public static void CreateFileExcel(string pathFile, string fileName, DataSet dSet)
        {
            try
            {
                CreateFileExcel(pathFile, fileName, dSet, false);
            }
            catch (Exception ex)
            {

                WriteLog("CreateFileExcel - Msg: " + ex.Message);
            }
        }

        public static void CreateFileExcelNumberCol(string pathFile, string fileName, DataSet dSet)
        {
            try
            {
                CreateFileExcel(pathFile, fileName, dSet, true);
            }
            catch (Exception ex)
            {

                WriteLog("CreateFileExcel - Msg: " + ex.Message);
            }
        }

        public static void CreateFileExcel(string fullPath, DataSet dSet)
        {
            string pathFile = Path.GetDirectoryName(fullPath);
            string fileName = Path.GetFileName(fullPath);
            try
            {
                CreateFileExcel(pathFile, fileName, dSet, false);
            }
            catch (Exception ex)
            {

                WriteLog("CreateFileExcel - Msg: " + ex.Message);
            }
        }

        public static void CreateFileExcel(string pathFile, string fileName, DataSet dSet, bool firstRow)
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet excelWorkSheet = excelPackage.Workbook.Worksheets.Add(fileName);
                var listFieldName = new List<string>();
                var countRow = 1;
                //DataTable dt = dSet.Tables[0];
                if (firstRow)
                {
                    dSet.Tables[0].Columns.Add("No.", typeof(Int16)).SetOrdinal(0);
                    int RowIndex = 0;
                    foreach (DataRow row in dSet.Tables[0].Rows)
                    {
                        ++RowIndex;
                        row["No."] = RowIndex;
                    }
                    dSet.Tables[0].AcceptChanges();

                }
                for (int i = 0; i < dSet.Tables[0].Columns.Count; i++)
                {
                    var sName = dSet.Tables[0].Columns[i].ColumnName.ToUpper();
                    listFieldName.Add(dSet.Tables[0].Columns[i].ColumnName);
                    if (!string.IsNullOrEmpty(sName))
                    {
                        excelWorkSheet.Cells[countRow, i + 1].Value = sName;
                        excelWorkSheet.Cells[countRow, i + 1].AutoFitColumns();
                    }
                    using (ExcelRange col = excelWorkSheet.Cells[countRow, i + 1])
                    {
                        col.Style.Font.Bold = true;
                    }
                }
                int itemIndex = 1;

                if (!Functions.IsEmptyDataSet(dSet))
                {
                    foreach (DataRow iRow in dSet.Tables[0].Rows)
                    {
                        countRow++;
                        var iColumn = 0;
                        foreach (var fName in listFieldName)
                        {
                            iColumn++;

                            //Common.EPPlusHelper.FormatExcelNumber(excelWorkSheet, countRow, iColumn, iRow[fName]);
                            excelWorkSheet.Cells[countRow, iColumn].Value = iRow[fName];
                            //FormatExcelAlign(ws, iRow, iColumn, ExcelAlignment.);
                        }

                        itemIndex++;
                    }
                }

                File.WriteAllBytes(Path.Combine(pathFile, fileName) + ".xlsx", excelPackage.GetAsByteArray());
            }
        }
        #endregion


        #region Get Data from filePath (xlsx, csv)
        /// <summary>
        /// GetDataFromExcel from filePath
        /// </summary>        
        /// <param name="filePath">filePath to get data</param>        
        /// <returns></returns>
        public static DataSet GetDataFromExcel(string filePath)
        {
            try
            {
                string strConn;

                if (Path.GetExtension(filePath).IndexOf("csv") > -1)
                {
                    //strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Path.GetDirectoryName(filePath) + ";" + "Extended Properties=\"text;HDR=YES;FMT=CSVDelimited\"";

                    //using (OleDbConnection excelConn = new OleDbConnection(strConn))
                    //{
                    //    excelConn.Open();
                    //    OleDbCommand excelCommand = new OleDbCommand();
                    //    OleDbDataAdapter excelDataAdapter = new OleDbDataAdapter();
                    //    DataSet ds = new DataSet();
                    //    excelCommand = new OleDbCommand("SELECT * FROM [" + Path.GetFileName(filePath) + "]", excelConn);
                    //    excelDataAdapter.SelectCommand = excelCommand;
                    //    excelDataAdapter.Fill(ds);
                    //    return ds;
                    //}

                    DataTable dtRes = new DataTable();
                    DataSet ds = new DataSet();
                    if (File.Exists(filePath))
                    {
                        StreamReader Reader = new StreamReader(filePath);
                        //read the first line and make columns in result table.
                        string strLine = Reader.ReadLine();
                        if (strLine != null && strLine.Length > 0)
                        {
                            string[] Cols = strLine.Split(new char[] { ',' });
                            //Globals.WriteLog("[GetDataFromExcel] Headers: " + Cols.Length);
                            if (Cols.Length < 2)
                                return null;
                            foreach (string Col in Cols)
                                dtRes.Columns.Add(Col.ToLower().Trim(), typeof(string));

                            //Full up the result table.
                            while ((strLine = Reader.ReadLine()) != null)
                            {
                                string[] strCells = strLine.Split(',');
                                object[] objCellDatas = new object[Cols.Length];
                                for (int i = 0; i < Cols.Length; i++)
                                    if (i < strCells.Length && strCells[i] != null)
                                        objCellDatas[i] = strCells[i];

                                dtRes.Rows.Add(objCellDatas);
                            }
                            Reader.Close();
                            ds.Tables.Add(dtRes);
                        }
                    }
                    //Globals.WriteLog("[GetDataFromExcel] --------DONE--------");
                    return ds;

                    //DataTable dt = new DataTable();
                    //DataSet ds = new DataSet();
                    //using (StreamReader sr = new StreamReader(filePath))
                    //{
                    //    string[] headers = sr.ReadLine().Split(',');
                    //    Globals.WriteLog("[GetDataFromExcel] Headers: " + headers.Length);
                    //    foreach (string header in headers)
                    //    {
                    //        dt.Columns.Add(header);
                    //    }
                    //    while (!sr.EndOfStream)
                    //    {
                    //        string[] rows = sr.ReadLine().Split(',');
                    //        DataRow dr = dt.NewRow();
                    //        for (int i = 0; i < headers.Length; i++)
                    //        {
                    //            dr[i] = rows[i];
                    //        }
                    //        dt.Rows.Add(dr);
                    //    }
                    //    ds.Tables.Add(dt);
                    //}
                    //Globals.WriteLog("[GetDataFromExcel] --------DONE--------");
                    //return ds;
                }
                else
                {
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + filePath + ";" + "Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text\"";
                    bool isHTML = false;
                    #region Open file Excel
                    try
                    {
                        using (OleDbConnection oleDB = new OleDbConnection(strConn))
                        {
                            oleDB.Open();
                        }
                    }
                    catch (Exception ex)
                    {
                        strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + filePath + ";" + "Extended Properties=\"HTML Import;HDR=YES;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text\"";
                        isHTML = true;
                    }

                    DataTable dt = new DataTable();
                    dt = null;
                    using (OleDbConnection oleDB = new OleDbConnection(strConn))
                    {
                        oleDB.Open();
                        dt = oleDB.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        if (dt == null)
                            return null;

                        List<string> items = new List<string>();
                        int i = 0;

                        //if (dt.Rows.Count > 1)
                        //return null;  

                        for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
                        {
                            string excelSheetName;
                            string lastCharacter = "";

                            excelSheetName = dt.Rows[rowIndex]["TABLE_NAME"].ToString();
                            excelSheetName = excelSheetName.Replace("'", "");
                            lastCharacter = excelSheetName.Substring(excelSheetName.Length - 1, 1);
                            if (lastCharacter == "$" || isHTML)
                            {
                                items.Add(dt.Rows[rowIndex]["TABLE_NAME"].ToString());
                            }
                        }
                        if (items.Count < 1)
                            return null;

                        string sName;
                        string query;

                        sName = items[0].ToString();
                        sName = sName.Replace("'", "");
                        sName = sName.Replace("$", "");

                        query = "";
                        if (isHTML)
                            query = String.Format("select * from [{0}]", sName);
                        else
                            query = String.Format("select * from [{0}$]", sName);
                        OleDbDataAdapter da = new OleDbDataAdapter(query, strConn);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        return ds;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                WriteLog("[GetDataFromExcel] Msg: " + ex.Message);
            }
            return null;
        }
        #endregion

        #region Create File CSV from DataTable
        /// <summary>
        /// CreateFileCSV from DataTable
        /// </summary>
        /// <param name="dt">DataTable to Create CSV</param>
        /// <param name="filePath">Path to Save File csv (must to extension .csv)</param>        
        /// <returns></returns>
        public static void CreateFileCSV(DataTable dt, string filePath)
        {
            try
            {
                CreateFileCSV(dt, filePath, false);
            }
            catch (Exception ex)
            {

                WriteLog("[CreateFileCSV]: " + ex.Message);
            }
        }

        /// <summary>
        /// CreateFileCSV from DataTable
        /// </summary>
        /// <param name="sourceTable">DataTable to Create CSV</param>
        /// <param name="filePath">Path to Save File csv (must to extension .csv)</param>
        /// /// <param name="includeHeaders">if true is included headers, false is not headers.</param>
        /// <returns></returns>
        public static void CreateFileCSV(DataTable sourceTable, string filePath, bool includeHeaders)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                if (includeHeaders)
                {
                    IEnumerable<String> headerValues = sourceTable.Columns
                        .OfType<DataColumn>()
                        .Select(column => QuoteValue(column.ColumnName));

                    writer.WriteLine(String.Join(",", headerValues));
                }

                IEnumerable<String> items = null;

                foreach (DataRow row in sourceTable.Rows)
                {
                    items = row.ItemArray.Select(o => QuoteValue(o?.ToString() ?? String.Empty));
                    writer.WriteLine(String.Join(",", items));
                }

                writer.Flush();
            }
        }
        #endregion

        private static string QuoteValue(string value)
        {
            return String.Concat("\"",
            value.Replace("\"", "\"\""), "\"");
        }

        #region get key from config
        /// <summary>
        /// Lấy giá trị của một key từ file App.config
        /// </summary>
        /// <param name="sKey">Key cần lấy value</param>
        /// <returns></returns>
        public static string GetAppConfigByKey(string sKey)
        {
            string sResult = string.Empty;
            try
            {
                sResult = ConfigurationSettings.AppSettings[sKey];
            }
            catch (Exception ex)
            {
                WriteLog("[Utils.GetAppConfigByKey] Have an exception: " + ex.ToString());
            }
            return sResult;
        }

        /// <summary>
        /// Lấy giá trị của một key từ file Web.config
        /// </summary>
        /// <param name="name">Key cần lấy value</param>
        /// <returns></returns>
        public static string GetConfig(string name)
        {
            try
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings[name].ConnectionString;
            }
            catch (Exception) { }

            try
            {
                return System.Configuration.ConfigurationManager.AppSettings[name];
            }
            catch (Exception) { }

            return "";
        }

        #endregion

        #region  Send Email

        public static bool SendMail(string to, string subject, string body, List<string> pathFiles = null)
        {
            var sMessageError = string.Empty;
            var from = @"abc@gmail.com";
            var fromName = "Send infor";
            var pass = "abc";
            var smtpServer = "secure.***.com";
            var smtpPort = "25";
            bool boolEnableSsl = false;
            bool boolDefaultCredentials = false;
            return SendMail(from, fromName, pass, to, subject, body, smtpServer, smtpPort, boolEnableSsl, boolDefaultCredentials, pathFiles, out sMessageError);
        }

        public static bool SendMail(string from, string fromName, string pass, string to, string subject, string body, string smtpServer,
            string smtpPort, bool boolEnableSsl, bool boolDefaultCredentials, List<string> pathFiles, out string sMessageError)
        {
            sMessageError = string.Empty;
            var msg = new System.Net.Mail.MailMessage();
            msg.From = new MailAddress(from, fromName);
            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;
            if (pathFiles != null && pathFiles.Count > 0)
            {
                msg.BodyEncoding = Encoding.UTF8;
                foreach (var item in pathFiles)
                {
                    WriteLog("Send email: Path Files Attachment: {0}", item);
                    Attachment at = new Attachment(item);
                    msg.Attachments.Add(at);
                }
            }
            try
            {
                var client = new SmtpClient(smtpServer, ConvertObjectToInt(smtpPort, 0));
                client.EnableSsl = boolEnableSsl;
                if (!boolDefaultCredentials)
                {
                    var cre = new NetworkCredential(from, pass);
                    client.UseDefaultCredentials = boolDefaultCredentials;
                    client.Credentials = cre;
                }
                else
                {
                    client.UseDefaultCredentials = boolDefaultCredentials;
                }
                client.Send(msg);
                WriteLog(string.Format("Send email from={0} to={1}: Successful", from, to));
                return true;
            }
            catch (Exception ex)
            {
                sMessageError = ex.Message;
                WriteLog(string.Format("Send email: FromUserName: {0} - Password: {1} - Message: {2}", from, pass, ex.ToString()));
                return false;
            }
        }

        public static bool sendMail(string from, string to, string subject, string body, bool isVietNam, string[] attachs)
        {
            WriteLog("[sendMail]---------------Begin---------------");
            WriteLog("[sendMail] From: " + from);
            WriteLog("[sendMail] Subject: " + subject);
            WriteLog("[sendMail] To: " + to);
            System.Web.Mail.MailMessage msg = new System.Web.Mail.MailMessage();
            if (attachs.Length == 0)
            {
                return sendMail(from, to, subject, body, isVietNam);
            }

            else
            {
                foreach (var item in attachs)
                {
                    if (item != null && item != "")
                    {
                        MailAttachment attachment = new MailAttachment(item); //create the attachment
                        msg.Attachments.Add(attachment);    //add the attachment
                    }
                }
                msg.To = to;
                msg.From = from;
                msg.Body = body;
                msg.Subject = subject;
                if (isVietNam)
                    msg.BodyEncoding = Encoding.UTF8;
                else
                    msg.BodyEncoding = Encoding.ASCII;
                msg.BodyFormat = MailFormat.Html;

                SmtpMail.SmtpServer = Functions.GetConfig("SMTPServer");
                SmtpMail.Send(msg);
                WriteLog("[sendMail] Success");
                WriteLog("[sendMail]---------------End---------------");
                return true;
            }
        }

        public static bool sendMail(string from, string to, string subject, string body, bool isVietNam)
        {
            WriteLog("[sendMail]---------------Begin---------------");
            WriteLog("[sendMail] From: " + from);
            WriteLog("[sendMail] Subject: " + subject);
            WriteLog("[sendMail] To: " + to);
            System.Web.Mail.MailMessage msg = new System.Web.Mail.MailMessage();
            msg.To = to;
            msg.From = from;
            msg.Body = body;
            msg.Subject = subject;
            if (isVietNam)
                msg.BodyEncoding = Encoding.UTF8;
            else
                msg.BodyEncoding = Encoding.ASCII;
            msg.BodyFormat = MailFormat.Html;

            SmtpMail.SmtpServer = Functions.GetConfig("SMTPServer");
            SmtpMail.Send(msg);
            WriteLog("[sendMail] Success");
            WriteLog("[sendMail]---------------End---------------");
            return true;
        }

        public static void SendMail(string from, string to, string subject, string body, bool html)
        {
            if (Functions.IsEmpty(to))
                return;

            string[] ss1 = to.Split(';');
            foreach (string ss in ss1)
                foreach (string s in ss.Split(','))
                {
                    try
                    {
                        System.Web.Mail.MailMessage msg = new System.Web.Mail.MailMessage();
                        msg.To = s;
                        msg.From = from;
                        msg.Body = body;
                        msg.Subject = subject;

                        msg.BodyEncoding = Encoding.UTF8;

                        msg.BodyFormat = html ? MailFormat.Html : MailFormat.Text;

                        SmtpMail.SmtpServer = GetAppConfigByKey("SMTPServer");
                        SmtpMail.Send(msg);
                    }
                    catch (Exception ex)
                    { }
                }
        }
        public static void SendMail(string to, string subject, string body)
        {
            Functions.SendMail(GetAppConfigByKey("MailFrom"), to, subject, body, true);
        }
        public static void SendMail(string subject, string body)
        {
            Functions.SendMail(GetAppConfigByKey("MailFrom"), GetAppConfigByKey("MailTo"), subject, body, true);
        }
        #endregion

        #region DataSet
        public static bool IsEmpty(DataSet ds)
        {
            return ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0;
        }
        public static bool IsEmpty(DataTable table)
        {
            return table == null || table.Rows.Count == 0;
        }
        public static bool IsEmpty(string str)
        {
            return !NotEmpty(str);
        }
        public static bool NotEmpty(DataSet ds)
        {
            return ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;
        }
        public static bool NotEmpty_Mutiltable(DataSet ds)
        {
            return ds != null && ds.Tables.Count > 0;
        }
        public static bool NotEmpty(DataTable table)
        {
            return table != null && table.Rows.Count > 0;
        }
        public static bool NotEmpty(string str)
        {
            if (str != null && str != string.Empty)
                return true;
            return false;
        }
        /// <summary>
        /// Chuyen doi DataRow thanh bang Hashtable.
        /// Dung cho muc dich:
        ///		1.
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        public static Hashtable DataRowToHashtable(DataRow dataRow, Hashtable datas)
        {
            if (dataRow == null)
                return datas;
            if (datas == null)
                datas = new Hashtable();
            object value = null;
            foreach (DataColumn column in dataRow.Table.Columns)
            {
                value = dataRow[column];
                datas[column.ColumnName] = value == DBNull.Value ? null : value;
            }
            return datas;
        }
        #endregion

        #region write log
        private static object locker = new object();
        public static void WriteLog(string format, params object[] args)
        {
            lock (locker)
            {
                try
                {
                    string logFileName = string.Format(ConfigurationManager.AppSettings["LogFile"], DateTime.Today.ToString("MMMdd.yyyy"));
                    FileInfo file = new FileInfo(logFileName);
                    if (!file.Directory.Exists)
                        file.Directory.Create();

                    if (!file.Exists)
                        file.CreateText().Close();
                    using (StreamWriter ws = file.AppendText())
                    {
                        ws.WriteLine(string.Format("{0} - {1}", DateTime.Now.ToString("hh:mm:ss.fff tt"), string.Format(format, args)));
                    }
                }
                catch
                {
                }
            }
        }
        #endregion

        #region HttpMethodGet Url
        public static string HttpMethodGet(string strUrl)
        {
            try
            {
                if (!string.IsNullOrEmpty(strUrl))
                {
                    WriteLog("[HttpMethodGet] Url: " + strUrl);

                    var request = (HttpWebRequest)WebRequest.Create(strUrl);

                    HttpWebResponse response;
                    try
                    {
                        response = (HttpWebResponse)request.GetResponse();
                    }
                    catch (WebException ex)
                    {
                        response = (HttpWebResponse)ex.Response;
                    }



                    var dataStream = new StreamReader(response.GetResponseStream());
                    //show the response string on the console screen.
                    string xData = dataStream.ReadToEnd();
                    dataStream.Close();

                    WriteLog("[HttpMethodGet] Return data: " + xData);
                    return xData;
                }
                return "";
            }
            catch (Exception ex)
            {
                WriteLog("[HttpMethodGet] Error. Url=" + strUrl, ex);
                return "";
            }
        }
        #endregion

        #region Download File
        /// <summary>
        /// DownLoad file 
        /// </summary>
        /// <param name="Response">HTTP response information</param>
        /// <param name="path">Duong dan noi chua file</param>
        /// <param name="filename">Ten file</param>
        /// <param name="contentype">HTTP MIME type of output stream</param>
        public static void DownLoadFile(HttpResponse Response, string path, string filename, string contentType)
        {
            if (contentType == null || contentType.Length == 0)
                contentType = "text/plain";

            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Functions.MakeValidFileName(filename));
            Stream outf = Response.OutputStream;
            FileStream inf = null;
            try
            {
                inf = new FileStream(path + "/" + filename, FileMode.Open);
                byte[] buff = new byte[1024];
                int count = 0;
                while (true)
                {
                    count = inf.Read(buff, 0, 1024);
                    if (count <= 0) break;
                    outf.Write(buff, 0, count);
                }
            }
            catch
            {

            }
            finally
            {
                if (inf != null) inf.Close();
                outf.Flush();
                outf.Close();
            }

            try
            {
                File.Delete(filename);
            }
            catch (Exception) { }

            Response.End();
        }

        public static string MakeValidFileName(string name)
        {
            string invalidChars = Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidReStr = string.Format(@"[{0}]+", invalidChars);
            string replace = Regex.Replace(name, invalidReStr, "_")
                                .Replace(";", "")
                                .Replace(",", "")
                                .Replace("&", "")
                                .Replace("#", "");

            string sFileName = Path.GetFileNameWithoutExtension(replace);
            if (sFileName == "")
                replace = "NoName" + Path.GetExtension(replace);

            return replace;
        }

        public static void DownLoadFile(HttpResponse Response, string filename, bool IsDelete)
        {
            string contentType = "text/plain";
            if (filename.ToLower().IndexOf(".pdf") > -1)
                contentType = "application/pdf";
            else if (filename.ToLower().IndexOf(".csv") > -1)
                contentType = "text/csv";
            else if (filename.ToLower().IndexOf(".htm") > -1)
                contentType = "text/html";
            //else if (filename.ToLower().IndexOf(".doc") > -1)
            //    contentType = "application/msword";
            //else if (filename.ToLower().IndexOf(".xls") > -1)
            //    contentType = "application/vnd.ms-excel";

            Response.Clear();
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Functions.MakeValidFileName(Path.GetFileName(filename)));
            Stream outf = Response.OutputStream;
            FileStream inf = null;
            try
            {
                inf = new FileStream(filename, FileMode.Open);
                byte[] buff = new byte[1024];
                int count = 0;
                while (true)
                {
                    count = inf.Read(buff, 0, 1024);
                    if (count <= 0) break;
                    outf.Write(buff, 0, count);
                }
            }
            catch
            {

            }
            finally
            {
                if (inf != null) inf.Close();
                outf.Flush();
                outf.Close();
            }

            try
            {
                if (IsDelete)
                    File.Delete(filename);
            }
            catch (Exception) { }

            Response.End();
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
            //HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            //HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            //HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            //HttpContext.Current.Response.End();
        }

        public static void DownLoadFile(HttpResponse Response, string filename)
        {
            Functions.DownLoadFile(Response, filename, false);
        }
        #endregion

        public static string ConvertHtmlToPDF(string filePathHTML)
        {
            WriteLog("[ConvertHmlToPDF_CreateFile]: ----------Begin----------");
            WriteLog("[ConvertHmlToPDF_CreateFile] filePathHTML: " + filePathHTML);
            string result = string.Empty;
            bool rsCheck = XSettings.InstallLicense("X/VKS08wmMtAun4hGNvFONzmS/QQY7hZ9Z2488LHIg8X5nu5Qx7dYsZhez00hWZRXd5Xim0uoXp3ifxwDtAusQ0lPTnPXR1401Y=");

            if (!rsCheck)
            {
                result = "Invalid Websupergoo license.";
                WriteLog("[ConvertHmlToPDF_CreateFile] result:  " + result);
                return null;
            }
            string FilesDir = Functions.GetAppConfigByKey("FileDir");

            if (!System.IO.File.Exists(FilesDir))
            {
                System.IO.Directory.CreateDirectory(FilesDir);
            }

            string fileName = "File_Convert_PDF" + DateTime.Now.Ticks.ToString() + ".pdf";

            string fullPath = Path.Combine(FilesDir, fileName);

            WriteLog("[ConvertHmlToPDF_CreateFile] FilesDir: " + FilesDir);
            WriteLog("[ConvertHmlToPDF_CreateFile] fileName: " + fileName);
            WriteLog("[ConvertHmlToPDF_CreateFile] fullPath: " + fullPath);

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
                    //  + " window.external.ABCpdf_RenderComplete(); }, 10000);"
                    //  + "})();";
                    //doc.SetInfo(0, "RenderDelay", "45000");
                    //doc.SetInfo(0, "OneStageRender", 0);

                    //Render after 3 seconds
                    //doc.HtmlOptions.OnLoadScript = "(function(){"
                    //  + " window.ABCpdf_go = false;"
                    //  + " setTimeout(function(){ window.ABCpdf_go = true; }, 10000);"
                    //  + "})();";

                    doc.Page = doc.AddPage();
                    int theID;

                    theID = doc.AddImageUrl(filePathHTML);

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
                    result = ex2.Message;
                    WriteLog("[ConvertHmlToPDF_CreateFile] - Exception: " + result);
                    return null;
                }

                pages.Save(fullPath);
                pages.Clear();
                result = fullPath;

                WriteLog("[ConvertHmlToPDF_CreateFile]: ----------End----------");
                return result;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                WriteLog("[ConvertHmlToPDF_CreateFile] -- Exception: " + result);
                return null;
            }
            #endregion
        }

        public static string GetValidFileName(string fileName)
        {
            // remove any invalid character from the filename.
            var ret = Regex.Replace(fileName.Trim(), "[^A-Za-z0-9_. ]+", string.Empty);
            return ret.Replace(" ", String.Empty);
        }

        public static DateTime? ConvertDate(object date)
        {
            try
            {
                if (date == null) return null;
                return Convert.ToDateTime(date);
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public static string ConvertDateToString(object date, string format)
        {
            var dDate = ConvertDate(date);
            if (dDate == null)
            {
                return string.Empty;
            }
            else return dDate.Value.ToString(format);
        }
    }
}
