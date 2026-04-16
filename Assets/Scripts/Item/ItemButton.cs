using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public ItemInfo centerButton;
    public string id;
    public TextMeshProUGUI text;
    public Image image;

    private ItemData data;

    private void OnValidate()
    {
        data = DataTableManager.ItemTable.Get(id);
        text.text = data.StringName;
        image.sprite = data.SpriteIcon;
    }

    public void OnClick()
    {
        centerButton.Change(data.StringName, data.StringDesc, data.SpriteIcon);
    }
}
