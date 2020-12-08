using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleQuest1 : MonoBehaviour
{
    //must be automatic in future
    public GameObject player;
    public GameObject questFinishObject;
    public GameObject dialogueManager;

    float distX;
    float distY;
    float distZ;

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

        interactionDistance = 1.0f;
        goneAwayFromEmployer = false;
        wentToFinishingObject = false;

        if (qp.isQuestAvailable && qp.isQuestReadyToActive)
        {
            Debug.Log("Quest just ready to active");
        }

        //we will need to declare specific number of dialogue sequences during this quest
        dialogueSequences = new bool[10];
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
                            Debug.Log("I am very dissapointed with your attitude. I will not give You any job anymore.");//here instead of Debug.Log should be coorutine for handling response monologue of interlocutor(person, wchich we are just talkink to)
                            dialogueManager.GetComponent<DialogueManager>().deactivateDialogue();
                        }
                        else if (lastDecision == 0)
                        {
                            dialogueSequences[1] = true;
                            Debug.Log("OK, If You find a guy who ownes me some money, please tell him that Big Beard is sending regards.");//here instead of Debug.Log should be coorutine for handling response monologue of interlocutor(person, wchich we are just talkink to)
                        }

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
                    }


                    if ((distX_finisher < interactionDistance) && (distY_finisher < interactionDistance) && (distZ_finisher < interactionDistance))
                    {
                        wentToFinishingObject = true;

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
                            Debug.Log("OK, here You have his artefact. I don' need it anymore.");//here instead of Debug.Log should be coorutine for handling response monologue of interlocutor(person, wchich we are just talkink to)
                            dialogueManager.GetComponent<DialogueManager>().deactivateDialogue();
                        }
                        else if (lastDecision == 1)
                        {
                            dialogueSequences[3] = true;
                            Debug.Log("OK, here You have his artefact. I don' need it anymore.");//here instead of Debug.Log should be coorutine for handling response monologue of interlocutor(person, wchich we are just talkink to)
                            Debug.Log("But, you were very rude to me, so I will remember that.");//and there sould come some reputation mark--
                        }

                        //ending dialogue
                        dialogueManager.GetComponent<DialogueManager>().deactivateDialogue();

                        //clearing all dialogue options
                        dialogueManager.GetComponent<DialogueManager>().clearDialogueOptions();
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
                        Debug.Log("Good Job! Here's your reward.");
                        //here should come some income for propper quest ending
                    }

                }
            }
        }
    }
}
