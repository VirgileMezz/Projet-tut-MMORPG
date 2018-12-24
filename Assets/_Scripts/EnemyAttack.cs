using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    [SerializeField] private float range = 3f;
    [SerializeField] private float timeBetweenAttacks = 2f;
    private GameObject player; //[SerializeField] 

    // [SerializeField] Transform player;

    private PlayerHealth pHP;
    private Animator anim;
    //private GameObject player;
    private bool playerInRange;
    private BoxCollider[] weaponColliders; // tableau pour les ennemies qui ont plusieurs armes
    private EnemyHealth enemyHealth;
    private float tmpsAvtProchaineAtq;
    // Use this for initialization
    void Start () {
        enemyHealth = GetComponent<EnemyHealth>();
        weaponColliders = GetComponentsInChildren<BoxCollider>();
        player = GameManager.instance.Player;
        pHP = player.GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Vector3.Distance(transform.position, player.transform.position) < range && enemyHealth.IsAlive()) 
        {
            playerInRange = true;
        }else
        {
            playerInRange = false;
        }
        attack();
	}

    void attack()
    {
        if(playerInRange && !GameManager.instance.GameOver && tmpsAvtProchaineAtq <= Time.time)
        {
            tmpsAvtProchaineAtq = Time.time + timeBetweenAttacks;
            anim.Play("Attack");
            pHP.takeHit();
            Debug.Log("attaqueEnnemie");
        }
    }

    public void EnemyBeginAttack()
    {
        foreach(var weapon in weaponColliders)
        {
            weapon.enabled = true;
        }
    }

    public void EnemyEndAttack()
    {
        foreach (var weapon in weaponColliders)
        {
            weapon.enabled = false;
        }
    }
}
