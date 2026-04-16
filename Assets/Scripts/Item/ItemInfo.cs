using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ItemInfo : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI info;
    public Image image;

    //public static event Action<string, string, Sprite> OnChangeInfo;

    public void Change(string returnName, string returnInfo, Sprite sprite)
    {
        name.text = returnName;
        info.text = returnInfo;
        image.sprite = sprite;
    }
}
