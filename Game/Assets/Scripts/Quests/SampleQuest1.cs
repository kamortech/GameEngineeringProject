using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleQuest1 : MonoBehaviour
{
    //must be automatic in future
    public GameObject player;
    public GameObject questFinishObject;

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
                    }
                }

                if (qp.isQuestActive)
                {
                    distX_finisher = Mathf.Abs(player.transform.position.x - questFinishObject.transform.position.x);
                    distY_finisher = Mathf.Abs(player.transform.position.y - questFinishObject.transform.position.y);
                    distZ_finisher = Mathf.Abs(player.transform.position.z - questFinishObject.transform.position.z);

                    distX_starter = Mathf.Abs(player.transform.position.x - questActivationObject.transform.position.x);
                    distY_starter = Mathf.Abs(player.transform.position.y - questActivationObject.transform.position.y);
                    distZ_starter = Mathf.Abs(player.transform.position.z - questActivationObject.transform.position.z);

                    if (!(goneAwayFromEmployer) && (distX_starter > interactionDistance + 5.0f) && (distY_starter > interactionDistance + 5.0f) )
                    {
                        goneAwayFromEmployer = true;
                        Debug.Log("I went away from employer");
                    }


                    if ((distX_finisher < interactionDistance) && (distY_finisher < interactionDistance) && (distZ_finisher < interactionDistance))
                    {
                        wentToFinishingObject = true;

                        Debug.Log("Quest Progress");
                    }

                    if((!wentToFinishingObject) && (goneAwayFromEmployer) && (distX_starter < interactionDistance) && (distY_starter < interactionDistance) && (distZ_starter < interactionDistance))
                    {
                        Debug.Log("You did not come to the place I've send You to. Go away, and do not come to me again. I will not give You any job in the future!");

                        qp.isQuestFailure = true;
                    }
                    else if((wentToFinishingObject) && (distX_starter < interactionDistance) && (distY_starter < interactionDistance) && (distZ_starter < interactionDistance))
                    {
                        Debug.Log("Good Job! Here's your reward.");
                    }

                }
            }
        }
    }
}
