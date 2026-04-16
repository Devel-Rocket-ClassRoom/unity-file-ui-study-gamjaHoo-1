using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Dictionary : MonoBehaviour
{
    private string dictionaryDir;
    private string settingsPath;
    private string initialData;

    private Dictionary<string, string> settingDict;

    void Start()
    {
        dictionaryDir = Path.Combine(Application.persistentDataPath, "Dictionary");
        settingsPath = Path.Combine(dictionaryDir, "settings.cfg");
        initialData = "master_volume=80\nbgm_volume=70\nsfx_volume=90\nlanguage=kr\nshow_damage=true";
        settingDict = new Dictionary<string, string>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!Directory.Exists(dictionaryDir))
            {
                Directory.CreateDirectory(dictionaryDir);
                Debug.Log($"폴더 생성: {dictionaryDir}");
            }
            else
            {
                Debug.Log("폴더가 이미 존재함");
            }
            File.WriteAllText(settingsPath, initialData);
            Debug.Log("초기 세팅 파일 생성");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            using (StreamReader sr = new StreamReader(settingsPath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] split = line.Split('=');
                    settingDict[split[0]] = split[1];
                }
            }
            Debug.Log($"설정 로드 완료 (항목 {settingDict.Count}개)");
            Debug.Log($"--- 변경 전 ---");
            Debug.Log($"bgm_volume = {settingDict["bgm_volume"]}");
            Debug.Log($"language = {settingDict["language"]}");

            settingDict["bgm_volume"] = "50";
            settingDict["language"] = "en";
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            using (StreamWriter sw = new StreamWriter(settingsPath))
            {
                foreach (var dic in settingDict)
                {
                    sw.WriteLine($"{dic.Key}={dic.Value}");
                }
            }
            Debug.Log("--- 변경 후 저장 ---");
            Debug.Log($"bgm_volume = {settingDict["bgm_volume"]}");
            Debug.Log($"language = {settingDict["language"]}");

            Debug.Log("--- 최종 파일 내용 ---");
            Debug.Log(File.ReadAllText(settingsPath));
        }
    }
}