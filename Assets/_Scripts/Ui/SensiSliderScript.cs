using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SensiSliderScript : MonoBehaviour {
    private CameraController cam;
    private Slider slider;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        slider = gameObject.GetComponent<Slider>();
        slider.maxValue = 10f;
        slider.minValue = 0.1f;
        slider.value = cam.getVitesse();
        
        cam.setVitesse(slider.value);  
        
    }
   
    public void sliderValueSensi()
    {
        cam.setVitesse(slider.value);
    }

    public void OnLevelWasLoaded()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        slider = gameObject.GetComponent<Slider>();
        slider.maxValue = 10f;
        slider.minValue = 0.1f;
        slider.value = cam.getVitesse();

        cam.setVitesse(slider.value);


    }
}
