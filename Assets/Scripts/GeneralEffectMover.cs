using UnityEngine;
using System.Collections;

public class GeneralEffectMover : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
    public float lifetime;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = -1 * transform.right * speed;

        Destroy(gameObject, lifetime);
    }
	
	
}
