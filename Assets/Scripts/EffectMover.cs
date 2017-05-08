using UnityEngine;
using System.Collections;

public class EffectMover : MonoBehaviour {

    public float speed;
    public Rigidbody rb;
    public float lifetime;

    // Use this for initialization
    void Start () {
        lifetime = 0.58f;
        speed = 25;

        rb = GetComponent<Rigidbody>();
        rb.velocity = -1 * transform.right * speed;

        Destroy(gameObject, lifetime);
    }
	
	
}
