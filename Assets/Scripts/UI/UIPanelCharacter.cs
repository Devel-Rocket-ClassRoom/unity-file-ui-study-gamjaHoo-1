using UnityEngine;
using TMPro;

public class UIPanelCharacter : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    public UICharacterSlotList uiCharacterSlotList;
    public UICharacterInfo uiCharacterInfo;
    public UIItemSelectPanel uiItemSelectPanel;

    private void OnEnable()
    {
        OnLoad();
        OnChangeSorting(sorting.value);
        OnChangeFiltering(filtering.value);

        uiCharacterSlotList.onSelectSlot.AddListener(OnSelectCharacter);
        uiCharacterSlotList.onUpdateSlots.AddListener(OnUpdateSlots);
        uiCharacterInfo.onClickEquipSlot.AddListener(OnClickEquipSlot);
    }

    private void OnDisable()
    {
        uiCharacterSlotList.onSelectSlot.RemoveListener(OnSelectCharacter);
        uiCharacterSlotList.onUpdateSlots.RemoveListener(OnUpdateSlots);
        uiCharacterInfo.onClickEquipSlot.RemoveListener(OnClickEquipSlot);
    }

    private SaveCharacterData selectedCharacter;

    private void OnSelectCharacter(SaveCharacterData data)
    {
        selectedCharacter = data;
        uiCharacterInfo.SetCharacterData(data);
    }

    private void OnUpdateSlots()
    {
        selectedCharacter = null;
        uiCharacterInfo.SetEmpty();
    }

    private void OnClickEquipSlot(int slotIndex)
    {
        if (selectedCharacter == null) return;
        uiItemSelectPanel.Open(
            selectedCharacter,
            slotIndex,
            SaveLoadManager.Data.itemList,
            uiCharacterInfo
        );
    }

    public void OnChangeSorting(int index)
    {
        uiCharacterSlotList.Sorting = (UICharacterSlotList.SortingOptions)index;
    }

    public void OnChangeFiltering(int index)
    {
        uiCharacterSlotList.Filtering = (UICharacterSlotList.FilteringOptions)index;
    }

    public void OnSave()
    {
        SaveLoadManager.Data.characterList = uiCharacterSlotList.GetSaveCharacterDataList();
        SaveLoadManager.Data.CharacterSorting = (int)uiCharacterSlotList.Sorting;
        SaveLoadManager.Data.CharacterFiltering = (int)uiCharacterSlotList.Filtering;
        SaveLoadManager.Save();
    }

    public void OnLoad()
    {
        SaveLoadManager.Load();
        uiCharacterSlotList.SetSaveCharacterDataList(SaveLoadManager.Data.characterList);
        uiCharacterSlotList.Sorting = (UICharacterSlotList.SortingOptions)SaveLoadManager.Data.CharacterSorting;
        uiCharacterSlotList.Filtering = (UICharacterSlotList.FilteringOptions)SaveLoadManager.Data.CharacterFiltering;
        sorting.value = SaveLoadManager.Data.CharacterSorting;
        filtering.value = SaveLoadManager.Data.CharacterFiltering;
    }

    public void OnCreateCharacter()
    {
        uiCharacterSlotList.AddRandomCharacter();
    }

    public void OnRemoveCharacter()
    {
        uiCharacterSlotList.RemoveCharacter();
    }
}
