using System.Collections.Generic;
using UnityEngine;

public class ItemQuickSlotManager : MonoBehaviour
{
    public static ItemQuickSlotManager Instance;

    private List<ItemQuickSlot> _quickSlots = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RegisterSlot(ItemQuickSlot slot)
    {
        if (!_quickSlots.Contains(slot))
            _quickSlots.Add(slot);
    }

    public void UnassignItemFromOtherSlots(string itemName, ItemQuickSlot exceptSlot)
    {
        foreach (var slot in _quickSlots)
        {
            if (slot == exceptSlot) continue;

            if (slot.HasItem(itemName))
            {
                slot.ClearSlot();
            }
        }
    }
}
