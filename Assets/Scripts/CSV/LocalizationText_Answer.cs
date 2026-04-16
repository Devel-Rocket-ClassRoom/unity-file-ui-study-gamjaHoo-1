using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LocalizationText_Answer : MonoBehaviour
{
    // 에디터에서만 사용될 언어 변경 변수
#if UNITY_EDITOR
    public Languages editorLang;
#endif

    public string id;
    public TextMeshProUGUI text;

    public void OnEnable()
    {
        // 실행중이면 이벤트 구독
        if (Application.isPlaying)
        {
            Variables.OnLanguageChanged += OnChangeLanguage;

            OnChangedId();
        }
        // 에디터에서 언어 변경
#if UNITY_EDITOR
        else
        {
            OnChangeLanguage(editorLang);
            //OnChangedId();
        }
#endif
    }

    private void OnDisable()
    {
        Variables.OnLanguageChanged -= OnChangeLanguage;
    }

    // OnValidate(): 인스펙터 값이 변경될 때마다 실행되는 메서드
    private void OnValidate()
    {
#if UNITY_EDITOR
        OnChangeLanguage(editorLang);
        //OnChangedId();
#endif
    }

    public void OnChangedId()
    {
        text.text = DataTableManager.StringTable.Get(id);
    }

    private void OnChangeLanguage()
    {
        text.text = DataTableManager.StringTable.Get(id);
    }

#if UNITY_EDITOR
    private void OnChangeLanguage(Languages lang)
    {
        // 유니티 에디터에서 현재 설정된 언어와 상관없이 특정 언어 테이블 접근 
        var stringTable = DataTableManager.GetStringTable(lang);

        // 현재 설정된 id(키)에 맞는 값을 위에서 가져온 언어 테이블의 text로 출력
        text.text = stringTable.Get(id);
    }

    [ContextMenu("ChangeLanguage")]
    private void ChangeLanguage()
    {
        // 씬에 존재하는 모든 LocalizationText_Answer 컴포넌트를 배열로 가져옴
        // FindObjectsSortMode의 열거형을 무조건 넣어줘야함. None은 정렬하지 않고 가져와서 성능적으로 유리함
        var allText = Object.FindObjectsByType<LocalizationText_Answer>(FindObjectsSortMode.None);

        foreach (var text  in allText)
        {
            text.editorLang = this.editorLang;

            // 기존에는 Inspector의 값을 에디터에서 변경하면 자동으로 실행되는 코드지만, Inspector 값을 코드로 변경하면 OnValidate()가 호출되지 않아서 따로 호출
            text.OnChangeLanguage(editorLang);

            // 위의 코드로 scene에 있는 오브젝트들의 Inspector 값이 변경되지만 영구적인 저장이 안됨
            // 해당 오브젝트에 변경이 있음을 UnityEditor에게 알려서 Ctrl + S로 저장해야되는 내용임을 알려줌
            UnityEditor.EditorUtility.SetDirty(text);
        }
    }
#endif

    public void Describe()
    {
        Variables.OnLanguageChanged -= OnChangeLanguage;
    }
}
