using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundOption : MonoBehaviour {
    public AudioSource audio;
    public Slider slider;
	public void sliderValueChange()
    {
        audio.volume = slider.value;
    }
}
