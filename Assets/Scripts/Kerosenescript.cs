using UnityEngine;
using System.Collections;

public class Kerosenescript : MonoBehaviour {

    public float lifetime;
    public GameObject fire;
    public GameObject fireHitBox;
    public GameObject liquid;

    private int count;

	// Use this for initialization
	void Start () {
        //Destroy(gameObject, lifetime);
        count = 0;
    }

    void Update()
    {
        if(count < 60)
        {
            count++;
        }
        else if(count == 60)
        {
            fire.SetActive(true);
            fireHitBox.SetActive(true);
            liquid.SetActive(false);
            Destroy(gameObject, lifetime);
            count++;
        }
    }

    //void OnTriggerEnter(Collider other)
    //{
        

    //    if (other.gameObject.CompareTag("LighterBox"))
    //    {
            

    //        other.gameObject.SetActive(false);           
    //        fire.SetActive(true);
    //        fireHitBox.SetActive(true);
    //        liquid.SetActive(false);
    //        Destroy(gameObject, lifetime);
    //        //myCharacter.projectileDamageToRecieve = other.gameObject.GetComponent<HadoukenMover>().damage;
    //    }
    //}


}
