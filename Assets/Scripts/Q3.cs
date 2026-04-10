using UnityEngine;


//### 문제 3. 간이 키-값 설정 파일 시스템

//게임 설정을 `키=값` 형식의 텍스트 파일로 관리하는 시스템을 구현하시오. 설정 값을 수정한 뒤 파일에 다시 저장할 수 있어야 한다.

//**요구사항**

//- 아래 형식의 설정 파일(`settings.cfg`)을 생성할 것
//  ```
//  master_volume=80
//  bgm_volume=70
//  sfx_volume=90
//  language=kr
//  show_damage=true
//  ```
//- `StreamReader`로 파일을 읽어 `Dictionary<string, string>`에 파싱하여 저장할 것
//  - `=`를 기준으로 키와 값을 분리
//- Dictionary에서 `bgm_volume`을 `50`으로, `language`를 `en`으로 변경할 것
//- 변경된 Dictionary 내용을 다시 `StreamWriter`로 파일에 덮어쓸 것
//- 최종 파일 내용을 `File.ReadAllText`로 읽어 출력하여 변경이 반영되었는지 확인할 것

//**예상 출력**

//```
//설정 로드 완료 (항목 5개)
//--- 변경 전 ---
//bgm_volume = 70
//language = kr
//--- 변경 후 저장 ---
//bgm_volume = 50
//language = en
//--- 최종 파일 내용 ---
//master_volume=80
//bgm_volume=50
//sfx_volume=90
//language=en
//show_damage=true
//```
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Q3 : MonoBehaviour
{
    void Start()
    {
        string path = "settings.cfg";

        string initialContent =
            "master_volume=80\n" +
            "bgm_volume=70\n" +
            "sfx_volume=90\n" +
            "language=kr\n" +
            "show_damage=true";
        File.WriteAllText(path, initialContent);

        var settings = new Dictionary<string, string>();
        using (StreamReader sr = new StreamReader(path))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                int sep = line.IndexOf('=');
                if (sep < 0) continue;
                string key = line.Substring(0, sep);
                string value = line.Substring(sep + 1);
                settings[key] = value;
            }
        }
        Debug.Log($"설정 로드 완료 (항목 {settings.Count}개)");

        Debug.Log("--- 변경 전 ---");
        Debug.Log($"bgm_volume = {settings["bgm_volume"]}");
        Debug.Log($"language = {settings["language"]}");

        settings["bgm_volume"] = "50";
        settings["language"] = "en";


        using (StreamWriter sw = new StreamWriter(path, append: false))
        {
            foreach (var pair in settings)
            sw.WriteLine($"{pair.Key}={pair.Value}");
        }

        Debug.Log("--- 변경 후 저장 ---");
        Debug.Log($"bgm_volume = {settings["bgm_volume"]}");
        Debug.Log($"language = {settings["language"]}");

        Debug.Log("--- 최종 파일 내용 ---");
        Debug.Log(File.ReadAllText(path));
    }

    void Update() { }
}