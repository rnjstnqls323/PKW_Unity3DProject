using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{
    public static ItemIcon Instance;

    [SerializeField] private GameObject hpPotionPrefab;
    [SerializeField] private GameObject mpPotionPrefab;
    [SerializeField] private GameObject curItemTextPrefab;

    [Header("Slot Root")]
    [SerializeField] private Transform contentParent;

    private List<Transform> _inventorySlots = new();
    private Dictionary<string, int> _itemCounts = new();
    private Dictionary<string, GameObject> _itemIcons = new();
    private Dictionary<string, InventorySlot> _itemSlots = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        foreach (Transform child in contentParent)
        {
            _inventorySlots.Add(child);
        }
    }

    public void AddItem(string itemName)
    {
        if (!_itemCounts.ContainsKey(itemName))
            _itemCounts[itemName] = 0;

        _itemCounts[itemName]++;

        if (_itemIcons.ContainsKey(itemName))
        {
            UpdateItemCountUI(itemName);
            return;
        }

        GameObject prefab = GetItemPrefab(itemName);
        if (prefab == null) return;

        Transform slot = FindFirstEmptySlot();
        if (slot == null)
        {
            return;
        }

        GameObject icon = Instantiate(prefab, slot);
        _itemIcons[itemName] = icon;

        RectTransform rt = icon.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
        rt.localScale = Vector3.one;

        InventorySlot slotComponent = slot.GetComponent<InventorySlot>();
        RegisterSlot(itemName, slotComponent);

        DraggableItem drag = icon.AddComponent<DraggableItem>();
        drag.Initialize(itemName, prefab, slotComponent);

        if (_itemCounts[itemName] > 1)
            CreateCountText(slot, itemName);
    }

    public GameObject GetItemPrefab(string itemName)
    {
        return itemName switch
        {
            "HpPotion" => hpPotionPrefab,
            "MpPotion" => mpPotionPrefab,
            _ => null
        };
    }

    private Transform FindFirstEmptySlot()
    {
        foreach (Transform slot in _inventorySlots)
        {
            if (slot.childCount == 0)
                return slot;
        }
        return null;
    }

    private void CreateCountText(Transform slot, string itemName)
    {
        GameObject textObj = Instantiate(curItemTextPrefab, slot);
        TextMeshProUGUI text = textObj.GetComponent<TextMeshProUGUI>();
        text.text = _itemCounts[itemName].ToString();

        RectTransform rt = textObj.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(1f, 1f);
        rt.anchorMax = new Vector2(1f, 1f);
        rt.pivot = new Vector2(1f, 1f);
        rt.anchoredPosition = new Vector2(1f, -96.5f);
        rt.sizeDelta = new Vector2(30f, 30f);
        rt.localScale = new Vector3(3.2f, 2f, 2f);
    }

    private void UpdateItemCountUI(string itemName)
    {
        GameObject icon = _itemIcons[itemName];
        TextMeshProUGUI text = icon.GetComponentInChildren<TextMeshProUGUI>();
        int count = _itemCounts[itemName];

        if (text == null)
        {
            CreateCountText(icon.transform, itemName);
        }
        else
        {
            text.text = _itemCounts[itemName].ToString();
        }

        foreach (ItemQuickSlot slot in FindObjectsOfType<ItemQuickSlot>())
        {
            if (slot.HasItem(itemName))
            {
                slot.UpdateCountText(count);
            }
        }
    }
    
    public InventorySlot GetSlotByItemName(string itemName)
    {
        if (_itemSlots.TryGetValue(itemName, out InventorySlot slot))
            return slot;
        return null;
    }

    public void RegisterSlot(string itemName, InventorySlot slot)
    {
        _itemSlots[itemName] = slot;
    }

    public int GetItemCount(string itemName)
    {
        if (_itemCounts.TryGetValue(itemName, out int count))
            return count;
        return 0;
    }
}
