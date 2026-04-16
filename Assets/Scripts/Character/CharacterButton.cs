using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
public class CharacterButton : MonoBehaviour
{
    public string characterId;

    public Image icon;
    public LocalizationText_Answer text;

    public CharacterInfo characterInfo;

    public void OnEnable()
    {
        OnChangeCharacterId();
    }

    public void OnValidate()
    {
        OnChangeCharacterId();
    }

    public void OnChangeCharacterId()
    {
        CharacterData characterData = DataTableManager.CharacterTable.Get(characterId);
        if (characterData != null)
        {
            icon.sprite = characterData.SpriteIcon;
            text.id = characterData.Name;
            text.OnChangedId();
        }
    }

    public void OnClick()
    {
        characterInfo.SetCharacterData(characterId);
    }
}
