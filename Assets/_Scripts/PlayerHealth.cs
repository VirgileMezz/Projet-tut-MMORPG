﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] int startingHealth = 100;
    [SerializeField] float timeSinceLastHit = 1f; // pour la régnération auto après avoir pris des degats.
    private Slider healthSlider;

    private float timer = 0f;
    private CharacterController characterController;
    private Animator anim;
    private int currentHealth,healthMax;
    private ParticleSystem blood;
    //private AudioSource audio;
    private GameObject resPanel;


    void Awake()
    {
        resPanel = GameObject.Find("DeathPanel");
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        Assert.IsNotNull(healthSlider);
        resPanel.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        currentHealth = startingHealth;
        healthMax = startingHealth;
        //audio = GetComponent<AudioSource>();
        blood = GetComponentInChildren<ParticleSystem>();
        healthSlider.value = currentHealth;

    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
	}

    private void OnTriggerEnter(Collider other)
    {
        if(timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if(other.tag == "Weapon")
            {
                Debug.Log("touche arme");

                timer = 0;
            }
        }
    }

    public void takeHit()
    {
        if(currentHealth > 0)
        {
            GameManager.instance.PlayerHit(healthMax);
            anim.Play("Hurt");
            currentHealth -= 10;
            healthSlider.value = currentHealth;
            //audio.PlayOneShot(audio.clip);
            blood.Play();
        }

        if(currentHealth <= 0)
        {
            killPlayer();

        }
    }

    void killPlayer()
    {
        GameManager.instance.PlayerHit(currentHealth);
        anim.SetTrigger("HeroDie");
        characterController.enabled = false;
        //audio.PlayOneShot(audio.clip);
        blood.Play();
        resPanel.SetActive(true);

    }
    public void resurectPlayer()
    {
        resPanel.SetActive(false);
        currentHealth = healthMax;
        healthSlider.value = currentHealth;
        characterController.enabled = true;

    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public int getMaxVie()
    {
        return healthMax;
    }

    public void setMaxVie(int maxVie)
    {
        this.healthMax= maxVie;
    }

    public void setCurrentHealth(int vie)
    {
        this.currentHealth = vie;
    }

    void OnLevelWasLoaded()
    {
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        Assert.IsNotNull(healthSlider);
        healthSlider.value = currentHealth;
    }
}
