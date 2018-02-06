using CreatedFile.Common;
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
            dSet1 = Functions.GetDataFromExcel("D:\\equal\\memfixzip.xlsx");
            string fname = "PLQ 0202 BW SNAP215K49K_60_26290";
            DataSet dSet2 = Functions.GetDataFromExcel(string.Format("D:\\equal\\{0}.csv", fname));

            int total = dSet2.Tables[0].Rows.Count;

            DataTable tb2 = new DataTable();
            for (int i = 0; i < dSet2.Tables[0].Columns.Count; i++)
            {
                tb2.Columns.Add(dSet2.Tables[0].Columns[i].ToString().ToUpper());
            }
            tb2.Columns.Add("NOTICE");

            #region Comment code
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
            #endregion 

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

                ++notsame;
                tb2.Rows.Add(row2["FIRST"], row2["LAST"], row2["ADDRESS"], row2["CITY"], row2["STATE"], row2["ZIP"], row2["PHONE NUMBER"]
                               , row2["JOB NUMBER"], row2["MAILING DATE"], row2["OFFER EXPIRATION"], row2["PRE-QUAL"], row2["DEBT"], row2["DEBT*3%"], row2["DEBT*1.5%"], row2["DEBT2"], row2["DEBT2*1.5%"], row2["DEBT3"], row2["DEBT3*1.5%"], "");
                #region old code
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

                //else
                //{
                //    if (tmpRow.Length > 1)
                //        tb2.Rows.Add(row2["FIRST"], row2["LAST"], row2["ADDRESS"], row2["CITY"], row2["STATE"], tmpRow[0]["memZip"], row2["PHONE NUMBER"]
                //        , row2["JOB NUMBER"], row2["MAILING DATE"], row2["OFFER EXPIRATION"], row2["PRE-QUAL"], row2["DEBT"], row2["DEBT*3%"], row2["DEBT*1.5%"], row2["DEBT2"], row2["DEBT2*1.5%"], row2["DEBT3"], row2["DEBT3*1.5%"]);
                //    //else
                //    //    tb2.Rows.Add(row1["PRE-QUAL"], row1["ZIP"], ">1");
                //}
                #endregion
            }

            DataSet db = new DataSet();
            db.Tables.Add(tb2);
            db.AcceptChanges();
            string filePath = string.Format("D:\\equal\\result of {0}", fname);
            Functions.DownloadFileExcel(filePath, filePath, db, false);

            Functions.CreateFileCSV(tb2, filePath + ".csv");
        }
    }
}
