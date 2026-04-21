using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
//using UnityEngine.Linq;
using System.Linq;

public class UIInvenSlotList : MonoBehaviour
{
    public enum SortingOptions
    {
        CreationTimeAscending,
        CreationTimeDescending,
        NameAscending,
        NameDescending,
        CostAscending,
        CostDescending,
    }
    public enum FilteringOptions
    {
        None,
        Weapon,
        Equip,
        Consumable,
        NotConsumable,
    }
    public readonly System.Comparison<SaveItemData>[] comparisons =
    {
        (lhs, rhs) => lhs.CreationTime.CompareTo(rhs.CreationTime),
        (lhs, rhs) => rhs.CreationTime.CompareTo(lhs.CreationTime),
        (lhs, rhs) => lhs.ItemData.StringName.CompareTo(rhs.ItemData.StringName),
        (lhs, rhs) => rhs.ItemData.StringName.CompareTo(lhs.ItemData.StringName),
        (lhs, rhs) => lhs.ItemData.Cost.CompareTo(rhs.ItemData.Cost),
        (lhs, rhs) => rhs.ItemData.Cost.CompareTo(lhs.ItemData.Cost),
    };

    public readonly System.Func<SaveItemData, bool>[] filterings =
    {
        (x) => true,
        (x) => x.ItemData.Type == ItemTypes.Weapon,
        (x) => x.ItemData.Type == ItemTypes.Equip,
        (x) => x.ItemData.Type == ItemTypes.Consumable,
        (x) => x.ItemData.Type != ItemTypes.Consumable,
    };
    public UIInvenSlot prefab;
    public ScrollRect scrollRect;
    private List<UIInvenSlot> uiSlotList = new List<UIInvenSlot>();
    //public int uiSlotMaxCount = 58;

    private List<SaveItemData> saveItemDataList = new List<SaveItemData>();

    private SortingOptions sorting = SortingOptions.CreationTimeAscending;
    private FilteringOptions filtering = FilteringOptions.None;

    public SortingOptions Sorting
    {
        get => sorting;
        set
        {
            sorting = value;
            UpdateSlots();
        }
    }

    public FilteringOptions Filtering
    {
        get => filtering;
        set
        {
            if(filtering != value)
            {
                filtering = value;
                UpdateSlots();
            }
        }
    }

    private int selectedSlotIndex = -1;

    public UnityEvent onUpdateSlots;
    public UnityEvent<SaveItemData> onSelectSlot;

    private void OnSelectSlot(SaveItemData saveItemData)
    {
        Debug.Log(saveItemData);
    }

    private void Start()
    {
        onSelectSlot.AddListener(OnSelectSlot);
    }
    // private void OnEnable()
    // {
    //     SetSaveItemDataList(SaveLoadManager.Data.itemList);
    // }

    private void OnDisable()
    {
        SaveLoadManager.Data.itemList = saveItemDataList;
        SaveLoadManager.Data.Sorting = (int)sorting;
        SaveLoadManager.Data.Filtering = (int)filtering;
        SaveLoadManager.Save();

        saveItemDataList = null;
    }

    public void SetSaveItemDataList(List<SaveItemData> source)
    {
        saveItemDataList = source.ToList();
        UpdateSlots();
    }

    public List<SaveItemData> GetSaveItemDataList()
    {
        return saveItemDataList;
    }

    private void UpdateSlots()
    {
        var list = saveItemDataList.Where(filterings[(int) filtering]).ToList();
        list.Sort(comparisons[(int) sorting]);

        if(uiSlotList.Count < list.Count)
        {
            for(int i = uiSlotList.Count; i < list.Count; ++i)
            {
                var newSlot = Instantiate(prefab, scrollRect.content);
                newSlot.slotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                // empty슬롯일 때 아래 작업 안하게 뭐 추가해주자
                newSlot.button.onClick.AddListener(() =>
                {
                   selectedSlotIndex = newSlot.slotIndex;
                   onSelectSlot.Invoke(newSlot.SaveItemData); 
                });

                uiSlotList.Add(newSlot);
            }
        }
        for(int i = 0; i < uiSlotList.Count; ++i)
        {
            if(i < list.Count)
            {
                uiSlotList[i].gameObject.SetActive(true);
                uiSlotList[i].SetItem(list[i]);
            }
            else
            {
                uiSlotList[i].gameObject.SetActive(false);
                uiSlotList[i].SetEmpty();
            }
        }
        selectedSlotIndex = -1;
        onUpdateSlots.Invoke();
    }

    // 호출마다 랜덤아이템 추가
    public void AddRandomItem()
    {
        saveItemDataList.Add(SaveItemData.GetRandomItem());

        UpdateSlots();
    }

    public void RemoveItem()
    {
        if(selectedSlotIndex == -1)
        {
            return;
        }

        saveItemDataList.Remove(uiSlotList[selectedSlotIndex].SaveItemData);
        UpdateSlots();
    }
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Alpha1))
    //     {
    //         for(int i = 0; i < 10; i++)
    //         {
    //             var saveItemData = SaveItemData.GetRandomItem();
    //             var newInven = Instantiate(prefab, scrollRect.content);
    //             newInven.SetItem(saveItemData);
    //         }
    //     }
    // }
}
