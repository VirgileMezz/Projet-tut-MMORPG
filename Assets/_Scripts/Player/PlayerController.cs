using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour {
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject camera;
    [SerializeField] private PlayerHealth playerHealth;

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
    private float tmpsAvtAutoAtck;

    //private float expGagne = 0.40f;

    private int levelCharacter = 1;


    private bool canGainExp;
    private Slider expBar;

    private AttaqueScript[] aS;
    private GameObject barreAction;
    [SerializeField] private GameObject particleSpinAttaque;
    [SerializeField] Slider healthSlider;

    private float expAvantLvlUp;
    private float expReste;
    private float currentExp;
    private float puissanceAttaque = 10;
    private Text charaLvlText;

    private Camera camera1;
    public Interactable focus;
    
    private void Awake()
    {
      DontDestroyOnLoad(gameObject);
    }

    void Start() {

        sc = gameObject.GetComponent<SystemCiblage>();
        characterController = GetComponent<CharacterController>();
        playerHealth = gameObject.GetComponent<PlayerHealth>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        cam = camera.transform;
        anim = GetComponent<Animator>();
        swordColliders = GetComponentsInChildren<BoxCollider>();
        expBar = GameObject.Find("ExpBar").GetComponent<Slider>();
        barreAction = GameObject.Find("BarreAction");
        aS = barreAction.GetComponentsInChildren<AttaqueScript>();
        //aS1 = GameObject.Find("ActionButton1").GetComponent<AttaqueScript>();
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        charaLvlText = GameObject.Find("CharaLevelText").GetComponent<Text>();
        camera1 = camera.GetComponent<Camera>() ;

    }

    // Update is called once per frame
    void Update() {

        /*if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }*/
        
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



            if (Input.GetMouseButtonDown(0))
            {
            Ray ray = camera1.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    RemoveFocus();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = camera1.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        SetFocus(interactable);
                    }
                }
            }

            autoAttaque();

        }

        
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }
            focus = newFocus;
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        focus = null;
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
            Collider[] hitColliders = Physics.OverlapBox(transform.position , new Vector3(4f, 1f, 4f), Quaternion.identity);//- (Vector3.forward * 3)
            Debug.Log(transform.forward);

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

                        eHp.takeHit(1.5f);
                        augmentationExp(eHp);
                        anim.Play("DoubleAttack");
                        
                    }

                }

            }

        }
    }

     public void autoAttaque()
    {
        if (sc.getCible() != null)
        {
            //print("blabla1");
            RaycastHit hit;
            GameObject cible = sc.getCible();
            EnemyHealth eHp = cible.GetComponent<EnemyHealth>();
            float cooldown = 1.0f;
            if(Physics.Raycast(transform.position,(sc.getCible().transform.position-transform.position), out hit , 4f) && eHp.IsAlive())
            {
                //print("bla2");

                if (tmpsAvtAutoAtck<= Time.time)
                {
                    tmpsAvtAutoAtck = Time.time + cooldown;

                    eHp.takeHit(1f);
                    augmentationExp(eHp);
                    anim.Play("Hero_autoAttaque");

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
                    eHp.takeHit(1.25f);
                    if (!eHp.IsAlive())
                    {
                        augmentationExp(eHp);
                        Debug.Log("adversaire tué avec aoe");
                    }
                    //augmentationExp();
                    Debug.Log("touché");
                }
            }
            Instantiate(particleSpinAttaque, gameObject.transform);
            tmpsAvtProchaineAtq2 = Time.time + cooldown;
        }
    }

    public void augmentationExp(EnemyHealth eHp)
    {

        //if(sc.getCible() != null)
        //{

        //GameObject cible = sc.getCible();
        
        if (!eHp.IsAlive())
        {
            expAvantLvlUp = expBar.maxValue - expBar.value;
            expBar.value += eHp.getExpGagnable();

            Debug.Log("adversaire tué");

            // si j'arrive a l'xp max du slider 
            if (expBar.value == expBar.maxValue)
            {
                levelCharacter += 1;
                expBar.value = 0;
                expBar.maxValue = expBar.maxValue * 1.5f;
                puissanceAttaque = puissanceAttaque * 1.5f;
                playerHealth.setMaxVie(playerHealth.getMaxVie() + 20);  // j'up juste de 20 pv pour le moment
                playerHealth.setCurrentHealth(playerHealth.getMaxVie()); // je remet la vie actuel au max 


                healthSlider.maxValue = playerHealth.getMaxVie();
                healthSlider.value = healthSlider.maxValue;


                // on gagne un niveau alors on up les stats
                Debug.Log("exp avant lvl up "+expAvantLvlUp);
                Debug.Log("exp gagné "+eHp.getExpGagnable());
                Debug.Log(" valeur de la bar exp "+expBar.value);
                expReste = eHp.getExpGagnable();
                if (expAvantLvlUp < eHp.getExpGagnable())
                {
                    expReste -= expAvantLvlUp;
                    expBar.value += expReste;

                    Debug.Log("on rajoute l'exp restant");
                    //expBar.value += eHp.getExpGagnable() - expAvantLvlUp ;
                    /*while(expReste <= 0)
                    {
                        Debug.Log(" dans le while");
                        expAvantLvlUp = expBar.maxValue - expBar.value;
                        expBar.value += expReste;
                        //expReste -= expAvantLvlUp;
                        expReste -= 1;



                        Debug.Log(" exp reste dans le while "+expReste);
                        Debug.Log(" exp avant lvl up dans le while " + expAvantLvlUp);

                        //expReste = 0;

                    }*/

                }
                charaLvlText.text = levelCharacter.ToString();

            }
            currentExp = expBar.value;

            //}


        }
    }

    public void healthBonus()
    {
        playerHealth.setCurrentHealth(playerHealth.getCurrentHealth()+ (playerHealth.getMaxVie()/4));
        if(playerHealth.getCurrentHealth()> playerHealth.getMaxVie())
        {
            playerHealth.setCurrentHealth(playerHealth.getMaxVie());
        }
        healthSlider.value = playerHealth.getCurrentHealth();
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

    void OnLevelWasLoaded()
    {
        //if(camera == null)
        //{
        
        init();
        expBar.value = currentExp;
        healthSlider.maxValue = playerHealth.getMaxVie();
        healthSlider.value = healthSlider.maxValue;
        charaLvlText.text = levelCharacter.ToString();

        //}
    }

    private void init()
    {
        sc = gameObject.GetComponent<SystemCiblage>();
        characterController = GetComponent<CharacterController>();
        playerHealth = gameObject.GetComponent<PlayerHealth>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        cam = camera.transform;
        anim = GetComponent<Animator>();
        swordColliders = GetComponentsInChildren<BoxCollider>();
        expBar = GameObject.Find("ExpBar").GetComponent<Slider>();
        barreAction = GameObject.Find("BarreAction");
        aS = barreAction.GetComponentsInChildren<AttaqueScript>();
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        charaLvlText = GameObject.Find("CharaLevelText").GetComponent<Text>();
        camera1 = camera.GetComponent<Camera>();


    }

    public float getPuissanceAttaque()
    {
        return puissanceAttaque;
    }
}
