using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour {
    [SerializeField] private GameObject player;
    private PlayerHealth pHP;
    [SerializeField] private Text curHpTxt;
    [SerializeField] private Text MaxHpTxt;

    private GameObject pausePanel;
    private GameObject spellPanel;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pHP = player.GetComponent<PlayerHealth>();
        pausePanel = GameObject.Find("PauseMenuPanel");
        spellPanel = GameObject.Find("SpellPanel");
        pausePanel.SetActive(false);
        spellPanel.SetActive(false);
        //curHpTxt = GetComponent<Text>();
        //MaxHpTxt = GetComponent<Text>();

    }
    // Update is called once per frame
    void Update () {
        MaxHpTxt.text = pHP.getMaxVie().ToString();
        curHpTxt.text = pHP.getCurrentHealth().ToString();
        pauseMenu();
        spellList();
    }

    public void pauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(!pausePanel.activeSelf);
        }
    }
    
    public void spellList()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            spellPanel.SetActive(!spellPanel.activeSelf);
        }
    }
}
