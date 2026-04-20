using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIInvenSlot : MonoBehaviour
{
    public Image imageIcon;
    public TextMeshProUGUI textName;

    public SaveItemData SaveItemData {  get; private set; }
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //SetEmpty();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var saveItemData = new SaveItemData();
            saveItemData.ItemData = DataTableManager.ItemTable.Get("Item1");

            SetItem(saveItemData);
        }
    }
}
