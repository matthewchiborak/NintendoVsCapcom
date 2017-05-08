using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectController : MonoBehaviour {

    public int player1Position;
    public int player2Position;

    public int p1NoCharSelected;
    public int p2NoCharSelected;

    public int[] p1Characters = new int[3];
    public int[] p2Characters = new int[3];

    public Image player1Cursor;
    public Image player2Cursor;
    public Text p1CursorText;
    public Text p2CursorText;

    public Material[] portraits = new Material[29];

    public Image player1Char1;
    public Image player1Char2;
    public Image player1Char3;
    public Image player2Char1;
    public Image player2Char2;
    public Image player2Char3;

    public string[] names = new string[29];

    public Text player1Char1Text;
    public Text player1Char2Text;
    public Text player1Char3Text;
    public Text player2Char1Text;
    public Text player2Char2Text;
    public Text player2Char3Text;

    public Text startPrompt;

    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioClip moveSound;
    public AudioClip selectSound;
    public AudioClip cancelSound;
    public AudioClip errorSound;
    public AudioClip acceptSound;

    //Bools to ensure single button presses
    private bool p1Pressed = false;
    private bool p2Pressed = false;

    // Use this for initialization
    void Start ()
    {
        //Starting positions of the 2 cursors
        player1Position = 6;
        player2Position = 7;
        p1NoCharSelected = 0;
        p2NoCharSelected = 0;

        player1Char1.material = portraits[player1Position];
        player1Char1Text.text = names[player1Position];
        player1Char2.material = portraits[28];
        player1Char2Text.text = names[28];
        player1Char3.material = portraits[28];
        player1Char3Text.text = names[28];

        player2Char1.material = portraits[player2Position];
        player2Char1Text.text = names[player2Position];
        player2Char2.material = portraits[28];
        player2Char2Text.text = names[28];
        player2Char3.material = portraits[28];
        player2Char3Text.text = names[28];
        
    }

    void moveCursor1()
    {

        float xpos = -560;
        float ypos = 160;

        if(player1Position > 13)
        {
            ypos = 80;
        }

        xpos += (player1Position % 14) * 80;

        if (player1Position % 14 > 6)
        {
            xpos += 10;
        }

        player1Cursor.rectTransform.localPosition = new Vector3(xpos, ypos, 0);

        //Update the portraits
        if(p1NoCharSelected == 0)
        {
            player1Char1.material = portraits[player1Position];
            player1Char1Text.text = names[player1Position];
        }
        else if(p1NoCharSelected == 1)
        {
            player1Char2.material = portraits[player1Position];
            player1Char2Text.text = names[player1Position];
        }
        else
        {
            player1Char3.material = portraits[player1Position];
            player1Char3Text.text = names[player1Position];
        }
    }

    void moveCursor2()
    {

        float xpos = -560;
        float ypos = 160;

        if (player2Position > 13)
        {
            ypos = 80;
        }

        xpos += (player2Position % 14) * 80;

        if (player2Position % 14 > 6)
        {
            xpos += 10;
        }

        player2Cursor.rectTransform.localPosition = new Vector3(xpos, ypos, 0);

        //Update the portraits
        if (p2NoCharSelected == 0)
        {
            player2Char1.material = portraits[player2Position];
            player2Char1Text.text = names[player2Position];
        }
        else if (p2NoCharSelected == 1)
        {
            player2Char2.material = portraits[player2Position];
            player2Char2Text.text = names[player2Position];
        }
        else
        {
            player2Char3.material = portraits[player2Position];
            player2Char3Text.text = names[player2Position];
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Check if buttons are no longer being pressed
        if(Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Light1") == 0 && Input.GetAxisRaw("Medium1") == 0)
        {
            p1Pressed = false;
        }
        if (Input.GetAxisRaw("Vertical2") == 0 && Input.GetAxisRaw("Horizontal2") == 0 && Input.GetAxisRaw("Light2") == 0 && Input.GetAxisRaw("Medium2") == 0)
        {
            p2Pressed = false;
        }

        //If all characters selected prompt players to start
        if (p1NoCharSelected == 3 && p2NoCharSelected == 3)
        {
            startPrompt.enabled = true;
        }
        else
        {
            startPrompt.enabled = false;
        }

        //If all characters selected, enable functionallity to start the game
        //if (p1NoCharSelected == 3 && p2NoCharSelected == 3 && Input.GetKeyDown(KeyCode.Q))
        if (p1NoCharSelected == 3 && p2NoCharSelected == 3 && Input.GetAxisRaw("Start1") == 1)
        {
            audioSource1.clip = acceptSound;
            audioSource1.Play();

            startPrompt.text = "Loading...";

            //Store the selected characters
            StoredInfoScript.characterInfo.storeCharactersSelected(p1Characters[0], p1Characters[1], p1Characters[2], p2Characters[0], p2Characters[1], p2Characters[2]);

            //Switch scenes
            //SceneManager.LoadScene("fight", LoadSceneMode.Single);
            SceneManager.LoadScene("stageSelect", LoadSceneMode.Single);
        }

        //Player 1 Selecting
        //if (Input.GetKeyDown(KeyCode.W) && p1NoCharSelected < 3)
        if (Input.GetAxisRaw("Vertical") == 1 && !p1Pressed && p1NoCharSelected < 3)
        {
            p1Pressed = true;

            if(player1Position>13)
            {
                player1Position -= 14;
            }
            else
            {
                player1Position += 14;
            }

            audioSource1.clip = moveSound;
            audioSource1.time = 0.9f;
            audioSource1.Play();

            moveCursor1();
        }
        //if (Input.GetKeyDown(KeyCode.A) && p1NoCharSelected < 3)
        if (Input.GetAxisRaw("Horizontal") == -1 && !p1Pressed && p1NoCharSelected < 3)
        {
            p1Pressed = true;

            player1Position--;

            if(player1Position == 13)
            {
                player1Position = 27;
            }
            if(player1Position == -1)
            {
                player1Position = 13;
            }

            audioSource1.clip = moveSound;
            audioSource1.time = 0.9f;
            audioSource1.Play();

            moveCursor1();
        }
        //if (Input.GetKeyDown(KeyCode.S) && p1NoCharSelected < 3)
        if (Input.GetAxisRaw("Vertical") == -1 && !p1Pressed && p1NoCharSelected < 3)
        {
            p1Pressed = true;

            if (player1Position > 13)
            {
                player1Position -= 14;
            }
            else
            {
                player1Position += 14;
            }

            audioSource1.clip = moveSound;
            audioSource1.time = 0.9f;
            audioSource1.Play();

            moveCursor1();
        }
        //if (Input.GetKeyDown(KeyCode.D) && p1NoCharSelected < 3)
        if (Input.GetAxisRaw("Horizontal") == 1 && !p1Pressed && p1NoCharSelected < 3)
        {
            p1Pressed = true;

            player1Position++;

            if (player1Position == 14)
            {
                player1Position = 0;
            }
            if (player1Position == 28)
            {
                player1Position = 14;
            }

            audioSource1.clip = moveSound;
            audioSource1.time = 0.9f;
            audioSource1.Play();

            moveCursor1();
        }
        //if (Input.GetKeyDown(KeyCode.J) && p1NoCharSelected < 3)
        if (Input.GetAxisRaw("Light1") == 1 && !p1Pressed && p1NoCharSelected < 3)
        {
            p1Pressed = true;

            if (names[player1Position] == "")
            {
                audioSource1.clip = errorSound;
                audioSource1.time = 0.9f;
                audioSource1.Play();
                return;
            }

            p1Characters[p1NoCharSelected++] = player1Position;

            audioSource1.clip = selectSound;
            audioSource1.time = 0.9f;
            audioSource1.Play();

            if (p1NoCharSelected > 2)
            {
                player1Cursor.enabled = false;
                p1CursorText.enabled = false;
            }
            else
            {
                moveCursor1();
            }
        }
        // if (Input.GetKeyDown(KeyCode.K) && p1NoCharSelected > 0)
        if (Input.GetAxisRaw("Medium1") == 1 && !p1Pressed && p1NoCharSelected > 0)
        {
            p1Pressed = true;

            player1Cursor.enabled = true;
            p1CursorText.enabled = true;

            if (p1NoCharSelected == 2)
            {
                player1Char3.material = portraits[28];
                player1Char3Text.text = names[28];
            }
            else if (p1NoCharSelected == 1)
            {
                player1Char2.material = portraits[28];
                player1Char2Text.text = names[28];
            }

            audioSource1.clip = cancelSound;
            audioSource1.time = 0.9f;
            audioSource1.Play();

            p1NoCharSelected--;
            moveCursor1();
        }
        else if(Input.GetAxisRaw("Medium1") == 1 && !p1Pressed && p1NoCharSelected == 0)
        {
                //Switch scenes
               SceneManager.LoadScene("titleScreen", LoadSceneMode.Single);
            
        }


        //Player 2 Selecting
        //if (Input.GetKeyDown(KeyCode.UpArrow) && p2NoCharSelected < 3)
        if (Input.GetAxisRaw("Vertical2") == 1 && !p2Pressed && p2NoCharSelected < 3)
        {
            p2Pressed = true;

            if (player2Position > 13)
            {
                player2Position -= 14;
            }
            else
            {
                player2Position += 14;
            }

            audioSource2.clip = moveSound;
            audioSource2.time = 0.9f;
            audioSource2.Play();

            moveCursor2();
        }
        //if (Input.GetKeyDown(KeyCode.LeftArrow) && p2NoCharSelected < 3)
        if (Input.GetAxisRaw("Horizontal2") == -1 && !p2Pressed && p2NoCharSelected < 3)
        {
            p2Pressed = true;

            player2Position--;

            if (player2Position == 13)
            {
                player2Position = 27;
            }
            if (player2Position == -1)
            {
                player2Position = 13;
            }

            audioSource2.clip = moveSound;
            audioSource2.time = 0.9f;
            audioSource2.Play();

            moveCursor2();
        }
        //if (Input.GetKeyDown(KeyCode.RightArrow) && p2NoCharSelected < 3)
        if (Input.GetAxisRaw("Horizontal2") == 1 && !p2Pressed && p2NoCharSelected < 3)
        {
            p2Pressed = true;

            player2Position++;

            if (player2Position == 14)
            {
                player2Position = 0;
            }
            if (player2Position == 28)
            {
                player2Position = 14;
            }

            audioSource2.clip = moveSound;
            audioSource2.time = 0.9f;
            audioSource2.Play();

            moveCursor2();
        }
        //if (Input.GetKeyDown(KeyCode.DownArrow) && p2NoCharSelected < 3)
        if (Input.GetAxisRaw("Vertical2") == -1 && !p2Pressed && p2NoCharSelected < 3)
        {
            p2Pressed = true;

            if (player2Position > 13)
            {
                player2Position -= 14;
            }
            else
            {
                player2Position += 14;
            }

            audioSource2.clip = moveSound;
            audioSource2.time = 0.9f;
            audioSource2.Play();

            moveCursor2();
        }
        //if (Input.GetKeyDown(KeyCode.V) && p2NoCharSelected < 3)
        if (Input.GetAxisRaw("Light2") == 1 && !p2Pressed && p2NoCharSelected < 3)
        {
            p2Pressed = true;

            if(names[player2Position] == "")
            {
                audioSource2.clip = errorSound;
                audioSource2.time = 0.9f;
                audioSource2.Play();
                return;
            }

            p2Characters[p2NoCharSelected++] = player2Position;

            audioSource2.clip = selectSound;
            audioSource2.time = 0.9f;
            audioSource2.Play();

            if (p2NoCharSelected > 2)
            {
                player2Cursor.enabled = false;
                p2CursorText.enabled = false;
            }
            else
            {
                moveCursor2();
            }
        }
        //if (Input.GetKeyDown(KeyCode.B) && p2NoCharSelected > 0)
        if (Input.GetAxisRaw("Medium2") == 1 && !p2Pressed && p2NoCharSelected > 0)
        {
            p2Pressed = true;

            player2Cursor.enabled = true;
            p2CursorText.enabled = true;

            if (p2NoCharSelected == 2)
            {
                player2Char3.material = portraits[28];
                player2Char3Text.text = names[28];
            }
            else if (p2NoCharSelected == 1)
            {
                player2Char2.material = portraits[28];
                player2Char2Text.text = names[28];
            }

            audioSource2.clip = cancelSound;
            audioSource2.time = 0.9f;
            audioSource2.Play();

            p2NoCharSelected--;
            moveCursor2();
        }
        else if (Input.GetAxisRaw("Medium2") == 1 && !p2Pressed && p2NoCharSelected == 0)
        {
            //Switch scenes
            SceneManager.LoadScene("titleScreen", LoadSceneMode.Single);

        }
    }
}
