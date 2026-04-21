using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 1. CSV 파일 (ID / 이름 / 설명 / 공격력.../ 초상화(아이콘)...)
// 2. DataTable 상속
// 3. DataTableManager 등록
// 4. 테스트 패널 

public class CharacterTable : DataTable
{
    private readonly Dictionary<string, CharacterData> characterTable =
        new Dictionary<string, CharacterData>();

    private List<string> keyList;

    public override void Load(string filename)
    {
        characterTable.Clear();

        string path = string.Format(FormatPath, filename);
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        List<CharacterData> list = LoadCSV<CharacterData>(textAsset.text);

        foreach (var item in list)
        {
            if (!characterTable.ContainsKey(item.Id))
            {
                characterTable.Add(item.Id, item);
            }
            else
            {
                Debug.LogError("캐릭터 키 중복");
            }
        }
        keyList = characterTable.Keys.ToList();
    }

    public CharacterData Get(string id)
    {
        if (!characterTable.ContainsKey(id))
        {
            Debug.LogError("캐릭터 아이디 없음");
            return null;
        }
        return characterTable[id];
    }

    public CharacterData GetRandom()
    {
        return Get(keyList[UnityEngine.Random.Range(0, keyList.Count)]);
    }
}
