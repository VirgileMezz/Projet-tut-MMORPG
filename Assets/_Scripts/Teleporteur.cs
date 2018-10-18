using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Teleporteur : MonoBehaviour {

    [SerializeField] private int sceneIndex;
	
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
}
