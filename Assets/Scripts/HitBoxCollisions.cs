using UnityEngine;
using System.Collections;

public class HitBoxCollisions : MonoBehaviour
{
    public CharacterScript myCharacter;

    void Start()
    {
        myCharacter.hitByBlockBox = false;
        myCharacter.hitByAttackBox = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AttackBox"))
        {
            //Check if blocking
            myCharacter.hitByAttackBox = true;
            myCharacter.juggleHitHandled = false;

            //Take damage

            //Apply stun

        }

        if (other.gameObject.CompareTag("BlockBox"))
        {
            //Check if holding block set if blocking
            myCharacter.hitByBlockBox = true;
        }

        //if(other.gameObject.CompareTag("ProjectileBox"))
        //{
        //    myCharacter.projectileDamageToRecieve = other.gameObject.GetComponent<HadoukenMover>().damage;
        //}
    }


   /* void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BlockBox"))
        {
            //Check if holding block set if blocking
            myCharacter.hitByBlockBox = false;
            //print("Super Test");
        }
    }*/

}
