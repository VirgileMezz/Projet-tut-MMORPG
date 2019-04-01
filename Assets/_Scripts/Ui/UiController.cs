using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour {
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject Camera;
    [SerializeField] private GameObject GameController;
    [SerializeField] private GameObject GameManager;
    public void loadSceneOnClic(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }
    public void exitOnClic()
    {
        Application.Quit();
    }
    public void desactivateCanvasOnClic(GameObject o)
    {
        o.SetActive(false);
    }
    public void activateCanvasOnClic(GameObject o)
    {
        o.SetActive(true);
    }
    public void PlayerInstanciate()
    {
        if (HUD != null && player != null && Camera != null && GameController != null && GameManager != null)
        {
            Instantiate(HUD);
            Instantiate(player);
            Instantiate(Camera);
            Instantiate(GameController);
            Instantiate(GameManager);
        }

    }
}
