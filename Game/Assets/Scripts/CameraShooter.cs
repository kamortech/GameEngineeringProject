using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShooter : MonoBehaviour
{
    public float range = 50.0f;
    public int dmg = 20;
    public Camera playerCam;
    public float accurecy = 90.0f;
    public GameObject bulletHoleNonOrganic;
    public GameObject bulletHoleOrganic;
    public GameObject fireShoot;
    private float offset;
    float rX;
    float rY;

    // Start is called before the first frame update
    void Start()
    {
        offset = (100.0f - accurecy) * 0.001f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Shoot());


            RaycastHit hit;
            rX = Random.Range(-offset, offset);
            rY = Random.Range(-offset, offset);

            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward + new Vector3(rX, rY, 0.0f), out hit, range))
            {
                Debug.Log(hit.transform.name);//here should be method which will be giving damage to object that player is shooting

                if (hit.transform.CompareTag("NPC_indie") || hit.transform.CompareTag("NPC_nomad") || hit.transform.CompareTag("NPC_panther") || hit.transform.CompareTag("NPC_townie"))
                {
                    Debug.Log("Thats's NPC!!!!");
                    hit.transform.gameObject.GetComponent<CharacterStats>().takeDamage(dmg);
                    Instantiate(bulletHoleOrganic, hit.point, Quaternion.FromToRotation(transform.up, hit.normal));

                }
                else if (hit.transform.CompareTag("Destroyable"))
                {
                    Destroy(hit.transform.gameObject);
                }
                else
                {
                    Instantiate(bulletHoleNonOrganic, hit.point, Quaternion.FromToRotation(transform.up, hit.normal));

                }
            }
        }

    }

    IEnumerator Shoot()
    {
        fireShoot.transform.Rotate(fireShoot.transform.rotation.x, 30.0f, fireShoot.transform.rotation.z);
        fireShoot.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        fireShoot.SetActive(false);
    }
}