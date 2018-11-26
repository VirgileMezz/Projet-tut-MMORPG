using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragUi : MonoBehaviour {

	private bool dragging = false;
	// Use this for initialization
	void Start () {
        //Camera camera = GameObject.FindGameObjectWithTag("camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("appui sur lctrl");
            if (Physics.Raycast(ray, out hit, 9999f))
            {
                Debug.Log("passage raycast");

                if (hit.collider.gameObject == gameObject )
                {
                    Debug.Log("bon gameobject");

                    transform.position = new Vector3(0f,2f,0f);
                }
            }
        }


        /*
        if (dragging == true){
			Vector3 posSouris = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position = new Vector3(posSouris.x,posSouris.y,posSouris.z);
		}
        */
	}
    /*
	void OnMouseOver(){
		if(Input.GetKeyDown(KeyCode.V))
        {//Input.GetMouseButtonDown(0)
            dragging = true;
		}
	}
    */
}
