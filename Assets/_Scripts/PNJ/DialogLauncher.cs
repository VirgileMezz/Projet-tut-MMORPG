using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class DialogLauncher : MonoBehaviour
{
    public KeyCode keyDialog;
    public string dialogFilePath;

    public GameObject pressTalkPanel;
    public GameObject dialogPanel;
    public GameObject dialogText;
    public GameObject nameText;
    public GameObject continueText;

    public bool enableMultipleDialog;
    public int speedText;

    private Text text;
    private Dialog dialog;
    private bool isShowing;
    private bool isDisplay;

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        isShowing = false;
        isDisplay = false;
        pressTalkPanel.SetActive(false);
        dialogPanel.SetActive(false);
        text = dialogText.GetComponent<Text>();
        text.text = "";

        string[] t = File.ReadAllLines(dialogFilePath, Encoding.UTF8);
        dialog = new Dialog(t);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        	text.text = "";
            pressTalkPanel.SetActive(true);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(keyDialog) && other.CompareTag("Player"))
        {
            if (!isShowing)
            {
                isShowing = true;
                pressTalkPanel.SetActive(false);
                dialogPanel.SetActive(true);
                dialogText.SetActive(true);
                nameText.GetComponent<Text>().text = dialog.Name();
                coroutine = ShowText(dialog.Next());
                StartCoroutine(coroutine);
                return;
            }

            if (isShowing)
            {
                if (isDisplay)
                {
                    return;
                }

                if (dialog.HasNext())
                {
                    this.text.text = "";
                    continueText.GetComponent<Text>().text = "";
                    StartCoroutine(ShowText(dialog.Next()));
                }
                else
                {
                    dialogPanel.SetActive(false);
                    text.text = "";
                    dialog.Reset();
                    if (enableMultipleDialog)
                    {
                        pressTalkPanel.SetActive(true);
                        isShowing = false;
                    }
                }
                
                return;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
    	if(coroutine != null)
        {
        	StopCoroutine(coroutine);
        }

        if (other.CompareTag("Player"))
        {
            pressTalkPanel.SetActive(false);
            dialogPanel.SetActive(false);
            nameText.GetComponent<Text>().text = "";
            continueText.GetComponent<Text>().text = "";
            text.text = "";
            isShowing = false;
            dialog.Reset();
        }
    }

    private IEnumerator ShowText(string text)
    {
        isDisplay = true;
        yield return new WaitForSeconds(0.3f);

        char[] array = text.ToCharArray();
        for(int i = 0; i < text.Length; i++)
        {
            this.text.text = this.text.text + array[i];
            yield return new WaitForSeconds(speedText/1000f);
        }

        if (dialog.HasNext())
        {
            continueText.GetComponent<Text>().text = "Press E to continue...";
        }
        else
        {
            continueText.GetComponent<Text>().text = "Press E to leave";
        }

        isDisplay = false;
    }
}
