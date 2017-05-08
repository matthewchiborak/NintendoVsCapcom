using UnityEngine;
using System.Collections;

public class ZombieScript : MonoBehaviour {

    public float speed;
    public Rigidbody rb;
    public float lifetime;

    public float damage;
    public int hitstun;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * speed;

        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HitBox"))
        {
            other.gameObject.GetComponent<HitBoxCollisions>().myCharacter.projectileDamageToRecieve = damage;
            other.gameObject.GetComponent<HitBoxCollisions>().myCharacter.projectileHitStunToRecieve = hitstun;
            other.gameObject.GetComponent<HitBoxCollisions>().myCharacter.hitByAttackBox = true;
            other.gameObject.GetComponent<HitBoxCollisions>().myCharacter.hitByProjectile = true;
            //Destroy(gameObject);
            //myCharacter.projectileDamageToRecieve = other.gameObject.GetComponent<HadoukenMover>().damage;
        }
    }
}
