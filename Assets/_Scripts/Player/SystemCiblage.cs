﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemCiblage : MonoBehaviour {

    private GameObject cible;
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject effetCiblage;
    [Range(10f, 100f)] [SerializeField] private float range = 60f;
    private GameObject newEffect;
    private Vector3 transformEffetCiblage;
    private bool canInstatiateEffect = true;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        
        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray, out hit,range))
            {
                if (hit.collider.tag == "Enemy")
                {
                    Destroy(newEffect);
                    canInstatiateEffect = true;
                    Debug.Log("marche");
                    cible = hit.collider.gameObject;
                    Debug.Log(cible);
                    if (canInstatiateEffect) {
                        RaycastHit hit2;
                        if (Physics.Raycast(cible.transform.position, cible.transform.TransformDirection(-Vector3.up), out hit2)) {
                            transformEffetCiblage = new Vector3(cible.transform.position.x, (cible.transform.lossyScale.y - cible.transform.lossyScale.y)+0.4f, cible.transform.position.z);
                            //newEffect = Instantiate(effetCiblage ,cible.transform.position, Quaternion.Euler(90f, 0f, 0f)) as GameObject ;
                            newEffect = Instantiate(effetCiblage, transformEffetCiblage, Quaternion.Euler(90f, 0f, 0f)) as GameObject;
                            newEffect.transform.parent = cible.transform;
                            canInstatiateEffect = false;
                        }
                    }
                }
                else
                {
                    Destroy(newEffect);
                    canInstatiateEffect = true;
                    cible = null;

                }
            }
        }
    }
}
