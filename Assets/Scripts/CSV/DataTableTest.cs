using UnityEngine;

public class DataTableTest : MonoBehaviour
{

    public string NameStringTableKr = "StringTableKr";
    public string NameStringTableEn = "StringTableEn";
    public string NameStringTableJp = "StringTableJp";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Variables.Language = Languages.Korean;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Variables.Language = Languages.English;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Variables.Language = Languages.Japanese;
        }
    }

    public void OnClickStringTableKr()
    {
        //StringTable table = new StringTable();
        //table.Load(NameStringTableKr);
        Debug.Log(DataTableManager.StringTable.Get("YOU DIE"));
    }

    public void OnClickStringTableEn()
    {
        StringTable table = new StringTable();
        table.Load(NameStringTableEn);
        Debug.Log(table.Get("YOU DIE"));
        Debug.Log(table.Get("abdcas"));
    }

    public void OnClickStringTableJp()
    {
        StringTable table = new StringTable();
        table.Load(NameStringTableJp);
        Debug.Log(table.Get("YOU DIE"));
    }
}