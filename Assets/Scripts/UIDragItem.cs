using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerDownHandler
{
    public UIDropSlot currentSlot;
    private Canvas canvas;
    private GraphicRaycaster graphicRaycaster;
    private Core core;

    public delegate void PointerDownOnCoreDelegate(string type, float amount);
    public static event PointerDownOnCoreDelegate PointerDownOnCore;

    public delegate void OnCorePartAddDelegate(Core core);
    public static event OnCorePartAddDelegate OnCorePartAdd;

    public delegate void OnCorePartRemoveDelegate(Core core);
    public static event OnCorePartRemoveDelegate OnCorePartRemove;

    public delegate void SaveCoresAfterChangeDelegate();
    public static event SaveCoresAfterChangeDelegate SaveCoreAfterChange;

    public Vector2 initialPos;

    private void Start()
    {
        core = GetComponent<Core>();

        if (!canvas)
        {
            canvas = GetComponentInParent<Canvas>();
            graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(core.coreType == CoreType.SPEED)
        {
            PointerDownOnCore?.Invoke(core.coreType.ToString(), core.statValue);
        }
        if(core.coreType == CoreType.ACC)
        {
            PointerDownOnCore?.Invoke(core.coreType.ToString(), core.statValue);
        }
        if(core.coreType == CoreType.BOOST)
        {
            PointerDownOnCore?.Invoke(core.coreType.ToString(), core.statValue); 
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0) / transform.lossyScale.x;
        transform.SetParent(canvas.transform, true);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0) / transform.lossyScale.x; // Thanks to the canvas scaler we need to devide pointer delta by canvas scale to match pointer movement.
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var results = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, results);

        foreach (var hit in results)
        {
            var slot = hit.gameObject.GetComponent<UIDropSlot>();
            if (slot)
            {
                if(slot.gameObject.tag == Tags.CoreInUseContainer && currentSlot.gameObject.tag == Tags.CoreAvailableContainer)
                {
                    OnCorePartAdd?.Invoke(core);
                }
                if(slot.gameObject.tag == Tags.CoreAvailableContainer && currentSlot.gameObject.tag == Tags.CoreInUseContainer)
                {
                    OnCorePartRemove?.Invoke(core);
                    Debug.Log("On Core Part Remove");
                }
                if (slot.currentItem)
                {
                    slot.currentItem.SwapObject(currentSlot);
                    currentSlot = slot;
                    currentSlot.currentItem = this;
                }
                else
                {
                    currentSlot.currentItem = null;
                    currentSlot = slot;
                    currentSlot.currentItem = this;
                }
                break;
            }
        }
      
        transform.SetParent(currentSlot.transform,true);
        if(currentSlot.gameObject.tag == Tags.CoreAvailableContainer)
        {
            transform.GetComponent<RectTransform>().localPosition = initialPos;
            Debug.Log(initialPos);
        }
        else
        {
            Debug.Log(Vector3.zero);
            transform.localPosition = Vector3.zero;
        }

        SaveCoreAfterChange?.Invoke();
    }

    public void SwapObject(UIDropSlot slot)
    {
        currentSlot = slot;
        currentSlot.currentItem = this;
        transform.SetParent(currentSlot.transform);
        transform.localPosition = Vector3.zero;
    }
}