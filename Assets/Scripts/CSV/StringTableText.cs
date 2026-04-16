using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class StringTableText : MonoBehaviour
{
    public string id;
    public TextMeshProUGUI text;
    public Languages languages;

    //private void Start()
    //{
    //    OnChangedId();
    //}

    private void Update()
    {
        OnChangedId();
    }

    private void OnChangedId()
    {
        Variables.Language = languages;
        text.text = DataTableManager.StringTable.Get(id);
    }

    /*
    StringTableText 스크립트와 동일한 동작을 플레이 버튼 누르기 전에 실행
    열거형으로 언어 눌러보고 에디터에서 갱신할 수 있도록, 
    실행중에는 1번 영어, 2번 한국어.. 등으로 플레이중에 눌러서
    에디터에서 동작과 실행 동작 구분
    */

    public void ChangeLanguage(Languages language)
    {
        languages = language;
    }

}