using UnityEngine;
using System.Collections;

public class SpinTornado : MonoBehaviour {

    void Start()
    {
        Destroy(gameObject, 3);
    }

	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(0, 0, 0));
	}
}
