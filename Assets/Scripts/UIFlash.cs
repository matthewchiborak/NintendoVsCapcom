using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIFlash : MonoBehaviour {

    private int count;
    public int flashRate;
    public Text myText;

	// Use this for initialization
	void Start () {
        count = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        count++;

        if((count) == flashRate)
        {
            myText.enabled = false;
        }

        if ((count) == 2 * flashRate)
        {
            myText.enabled = true;
            count = 0;
        }
    }
}
