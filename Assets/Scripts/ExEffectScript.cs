using UnityEngine;
using System.Collections;

public class ExEffectScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, 0.75f);
	}
	
	// Update is called once per frame
	void Update()
    {
        transform.Rotate(new Vector3(0, 10, 0));
    }
}
