using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

[System.Serializable]
public abstract class SaveData
{
    public int Version { get; protected set; }

    public abstract SaveData VersionUp();
}

[System.Serializable]
public class SaveDataV1 : SaveData
{
    public string PlayerName { get; set; } = string.Empty;

    public SaveDataV1()
    {
        Version = 1;
    }

    public override SaveData VersionUp()
    {
        var saveData = new SaveDataV2();
        saveData.Name = PlayerName;
        return saveData;
    }
}

[System.Serializable]
public class SaveDataV2 : SaveData
{
    public string Name { get; set; } = string.Empty;
    public int Gold = 0;
    public SaveDataV2()
    {
        Version = 2;
    }

    public override SaveData VersionUp()
    {
        throw new System.NotImplementedException();
    }
}

[System.Serializable]
public class SaveDataV3 : SaveDataV2
{
    public List<string> itemList = new List<string>();
    string[] ids = { "Item1", "Item2", "Item3", "Item4" };


    public SaveDataV3()
    {
        Version = 3;

    }

    public override SaveData VersionUp()
    {
        SaveDataV4 data = new SaveDataV4();
        data.Name = Name;
        data.Gold = Gold;
        foreach (string id in itemList)
        {
            SaveItemData itemData = new SaveItemData();
            itemData.ItemData = DataTableManager.ItemTable.Get(id);
            data.itemList.Add(itemData);
        }
        return data;
    }


}
[System.Serializable]
public class SaveDataV4 : SaveDataV2
{
    public List<SaveItemData> itemList = new List<SaveItemData>();
    string[] ids = { "Item1", "Item2", "Item3", "Item4" };


    public SaveDataV4()
    {
        Version = 4;

    }
}