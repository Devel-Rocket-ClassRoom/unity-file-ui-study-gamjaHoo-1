using UnityEngine;
using SaveDataVC = SaveDataV4;
using Newtonsoft.Json;
using System.IO;

public static class SaveLoadManager
{
    public enum SaveMode
    {
        Text,               // JSON 텍스트(.json) - 개발용
        Encrypted,          // AES 암호화 바이너리(.dat) - 릴리즈용
    }

    public static SaveMode Mode {  get; set; } = SaveMode.Text;

    private static readonly string SaveDirectory = $"{Application.persistentDataPath}/Save";

    private static readonly string[] SaveFileNamesText =
    {
        "SaveAuto.json", "Save1.json", "Save2.json", "Save3.json",
    };
    private static readonly string[] SaveFileNamesEncrypted =
    {
        "SaveAuto.dat", "Save1.dat", "Save2.dat", "Save3.dat",
    };

    private static string GetFileName(int slot) =>
    Mode == SaveMode.Encrypted ? SaveFileNamesEncrypted[slot] : SaveFileNamesText[slot];

    public static int SaveDataVersion { get; } = 3;

    public static SaveDataVC Data { get; set; } = new SaveDataVC();

    private static JsonSerializerSettings settings = new JsonSerializerSettings()
    {
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.All,
    };

    public static bool Save(int slot = 0)
    {
        if (Data == null || slot < 0 || slot >= SaveFileNamesText.Length) return false;

        try
        {
            if (!Directory.Exists(SaveDirectory))
                Directory.CreateDirectory(SaveDirectory);

            var json = JsonConvert.SerializeObject(Data, settings);
            string path = Path.Combine(SaveDirectory, GetFileName(slot));

            if (Mode == SaveMode.Encrypted)
                File.WriteAllBytes(path, CryptoUtil.Encrypt(json));
            else
                File.WriteAllText(path, json);

            return true;
        }
        catch { Debug.LogError("Save 예외"); return false; }
    }

    public static bool Load(int slot = 0)
    {
        string path = Path.Combine(SaveDirectory, GetFileName(slot));
        if (!File.Exists(path)) return false;

        try
        {
            string json;

            if (Mode == SaveMode.Encrypted)
                json = CryptoUtil.Decrypt(File.ReadAllBytes(path));
            else
                json = File.ReadAllText(path);

            var saveData = JsonConvert.DeserializeObject<SaveData>(json, settings);

            while (saveData.Version < SaveDataVersion)
            {
                Debug.Log($"마이그레이션: V{saveData.Version} → V{saveData.Version + 1}");
                saveData = saveData.VersionUp();
            }

            Data = saveData as SaveDataVC;
            return true;
        }
        catch { Debug.LogError("Load 예외"); return false; }
    }
}