using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class DragUi : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject objectDragged;
    private GameObject clone;
    private bool keepObject = false;
    Vector3 startPosition;
    Transform startParent;

    private void Start()
    {
        //if(transform.parent.gameObject != null)
        //{
        if (transform.parent.gameObject.tag == "SpellSlot")
        {
            print("iauzuie");
            keepObject = true;
        }
        startParent = transform.parent;
        //}
        
        

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (keepObject)
        {
            clone = Instantiate(gameObject,startParent);
            keepObject = false;
        }
       
        objectDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        transform.SetParent(transform.root);

        GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        objectDragged = null;
        if (transform.parent == startParent || transform.parent == transform.root)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;

    }


}
