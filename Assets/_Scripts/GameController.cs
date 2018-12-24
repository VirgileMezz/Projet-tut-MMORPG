using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour {
    [SerializeField] private GameObject player;
    private PlayerHealth pHP;
    [SerializeField] private Text curHpTxt;
    [SerializeField] private Text MaxHpTxt;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pHP = player.GetComponent<PlayerHealth>();
        //curHpTxt = GetComponent<Text>();
        //MaxHpTxt = GetComponent<Text>();

    }
    // Update is called once per frame
    void Update () {
        MaxHpTxt.text = pHP.getMaxVie().ToString();
        curHpTxt.text = pHP.getCurrentHealth().ToString();

    }
}
