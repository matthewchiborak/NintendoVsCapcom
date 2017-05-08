using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectControlScript : MonoBehaviour {

    public int stagePosition;
    public Image player1Cursor;
    public Image portrait;
    public Text portraitText;

    public AudioSource audioSource;
    public AudioClip moveSound;
    public AudioClip selectSound;
    public AudioClip errorSound;

    public Text loadingText;

    public Material[] portraits = new Material[9];
    public string[] names = new string[9];

    private bool buttonPressed = false;

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        loadingText.enabled = false;
        stagePosition = 0;
        portrait.material = portraits[stagePosition];
        portraitText.text = names[stagePosition];
    }

    void moveCursor()
    {

        float xpos = -440;
        float ypos = 105;

        if (stagePosition > 3)
        {
            ypos = -5;
        }

        xpos += (stagePosition % 4) * 110;

        player1Cursor.rectTransform.localPosition = new Vector3(xpos, ypos, 0);

        //Update the portraits
        portrait.material = portraits[stagePosition];
        portraitText.text = names[stagePosition];
    }

    // Update is called once per frame
    void Update ()
    {
        //Reset button presses
        if(Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Light1") == 0)
        {
            buttonPressed = false;
        }

        //Get user input
        //if (Input.GetKeyDown(KeyCode.W))
        if(Input.GetAxisRaw("Vertical") == 1 && !buttonPressed)
        {
            buttonPressed = true;

            if (stagePosition > 3)
            {
                stagePosition -= 4;
            }
            else
            {
                stagePosition += 4;
            }

            audioSource.clip = moveSound;
            audioSource.time = 0.9f;
            audioSource.Play();

            moveCursor();
        }
        //if (Input.GetKeyDown(KeyCode.A))
        if (Input.GetAxisRaw("Horizontal") == -1 && !buttonPressed)
        {
            buttonPressed = true;

            stagePosition--;

            if (stagePosition == 3)
            {
                stagePosition = 7;
            }
            if (stagePosition == -1)
            {
                stagePosition = 3;
            }

            audioSource.clip = moveSound;
            audioSource.time = 0.9f;
            audioSource.Play();

            moveCursor();
        }
        // if (Input.GetKeyDown(KeyCode.S))
        if (Input.GetAxisRaw("Vertical") == -1 && !buttonPressed)
        {
            buttonPressed = true;

            if (stagePosition > 3)
            {
                stagePosition -= 4;
            }
            else
            {
                stagePosition += 4;
            }

            audioSource.clip = moveSound;
            audioSource.time = 0.9f;
            audioSource.Play();

            moveCursor();
        }
        //if (Input.GetKeyDown(KeyCode.D))
        if (Input.GetAxisRaw("Horizontal") == 1 && !buttonPressed)
        {
            buttonPressed = true;

            stagePosition++;

            if (stagePosition == 4)
            {
                stagePosition = 0;
            }
            if (stagePosition == 8)
            {
                stagePosition = 4;
            }

            audioSource.clip = moveSound;
            audioSource.time = 0.9f;
            audioSource.Play();

            moveCursor();
        }
        //if (Input.GetKeyDown(KeyCode.J))
        if (Input.GetAxisRaw("Light1") == 1 && !buttonPressed)
        {
            buttonPressed = true;

            if (names[stagePosition] == "")
            {
                audioSource.clip = errorSound;
                audioSource.time = 0.9f;
                audioSource.Play();
                return;
            }

            loadingText.enabled = true;

            //Give the singleton the stage that's wanted
            StoredInfoScript.characterInfo.currentStage = stagePosition;

            audioSource.clip = selectSound;
            audioSource.time = 0.9f;
            audioSource.Play();

            //Load the fight scene
            SceneManager.LoadScene("fight", LoadSceneMode.Single);
        }
    }
}
