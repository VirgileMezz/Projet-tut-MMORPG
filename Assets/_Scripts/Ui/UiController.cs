using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour {

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
}
