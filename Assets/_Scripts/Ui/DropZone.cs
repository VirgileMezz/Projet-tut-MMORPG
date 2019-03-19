using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DropZone : MonoBehaviour, IDropHandler
{

    

    public GameObject ObjectDragged
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;


        }
    }

    

    #region IdropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {
        if (!ObjectDragged)
        {
            DragUi.objectDragged.transform.SetParent(transform);
            DragUi.objectDragged.GetComponent<RectTransform>().localPosition = Vector3.zero;

        }
    }
    #endregion
}
