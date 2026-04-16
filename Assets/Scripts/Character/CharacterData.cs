using UnityEngine;

public class CharacterData
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public int Attack { get; set; }
    public int Deffense { get; set; }
    public string Icon { get; set; }

    // 현재 언어 설정에 맞게 이름이나 설명을 가져오려고 추가한 프로퍼티
    public string StringName => DataTableManager.StringTable.Get(Name);
    public string StringDesc => DataTableManager.StringTable.Get(Desc);
    public string KeyAttack = "Attack";
    public string KeyDeffense = "Deffense";

    public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}");

    public override string ToString()
    {
        return $"{Id} / {Name} / {Desc} / {Attack} / {Deffense} / {Icon}";
    }
}
