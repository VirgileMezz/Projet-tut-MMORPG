using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Teleporteur : MonoBehaviour {

    [SerializeField] private Slider barreDeProgression;
    [SerializeField] private GameObject ecranDeChargement;

    
    public void LoadScene(int index)
    {
        StartCoroutine(Chargement(index));
    }

    IEnumerator Chargement(int index)
    {
        ecranDeChargement.SetActive(true);
        AsyncOperation ao = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        //ao.allowSceneActivation = false;
        //SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        while (ao.isDone == false)
        {
            barreDeProgression.value = ao.progress;
            if (ao.progress == 0.9f)
            {
                barreDeProgression.value = 1f;
                //ao.allowSceneActivation = true;
            }
            yield return null;

        }
    }
}
