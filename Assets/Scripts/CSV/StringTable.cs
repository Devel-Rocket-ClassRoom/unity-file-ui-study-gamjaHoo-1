using System.Collections.Generic;
using UnityEngine;

public class StringTable : DataTable
{

    // Id, String은 공개 프로퍼티이며, .csv 파일의 헤더와 이름이 일치해야 함.
    public class Data
    {
        public string Id { get; set; }
        public string String { get; set; }
    }

    public static readonly string Unknown = "키 없음";

    private readonly Dictionary<string, string> table = new Dictionary<string, string>();

    public override void Load(string filename)
    {
        // 불러올 때, 기존 table을 초기화
        table.Clear();

        // 경로 지정 (추상 메서드의 FormatPath = "DataTables/{0}"에서 0 자리에 파일 이름을 넣어 경로 완성)
        string path = string.Format(FormatPath, filename);

        // 파일을 읽어와서 임시 저장
        TextAsset textAsset = Resources.Load<TextAsset>(path);

        // List<Data>형으로 읽어온 파일 저장
        var list = LoadCSV<Data>(textAsset.text);

        // table에 키-값 형태로 저장
        foreach (Data data in list)
        {
            if (!table.ContainsKey(data.Id))
            {
                table.Add(data.Id, data.String);
            }
            else
            {
                Debug.LogError($"키 중복: {data.Id}");
            }
        }
    }

    // 현재 table에 키 값으로 접근해서 value return
    public string Get(string key)
    {
        if (!table.ContainsKey(key))
        {
            return Unknown;
        }
        return table[key];
    }
}
