using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour {

    [SerializeField] private GameObject canvasBouton;
    private void OnTriggerEnter(Collider other)
    {
        canvasBouton.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        canvasBouton.SetActive(false);
    }
}
