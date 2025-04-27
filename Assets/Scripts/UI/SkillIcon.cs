using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject iconPrefab;
    private GameObject draggingIcon;
    private RectTransform draggingRect;

    private Canvas canvas;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        draggingIcon = Instantiate(iconPrefab, canvas.transform);
        draggingIcon.GetComponent<Image>().sprite = GetComponent<Image>().sprite;

        draggingRect = draggingIcon.GetComponent<RectTransform>();

        draggingRect.localScale = new Vector3(0.029f, 0.048f, 1f);

        draggingIcon.transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out pos);

        draggingRect.anchoredPosition = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject target = GetSlotUnderPointer(eventData);
        if (target != null)
        {
            RemoveDuplicateIcons();

            foreach (Transform child in target.transform)
            {
                Destroy(child.gameObject);
            }

            draggingIcon.transform.SetParent(target.transform, false);

            draggingRect.anchorMin = new Vector2(0.5f, 0.5f);
            draggingRect.anchorMax = new Vector2(0.5f, 0.5f);
            draggingRect.pivot = new Vector2(0.5f, 0.5f);

            draggingRect.anchoredPosition = Vector2.zero;

            draggingRect.anchoredPosition = new Vector2(0.1f, 1f);
            draggingRect.sizeDelta = new Vector2(2569f, 1522f);
        }
        else
        {
            Destroy(draggingIcon);
        }

        draggingIcon = null;
    }

    private void RemoveDuplicateIcons()
    {
        GameObject[] slots = GameObject.FindGameObjectsWithTag("SkillSlot");

        foreach (GameObject slot in slots)
        {
            foreach (Transform child in slot.transform)
            {
                Image iconImage = child.GetComponent<Image>();
                if (iconImage != null && iconImage.sprite == GetComponent<Image>().sprite)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    private GameObject GetSlotUnderPointer(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            if (result.gameObject.CompareTag("SkillSlot"))
            {
                return result.gameObject;
            }
        }
        return null;
    }
}
