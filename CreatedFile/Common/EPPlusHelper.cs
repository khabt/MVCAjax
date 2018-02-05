using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ROIDealer.Common
{
    public class EPPlusHelper
    {
        public static void FormatExcelNumber(ExcelWorksheet ws, int iRow, int iColumn, object value)
        {
            ws.Cells[iRow, iColumn].Value = HtmlHelper.ConvertObjectToInt(value, 0);
            using (ExcelRange col = ws.Cells[iRow, iColumn])
            {
                col.Style.Numberformat.Format = "#,##0";
                col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }
        }

        public static void FormatExcelDecimal(ExcelWorksheet ws, int iRow, int iColumn, object value)
        {
            ws.Cells[iRow, iColumn].Value = HtmlHelper.ConvertObjectToDecimal(value, 0);
            using (ExcelRange col = ws.Cells[iRow, iColumn])
            {
                col.Style.Numberformat.Format = "#,##0.00";
                col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }
        }

        public static void FormatExcelMoney(ExcelWorksheet ws, int iRow, int iColumn, object value)
        {
            ws.Cells[iRow, iColumn].Value = HtmlHelper.ConvertObjectToDecimal(value, 0);
            using (ExcelRange col = ws.Cells[iRow, iColumn])
            {
                col.Style.Numberformat.Format = "$#,##0.00";
                col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }
        }

        public static void FormatExcelPercent(ExcelWorksheet ws, int iRow, int iColumn, object value)
        {
            ws.Cells[iRow, iColumn].Value = HtmlHelper.ConvertObjectToDecimal(value, 0);
            using (ExcelRange col = ws.Cells[iRow, iColumn])
            {
                col.Style.Numberformat.Format = "#,##0%";
                col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }
        }

        public static void FormatExcelPercent2(ExcelWorksheet ws, int iRow, int iColumn, object value)
        {
            ws.Cells[iRow, iColumn].Value = HtmlHelper.ConvertObjectToDecimal(value, 0);
            using (ExcelRange col = ws.Cells[iRow, iColumn])
            {
                col.Style.Numberformat.Format = "#,###.#0%";
                col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }
        }

        public static void FormatExcelDate(ExcelWorksheet ws, int iRow, int iColumn, object value)
        {
            FormatExcelDate(ws, iRow, iColumn, value, "MM/dd/yyyy");
        }

        public static void FormatExcelDate(ExcelWorksheet ws, int iRow, int iColumn, object value, string format)
        {
            var date = HtmlHelper.ConvertToDate(value);
            ws.Cells[iRow, iColumn].Value = date;
            using (ExcelRange col = ws.Cells[iRow, iColumn])
            {
                if (date != null)
                {
                    col.Style.Numberformat.Format = format;
                }              
                col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            }            
        }

        public static void FormatExcelHeader(ExcelWorksheet ws, int iRow, int iColumn, object value, ExcelAlignment align)
        {
            try
            {
                ws.Cells[iRow, iColumn].Value = value;
                using (ExcelRange col = ws.Cells[iRow, iColumn])
                {
                    col.Style.Font.Bold = true;
                }
                FormatExcelAlign(ws, iRow, iColumn, align);
            }
            catch (Exception ex)
            {
            }
        }

        public static void FormatExcelHeader(ExcelWorksheet ws, int iRow, int iColumn, object value)
        {
            try
            {
                ws.Cells[iRow, iColumn].Value = value;
                using (ExcelRange col = ws.Cells[iRow, iColumn])
                {
                    col.Style.Font.Bold = true;
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void FormatExcelString(ExcelWorksheet ws, int iRow, int iColumn, object value, ExcelAlignment align)
        {
            try
            {
                ws.Cells[iRow, iColumn].Value = value;
                FormatExcelAlign(ws, iRow, iColumn, align);
            }
            catch (Exception ex) { }
        }

        public static void FormatExcelAlign(ExcelWorksheet ws, int iRow, int iColumn, ExcelAlignment align)
        {
            using (ExcelRange col = ws.Cells[iRow, iColumn])
            {
                switch (align)
                {
                    case ExcelAlignment.Center:
                        col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        break;
                    case ExcelAlignment.CenterContinuous:
                        col.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                        break;
                    case ExcelAlignment.Distributed:
                        col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Distributed;
                        break;
                    case ExcelAlignment.Fill:
                        col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Fill;
                        break;
                    case ExcelAlignment.General:
                        col.Style.HorizontalAlignment = ExcelHorizontalAlignment.General;
                        break;
                    case ExcelAlignment.Justify:
                        col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                        break;
                    case ExcelAlignment.Left:
                        col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        break;
                    case ExcelAlignment.Right:
                        col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        break;
                    default:
                        col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        break;
                }
            }
        }

        public static void FormatExcelDataType(ExcelWorksheet ws, int iRow, int iColumn, DataType type)
        {

        }

        public static void FormatExcel(ExcelWorksheet ws, int iRow, int iColumn, object value, string format, ExcelAlignment align,
            System.Drawing.Color bgcolor, System.Drawing.Color color, System.Drawing.Font fontFamily)
        {
            try
            {
                ws.Cells[iRow, iColumn].Value = value;
                using (ExcelRange col = ws.Cells[iRow, iColumn])
                {
                    col.Style.Font.SetFromFont(fontFamily);
                    col.Style.Font.Color.SetColor(color);
                    if (bgcolor != System.Drawing.Color.White)
                    {
                        col.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        col.Style.Fill.BackgroundColor.SetColor(bgcolor);

                        col.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        col.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        col.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        col.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        col.Style.Border.Top.Color.SetColor(System.Drawing.Color.Silver);
                        col.Style.Border.Right.Color.SetColor(System.Drawing.Color.Silver);
                        col.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Silver);
                        col.Style.Border.Left.Color.SetColor(System.Drawing.Color.Silver);
                    }
                    if (!string.IsNullOrEmpty(format))
                    {
                        col.Style.Numberformat.Format = format;
                    }
                    switch (align)
                    {
                        case ExcelAlignment.Center:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            break;
                        case ExcelAlignment.CenterContinuous:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                            break;
                        case ExcelAlignment.Distributed:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Distributed;
                            break;
                        case ExcelAlignment.Fill:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Fill;
                            break;
                        case ExcelAlignment.General:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.General;
                            break;
                        case ExcelAlignment.Justify:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                            break;
                        case ExcelAlignment.Left:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            break;
                        case ExcelAlignment.Right:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            break;
                        default:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            break;
                    }
                }
            }
            catch (Exception ex) { }
        }

        public static void FormulaFormatExcel(ExcelWorksheet ws, int iRow, int iColumn, string formula, string format, ExcelAlignment align,
            System.Drawing.Color bgcolor, System.Drawing.Color color, System.Drawing.Font fontFamily)
        {
            try
            {
                ws.Cells[iRow, iColumn].Formula = formula;
                using (ExcelRange col = ws.Cells[iRow, iColumn])
                {
                    col.Style.Font.SetFromFont(fontFamily);
                    col.Style.Font.Color.SetColor(color);
                    if (bgcolor != System.Drawing.Color.White)
                    {
                        col.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        col.Style.Fill.BackgroundColor.SetColor(bgcolor);

                        col.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        col.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        col.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        col.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        col.Style.Border.Top.Color.SetColor(System.Drawing.Color.Silver);
                        col.Style.Border.Right.Color.SetColor(System.Drawing.Color.Silver);
                        col.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Silver);
                        col.Style.Border.Left.Color.SetColor(System.Drawing.Color.Silver);
                    }
                    if (!string.IsNullOrEmpty(format))
                    {
                        col.Style.Numberformat.Format = format;
                    }
                    switch (align)
                    {
                        case ExcelAlignment.Center:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            break;
                        case ExcelAlignment.CenterContinuous:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                            break;
                        case ExcelAlignment.Distributed:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Distributed;
                            break;
                        case ExcelAlignment.Fill:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Fill;
                            break;
                        case ExcelAlignment.General:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.General;
                            break;
                        case ExcelAlignment.Justify:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                            break;
                        case ExcelAlignment.Left:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            break;
                        case ExcelAlignment.Right:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            break;
                        default:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            break;
                    }
                }
            }
            catch (Exception ex) { }
        }

        public static void FormatExcelMerge(ExcelWorksheet ws, string RangeRowColumn, object value, string format, ExcelAlignment align,
            System.Drawing.Color bgcolor, System.Drawing.Color color, System.Drawing.Font fontFamily)
        {
            try
            {
                ws.Cells[RangeRowColumn].Merge = true;
                ws.Cells[RangeRowColumn].Value = value;
                using (ExcelRange col = ws.Cells[RangeRowColumn])
                {
                    col.Style.Font.SetFromFont(fontFamily);
                    col.Style.Font.Color.SetColor(color);
                    if (bgcolor != System.Drawing.Color.White)
                    {
                        col.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        col.Style.Fill.BackgroundColor.SetColor(bgcolor);

                        col.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        col.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        col.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        col.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        col.Style.Border.Top.Color.SetColor(System.Drawing.Color.Silver);
                        col.Style.Border.Right.Color.SetColor(System.Drawing.Color.Silver);
                        col.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Silver);
                        col.Style.Border.Left.Color.SetColor(System.Drawing.Color.Silver);
                    }
                    if (!string.IsNullOrEmpty(format))
                    {
                        col.Style.Numberformat.Format = format;
                    }
                    switch (align)
                    {
                        case ExcelAlignment.Center:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            break;
                        case ExcelAlignment.CenterContinuous:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                            break;
                        case ExcelAlignment.Distributed:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Distributed;
                            break;
                        case ExcelAlignment.Fill:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Fill;
                            break;
                        case ExcelAlignment.General:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.General;
                            break;
                        case ExcelAlignment.Justify:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                            break;
                        case ExcelAlignment.Left:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            break;
                        case ExcelAlignment.Right:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            break;
                        default:
                            col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            break;
                    }
                }
            }
            catch (Exception ex) { }
        }
    }

    public enum ExcelAlignment
    {
        Center = 1,
        CenterContinuous = 2,
        Distributed = 3,
        Fill = 4,
        General = 5,
        Justify = 6,
        Left = 7,
        Right = 8,
    }

    public enum DataType
    {
        NoData = 0,
        String = 1,
        Byte = 2,
        Integer = 3,
        Long = 4,
        Single = 5,
        Double = 6,
        Decimal = 7,
        Boolean = 8,
        DateTime = 9,
        Money = 10,
    }
}
