using UnityEngine;
using System.Collections;

public class CFAssistScript : AssistScript
{
    public GameObject switchEffect;
    public float speed;
    public Rigidbody rb;
    public float lifetime;

    public Animator anim;

    public bool entered;

    public float damage;
    public int hitstun;

    // Use this for initialization
    void Start()
    {
        damage = 5;
        hitstun = 50;
        lifetime = 1;
        speed = 40;
        rb = GetComponentInParent<Rigidbody>();
        //rb = GetComponent<Rigidbody>();
        rb.velocity = -1 * transform.right * speed;

        entered = false;

        Destroy(gameObject, lifetime);
        anim.Play("Armature|FalconKick", -1, 0f);
        GetComponentInParent<Transform>().localScale = new Vector3(1200f, 1200f, 1200f);
        Instantiate(switchEffect, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
    }

    void OnTriggerEnter(Collider other)
    {
        if (entered)
        {
            return;
        }

        if (other.gameObject.CompareTag("HitBox") && ((other.gameObject.GetComponentInParent<CharacterScript>().isPlayer1 && !isPlayer1Assist) || (!(other.gameObject.GetComponentInParent<CharacterScript>().isPlayer1) && isPlayer1Assist)))
        //if (other.gameObject.CompareTag("HitBox") )
        {
            
            entered = true;
            other.gameObject.GetComponent<HitBoxCollisions>().myCharacter.projectileDamageToRecieve = damage;
            other.gameObject.GetComponent<HitBoxCollisions>().myCharacter.projectileHitStunToRecieve = hitstun;
            other.gameObject.GetComponent<HitBoxCollisions>().myCharacter.hitByAttackBox = true;
            other.gameObject.GetComponent<HitBoxCollisions>().myCharacter.hitByProjectile = true;
            //Destroy(gameObject);
            //myCharacter.projectileDamageToRecieve = other.gameObject.GetComponent<HadoukenMover>().damage;
        }
    }
}
