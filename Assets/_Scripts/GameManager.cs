using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public static GameObject instancePlayer = null;


    [SerializeField] GameObject player;
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] List<GameObject> spawnPoint;

    [SerializeField] GameObject tanker;
    [SerializeField] GameObject ranger;
    [SerializeField] GameObject soldier;
    [SerializeField] Text levelText;


    private bool gameOver = false;
    private int currentLevel;
    private float generatedSpawnTime = 1;
    private float currentSpawnTime = 0;
    private GameObject newEnemy;

    private List<EnemyHealth> enemies = new List<EnemyHealth>();
    private List<EnemyHealth> killedEnemies = new List<EnemyHealth>();

    public void RegisterEnemy(EnemyHealth enemy)
    {
        enemies.Add(enemy);
    }

    public void KilledEnemy(EnemyHealth enemy)
    {
        killedEnemies.Add(enemy);
    }



    public bool GameOver
    {
        get { return gameOver; }
    }

    public GameObject Player
    {
        get { return player; }
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance !=this ) // pour éviter une création répété d'instance
        {
            Destroy(gameObject);
        }
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
     

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(spawn());
        currentLevel = 1;
        Debug.Log("game manager fonction start called");
	}
	
	// Update is called once per frame
	void Update () {
        currentSpawnTime += Time.deltaTime;
	}

    public void PlayerHit(int currentHP)
    {
        if (currentHP > 0)
        {
            gameOver = false;
        }else
        {
            gameOver = true;
        }
    }

    IEnumerator spawn()
    {
        if (spawnPoints != null && tanker != null && ranger != null && soldier != null && levelText != null)
        {

       
            if(currentSpawnTime > generatedSpawnTime)
            {
                currentSpawnTime = 0;

                if (enemies.Count < currentLevel)
                {
                    //int randomNumber = Random.Range(0, spawnPoints.Length - 1);
                    int randomNumber = Random.Range(0, spawnPoint.Count - 1);
                    //GameObject spawnLocation = spawnPoints[randomNumber];
                    GameObject spawnLocation = spawnPoint[randomNumber];
                    Debug.Log(spawnPoint[randomNumber]);
                    int randomEnemy = Random.Range(0, 3);
                
                    if(randomEnemy == 0)
                        newEnemy = Instantiate(soldier) as GameObject;
                    else if(randomEnemy == 1 )
                        newEnemy = Instantiate(tanker) as GameObject;
                    else if(randomEnemy == 2)
                        newEnemy = Instantiate(soldier) as GameObject; // mettre le ranger, quand on aura rendu le ranger opérationnel

                    newEnemy.transform.position = spawnLocation.transform.position;
                }
                if(killedEnemies.Count == currentLevel)
                {
                    enemies.Clear();
                    killedEnemies.Clear();
                    yield return new WaitForSeconds(3f); // on attend 3 sec a la fin de la wave
                    currentLevel++;
                    levelText.text = "Wave : " + currentLevel;

                }
            }
            yield return null;
            StartCoroutine(spawn());
        }
    }
    void OnLevelWasLoaded()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        levelText = GameObject.Find("WaveText").GetComponent<Text>();
        player.transform.position = new Vector3(60f, 0.1f, 50f);

        Transform spawnPrt = GameObject.Find("SpawnMobs").GetComponent<Transform>();
        foreach(Transform child in spawnPrt)
        {
            spawnPoint.Add(child.gameObject);
        }
        //spawnPoints = GameObject.Find("SpawnMobs").GetComponentsInChildren<Transform>();
        Debug.Log(spawnPoint.Count);
        //spawnPoints[1] = spawn.transform.GetChild(1).gameObject;

        // get gameobject de soldier truc et muche
        //tanker = GameObject.Find();
        StartCoroutine(spawn());
        Debug.Log("nouvelle scene");

    }
}
