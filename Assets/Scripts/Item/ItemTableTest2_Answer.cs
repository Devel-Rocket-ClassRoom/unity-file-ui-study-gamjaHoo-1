using UnityEngine;
using UnityEngine.UI;

public class ItemTableTest2_Answer : MonoBehaviour
{
    public Image icon;
    public LocalizationText_Answer textName;
    public LocalizationText_Answer textDesc;

    public void SetEmpty()
    {
        icon.sprite = null;
        textName.id = string.Empty;
        textDesc.id = string.Empty;
        
        textName.text.text = string.Empty;
        textDesc.text.text = string.Empty;
    }

    public void SetItemData(string itemId)
    {
        ItemData data = DataTableManager.ItemTable.Get(itemId);
        SetItemData(data);
    }

    public void SetItemData(ItemData data)
    {
        icon.sprite = data.SpriteIcon;
        textName.id = data.Name;
        textDesc.id = data.Desc;

        textName.OnChangedId();
        textDesc.OnChangedId();
    }
}
