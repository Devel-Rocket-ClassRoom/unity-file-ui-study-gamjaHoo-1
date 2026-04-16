using System.Collections.Generic;

public static class DataTableManager
{
    // DataTable들을 저장할 Dictionary
    private static readonly Dictionary<string, DataTable> tables = new Dictionary<string, DataTable>();

    // 맨 아래의 메서드 T Get<T>를 통해 현재 선택된 언어에 맞는 StringTable을 가져옴
    public static StringTable StringTable => Get<StringTable>(DatableIds.String);

    public static ItemTable ItemTable => Get<ItemTable>(DatableIds.Item);

    public static CharacterTable CharacterTable => Get<CharacterTable>(DatableIds.Character);

    // 현재 설정된 언어와 무관하게 원하는 언어의 파일 불러옴
#if UNITY_EDITOR
    public static StringTable GetStringTable(Languages lang)
    {
        return Get<StringTable>(DatableIds.StringTableIds[(int)lang]);
    }
#endif

    // DataTableManager 최초 호출 시 초기화
    static DataTableManager()
    {
        Init();
    }


    private static void Init()
    {
        // 런타임 실행 시 tables에 현재 설정된 언어에 맞는 StringTable을 추가함.
#if !UNITY_EDITOR
        var stringTable = new StringTable();
        stringTable.Load(DatableIds.String);
        tables.Add(DatableIds.String, stringTable);

        
#else
        // 에디터에서 실행 시 모든 StringTable을 추가함.
        foreach (var id in DatableIds.StringTableIds)
        { 
            var stringTable = new StringTable();
            stringTable.Load(id);
            tables.Add(id, stringTable);
        }
#endif

        var itemTable = new ItemTable();
        itemTable.Load(DatableIds.Item);
        tables.Add(DatableIds.Item, itemTable);

        var characterTable = new CharacterTable();
        characterTable.Load(DatableIds.Character);
        tables.Add(DatableIds.Character, characterTable);
    }
    public static void ChangeLanguage(Languages lang)
    {
        // 바꿀 언어의 파일 이름을 읽어옴
        string newId = DatableIds.StringTableIds[(int)lang];

        // 현재 테이블에 해당 이름의 키에 해당하는 테이블이 추가되어 있으면 return
        if (tables.ContainsKey(newId))
            return;

        // 파일 이름들을 순회하면서 현재 테이블에 등록된 파일 이름을 찾아옴
        string oldId = string.Empty;
        foreach(var id in DatableIds.StringTableIds)
        {
            if (tables.ContainsKey(id))
            {
                oldId = id;
                break;
            }
        }

        // 기존의 파일 이름으로 된 DataTable을 가져옴
        var stringTable = tables[oldId];

        // 다형성 적용으로 실제 저장된 객체가 StringTable이기 때문에 오버라딩된 메서드가 실행됨
        // StringTable의 Load()는 StringTable 내부의 table을 초기화하고 csv파일을 저장함.
        stringTable.Load(newId);

        // 이제 바꿔서 사용되지 않는 StringTable을 날림
        tables.Remove(oldId);

        // 새로운 파일 이름에 맞게 갱신된 stringTable을 tables에 저장
        tables.Add(newId, stringTable);

        //var stringTable = StringTable;
        //stringTable.Load(DatableIds.StringTableIds[(int)lang]);
    }

    // 파일 이름을 넘기면 그 이름에 맞는 DataTable을 상속받는 T형을 불러옴 (현재는 StringTable 불러오는데 사용)
    public static T Get<T>(string id) where T : DataTable
    {
        if (!tables.ContainsKey(id))
        {
            Init();
        }
        return tables[id] as T;
    }
}
