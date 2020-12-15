using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;
    private bool dialogueActive;
    private int currentOption;
    private int maxOption;
    private Transform pointer;
    private GameObject subtitlesPanel;
    private GameObject subtitles;

    // Start is called before the first frame update
    void Start()
    {
        dialogueActive = false;
        currentOption = 0;
        maxOption = 0;

        pointer = canvas.transform.GetChild(0).gameObject.transform.GetChild(8);
        subtitlesPanel = canvas.transform.GetChild(1).gameObject;
        subtitles = subtitlesPanel.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueActive)
        {
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && (currentOption > 0))
            {
                currentOption--;
                Debug.Log(currentOption);
                movePointerUp();
            }

            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && (currentOption < maxOption))
            {
                currentOption++;
                Debug.Log(currentOption);
                movePointerDown();
            }
        }
    }

    private void movePointerUp()
    {
        pointer.position += new Vector3(0, 40.0f, 0);
    }

    private void movePointerDown()
    {
        pointer.position += new Vector3(0, -40.0f, 0);
    }

    public void activateSubtitles()
    {
        subtitlesPanel.active = true;
    }

    public void deactivateSubtitles()
    {
        subtitlesPanel.active = false;
    }

    public void sendSubtitle(string txt)
    {
        subtitles.GetComponent<Text>().text = txt;
    }

    public int getDecision()
    {
        return currentOption;
    }

    public void setNumberOfOptions(int num)
    {
        maxOption = num;
    }

    public void activateDialogue()
    {
        dialogueActive = true;
        canvas.transform.GetChild(0).gameObject.active = true;
        player.GetComponent<FirstPersonAIO>().enableCameraMovement = false;
        player.GetComponent<FirstPersonAIO>().playerCanMove = false;
    }

    public void deactivateDialogue()
    {
        dialogueActive = false;
        canvas.transform.GetChild(0).gameObject.active = false;
        player.GetComponent<FirstPersonAIO>().enableCameraMovement = true;
        player.GetComponent<FirstPersonAIO>().playerCanMove = true;

        currentOption = 0;
        maxOption = 0;
    }

    public void setDialogueOption(int x, string txt)
    {
        if (maxOption + 1 == x)
        {
            maxOption++;
            canvas.transform.GetChild(0).gameObject.transform.GetChild(x).GetComponent<Text>().text = txt;
        }
        else if (maxOption + 1 < x)
        {
            maxOption++;
            canvas.transform.GetChild(0).gameObject.transform.GetChild(maxOption).GetComponent<Text>().text = txt;
            Debug.Log("It's not allowed do declare number of dialogue option bigger than max option value plus one. It was declared as maxOption + 1 instead.");
        }
        else if (x < 0)
        {
            canvas.transform.GetChild(0).gameObject.transform.GetChild(x).GetComponent<Text>().text = txt;
            Debug.Log("It's not allowed do declare number of dialogue option smaller than 0. It was declared as 0.");
        }
        else
            canvas.transform.GetChild(0).gameObject.transform.GetChild(x).GetComponent<Text>().text = txt;
    }

    public void clearDialogueOption(int x)
    {
        canvas.transform.GetChild(0).gameObject.transform.GetChild(x).GetComponent<Text>().text = "";
        //we will need to assign all options from the beginning, to prevent some option missing in beetween some others
    }

    public void clearDialogueOptions()
    {
        for (int i = 0; i < 8; i++)
            canvas.transform.GetChild(0).gameObject.transform.GetChild(i).GetComponent<Text>().text = "";

        currentOption = 0;
        maxOption = 0;
    }
}
