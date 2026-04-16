using UnityEngine;

public class JsonTestObject : MonoBehaviour
{
    public string prefabName;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }
    public void Set(ObjectSaveData data)
    {
        // prefabName = data.prefabName;
        transform.position = data.pos;
        transform.rotation = data.rot;
        transform.localScale = data.scale;
        _renderer.material.color = data.color;
    }

    public ObjectSaveData GetSaveData()
    {
        var data = new ObjectSaveData();

        data.prefabName = prefabName;
        data.pos = transform.position;
        data.rot = transform.rotation;
        data.scale = transform.localScale;
        data.color = _renderer.material.color;

        return data;
    }
}


