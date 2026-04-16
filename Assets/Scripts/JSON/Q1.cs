using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class SomeClass
{
    [JsonConverter(typeof(Vector3Converter))] public Vector3 pos;
    [JsonConverter(typeof(QuaternionConverter))] public Quaternion rot;
    [JsonConverter(typeof(Vector3Converter))] public Vector3 scale;
    [JsonConverter(typeof(ColorConverter))] public Color color;
    [JsonIgnore]
    public static PrimitiveType[] types = {
        PrimitiveType.Capsule,
        PrimitiveType.Cube,
        PrimitiveType.Cylinder,
        PrimitiveType.Sphere,
    };
}

[System.Serializable]
public class ObjectSaveData
{
    public string prefabName;
    [JsonConverter(typeof(Vector3Converter))] public Vector3 pos;
    [JsonConverter(typeof(QuaternionConverter))] public Quaternion rot;
    [JsonConverter(typeof(Vector3Converter))] public Vector3 scale;
    [JsonConverter(typeof(ColorConverter))] public Color color;
}

public class Q1 : MonoBehaviour
{
    private string path;
    private JsonTestObject[] targets = new JsonTestObject[3];
    public string[] prefabNames = {
        "Capsule",
        "Cube",
        "Cylinder",
        "Sphere"
    };

    private void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "JsonTest");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        path = Path.Combine(path, "Randoms.json");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Clear();
        }
    }

    public void Save()
    {
        List<ObjectSaveData> list = new();
        foreach (var target in targets)
        {
            list.Add(target.GetSaveData());
        }
        var json = JsonConvert.SerializeObject(list, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    public void Load()
    {
        var json = File.ReadAllText(path);
        var list = JsonConvert.DeserializeObject<List<ObjectSaveData>>(json);
        for (int i = 0; i < list.Count; i++)
        {
            if (targets[i] != null) Destroy(targets[i].gameObject);

            var prefab = Resources.Load<JsonTestObject>(list[i].prefabName);
            var obj = Instantiate(prefab);
            obj.transform.position = list[i].pos;
            obj.transform.rotation = list[i].rot;
            obj.transform.localScale = list[i].scale;
            obj.GetComponent<Renderer>().material.color = list[i].color;

            targets[i] = obj;
        }
    }

    public void Create()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i]) continue;

            if (targets[i] == null) targets[i] = CreateRandomObject();
        }

    }

    private JsonTestObject CreateRandomObject()
    {
        var prefabName = prefabNames[Random.Range(0, prefabNames.Length)];
        var prefab = Resources.Load<JsonTestObject>(prefabName);
        var obj = Instantiate(prefab);
        obj.transform.position = Random.insideUnitSphere * 7f;
        obj.transform.rotation = Random.rotation;
        obj.transform.localScale = Vector3.one * Random.Range(0.5f, 2f);
        obj.GetComponent<Renderer>().material.color = Random.ColorHSV();

        return obj;
    }

    public void Clear()
    {
        foreach (var target in targets)
        {
            Destroy(target.gameObject);
        }
        targets = new JsonTestObject[targets.Length];
    }
}
