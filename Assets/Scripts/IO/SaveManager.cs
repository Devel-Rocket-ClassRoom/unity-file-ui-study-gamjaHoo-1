using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string saveDir;
    private string save1Path;
    private string save2Path;
    private string save3Path;
    private string copyPath;

    private string saveData1;
    private string saveData2;
    private string saveData3;

    private void Start()
    {
        saveDir = Path.Combine(Application.persistentDataPath, "SaveData");
        save1Path = Path.Combine(saveDir, "save1.txt");
        save2Path = Path.Combine(saveDir, "save2.txt");
        save3Path = Path.Combine(saveDir, "save3.txt");
        copyPath = Path.Combine(saveDir, "save1_backup.txt");

        saveData1 = "save-01";
        saveData2 = "save-02";
        saveData3 = "save-03";
    }

    private void Update()
    {      
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
                Debug.Log($"폴더 생성: {saveDir}");
            }
            else
            {
                Debug.Log("폴더가 이미 존재함");
            }

            File.WriteAllText(save1Path, saveData1);
            Debug.Log("save1 완료");

            File.WriteAllText(save2Path, saveData2);
            Debug.Log("save2 완료");

            File.WriteAllText(save3Path, saveData3);
            Debug.Log("save3 완료");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("=== 세이브 파일 목록 ===");
            string[] files = Directory.GetFiles(saveDir);
            foreach (string file in files)
            {
                Debug.Log($"- {Path.GetFileName(file)} ({Path.GetExtension(file)})");
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!File.Exists(copyPath))
            {
                File.Copy(save1Path, copyPath);
                Debug.Log($"{save1Path} → {copyPath} 복사 완료");
            }
            else
            {
                Debug.Log($"카피 파일 이미 존재함");
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (File.Exists(save3Path))
            {
                File.Delete(save3Path);
                Debug.Log($"{save3Path} 삭제 완료");
            }
            else
            {
                Debug.Log("save3 이미 삭제됨");
            }
        }

        if (Input.GetKeyDown (KeyCode.R))
        {
            Debug.Log("=== 작업 후 파일 목록 ===");
            string[] files = Directory.GetFiles(saveDir);
            foreach (string file in files)
            {
                Debug.Log($"- {Path.GetFileName(file)} ({Path.GetExtension(file)})");
            }
        }
    }
}
