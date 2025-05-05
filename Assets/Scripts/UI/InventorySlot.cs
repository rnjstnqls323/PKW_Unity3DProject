using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public bool HasItem()
    {
        return transform.childCount > 0;
    }

    public Transform GetItem()
    {
        return HasItem() ? transform.GetChild(0) : null;
    }
}
