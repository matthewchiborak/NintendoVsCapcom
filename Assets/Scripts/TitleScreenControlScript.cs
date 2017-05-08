using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenControlScript : MonoBehaviour {

    public AudioSource audioSource;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if ( Input.GetKeyDown(KeyCode.Q))
        if (Input.GetAxis("Start1") > 0)
        {
            
            audioSource.Play();
            
            //Switch scenes
            SceneManager.LoadScene("characterSelect", LoadSceneMode.Single);
        }

        if (Input.GetAxisRaw("Medium1") == 1)
        {
            Application.Quit();
        }
    }
}
