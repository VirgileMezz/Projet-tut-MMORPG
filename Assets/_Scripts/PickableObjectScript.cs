using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObjectScript : MonoBehaviour {

    private PlayerController pc;
    private Vector3 transformSave;
	// Use this for initialization
	void Start () {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        transformSave = new Vector3(transform.position.x, transform.position.y, transform.position.z);
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

    private void Update()
    {
        transform.position = new Vector3(transform.position.x,Mathf.PingPong(Time.time ,0.5f)+transformSave.y, transform.position.z);
    }
}
