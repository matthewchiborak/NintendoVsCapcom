using UnityEngine;
using System.Collections;

public class BlueFalconMover : MonoBehaviour
{

    public float speed;
    public Rigidbody rb;
    public float lifetime;

    public float damage;
    public int hitstun;

    // Use this for initialization
    void Start()
    {
        damage = 400;
        hitstun = 10000;
        lifetime = 10;
        speed = 100;

        rb = GetComponent<Rigidbody>();
       // rb.transform.localPosition = new Vector3(50, 0, 0);
        //rb.transform.localRotation = Quaternion.Euler(0, -90, 0);
        rb.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        //rb.velocity = -1 * transform.right * speed;
        rb.velocity = transform.forward * speed;

        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HitBox"))
        {
            other.gameObject.GetComponent<HitBoxCollisions>().myCharacter.projectileDamageToRecieve = damage;
            other.gameObject.GetComponent<HitBoxCollisions>().myCharacter.projectileHitStunToRecieve = hitstun;
            other.gameObject.GetComponent<HitBoxCollisions>().myCharacter.juggleSpeedToRecieve  = 360;
            other.gameObject.GetComponent<HitBoxCollisions>().myCharacter.hitByAttackBox = true;
            other.gameObject.GetComponent<HitBoxCollisions>().myCharacter.hitByProjectile = true;
           // Destroy(gameObject);
            //myCharacter.projectileDamageToRecieve = other.gameObject.GetComponent<HadoukenMover>().damage;
        }
    }
}
