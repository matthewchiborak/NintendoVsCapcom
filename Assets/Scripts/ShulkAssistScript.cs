using UnityEngine;
using System.Collections;

public class ShulkAssistScript : AssistScript
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
        hitstun = 10000;
        lifetime = 1;
        speed = 40;
        rb = GetComponentInParent<Rigidbody>();

        entered = false;

        Instantiate(switchEffect, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));

        Destroy(gameObject, lifetime);
        anim.Play("Armature|Launcher", -1, 0f);
        GetComponentInParent<Transform>().localScale = new Vector3(10f, 10f, 10f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (entered)
        {
            return;
        }

        if (other.gameObject.CompareTag("HitBox") && ((other.gameObject.GetComponentInParent<CharacterScript>().isPlayer1 && !isPlayer1Assist) || (!(other.gameObject.GetComponentInParent<CharacterScript>().isPlayer1) && isPlayer1Assist)))
        {
           
            entered = true;
            other.gameObject.GetComponent<HitBoxCollisions>().myCharacter.projectileDamageToRecieve = damage;
            other.gameObject.GetComponent<HitBoxCollisions>().myCharacter.projectileHitStunToRecieve = hitstun;
            other.gameObject.GetComponent<HitBoxCollisions>().myCharacter.hitByAttackBox = true;
            other.gameObject.GetComponent<HitBoxCollisions>().myCharacter.hitByProjectile = true;
        }
    }
}
