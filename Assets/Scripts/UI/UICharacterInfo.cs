using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class UICharacterInfo : MonoBehaviour
{
    [Header("기본 정보")]
    public Image imageIcon;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDesc;
    public TextMeshProUGUI textAttack;
    public TextMeshProUGUI textDeffense;

    [Header("장착 슬롯")]
    public Button weaponSlotButton;
    public Image weaponSlotIcon;
    public TextMeshProUGUI weaponSlotText;

    public Button equipSlotButton;
    public Image equipSlotIcon;
    public TextMeshProUGUI equipSlotText;

    // 슬롯 클릭 시 어떤 슬롯인지 외부에 알림 (Weapon=0, Equip=1)
    public UnityEvent<int> onClickEquipSlot;

    private SaveCharacterData current;

    private void Awake()
    {
        weaponSlotButton.onClick.AddListener(() => onClickEquipSlot.Invoke(0));
        equipSlotButton.onClick.AddListener(() => onClickEquipSlot.Invoke(1));
    }

    public void SetEmpty()
    {
        current = null;
        imageIcon.sprite = null;
        textName.text = string.Empty;
        textDesc.text = string.Empty;
        textAttack.text = string.Empty;
        textDeffense.text = string.Empty;

        weaponSlotIcon.sprite = null;
        weaponSlotText.text = DataTableManager.StringTable.Get("EMPTY_SLOT");
        equipSlotIcon.sprite = null;
        equipSlotText.text = DataTableManager.StringTable.Get("EMPTY_SLOT");

        weaponSlotButton.interactable = false;
        equipSlotButton.interactable = false;
    }

    public void SetCharacterData(SaveCharacterData data)
    {
        current = data;

        imageIcon.sprite = data.CharacterData.SpriteIcon;
        textName.text = data.CharacterData.StringName;
        textDesc.text = data.CharacterData.StringDesc;
        textAttack.text = $"{DataTableManager.StringTable.Get("Attack")}: {data.TotalAttack}";
        textDeffense.text = $"{DataTableManager.StringTable.Get("Deffense")}: {data.TotalDeffense}";

        RefreshEquipSlots();

        weaponSlotButton.interactable = true;
        equipSlotButton.interactable = true;
    }

    public void RefreshEquipSlots()
    {
        if (current == null) return;

        if (current.WeaponItem != null)
        {
            weaponSlotIcon.sprite = current.WeaponItem.SpriteIcon;
            weaponSlotText.text = current.WeaponItem.StringName;
        }
        else
        {
            weaponSlotIcon.sprite = null;
            weaponSlotText.text = DataTableManager.StringTable.Get("EMPTY_SLOT");
        }

        if (current.EquipItem != null)
        {
            equipSlotIcon.sprite = current.EquipItem.SpriteIcon;
            equipSlotText.text = current.EquipItem.StringName;
        }
        else
        {
            equipSlotIcon.sprite = null;
            equipSlotText.text = DataTableManager.StringTable.Get("EMPTY_SLOT");
        }

        // 장착 후 스탯 갱신
        textAttack.text = $"{DataTableManager.StringTable.Get("Attack")}: {current.TotalAttack}";
        textDeffense.text = $"{DataTableManager.StringTable.Get("Deffense")}: {current.TotalDeffense}";
    }
}
