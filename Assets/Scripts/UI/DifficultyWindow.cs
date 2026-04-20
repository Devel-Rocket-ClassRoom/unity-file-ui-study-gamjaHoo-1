using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyWindow : GenericWindow
{
    public Toggle[] toggles;

    public int selected;

    private string saveDir;
    private string savePath;
    private const string DifficultyKey = "difficulty";
    private const int DefaultDifficulty = 1;

    private void Awake()
    {
        saveDir = Path.Combine(Application.persistentDataPath, "Settings");
        savePath = Path.Combine(saveDir, "difficulty.cfg");

        toggles[0].onValueChanged.AddListener(OnEasy);
        toggles[1].onValueChanged.AddListener(OnNormal);
        toggles[2].onValueChanged.AddListener(OnHard);
    }

    public override void Open()
    {
        base.Open();

        selected = LoadDifficulty();
        toggles[selected].isOn = true;
    }

    public override void Close()
    {
        base.Close();
    }

    public void OnEasy(bool active)
    {
        if (active) selected = 0;
        Debug.Log("OnEasy");
    }

    public void OnNormal(bool active)
    {
        if (active) selected = 1;
        Debug.Log("OnNormal");
    }

    public void OnHard(bool active)
    {
        if (active) selected = 2;
        Debug.Log("OnHard");
    }

    public void OnCancel()
    {
        windowManager.Open(0);
    }

    public void OnApply()
    {
        SaveDifficulty(selected);
        windowManager.Open(0);
    }

    private void SaveDifficulty(int value)
    {
        if (!Directory.Exists(saveDir))
        {
            Directory.CreateDirectory(saveDir);
        }

        File.WriteAllText(savePath, $"{DifficultyKey}={value}");
        Debug.Log($"난이도 저장 완료: {savePath}\n{DifficultyKey}={value}");
    }

    private int LoadDifficulty()
    {
        if (!File.Exists(savePath))
        {
            return DefaultDifficulty;
        }

        string line = File.ReadAllText(savePath);
        string[] split = line.Split('=');

        if (split.Length == 2 && split[0] == DifficultyKey && int.TryParse(split[1], out int value))
        {
            return Mathf.Clamp(value, 0, toggles.Length - 1);
        }

        return DefaultDifficulty;
    }
}