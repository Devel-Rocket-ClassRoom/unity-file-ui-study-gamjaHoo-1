using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIInvenSlot : MonoBehaviour
{
    public int slotIndex = -1;
    public Image imageIcon;
    public TextMeshProUGUI textName;

    public SaveItemData SaveItemData {  get; private set; }

    public Button button;
    public void SetEmpty()
    {
        imageIcon.sprite = null;
        textName.text = string.Empty;
        SaveItemData = null;
    }

    public void SetItem(SaveItemData data)
    {
        SaveItemData = data;
        imageIcon.sprite = SaveItemData.ItemData.SpriteIcon;
        textName.text = SaveItemData.ItemData.StringName;
    }
}
