using CodeAwareTriage.Client.Models;
using ExcelDataReader;
using System.Data;

namespace CodeAwareTriage.Client.Services;

public class ExcelService
{
    public async Task<(List<string> Headers, List<DynamicRow> Rows)> ParseExcel(Stream fileStream)
    {
        // Must register code pages provider for ExcelDataReader
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        var headers = new List<string>();
        var rows = new List<DynamicRow>();

        using (var reader = ExcelReaderFactory.CreateReader(fileStream))
        {
            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });

            var table = result.Tables[0];
            
            // Read Headers
            foreach (DataColumn column in table.Columns)
            {
                headers.Add(column.ColumnName);
            }

            // Read Rows
            foreach (DataRow row in table.Rows)
            {
                var dynamicRow = new DynamicRow();
                foreach (DataColumn col in table.Columns)
                {
                    dynamicRow[col.ColumnName] = row[col];
                }
                rows.Add(dynamicRow);
            }
        }

        return await Task.FromResult((headers, rows));
    }
}
