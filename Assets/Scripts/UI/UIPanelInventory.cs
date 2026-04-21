using UnityEngine;
using TMPro;

public class UIPanelInventory : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    public UIInvenSlotList uiInvenSlotList;

    private void OnEnable()
    {
        OnLoad();
        OnChangeSorting(sorting.value);
        OnChangeFiltering(filtering.value);
    }

    public void OnChangeSorting(int index)
    {
        uiInvenSlotList.Sorting = (UIInvenSlotList.SortingOptions)index;
    }

    public void OnChangeFiltering(int index)
    {
        uiInvenSlotList.Filtering = (UIInvenSlotList.FilteringOptions)index;
    }

    public void OnSave()
    {
        SaveLoadManager.Data.itemList = uiInvenSlotList.GetSaveItemDataList();
        SaveLoadManager.Data.Sorting = (int)uiInvenSlotList.Sorting;
        SaveLoadManager.Data.Filtering = (int)uiInvenSlotList.Filtering;
        SaveLoadManager.Save();
    }

    public void OnLoad()
    {
        SaveLoadManager.Load();
        uiInvenSlotList.SetSaveItemDataList(SaveLoadManager.Data.itemList);
        uiInvenSlotList.Sorting = (UIInvenSlotList.SortingOptions)SaveLoadManager.Data.Sorting;
        uiInvenSlotList.Filtering = (UIInvenSlotList.FilteringOptions)SaveLoadManager.Data.Filtering;
        sorting.value = SaveLoadManager.Data.Sorting;
        filtering.value = SaveLoadManager.Data.Filtering;
    }

    public void OnCreateItem()
    {
        uiInvenSlotList.AddRandomItem();
    }

    public void OnRemoveItem()
    {
        uiInvenSlotList.RemoveItem();
    }
}