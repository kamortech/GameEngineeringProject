using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClose : MonoBehaviour
{
    public float minDistance = 1.5f;
    public float speed = 10.0f;
    public Animator animator;

    private GameObject window;
    private GameObject player;
    private bool isOpened = false;
    private bool rotateLeft = false;
    private bool rotateRight = false;


    // Start is called before the first frame update
    void Start()
    {
        window = this.gameObject.transform.GetChild(1).gameObject;
        player = GameObject.Find("FirstPerson-AIO");
    }

    // Update is called once per frame
    void Update()
    {
        if (countDistanceFromPlayer() <= minDistance)
        {
            //set interaction in UI as visiable

            if(Input.GetKeyDown(KeyCode.E))
            {
                if (isOpened == false)
                {
                    isOpened = true;
                    Debug.Log("Door Opened");
                    //window.transform.rotation = new Quaternion(window.transform.rotation.x, window.transform.rotation.y, window.transform.rotation.z + 1.0f, 1.0f);
                    StartCoroutine(Open());
                }
                else
                {
                    isOpened = false;
                    Debug.Log("Door Closed");
                    //window.transform.rotation = new Quaternion(window.transform.rotation.x, window.transform.rotation.y, window.transform.rotation.z - 1.0f, 1.0f);
                    StartCoroutine(Close());
                }

            }
        }


    }

    IEnumerator Open()
    {
        animator.SetBool("isOpening", true);

        yield return new WaitForSeconds(0.1f);

        animator.SetBool("isOpening", false);
    }

    IEnumerator Close()
    {
        animator.SetBool("isClosing", true);

        yield return new WaitForSeconds(0.1f);

        animator.SetBool("isClosing", false);
    }

    private float countDistanceFromPlayer()
    {
        return Vector3.Distance(window.transform.position, player.transform.position);
    }
}
