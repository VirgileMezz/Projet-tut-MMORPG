using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    private float tmpsAvtProchaineAtq1;
    private float tmpsAvtProchaineAtq2;
    private float expGagne = 0.2f;


    private bool canGainExp;
    private Slider expBar; 

    private AttaqueScript aS;
    //private void Awake()
    //{
    //  DontDestroyOnLoad(gameObject);
    //}

    void Start() {

        sc = gameObject.GetComponent<SystemCiblage>();
        characterController = GetComponent<CharacterController>();
        cam = camera.transform;
        anim = GetComponent<Animator>();
        swordColliders = GetComponentsInChildren<BoxCollider>();
        expBar = GameObject.Find("ExpBar").GetComponent<Slider>();
        aS = GameObject.Find("ActionButton2").GetComponent<AttaqueScript>();
    }

    // Update is called once per frame
    void Update() {
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
                doubleAttaque();
            }
            if (aS.getCanAttaque2())//Input.GetKeyDown(KeyCode.Alpha2)
            {
                spinAttaque();
                aS.setCanAttaque2(false);
            }

        }
    }



    public void doubleAttaque()
    {
        if (sc.getCible() != null)
        {
            RaycastHit hit;
            print(sc.getCible());
            GameObject cible = sc.getCible();
            EnemyHealth eHp = cible.GetComponent<EnemyHealth>();
            float cooldown = 2.0f;
            Collider[] hitColliders = Physics.OverlapBox(transform.position - (Vector3.forward * 3), new Vector3(2f, 1f, 2f), Quaternion.identity);


            //Vector3 point1 = transform.position + (-3*Vector3.right);
            //Vector3 point2 = transform.position + (3 * Vector3.right);
            //characterController.height / 2
            //if (Physics.SphereCast(transform.position, 5f , transform.forward, out hit, 5))
            // if (Physics.CapsuleCast(point1, point2, 0.5f , transform.forward, out hit, 5))
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].gameObject == cible)
                {
                    if (tmpsAvtProchaineAtq1 <= Time.time)
                    {
                        tmpsAvtProchaineAtq1 = Time.time + cooldown;

                        eHp.takeHit();
                        augmentationExp();
                        anim.Play("DoubleAttack");
                    }

                }

            }

        }
    }

    public void spinAttaque()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f);
        float cooldown = 5.0f;
        if (tmpsAvtProchaineAtq2 <= Time.time)
        {
            anim.Play("SpinAttack");

            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].gameObject.tag == "Enemy")
                {

                    EnemyHealth eHp = hitColliders[i].gameObject.GetComponent<EnemyHealth>();
                    eHp.takeHit();
                    if (!eHp.IsAlive())
                    {
                        expBar.value += 0.2f;
                        Debug.Log("adversaire tué avec aoe");
                    }
                    //augmentationExp();
                    Debug.Log("touché");
                }
            }
            tmpsAvtProchaineAtq2 = Time.time + cooldown;
        }
    }

    public void augmentationExp()
    {

        if(sc.getCible() != null)
        {
            //GameObject cible = sc.getCible();
            EnemyHealth eHp = sc.getCible().GetComponent<EnemyHealth>();
            if (!eHp.IsAlive())
            {
                expBar.value += expGagne;
                Debug.Log("adversaire tué");

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
