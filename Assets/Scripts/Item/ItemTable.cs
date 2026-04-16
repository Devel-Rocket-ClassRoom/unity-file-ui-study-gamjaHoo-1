using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public string Id { get; set; }
    public ItemTypes Type { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public int Value { get; set; }
    public int Cost { get; set; }
    public string Icon { get; set; }

    // 현재 언어 설정에 맞게 이름이나 설명을 가져오려고 추가한 프로퍼티
    public string StringName => DataTableManager.StringTable.Get(Name);
    public string StringDesc => DataTableManager.StringTable.Get(Desc);

    // 경로를 통해서 Icon을 스프라이트로 불러오기 위한 프로퍼티
    public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}");

    public override string ToString()
    {
        return $"{Id} / {Type} / {Name} / {Desc} / {Value} / {Cost} / {Icon}";
    }
}

public class ItemTable : DataTable
{
    private readonly Dictionary<string, ItemData> itemTable =
        new Dictionary<string, ItemData>();
    public override void Load(string filename)
    {
        itemTable.Clear();

        string path = string.Format(FormatPath, filename);
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        List<ItemData> list = LoadCSV<ItemData>(textAsset.text);

        foreach (var item in list)
        {
            if (!itemTable.ContainsKey(item.Id))
            {
                itemTable.Add(item.Id, item);
            }
            else
            {
                Debug.LogError("아이템 아이디 중복");
            }
        }
    }

    public ItemData Get(string id)
    {
        if (!itemTable.ContainsKey(id))
        {
            Debug.LogError("아이템 아이디 없음");
            return null;
        }
        return itemTable[id];
    }

    //  랜덤 선택 시 활용하기 위한 전체 아이템 목록 반환
    public List<ItemData> GetAll()
    {
        return new List<ItemData>(itemTable.Values);
    }
}