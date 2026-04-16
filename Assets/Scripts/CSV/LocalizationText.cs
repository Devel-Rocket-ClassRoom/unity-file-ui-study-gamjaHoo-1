using UnityEngine;
using TMPro;

public class LocalizationText : MonoBehaviour
{
    private StringTableText[] allObjects;

    void Start()
    {
        allObjects = Object.FindObjectsByType<StringTableText>(FindObjectsSortMode.None);
        Localizing(Languages.Korean);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Localizing(Languages.Korean);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Localizing(Languages.English);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Localizing(Languages.Japanese);
        }
    }

    private void Localizing(Languages language)
    {
        foreach (var obj in allObjects)
        {
            obj.ChangeLanguage(language);
        }
    }

    /*
    StringTableText 스크립트와 동일한 동작을 플레이 버튼 누르기 전에 실행
    열거형으로 언어 눌러보고 에디터에서 갱신할 수 있도록, 
    실행중에는 1번 영어, 2번 한국어.. 등으로 플레이중에 눌러서
    에디터에서 동작과 실행 동작 구분
    */

}
