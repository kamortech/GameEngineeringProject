using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    struct QuestParameters
    {
        GameObject questActivationObject;

        bool isQuestOptional;
        bool isQuestAvailable;
        bool isQuestReadyToActive;
        bool isQuestActive;
        bool isQuestSuccess;
        bool isQuestFailure;
    }

    public GameObject[] quests;

    // Start is called before the first frame update
    void Start()
    {
        //quests = new GameObject[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
