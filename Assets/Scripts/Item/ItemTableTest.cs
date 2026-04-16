using UnityEngine;

public class ItemTableTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(DataTableManager.ItemTable.Get("Item1"));
        }
    }
}
