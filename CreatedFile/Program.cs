using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;

namespace CreatedFile
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSet dSet1 = new DataSet();
            dSet1 = GetDataFromExcel("D:\\equal\\memfixzip.xlsx");
            //string fname = "1223 PLP BW SNAP0K21K_48_40000";
            //string fname = "PLP 0105 BW SNAP0K21K_50_50000";
            //string fname = "PLP 1230 BW SNAP0K21K_49_73176";
            //string fname = "PLQ 0112 BW SNAP0K21K_51_46465 (2)";
            //string fname = "PLQ 0119 BW SNAP 90000K21K_52_57085 (2)";



            //string fname = "PLQ 0119 BW SNAP 90000K21K_52_57085";

            string fname = "PLQ 0202 BW SNAP215K49K_60_26290";


            DataSet dSet2 = GetDataFromExcel(string.Format("D:\\equal\\{0}.csv", fname));
            //DataSet dSet3 = GetDataFromExcel(string.Format("D:\\equal\\{0}.csv", fname1));

            //int totaldSet2 = dSet2.Tables[0].Rows.Count;
            //int totaldSet3 = dSet3.Tables[0].Rows.Count;
            ////dSet2.Tables.Add(dSet3.Tables[0]);

            //foreach (DataRow item in dSet3.Tables[0].Rows)
            //{
            //    List<string> lst = new List<string>();
            //    for (int i = 0; i < dSet2.Tables[0].Columns.Count; i++)
            //    {
            //        lst.Add(item[dSet2.Tables[0].Columns[i].ToString()].ToString());
            //    }
            //    DataRow row = dSet2.Tables[0].NewRow();
            //    row.ItemArray = lst.ToArray();
            //    dSet2.Tables[0].Rows.Add(row);
            //}

            //for (int i = 0; i < dSet2.Tables[0].Columns.Count; i++)
            //{
            //    dSet2.Tables[0];
            //}

            //dSet2.AcceptChanges();

            int total = dSet2.Tables[0].Rows.Count;

            DataTable tb2 = new DataTable();
            for (int i = 0; i < dSet2.Tables[0].Columns.Count; i++)
            {
                tb2.Columns.Add(dSet2.Tables[0].Columns[i].ToString().ToUpper());
            }
            tb2.Columns.Add("NOTICE");

            //tb2.Columns.Add("ApprovalCode");
            //tb2.Columns.Add("ErrorZip");
            //tb2.Columns.Add("Zip");

            //DataTable dt = dSet1.Tables[0];
            //for (int i = 0; i < dSet2.Tables[0].Rows.Count; i++)
            //{
            //    DataRow row1 = dSet2.Tables[0].Rows[i];

            //    string condition = "memFirstName='{0}' and memLastName='{1}' and memCity='{2}' and memState='{3}' and memESTDebt={4} and memAddress='{5}' ";
            //    //string condition = "FIRST='{0}' and LAST='{1}' and CITY='{2}' and STATE='{3}' and DEBT={4} and ADDRESS='{5}' ";
            //    DataRow[] tmpRow = dt.Select(string.Format(condition, row1["FIRST"].ToString().Trim(),
            //        row1["LAST"].ToString().Trim(),
            //        row1["CITY"].ToString().Trim(),
            //        row1["STATE"].ToString().Trim(),
            //        row1["DEBT"],
            //        row1["ADDRESS"].ToString().Trim()));

            //    if (tmpRow.Length == 1)
            //    {
            //        tb2.Rows.Add(row1["PRE-QUAL"], row1["ZIP"], tmpRow[0]["memZip"]);
            //    }
            //    else
            //    {
            //        if (tmpRow.Length > 1)
            //            tb2.Rows.Add(row1["PRE-QUAL"], row1["ZIP"], tmpRow[0]["memZip"]);
            //        //else
            //        //    tb2.Rows.Add(row1["PRE-QUAL"], row1["ZIP"], ">1");
            //    }

            //}

            DataTable dt = dSet1.Tables[0];
            float count = 0;
            float number = 0;
            float notsame = 0;
            for (int i = 0; i < dSet2.Tables[0].Rows.Count; i++)
            {
                ++count;
                DataRow row2 = dSet2.Tables[0].Rows[i];

                string condition = "memFirstName='{0}' and memLastName='{1}' and memCity='{2}' and memState='{3}' and memESTDebt={4} and memAddress='{5}' ";
                //string condition = "FIRST='{0}' and LAST='{1}' and CITY='{2}' and STATE='{3}' and DEBT={4} and ADDRESS='{5}' ";
                DataRow[] tmpRow = dt.Select(string.Format(condition, row2["FIRST"].ToString().Trim(),
                    row2["LAST"].ToString().Trim(),
                    row2["CITY"].ToString().Trim(),
                    row2["STATE"].ToString().Trim(),
                    row2["DEBT"],
                    row2["ADDRESS"].ToString().Trim()));

                //if (tmpRow.Length == 1)
                //{
                //    tb2.Rows.Add(row2["FIRST"], row2["LAST"], row2["ADDRESS"], row2["CITY"], row2["STATE"], row2["ZIP"], row2["PHONE NUMBER"]
                //        , row2["JOB NUMBER"], row2["MAILING DATE"], row2["OFFER EXPIRATION"], row2["PRE-QUAL"], row2["DEBT"], row2["DEBT*3%"], row2["DEBT*1.5%"], row2["DEBT2"], row2["DEBT2*1.5%"], row2["DEBT3"], row2["DEBT3*1.5%"]);
                //}
                //else
                //{
                //    if (tmpRow.Length > 1)
                //        tb2.Rows.Add(row2["FIRST"], row2["LAST"], row2["ADDRESS"], row2["CITY"], row2["STATE"], row2["ZIP"], row2["PHONE NUMBER"]
                //        , row2["JOB NUMBER"], row2["MAILING DATE"], row2["OFFER EXPIRATION"], row2["PRE-QUAL"], row2["DEBT"], row2["DEBT*3%"], row2["DEBT*1.5%"], row2["DEBT2"], row2["DEBT2*1.5%"], row2["DEBT3"], row2["DEBT3*1.5%"]);
                //    //else
                //    //    tb2.Rows.Add(row1["PRE-QUAL"], row1["ZIP"], ">1");
                //}


                //if (tmpRow.Length >= 1)
                //{
                //    ++number;
                //    tb2.Rows.Add(row2["FIRST"], row2["LAST"], row2["ADDRESS"], row2["CITY"], row2["STATE"], tmpRow[0]["memZip"], row2["PHONE NUMBER"]
                //        , row2["JOB NUMBER"], row2["MAILING DATE"], row2["OFFER EXPIRATION"], row2["PRE-QUAL"], row2["DEBT"], row2["DEBT*3%"], row2["DEBT*1.5%"], row2["DEBT2"], row2["DEBT2*1.5%"], row2["DEBT3"], row2["DEBT3*1.5%"], ">=1");
                //}
                //else
                //{
                //    ++notsame;
                //    tb2.Rows.Add(row2["FIRST"], row2["LAST"], row2["ADDRESS"], row2["CITY"], row2["STATE"], row2["ZIP"], row2["PHONE NUMBER"]
                //                   , row2["JOB NUMBER"], row2["MAILING DATE"], row2["OFFER EXPIRATION"], row2["PRE-QUAL"], row2["DEBT"], row2["DEBT*3%"], row2["DEBT*1.5%"], row2["DEBT2"], row2["DEBT2*1.5%"], row2["DEBT3"], row2["DEBT3*1.5%"], "");
                //}
                ++notsame;
                tb2.Rows.Add(row2["FIRST"], row2["LAST"], row2["ADDRESS"], row2["CITY"], row2["STATE"], row2["ZIP"], row2["PHONE NUMBER"]
                               , row2["JOB NUMBER"], row2["MAILING DATE"], row2["OFFER EXPIRATION"], row2["PRE-QUAL"], row2["DEBT"], row2["DEBT*3%"], row2["DEBT*1.5%"], row2["DEBT2"], row2["DEBT2*1.5%"], row2["DEBT3"], row2["DEBT3*1.5%"], "");
                //else
                //{
                //    if (tmpRow.Length > 1)
                //        tb2.Rows.Add(row2["FIRST"], row2["LAST"], row2["ADDRESS"], row2["CITY"], row2["STATE"], tmpRow[0]["memZip"], row2["PHONE NUMBER"]
                //        , row2["JOB NUMBER"], row2["MAILING DATE"], row2["OFFER EXPIRATION"], row2["PRE-QUAL"], row2["DEBT"], row2["DEBT*3%"], row2["DEBT*1.5%"], row2["DEBT2"], row2["DEBT2*1.5%"], row2["DEBT3"], row2["DEBT3*1.5%"]);
                //    //else
                //    //    tb2.Rows.Add(row1["PRE-QUAL"], row1["ZIP"], ">1");
                //}

            }

            //float count = 0;
            //float number = 0;

            //foreach (DataRow row1 in dSet1.Tables[0].Rows)
            //{
            //    ++count;
            //    IList<object> strList1 = new List<object>() { row1["memFirstName"], row1["memLastName"], row1["memCity"], row1["memState"], row1["memESTDebt"], row1["memAddress"] };

            //    foreach (DataRow row2 in dSet2.Tables[0].Rows)
            //    {


            //        IList<object> strList2 = new List<object>() { row2["FIRST"], row2["LAST"], row2["CITY"], row2["STATE"], row2["DEBT"], row2["ADDRESS"] };

            //        bool isEqual = strList1.SequenceEqual(strList2);
            //        if (isEqual)
            //        {
            //            ++number;
            //            tb2.Rows.Add(row1["PRE-QUAL"], row2["ZIP"], row1["memZip"]);
            //        }

            //    }

            //}

            //using (StreamWriter writer = new StreamWriter(string.Format("D:\\equal\\result of {0}.csv", fname)))
            //{
            //    WriteDataTable(tb2, writer, true);
            //}

            DataSet db = new DataSet();
            db.Tables.Add(tb2);
            db.AcceptChanges();
            DownloadFileExcel(string.Format("D:\\equal\\result of {0}", fname), string.Format("D:\\equal\\result\\result of {0}", fname), db, false);
        }
        public static void WriteDataTable(DataTable sourceTable, TextWriter writer, bool includeHeaders)
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

        private static string QuoteValue(string value)
        {
            return String.Concat("\"",
            value.Replace("\"", "\"\""), "\"");
        }

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
                //Globals.WriteLog("[GetDataFromExcel] Msg: " + ex.Message);
                throw ex;
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

                File.WriteAllBytes(fileName + ".xlsx", excelPackage.GetAsByteArray());
            }
        }

    }
}
