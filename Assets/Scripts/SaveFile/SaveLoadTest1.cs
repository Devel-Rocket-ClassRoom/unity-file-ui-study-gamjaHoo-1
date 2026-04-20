using UnityEngine;

public class SaveLoadTest1 : MonoBehaviour
{
    void Start()
    {
        //SaveLoadManager.Mode = SaveLoadManager.SaveMode.Text;

        SaveLoadManager.Mode = SaveLoadManager.SaveMode.Encrypted;
    }
    void Update()
    {
        // 1: 저장
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var data = new SaveDataV4();
            data.Name = "TEST1234";
            data.Gold = 4321;

            // DataTableManager의 ItemTable에서 전체 목록을 가져와 랜덤하게 아이템 추가
            var allItems = DataTableManager.ItemTable.GetAll();
            int count = Random.Range(1, allItems.Count + 1);
            for (int i = 0; i < count; i++)
            {
                var randomItem = allItems[Random.Range(0, allItems.Count)];
                var itemData = new SaveItemData();
                itemData.ItemData = randomItem;
                data.itemList.Add(itemData);
            }

            SaveLoadManager.Data = data;
            SaveLoadManager.Save();
            Debug.Log($"저장 완료 - Name: {data.Name}, Gold: {data.Gold}, 아이템 {data.itemList.Count}개");
        }

        // 2: 불러오기
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (SaveLoadManager.Load())
            {
                Debug.Log(SaveLoadManager.Data.Name);
                Debug.Log(SaveLoadManager.Data.Gold);

                foreach (var saveItemData in SaveLoadManager.Data.itemList)
                {
                    Debug.Log(saveItemData.InstanceId);
                    Debug.Log(saveItemData.ItemData.StringName);
                    Debug.Log(saveItemData.CreationTime);
                }
            }
            else
            {
                Debug.Log("세이브 파일 없음");
            }
        }

        // 3: V1 → V3 마이그레이션 테스트
        // V1 데이터를 Save1.json에 직접 써서 저장한 뒤, 2번으로 불러오면 자동 마이그레이션
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            var v1Data = new SaveDataV1();
            v1Data.PlayerName = "OldPlayer_V1";

            var testSettings = new Newtonsoft.Json.JsonSerializerSettings()
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All,
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(v1Data, testSettings);
            string path = System.IO.Path.Combine(
                Application.persistentDataPath, "Save", "SaveAuto.json"
            );
            System.IO.Directory.CreateDirectory(
                System.IO.Path.GetDirectoryName(path)
            );
            System.IO.File.WriteAllText(path, json);
            Debug.Log($"V1 테스트 데이터 저장 완료 → 2번으로 마이그레이션 확인");
        }
    }
}