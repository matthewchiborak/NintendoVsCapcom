using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DelayToEnable : MonoBehaviour {

    public int framesBeforeEnable;
    private int count;
    public Image imageToDisplay;

    //public GameObject explosion;

	// Use this for initialization
	void Start ()
    {
        count = 0;
        imageToDisplay.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(count == framesBeforeEnable)
        {
            imageToDisplay.enabled = true;
            //Instantiate(explosion, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            GetComponent<AudioSource>().time = 0.8f;
            GetComponent<AudioSource>().Play();
            count++;
        }
        else if(count < framesBeforeEnable)
        {
            count++;
        }
	}
}
