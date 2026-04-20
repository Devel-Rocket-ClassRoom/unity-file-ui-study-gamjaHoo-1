using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Text;

public class KeyboardWindow : GenericWindow
{
    public TextMeshProUGUI displayText;
    public int maxLength = 8;

    public float cursorBlinkRate = 0.5f;
    private bool cursorVisible = true;
    private Coroutine cursorRoutine;

    private StringBuilder input = new StringBuilder();
    private const string CURSOR = "|";


    //public Button deleteButton;
    //public Button cancelButton;
    //public Button acceptButton;

    //private void Awake()
    //{
    //    Button[] allButtons = GetComponentsInChildren<Button>();

    //    foreach (Button btn in allButtons)
    //    {
    //        if (btn == deleteButton || btn == cancelButton || btn == acceptButton) continue;

    //        Button captured = btn;
    //        btn.onClick.AddListener(() => OnKeyPress(captured));
    //    }

    //    deleteButton.onClick.AddListener(OnDelete);
    //    cancelButton.onClick.AddListener(OnCancel);
    //    acceptButton.onClick.AddListener(OnAccept);
    //}
    public override void Open()
    {
        base.Open();
        input.Clear();
        cursorVisible = true;

        if (cursorRoutine != null) StopCoroutine(cursorRoutine);
        cursorRoutine = StartCoroutine(BlinkCursor());

        RefreshDisplay();
    }

    public override void Close()
    {
        if (cursorRoutine != null) StopCoroutine(cursorRoutine);
        base.Close();
    }
   public void OnKeyPress(Button button)
    {
        if (input.Length >= maxLength) return;

        string key = button.GetComponentInChildren<TextMeshProUGUI>().text;
        input.Append(key);
        RefreshDisplay();
    }

    public void OnDelete()
    {
        if (input.Length == 0) return;
        input.Remove(input.Length - 1, 1);
        RefreshDisplay();
    }

    public void OnCancel()
    {
        input.Clear();
        RefreshDisplay();
    }

    public void OnAccept()
    {
        windowManager.Open(0);
    }

    private void RefreshDisplay()
    {
        bool isFull = input.Length >= maxLength;
        string cursor = (!isFull && cursorVisible) ? CURSOR : "";
        displayText.text = input.ToString() + cursor;
    }

    private IEnumerator BlinkCursor()
    {
        while (true)
        {
            yield return new WaitForSeconds(cursorBlinkRate);
            cursorVisible = !cursorVisible;
            RefreshDisplay();
        }
    }
}