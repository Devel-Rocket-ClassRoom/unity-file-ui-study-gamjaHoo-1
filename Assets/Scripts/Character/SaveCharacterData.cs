using System;
using Newtonsoft.Json;

[Serializable]
public class SaveCharacterData
{
    public Guid InstanceId { get; set; }
    public string CharacterDataId { get; set; }
    public DateTime CreationTime { get; set; }

    // null = 미장착
    public string WeaponItemId { get; set; } = null;
    public string EquipItemId { get; set; } = null;

    [JsonIgnore]
    public ItemData WeaponItem => WeaponItemId != null ? DataTableManager.ItemTable.Get(WeaponItemId) : null;
    [JsonIgnore]
    public ItemData EquipItem => EquipItemId != null ? DataTableManager.ItemTable.Get(EquipItemId) : null;

    [JsonIgnore]
    public int TotalAttack => CharacterData.Attack + (WeaponItem?.Value ?? 0);
    [JsonIgnore]
    public int TotalDeffense => CharacterData.Deffense + (EquipItem?.Value ?? 0);

    [JsonIgnore]
    public CharacterData CharacterData => DataTableManager.CharacterTable.Get(CharacterDataId);

    public SaveCharacterData()
    {
        InstanceId = Guid.NewGuid();
        CreationTime = DateTime.Now;
    }

    public static SaveCharacterData GetRandom()
    {
        var data = new SaveCharacterData();
        data.CharacterDataId = DataTableManager.CharacterTable.GetRandom().Id;
        return data;
    }

    public override string ToString()
    {
        return $"{InstanceId}\n{CreationTime}\n{CharacterDataId}";
    }
}
