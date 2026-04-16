public enum Languages
{
    Korean,
    English,
    Japanese,
}

public enum ItemTypes
{
    Weapon,
    Equip,
    Consumable,
}

public static class Variables
{   
    // 런타임에 언어가 바뀔 때 사용될 이벤트
    public static event System.Action OnLanguageChanged;

    private static Languages language = Languages.Korean;
    public static Languages Language
    {
        get
        {
            return language;
        }
        set
        {
            // 바꾸려는 언어 설정이 이미 같은 값이면 return
            if (language == value)
            {
                return;
            }
            language = value;

            // DataTableManager의 tables에 바뀐 언어의 DataTable을 추가
            DataTableManager.ChangeLanguage(language);
            
            // 언어가 바뀌었음을 이벤트로 알림
            OnLanguageChanged.Invoke();
        }
    }
}

public static class DatableIds
{
    public static readonly string[] StringTableIds =
    {
        "StringTableKr",
        "StringTableEn",
        "StringTableJp"
    };

    // Variables에서 열거형 변수 Language 값을 (int)로 형변환 해서 일치하는 테이블 Id를 매칭시켜줌
    public static string String => StringTableIds[(int)Variables.Language];

    public static readonly string Item = "ItemTable";

    public static readonly string Character = "CharacterTable";
}