using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarActionPanelEmplacementScript : MonoBehaviour {
    public string keyC ;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(keyC))
        {
            Debug.Log("fait untruc");
        }
	}
}
