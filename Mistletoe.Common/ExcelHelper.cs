// <copyright file="ExcelHelper.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using ClosedXML.Excel;

    /// <summary>
    ///     Excel helper
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        ///     Get sample data
        /// </summary>
        /// <param name="dataFilePath">Data file path</param>
        /// <returns>Excel data</returns>
        public static ExcelData GetSampleData(string dataFilePath)
        {
            bool decryptFileSuccess = FileHelper.DecryptFile(dataFilePath);
            if (decryptFileSuccess)
            {
                var camapignData = GetCampaignData(dataFilePath);
                return camapignData[0];
            }
            else
            {
                return new ExcelData();
            }
        }

        /// <summary>
        ///     Get campaign data
        /// </summary>
        /// <param name="dataFilePath">Data file path</param>
        /// <returns>Excel data</returns>
        public static List<ExcelData> GetCampaignData(string dataFilePath)
        {
            bool decryptFileSuccess = FileHelper.DecryptFile(dataFilePath);
            if (decryptFileSuccess)
            {
                var workbook = new XLWorkbook(dataFilePath);
                return GetData(workbook.Worksheet("Data"));
            }
            else
            {
                return new List<ExcelData>();
            }
        }

        private static List<ExcelData> GetData(IXLWorksheet worksheet)
        {
            var excelData = new List<ExcelData>();

            var totalRows = worksheet.RowsUsed().Count();

            for (int i = 2; i <= totalRows; i++)
            {
                var excelRowData = worksheet.Row(i);
                excelData.Add(GetExcelData(excelRowData));
            }

            return excelData;
        }

        private static ExcelData GetExcelData(IXLRow excelRowData)
        {
            var excelData = new ExcelData();
            excelData.Receiver = excelRowData.Cell(1).GetValue<string>();
            excelData.Placeholders.Add(excelRowData.Cell(2).GetValue<string>());
            excelData.Placeholders.Add(excelRowData.Cell(3).GetValue<string>());
            excelData.Placeholders.Add(excelRowData.Cell(4).GetValue<string>());
            excelData.Placeholders.Add(excelRowData.Cell(5).GetValue<string>());
            excelData.Placeholders.Add(excelRowData.Cell(6).GetValue<string>());
            excelData.Placeholders.Add(excelRowData.Cell(7).GetValue<string>());
            excelData.Placeholders.Add(excelRowData.Cell(8).GetValue<string>());
            excelData.Placeholders.Add(excelRowData.Cell(9).GetValue<string>());
            excelData.Placeholders.Add(excelRowData.Cell(10).GetValue<string>());
            excelData.Placeholders.Add(excelRowData.Cell(11).GetValue<string>());
            return excelData;
        }
    }
}