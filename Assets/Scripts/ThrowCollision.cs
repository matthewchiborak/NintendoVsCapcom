using UnityEngine;
using System.Collections;

public class ThrowCollision : MonoBehaviour {

    public CharacterScript myCharacter;

    void Start()
    {
        myCharacter.hitByGrabBox = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GrabBox"))
        {
            //Check if blocking
            myCharacter.recovery = other.gameObject.GetComponentInParent<CharacterScript>().hitstunToPass;
            //myCharacter.currentHealth -= other.gameObject.GetComponentInParent<CharacterScript>().damageToPass;
           // if(myCharacter.currentHealth < 0)
            //{
             //   myCharacter.currentHealth = 0;
            //}

            if(myCharacter.isPlayer1)
            {
                StoredInfoScript.characterInfo.player1Health[0] -= (other.gameObject.GetComponentInParent<CharacterScript>().damageToPass / StoredInfoScript.characterInfo.maxHealths[StoredInfoScript.characterInfo.player1Characters[0]]);
                if(StoredInfoScript.characterInfo.player1Health[0] < 0)
                {
                    StoredInfoScript.characterInfo.player1Health[0] = 0;
                }
            }
            else
            {
                StoredInfoScript.characterInfo.player2Health[0] -= (other.gameObject.GetComponentInParent<CharacterScript>().damageToPass / StoredInfoScript.characterInfo.maxHealths[StoredInfoScript.characterInfo.player2Characters[0]]);
                if (StoredInfoScript.characterInfo.player2Health[0] < 0)
                {
                    StoredInfoScript.characterInfo.player2Health[0] = 0;
                }
            }

            myCharacter.v0 = myCharacter.transform.position;
            myCharacter.startingTime = Time.time;
            myCharacter.hitByGrabBox = true;
            //other.gameObject.GetComponent<CharacterScript>().throwSuccessful = true;
            other.gameObject.GetComponentInParent<CharacterScript>().throwSuccessful = true;
        }
    }
}
