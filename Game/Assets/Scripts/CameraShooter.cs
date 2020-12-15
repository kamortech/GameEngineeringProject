using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShooter : MonoBehaviour
{
    public float range = 50.0f;
    public float dmg = 20.0f;
    public Camera playerCam;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;

            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);//here should be method which will be giving damage to object that player is shooting
            }
        }
    }
}
