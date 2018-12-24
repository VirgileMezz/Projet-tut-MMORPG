using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttaqueScript : MonoBehaviour {



    private PlayerController pc;
    void Start () {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }



    public void doubleAttaque()
    {
        pc.doubleAttaque();
    }

    public void spinAttaque()
    {
        pc.spinAttaque();
    }

}
