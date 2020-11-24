using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleQuest1 : MonoBehaviour
{
    //must be automatic in future
    public GameObject player;

    float distX;
    float distY;
    float distZ;

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

                distX = Mathf.Abs(player.transform.position.x - questActivationObject.transform.position.x);
                distY = Mathf.Abs(player.transform.position.y - questActivationObject.transform.position.y);
                distZ = Mathf.Abs(player.transform.position.z - questActivationObject.transform.position.z);

                if ((!qp.isQuestActive) && (distX <= 1) && (distY <= 1) && (distZ <= 1)) ;
                {
                    qp.isQuestActive = true;

                    //Debug.Log("Quest Activated");
                    Debug.Log("player(x | y | z) == (" + distX + " | " + distY + " | " + distZ + ")");
                }
                if (qp.isQuestActive)
                {
                    //ready to do something with quest
                }
            }
        }
    }
}
