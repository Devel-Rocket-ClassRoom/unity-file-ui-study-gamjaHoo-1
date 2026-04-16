using System.IO;
using UnityEngine;

public class PlayerInfo
{
    public string playerName;
    public int lives;
    public float health;
    public Vector3 position;
    // public Dictionary<string, int> scores = new()
    // {
    //     {"stage1", 100},
    //     {"stage2", 200},
    //     {"stage3", 300}
    // };
}

public class JsonUtilityTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Save
            PlayerInfo playerInfo = new()
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
                "player.json"
            );

            string json = JsonUtility.ToJson(playerInfo, prettyPrint: true);
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
                "player.json"
            );

            string json = File.ReadAllText(path);
            // var playerInfo = JsonUtility.FromJson<PlayerInfo>(json);
            PlayerInfo playerInfo = new();
            JsonUtility.FromJsonOverwrite(json, playerInfo);

            Debug.Log(playerInfo.playerName);
            Debug.Log(playerInfo.lives);
            Debug.Log(playerInfo.health);
            Debug.Log(playerInfo.position);
        }

    }
}
