using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public abstract class DataTable
{
    // {0} 자리에 String.Format()를 사용해서 경로를 합칠거임.
    public static readonly string FormatPath = "DataTables/{0}";

    public abstract void Load(string filename);

    // CSV를 읽어서 리스트로 리턴하는 메서드
    public static List<T> LoadCSV<T>(string csvText)
    {
        using (var reader = new StringReader(csvText))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // 현재 CSV 파일의 헤더와 일치하는 값을 넣어줌. - 
            // LoadCSV<Data>를 사용할 때, CSV 파일의 헤더와  Data 클래스 프로퍼티와 일치하는 값을 자동으로 넣어줌. Id, String 프로퍼티에 값을 자동으로 넣어줌. 
            var records = csvReader.GetRecords<T>();
            return records.ToList();
        }
    }
}
