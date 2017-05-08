using UnityEngine;
using System.Collections;

public class GrenadeMover : MonoBehaviour {

    public float speed;
    public Rigidbody rb;
    public float lifetime;

    public GameObject explosion;

    public float damage;
    public int hitstun;

    // Use this for initialization
    void Start()
    {
        //damage = 50;
        hitstun = 50;
        lifetime = 10;
        //speed = 60;

        rb = GetComponent<Rigidbody>();
        rb.velocity = -1 * transform.right * speed;

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

            //Create an explosion
            Instantiate(explosion, transform.position, transform.rotation);

            Destroy(gameObject);
            //myCharacter.projectileDamageToRecieve = other.gameObject.GetComponent<HadoukenMover>().damage;
        }
    }
}
