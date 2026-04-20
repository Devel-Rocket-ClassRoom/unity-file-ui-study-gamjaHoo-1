using UnityEngine;
using UnityEngine.UI;

public class UIInvenSlotList : MonoBehaviour
{
    public UIInvenSlot prefab;
    public ScrollRect scrollRect;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for(int i = 0; i < 10; i++)
            {
                var saveItemData = SaveItemData.GetRandomItem();
                var newInven = Instantiate(prefab, scrollRect.content);
                newInven.SetItem(saveItemData);
            }
        }
    }
}
