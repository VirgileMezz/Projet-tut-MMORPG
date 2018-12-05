using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttaqueScript : MonoBehaviour {

    private bool canAttaque2 = false;
    private bool canAttaque1 = false;
    void Start () {

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void doubleAttaque()
    {
        canAttaque1 = true;
    }

    public void spinAttaque()
    {
        canAttaque2 = true;
    }

    public bool getCanAttaque2()
    {
        return canAttaque2;
    }
    public void setCanAttaque2(bool a)
    {
        this.canAttaque2 = a;
    }

    public bool getCanAttaque1()
    {
        return canAttaque1;
    }
    public void setCanAttaque1(bool a)
    {
        this.canAttaque1 = a;
    }

}
