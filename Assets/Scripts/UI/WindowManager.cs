using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public GenericWindow[] windows; // 일단은 인덱스. 나중에 열거형으로 바꿈

    public int currentWindowId;
    public int defaultWindowId;

    private void Awake()
    {
        foreach(var window in windows)
        {
            window.gameObject.SetActive(false);
            window.Init(this);
        }

        currentWindowId = defaultWindowId;
        windows[currentWindowId].Open();
    }

    public GenericWindow Open(int id)
    {
        windows[currentWindowId].Close();
        currentWindowId = id;
        windows[currentWindowId].Open();

        return windows[currentWindowId];
    }
}
