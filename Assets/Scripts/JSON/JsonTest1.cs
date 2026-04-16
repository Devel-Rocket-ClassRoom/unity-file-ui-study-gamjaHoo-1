using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class PlayerState
{
    public string playerName;
    public int lives;
    public float health;
    public Vector3 position;

    public override string ToString() => $"{playerName} / {lives} / {health} / {position}";
}

public class JsonTest1 : MonoBehaviour
{
    private JsonSerializerSettings jsonSetting;


    private void Awake()
    {
        jsonSetting = new();
        jsonSetting.Formatting = Formatting.Indented;
        jsonSetting.Converters.Add(new Vector3Converter());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Save
            PlayerState playerInfo = new()
            {
                playerName = "ABC",
                lives = 10,
                health = 10.999f,
                position = new(1f, 2f, 3f)
            };

            string dirPath = Path.Combine(
                Application.persistentDataPath,
                "JsonTest"
            );

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string path = Path.Combine(
                dirPath,
                "player2.json"
            );

            string json = JsonConvert.SerializeObject(playerInfo, jsonSetting);
            File.WriteAllText(path, json);

            Debug.Log(path);
            Debug.Log(json);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Load
            string dirPath = Path.Combine(
                Application.persistentDataPath,
                "JsonTest"
            );

            if (!Directory.Exists(dirPath)) return;

            string path = Path.Combine(
                dirPath,
                "player2.json"
            );

            string json = File.ReadAllText(path);
            var playerInfo = JsonConvert.DeserializeObject<PlayerInfo>(json, jsonSetting);

            Debug.Log(playerInfo);
        }
    }
}