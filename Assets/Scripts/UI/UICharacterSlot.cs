using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UICharacterSlot : MonoBehaviour
{
    public int slotIndex = -1;
    public Image imageIcon;
    public TextMeshProUGUI textName;

    public SaveCharacterData SaveCharacterData { get; private set; }

    public Button button;

    public void SetEmpty()
    {
        imageIcon.sprite = null;
        textName.text = string.Empty;
        SaveCharacterData = null;
    }

    public void SetCharacter(SaveCharacterData data)
    {
        SaveCharacterData = data;
        imageIcon.sprite = data.CharacterData.SpriteIcon;
        textName.text = data.CharacterData.StringName;
    }
}
