using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObjectScript : MonoBehaviour {

    private PlayerController pc;
	// Use this for initialization
	void Start () {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (gameObject.tag == "HealthBonus")
            {
                pc.healthBonus();
            }
            if(gameObject.tag == "SpeedBonus")
            {

            }
            Destroy(gameObject);
        }
        
    }
}
