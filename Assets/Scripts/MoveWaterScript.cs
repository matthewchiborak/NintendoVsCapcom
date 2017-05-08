using UnityEngine;
using System.Collections;

public class MoveWaterScript : MonoBehaviour {

    private Vector3 startPosition;
    private int currentCount;
    private int maxCount;

	// Use this for initialization
	void Start () {

        maxCount = 458;
        currentCount = 0;
        startPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(currentCount >= maxCount)
        {
            currentCount = 0;
            transform.localPosition = startPosition;
        }   
        else
        {
            currentCount++;
            transform.localPosition = new Vector3(transform.localPosition.x - 1, transform.localPosition.y, transform.localPosition.z);
        } 
	}
}
