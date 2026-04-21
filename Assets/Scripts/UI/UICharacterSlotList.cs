using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;

public class UICharacterSlotList : MonoBehaviour
{
    public enum SortingOptions
    {
        CreationTimeAscending,
        CreationTimeDescending,
        NameAscending,
        NameDescending,
    }

    public enum FilteringOptions
    {
        None,
        Warrior,
        Thief,
        Tanker,
    }

    public readonly System.Comparison<SaveCharacterData>[] comparisons =
    {
        (lhs, rhs) => lhs.CreationTime.CompareTo(rhs.CreationTime),
        (lhs, rhs) => rhs.CreationTime.CompareTo(lhs.CreationTime),
        (lhs, rhs) => lhs.CharacterData.StringName.CompareTo(rhs.CharacterData.StringName),
        (lhs, rhs) => rhs.CharacterData.StringName.CompareTo(lhs.CharacterData.StringName),
    };

    public readonly System.Func<SaveCharacterData, bool>[] filterings =
    {
        (x) => true,
        (x) => x.CharacterData.Name == "Warrior",
        (x) => x.CharacterData.Name == "Thief",
        (x) => x.CharacterData.Name == "Tanker",
    };

    public UICharacterSlot prefab;
    public ScrollRect scrollRect;
    private List<UICharacterSlot> uiSlotList = new List<UICharacterSlot>();

    private List<SaveCharacterData> saveCharacterDataList = new List<SaveCharacterData>();

    private SortingOptions sorting = SortingOptions.CreationTimeAscending;
    private FilteringOptions filtering = FilteringOptions.None;

    public SortingOptions Sorting
    {
        get => sorting;
        set
        {
            sorting = value;
            UpdateSlots();
        }
    }

    public FilteringOptions Filtering
    {
        get => filtering;
        set
        {
            if (filtering != value)
            {
                filtering = value;
                UpdateSlots();
            }
        }
    }

    private int selectedSlotIndex = -1;

    public UnityEvent onUpdateSlots;
    public UnityEvent<SaveCharacterData> onSelectSlot;

    private void OnDisable()
    {
        SaveLoadManager.Data.characterList = saveCharacterDataList;
        SaveLoadManager.Data.CharacterSorting = (int)sorting;
        SaveLoadManager.Data.CharacterFiltering = (int)filtering;
        SaveLoadManager.Save();

        saveCharacterDataList = null;
    }

    public void SetSaveCharacterDataList(List<SaveCharacterData> source)
    {
        saveCharacterDataList = source.ToList();
        UpdateSlots();
    }

    public List<SaveCharacterData> GetSaveCharacterDataList()
    {
        return saveCharacterDataList;
    }

    private void UpdateSlots()
    {
        var list = saveCharacterDataList.Where(filterings[(int)filtering]).ToList();
        list.Sort(comparisons[(int)sorting]);

        if (uiSlotList.Count < list.Count)
        {
            for (int i = uiSlotList.Count; i < list.Count; ++i)
            {
                var newSlot = Instantiate(prefab, scrollRect.content);
                newSlot.slotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                newSlot.button.onClick.AddListener(() =>
                {
                    selectedSlotIndex = newSlot.slotIndex;
                    onSelectSlot.Invoke(newSlot.SaveCharacterData);
                });

                uiSlotList.Add(newSlot);
            }
        }

        for (int i = 0; i < uiSlotList.Count; ++i)
        {
            if (i < list.Count)
            {
                uiSlotList[i].gameObject.SetActive(true);
                uiSlotList[i].SetCharacter(list[i]);
            }
            else
            {
                uiSlotList[i].gameObject.SetActive(false);
                uiSlotList[i].SetEmpty();
            }
        }

        selectedSlotIndex = -1;
        onUpdateSlots.Invoke();
    }

    public void AddRandomCharacter()
    {
        saveCharacterDataList.Add(SaveCharacterData.GetRandom());
        UpdateSlots();
    }

    public void RemoveCharacter()
    {
        if (selectedSlotIndex == -1) return;

        saveCharacterDataList.Remove(uiSlotList[selectedSlotIndex].SaveCharacterData);
        UpdateSlots();
    }
}
