using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace TestLibrary.Utilities;

public class CsvUtils
{
    public static List<T> GetList<T>(string filePath, CsvConfiguration? config = null)
    {
        config ??= new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            NewLine = Environment.NewLine,
            HasHeaderRecord = false
        };
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<T>().ToList();

        return records;
    }
}