using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject camera;

    private CharacterController characterController;
    private Vector3 currentLookTarget = Vector3.zero;
    private Transform cam;                 
    private Vector3 camForward;             
    private Vector3 move;

    private Vector3 playerFwd;

    private Animator anim;
    private BoxCollider[] swordColliders;

    private SystemCiblage sc;

    private float tmpsAvtProchaineAtq;
    
    //private void Awake()
    //{
      //  DontDestroyOnLoad(gameObject);
    //}
    
    void Start () {

        sc = gameObject.GetComponent<SystemCiblage>();
        characterController = GetComponent<CharacterController>();
        cam = camera.transform;
        anim = GetComponent<Animator>();
        swordColliders = GetComponentsInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update () {
        camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        playerFwd = Vector3.Scale(transform.forward, new Vector3(1, 0, 1)).normalized;

        sc.getCible();


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //test
        if (!GameManager.instance.GameOver)
        {
            //fin test
            //transform.rotation = Quaternion.LookRotation(moveDirection);
            move = vertical * playerFwd + horizontal * cam.right;
            //move = vertical * camForward + horizontal * cam.right;
            characterController.SimpleMove(move * moveSpeed);

            // Pour l'anim
            if (move == Vector3.zero)
            {
                anim.SetBool("IsWalking", false);
            }
            else
            {
                anim.SetBool("IsWalking", true);
            }

            // pour l'anim des attacks

            if (Input.GetKeyDown(KeyCode.Alpha1)) // Je met les boutons de la souris pour le moment
            {
                if (sc.getCible() != null)
                {
                    RaycastHit hit;
                    print(sc.getCible());
                    GameObject cible = sc.getCible();
                    EnemyHealth eHp = cible.GetComponent<EnemyHealth>();
                    float cooldown = 2.0f;

                    //characterController.height / 2
                    if (Physics.SphereCast(transform.position, 10f , transform.forward, out hit, 5))
                    {
                        if(cible = hit.collider.gameObject)
                        {
                            if (tmpsAvtProchaineAtq <= Time.time)
                            {
                                tmpsAvtProchaineAtq = Time.time + cooldown;

                                eHp.takeHit();
                                anim.Play("DoubleAttack");
                            }
                                                      
                        }
                   
                    }   

                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f);
                float cooldown = 5.0f;
                if(tmpsAvtProchaineAtq <= Time.time) {
                    anim.Play("SpinAttack");

                    for (int i = 0; i < hitColliders.Length; i++)
                    {
                        if ( hitColliders[i].gameObject.tag =="Enemy")
                        {

                            EnemyHealth eHp = hitColliders[i].gameObject.GetComponent<EnemyHealth>();
                            eHp.takeHit();
                            Debug.Log("touché");
                        }
                    }
                    tmpsAvtProchaineAtq = Time.time + cooldown;
                }
            }

        }
    }

    public void BeginAttack()
    {
        foreach( var weapon in swordColliders)
        {
            weapon.enabled = true;

        }
    }

    public void EndAttack()
    {
        foreach (var weapon in swordColliders)
        {
            weapon.enabled = false;

        }
    }

    

}
