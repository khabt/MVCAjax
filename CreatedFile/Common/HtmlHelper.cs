using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

namespace CreatedFile.Common
{
    class HtmlHelper
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
            return false;
        }
    }
}
