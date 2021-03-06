﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleQuest1 : MonoBehaviour
{
    //must be automatic in future
    public GameObject player;
    public GameObject questFinishObject;
    public GameObject dialogueManager;

    float distX = 500.0f;
    float distY = 500.0f;
    float distZ = 500.0f;

    float distX_starter;
    float distY_starter;
    float distZ_starter;

    float distX_finisher;
    float distY_finisher;
    float distZ_finisher;

    //some variables for purposes of this excact task;
    bool goneAwayFromEmployer;
    bool wentToFinishingObject;

    float interactionDistance;

    //must consider to use connection from QuestManager's definition of this struct
    struct QuestParameters
    {
        public GameObject questActivationObject;

        public bool isQuestOptional;
        public bool isQuestAvailable;
        public bool isQuestReadyToActive;
        public bool isQuestActive;
        public bool isQuestSuccess;
        public bool isQuestFailure;
    }


    QuestParameters qp;
    public GameObject questActivationObject;

    //table for storing dialogue sequences, last is always 1, if there is a 0 somewere in the middle or beginning it means, that there was some negative attitude
    private bool[] dialogueSequences;

    //variable for storin last dialogue decision
    private int lastDecision;

    void Start()
    {
        //in future this could 'come' from QuestManager
        qp.questActivationObject = questActivationObject;

        qp.isQuestOptional = false;
        qp.isQuestAvailable = true;
        qp.isQuestReadyToActive = true;
        qp.isQuestActive = false;
        qp.isQuestSuccess = false;
        qp.isQuestFailure = false;

        interactionDistance = 2.0f;
        goneAwayFromEmployer = false;
        wentToFinishingObject = false;

        if (qp.isQuestAvailable && qp.isQuestReadyToActive)
        {
            Debug.Log("Quest just ready to active");
        }

        //we will need to declare specific number of dialogue sequences during this quest
        dialogueSequences = new bool[10];
    }

    IEnumerator waitCoroutine(int s)
    {
        yield return new WaitForSeconds(s);

        dialogueManager.GetComponent<DialogueManager>().deactivateSubtitles();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(qp.isQuestSuccess))
        {
            if (!(qp.isQuestFailure))
            {
                if (!(qp.isQuestActive))
                {
                    distX = Mathf.Abs(player.transform.position.x - questActivationObject.transform.position.x);
                    distY = Mathf.Abs(player.transform.position.y - questActivationObject.transform.position.y);
                    distZ = Mathf.Abs(player.transform.position.z - questActivationObject.transform.position.z);


                    if ((distX < interactionDistance) && (distY < interactionDistance) && (distZ < interactionDistance))
                    {
                        qp.isQuestActive = true;
                        Debug.Log("Quest Activated");

                        dialogueManager.GetComponent<DialogueManager>().activateSubtitles();
                        dialogueManager.GetComponent<DialogueManager>().sendSubtitle("Welcome, stranger. I think even such a noob like you can do something for me. Besides, what the hell are you doing at this land?");

                        //activating dialogue
                        dialogueManager.GetComponent<DialogueManager>().activateDialogue();

                        //clearing all dialogue options
                        dialogueManager.GetComponent<DialogueManager>().clearDialogueOptions();

                        //setting dialogue options
                        dialogueManager.GetComponent<DialogueManager>().setDialogueOption(0, "Nothing. I just got here.");
                        dialogueManager.GetComponent<DialogueManager>().setDialogueOption(1, "What's your problem?");

                        //starting first (0) dialogue sequence
                        dialogueSequences[0] = true;
                    }
                }

                if (qp.isQuestActive && dialogueSequences[0] && !dialogueSequences[1])
                {
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        lastDecision = dialogueManager.GetComponent<DialogueManager>().getDecision();

                        if (lastDecision == 1)
                        {
                            dialogueSequences[1] = false;
                            dialogueManager.GetComponent<DialogueManager>().sendSubtitle("I am very dissapointed with your attitude. I will not give You any job anymore.");//here instead of Debug.Log should be coorutine for handling response monologue of interlocutor(person, wchich we are just talkink to)
                        }
                        else if (lastDecision == 0)
                        {
                            dialogueSequences[1] = true;
                            dialogueManager.GetComponent<DialogueManager>().sendSubtitle("OK, If You find a guy who ownes me some money, please tell him that Big Beard is sending regards.");//here instead of Debug.Log should be coorutine for handling response monologue of interlocutor(person, wchich we are just talkink to)
                        }

                        StartCoroutine(waitCoroutine(5));
                    }
                }

                if (qp.isQuestActive && dialogueSequences[1] && !dialogueSequences[2])
                {
                    //ending dialogue
                    dialogueManager.GetComponent<DialogueManager>().deactivateDialogue();

                    //clearing all dialogue options
                    dialogueManager.GetComponent<DialogueManager>().clearDialogueOptions();

                    distX_finisher = Mathf.Abs(player.transform.position.x - questFinishObject.transform.position.x);
                    distY_finisher = Mathf.Abs(player.transform.position.y - questFinishObject.transform.position.y);
                    distZ_finisher = Mathf.Abs(player.transform.position.z - questFinishObject.transform.position.z);

                    distX_starter = Mathf.Abs(player.transform.position.x - questActivationObject.transform.position.x);
                    distY_starter = Mathf.Abs(player.transform.position.y - questActivationObject.transform.position.y);
                    distZ_starter = Mathf.Abs(player.transform.position.z - questActivationObject.transform.position.z);

                    if (!(goneAwayFromEmployer) && (distX_starter > interactionDistance + 5.0f) && (distY_starter > interactionDistance + 5.0f))
                    {
                        goneAwayFromEmployer = true;
                        Debug.Log("I went away from employer");

                        dialogueManager.GetComponent<DialogueManager>().deactivateSubtitles();//
                        dialogueManager.GetComponent<DialogueManager>().deactivateDialogue();//This all should be with coorutine while listening to MPC's monologue
                    }


                    if ((distX_finisher < interactionDistance) && (distY_finisher < interactionDistance) && (distZ_finisher < interactionDistance))
                    {
                        wentToFinishingObject = true;

                        dialogueManager.GetComponent<DialogueManager>().activateSubtitles();
                        dialogueManager.GetComponent<DialogueManager>().sendSubtitle("What do you want, stranger?");

                        //activating dialogue
                        dialogueManager.GetComponent<DialogueManager>().activateDialogue();

                        //clearing all dialogue options
                        dialogueManager.GetComponent<DialogueManager>().clearDialogueOptions();

                        //setting dialogue options
                        dialogueManager.GetComponent<DialogueManager>().setDialogueOption(0, "Big Beard is sending regards. Give me what you own to him.");
                        dialogueManager.GetComponent<DialogueManager>().setDialogueOption(1, "Hey thief, Big Beard send me to get what You've taken from him.");

                        dialogueSequences[2] = true;

                        Debug.Log("Quest Progress");
                    }
                }

                if (qp.isQuestActive && dialogueSequences[2] && !dialogueSequences[3])
                {
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        lastDecision = dialogueManager.GetComponent<DialogueManager>().getDecision();

                        if (lastDecision == 0)
                        {
                            dialogueSequences[3] = true;
                            dialogueManager.GetComponent<DialogueManager>().sendSubtitle("OK, here You have his artefact. I don' need it anymore.");//here instead of Debug.Log should be coorutine for handling response monologue of interlocutor(person, wchich we are just talkink to)

                            //ending dialogue
                            dialogueManager.GetComponent<DialogueManager>().deactivateDialogue();

                            //clearing all dialogue options
                            dialogueManager.GetComponent<DialogueManager>().clearDialogueOptions();

                            StartCoroutine(waitCoroutine(5));

                            //dialogueManager.GetComponent<DialogueManager>().deactivateDialogue();
                        }
                        else if (lastDecision == 1)
                        {
                            dialogueSequences[3] = true;
                            dialogueManager.GetComponent<DialogueManager>().sendSubtitle("OK, here You have his artefact. I don' need it anymore. But, you were very rude to me, so I will remember that.");//here instead of Debug.Log should be coorutine for handling response monologue of interlocutor(person, wchich we are just talkink to) //and there sould come some reputation mark--

                            //ending dialogue
                            dialogueManager.GetComponent<DialogueManager>().deactivateDialogue();

                            //clearing all dialogue options
                            dialogueManager.GetComponent<DialogueManager>().clearDialogueOptions();

                            StartCoroutine(waitCoroutine(8));
                        }

                        //ending dialogue
                        dialogueManager.GetComponent<DialogueManager>().deactivateDialogue();

                        //clearing all dialogue options
                        dialogueManager.GetComponent<DialogueManager>().clearDialogueOptions();

                       // dialogueManager.GetComponent<DialogueManager>().deactivateSubtitles();
                    }
                }

                if (qp.isQuestActive && dialogueSequences[3])
                {
                    distX_finisher = Mathf.Abs(player.transform.position.x - questFinishObject.transform.position.x);
                    distY_finisher = Mathf.Abs(player.transform.position.y - questFinishObject.transform.position.y);
                    distZ_finisher = Mathf.Abs(player.transform.position.z - questFinishObject.transform.position.z);

                    distX_starter = Mathf.Abs(player.transform.position.x - questActivationObject.transform.position.x);
                    distY_starter = Mathf.Abs(player.transform.position.y - questActivationObject.transform.position.y);
                    distZ_starter = Mathf.Abs(player.transform.position.z - questActivationObject.transform.position.z);

                    if ((distX_starter < interactionDistance) && (distY_starter < interactionDistance) && (distZ_starter < interactionDistance))
                    {
                        dialogueManager.GetComponent<DialogueManager>().activateSubtitles();

                        dialogueManager.GetComponent<DialogueManager>().sendSubtitle("Good Job! Here's your reward.");

                        Debug.Log("Good Job! Here's your reward.");

                        dialogueManager.GetComponent<DialogueManager>().deactivateDialogue();

                        StartCoroutine(waitCoroutine(5));

                        //here should come some income for propper quest ending
                    }

                }
            }
        }
    }
}
