using UnityEngine;
using UnityEngine.UI;


public class ItemTableTest1_Answer : MonoBehaviour
{
    public string itemId;

    public Image icon;
    public LocalizationText_Answer textName;

    public ItemTableTest2_Answer itemInfo;

    public void OnEnable()
    {
        OnChangeItemId();
    }

    public void OnValidate()
    {
        OnChangeItemId();
    }

    public void OnChangeItemId()
    {
        ItemData data = DataTableManager.ItemTable.Get(itemId);
        if (data != null)
        {
            icon.sprite = data.SpriteIcon;
            textName.id = data.Name;
            textName.OnChangedId();
        }
    }

    public void OnClick()
    {
        itemInfo.SetItemData(itemId);
    }
}
