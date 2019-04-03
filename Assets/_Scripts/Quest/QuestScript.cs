using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestScript : MonoBehaviour
{

    public bool validateQuest;
    public bool activateQuest;
    private RaycastHit hit;
    public GameObject boss;

    // Use this for initialization
    void Start()
    {
        validateQuest = false;
        activateQuest = false;

    }

    void Update()
    {

    }

    public QuestScript()
    {
        this.validateQuest = false;
        this.activateQuest = false;
    }

    public QuestScript(bool activQuest, bool validQuest)
    {
        this.validateQuest = validQuest;
        this.activateQuest = activQuest;
    }

    public bool getActiveQuest()
    {
        return activateQuest;
    }

    public bool getValidateQuest()
    {
        return validateQuest;
    }

    public void setActiveQuest(bool quest)
    {
        this.activateQuest = quest;
    }

    public void setValidateQuest(bool quest)
    {
        this.validateQuest = quest;
    }

    public int getMoneyFinQuete()
    {
        return 10000;
    }


}
