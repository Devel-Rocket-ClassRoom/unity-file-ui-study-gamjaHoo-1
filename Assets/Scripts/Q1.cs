using System.IO;
using UnityEngine;

public class Q1 : MonoBehaviour
{

    //    ### 문제 1. 세이브 파일 관리자

    //`Application.persistentDataPath` 아래의 세이브 폴더를 탐색하여 저장된 파일 정보를 출력하고, 특정 파일을 복사/삭제할 수 있는 스크립트를 작성하시오.

    //** 요구사항**

    //1. **세이브 폴더 준비**: `SaveData` 폴더를 만들고, `File.WriteAllText`로 테스트용 파일 3개를 생성할 것
    //   - `save1.txt`, `save2.txt`, `save3.txt` (내용은 자유)
    //2. **파일 목록 출력**: `Directory.GetFiles`로 폴더 내 모든 파일을 조회하고, 각 파일의 이름과 확장자를 출력할 것
    //3. **파일 복사**: `save1.txt`를 `save1_backup.txt`로 복사할 것(`File.Copy`)
    //4. **파일 삭제**: `save3.txt`를 삭제할 것(`File.Delete`)
    //5. **최종 확인**: 작업 후 파일 목록을 다시 출력하여 결과를 확인할 것

    //* *예상 출력**

    //```
    //=== 세이브 파일 목록 ===
    //- save1.txt(.txt)
    //- save2.txt(.txt)
    //- save3.txt(.txt)
    //save1.txt → save1_backup.txt 복사 완료
    //save3.txt 삭제 완료
    //=== 작업 후 파일 목록 ===
    //- save1.txt (.txt)
    //- save1_backup.txt(.txt)
    //- save2.txt(.txt)
    //```

    string saveDir;
    string[] fileList = {"", "", "", ""};
    void Start()
    {

        saveDir = Path.Combine(Application.persistentDataPath, "SaveData");

        Debug.Log(saveDir);
        // 폴더가 없으면 생성
        if (!Directory.Exists(saveDir))
        {
            Directory.CreateDirectory(saveDir);
            Debug.Log($"폴더 생성: {saveDir}");
        }
        else
        {
            Debug.Log("폴더가 이미 존재합니다.");
        }

        // FileMode를 직접 지정하여 FileStream 생성
        using (FileStream fs = new FileStream(Path.Combine(saveDir, "save1.txt"), FileMode.Create)) { }
        using (FileStream fs = new FileStream(Path.Combine(saveDir, "save2.txt"), FileMode.Create)) { }
        using (FileStream fs = new FileStream(Path.Combine(saveDir, "save3.txt"), FileMode.Create)) { }

        // 폴더 내 파일 목록
        string[] files = Directory.GetFiles(saveDir);
        Debug.Log("=== 세이브 파일 목록 ===");
        int i = 0;
        foreach (string file in files)
        {
            Debug.Log($"{Path.GetFileName(file)}, ({Path.GetExtension(file)})");
            fileList[i] = files[i];
            i++;

        }



    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            File.Copy(fileList[0], Path.Combine(saveDir, "save1_backup.txt"));
            Debug.Log("save1.txt → save1_backup.txt 복사 완료");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            File.Delete(fileList[2]);
            Debug.Log("save3.txt 삭제 완료");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("=== 작업 후 파일 목록 ===");
            string[] finalFiles = Directory.GetFiles(saveDir);
            foreach (string file in finalFiles)
            {
                Debug.Log($"{Path.GetFileName(file)}, ({Path.GetExtension(file)})");
            }
        }
    }
}
