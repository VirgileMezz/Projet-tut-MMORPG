using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] int startingHealth = 100;
    [SerializeField] float timeSinceLastHit = 2f; // pour la régnération auto après avoir pris des degats.

    private float timer = 0f;
    private CharacterController characterController;
    private Animator anim;
    private int currentHealth;
    private AudioSource audio;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        currentHealth = startingHealth;
        audio = GetComponent<AudioSource>();
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
                takeHit();
                timer = 0;
            }
        }
    }

    void takeHit()
    {
        if(currentHealth> 0)
        {
            GameManager.instance.PlayerHit(currentHealth);
            anim.Play("Hurt");
            currentHealth -= 10;
            audio.PlayOneShot(audio.clip);
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
        audio.PlayOneShot(audio.clip);
    }
}
