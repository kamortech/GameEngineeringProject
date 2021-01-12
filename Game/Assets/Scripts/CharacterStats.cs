using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int HP = 100;
    public float aimAccurency = 65.0f;
    private bool isAlive = true;

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0 && isAlive)
        {
            Debug.Log("Death of " + this.gameObject.name);
            isAlive = false;
        }
    }

    public void takeDamage(int dmg)
    {
        HP -= dmg;
    }
}
