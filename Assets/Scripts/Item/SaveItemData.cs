using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class SaveItemData
{
    public Guid InstanceId {  get; set; }

    [JsonConverter(typeof (ItemDataConverter))]
    public ItemData ItemData { get; set; }

    public DateTime CreationTime {  get; set; }

    public static SaveItemData GetRandomItem()
    {
        SaveItemData newItem = new SaveItemData();
        newItem.ItemData = DataTableManager.ItemTable.GetRandom();
        return newItem;
    }
    
    public SaveItemData()
    {
        InstanceId = Guid.NewGuid(); // 엄청 큰 정수를 return 해주는 것이기 때문에 그냥 난수를 return 받는다 받아도 됨.
                                     // 운이 더럽게 없으면 겹침
        CreationTime = DateTime.Now;

    }
}
