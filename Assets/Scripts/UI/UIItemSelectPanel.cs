using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 장착 슬롯 클릭 시 나타나는 아이템 선택 패널
public class UIItemSelectPanel : MonoBehaviour
{
    public UIInvenSlot prefab;
    public ScrollRect scrollRect;
    public Button unequipButton;
    public Button closeButton;

    private List<UIInvenSlot> uiSlotList = new List<UIInvenSlot>();
    private SaveCharacterData targetCharacter;
    private int targetSlot; // 0=무기, 1=장비

    private UICharacterInfo characterInfo;

    private void Awake()
    {
        unequipButton.onClick.AddListener(OnUnequip);
        closeButton.onClick.AddListener(() => gameObject.SetActive(false));
    }

    public void Open(SaveCharacterData character, int slotIndex, List<SaveItemData> itemList, UICharacterInfo info)
    {
        targetCharacter = character;
        targetSlot = slotIndex;
        characterInfo = info;

        // 슬롯 타입에 맞는 아이템만 필터링
        ItemTypes targetType = slotIndex == 0 ? ItemTypes.Weapon : ItemTypes.Equip;
        var filtered = itemList.Where(x => x.ItemData.Type == targetType).ToList();

        RefreshSlots(filtered);
        gameObject.SetActive(true);
    }

    private void RefreshSlots(List<SaveItemData> list)
    {
        if (uiSlotList.Count < list.Count)
        {
            for (int i = uiSlotList.Count; i < list.Count; ++i)
            {
                var newSlot = Instantiate(prefab, scrollRect.content);
                newSlot.SetEmpty();
                int captured = i;
                newSlot.button.onClick.AddListener(() => OnSelectItem(newSlot.SaveItemData));
                uiSlotList.Add(newSlot);
            }
        }

        for (int i = 0; i < uiSlotList.Count; ++i)
        {
            if (i < list.Count)
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
    }

    private void OnSelectItem(SaveItemData itemData)
    {
        if (targetSlot == 0)
            targetCharacter.WeaponItemId = itemData.ItemData.Id;
        else
            targetCharacter.EquipItemId = itemData.ItemData.Id;

        characterInfo.RefreshEquipSlots();
        gameObject.SetActive(false);
    }

    private void OnUnequip()
    {
        if (targetSlot == 0)
            targetCharacter.WeaponItemId = null;
        else
            targetCharacter.EquipItemId = null;

        characterInfo.RefreshEquipSlots();
        gameObject.SetActive(false);
    }
}
