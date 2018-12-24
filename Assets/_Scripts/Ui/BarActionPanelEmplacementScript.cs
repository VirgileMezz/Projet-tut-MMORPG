using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarActionPanelEmplacementScript : MonoBehaviour {
    public string keyC ;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(keyC))
        {
            GetComponentInChildren<Button>().onClick.Invoke();
        }
	}
}
