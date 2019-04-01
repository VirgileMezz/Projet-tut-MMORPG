using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestScript : MonoBehaviour {

    private bool validateQuest;
    private RaycastHit hit;
    public GameObject boss;

    // Use this for initialization
    void Start () {
        validateQuest = false;

	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void ActivateQuest()
    {
        validateQuest = true;
        Debug.Log("Quete Terminée");
        

    }

  
}
