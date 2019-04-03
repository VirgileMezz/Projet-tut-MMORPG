using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour {

   // [SerializeField] Transform player;
    private Transform player;
    private NavMeshAgent nav;
    private Animator anim;
    private EnemyHealth enemyHealth;
    [SerializeField] private float detectionRange = 40f;
    void Awake()
    {
        //player = GameManager.instance.Player.transform;

        //Assert.IsNotNull(player);
    }

	// Use this for initialization
	void Start () {
        player = GameManager.instance.Player.transform;
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        if(nav == null)
        {
            nav = GetComponent<NavMeshAgent>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (nav.isOnNavMesh)
        {
            RaycastHit hit;
            //if (Physics.Raycast(transform.position, player.transform.position, detectionRange))
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
            for (int i = 0; i < hitColliders.Length; i++)
            {

                if (hitColliders[i].gameObject.tag == "Player")
                {
                    //Debug.Log(hit.collider.gameObject == player);
                    if (!GameManager.instance.GameOver && enemyHealth.IsAlive())
                    {
                        nav.enabled = true;
                        nav.SetDestination(player.position);
                        Debug.Log("on passe dans le set destination");
                    }

                    else if ((!GameManager.instance.GameOver || GameManager.instance.GameOver))
                    {
                        nav.enabled = false;
                        // anim.Play("Idle");
                    }
                    else
                    {
                        nav.enabled = false;
                        anim.Play("Idle");
                    }
                }
            }
        }
        
        

    }
    /*void OnLevelWasLoaded()
    {
        player = GameManager.instance.Player.transform;
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }*/
}
