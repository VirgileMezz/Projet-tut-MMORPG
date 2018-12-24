using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class DragUi : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

	private bool dragging = false;
    private EventSystem eventSystem;
    private GraphicRaycaster raycaster;
    private PointerEventData dataCurseur;

    private Vector3 startPosition;
    private GameObject objetDragged;
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        objetDragged = gameObject;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        objetDragged = null;
        transform.position = startPosition;
    }


    /*
    void Start () {
        //Camera camera = GameObject.FindGameObjectWithTag("camera").GetComponent<Camera>();
        raycaster = GetComponentInParent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        eventSystem =GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        //RaycastHit hit;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
                
            if (dragging == true){
			    Vector3 posSouris = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			    transform.position = new Vector3(posSouris.x,posSouris.y,posSouris.z);
		    }
            
            dataCurseur = new PointerEventData(eventSystem);
            dataCurseur.position = Input.mousePosition;
            List<RaycastResult> hit = new List<RaycastResult>();

            Debug.Log("appui sur lctrl");
            //if (Physics.Raycast(ray, out hit, 9999f))
            //{
            raycaster.Raycast(dataCurseur, hit);
            Debug.Log("passage raycast");
            foreach (RaycastResult hits in hit)
            {
                Debug.Log("Hit " + hits.gameObject.name);
            
                if (hits.gameObject == gameObject )
                    {
                        Debug.Log("bon gameobject");
                        Debug.Log(gameObject);


                        transform.position = new Vector3(0f,2f,0f);
                    }
                    //Debug.Log(gameObject);
                    //Debug.Log(hit.collider.gameObject);
            }
            //}
            
        }


        
	}*/

}
