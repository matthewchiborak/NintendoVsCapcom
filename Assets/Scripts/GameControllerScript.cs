using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameControllerScript : MonoBehaviour {

    public string[] names = new string[28];
    public string[] special1s = new string[28];
    public string[] special2s = new string[28];
    public string[] special3s = new string[28];
    public string[] superNames = new string[28];
    // 0 is qcf, 1 is qcb, 2 is dpf, 3 is dpb
    public int[] inputMoves1 = new int[28];
    public int[] inputMoves2 = new int[28];
    public int[] inputMoves3 = new int[28];
    public int[] inputMovesSuper = new int[28];
    public Material[] inputIcons = new Material[4];

    public GameObject[] characterObjects = new GameObject[28];
    public GameObject[] assistObjects = new GameObject[28];
    public AudioClip[] themeMusic = new AudioClip[28];
    public AudioSource audioSource;

    public CharacterScript player1;
    public CharacterScript player1Assist1;
    public CharacterScript player1Assist2;

    public CharacterScript player2;
    public CharacterScript player2Assist1;
    public CharacterScript player2Assist2;

    public Text name1;
    public Image healthbar1;
    public Text name1Assist1;
    public Image healthbar1Assist1;
    public Text name1Assist2;
    public Image healthbar1Assist2;

    public Image assist1;

    public Text name2;
    public Image healthbar2;
    public Text name2Assist1;
    public Image healthbar2Assist1;
    public Text name2Assist2;
    public Image healthbar2Assist2;

    public Image assist2;

    public Image superBar1;
    public Image superBar2;

    public Camera mainCamera;
    public float cameraPosition;
    public float cameraSize;

    public Text combo1;
    public Text combo2;

    //Cool down for assists
    public float maxAssist;
    public float coolDownRate; //Assist recovery per frame
    public float currentAssist1;
    public float currentAssist2;

    //Integers for check if the player is holding down the assist button to switch characters
    public int assist1Held;
    public int assist2Held;
    public int assistSwapTime;

    //Counters for auto swapping out a KO'd character
    private int player1KOswapCount;
    private int player2KOswapCount;
    private int autoSwapTime;

    public Text gameWinText;

    private int returnToSelectCount;
    private int timeToDisplayWin;
    private bool oneTeamKO;

    private int matchTimer;
    private int framesForTimeDec;
    private int frameCount;
    public Text timerText;
    private bool timeout;

    //Game pause stuff
    private bool menuOpen;
    //Pause Screen UI stuff
    public Image pauseScreen;
    public Text pauseText;
    public Text player1ControlName;
    public Text player2ControlName;
    public Text p1S1;
    public Text p1S2;
    public Text p1S3;
    public Text p1SuperName;
    public Text p2S1;
    public Text p2S2;
    public Text p2S3;
    public Text p2SuperName;

    public Image move11;
    public Image move12;
    public Image move13;
    public Image move1super;
    public Image move21;
    public Image move22;
    public Image move23;
    public Image move2super;

    //public GameObject pauseEmpty;
    public GameObject pauseEmpty;

    //Slider to adjust the game's volume on the pause screen
    public Slider musicVolumeSlider;
    public Slider SFXVolumeSlider;

    //Snapshots for pausing the game to change audio
    public AudioMixerSnapshot pausedSnapshot;
    public AudioMixerSnapshot unpausedSnapshot;
    public AudioMixer masterMixer;

    //Bools for ensuring button presses are only being called once
    private bool startPressed = false;
    private bool p1Assist1Pressed = false;
    private bool p1Assist2Pressed = false;
    private bool p2Assist1Pressed = false;
    private bool p2Assist2Pressed = false;

    //Variable for pausing to do camera zoom
    private float pauseTime;
    private float unpauseTime;

    //Variable for actually doing the camera zoome
    public bool zooming;
    public bool isPlayer1Zoom; 
    public Vector3 startPoint;
    public Vector3 zoomPoint;
    public Vector3 lookatPoint;
    //private int framesForZoom = 30;
    private int framesForZoom = 45;
    private int totalZoomFrames = 120;
    private int currentZoomFrame = 0;
    private bool zoomDirection;

    private float startSize;
    private float targetSize;

    //Super sound effect
    public AudioSource superSound;
    public AudioSource speedUpSound;

    //Variables for the fight intro
    private int frameForIntro = 150;
    private int currentIntroFrame;
    public Text introText;
    public Image introBar;

    

    private bool onLeft; //True for player 1; False for player 2
    private int barSize;
    public bool paused;

    //Set the audio levels from the pause screen
    public void setSFXLevel(float SFXLevel)
    {
        masterMixer.SetFloat("SFXVolume", SFXLevel);
    }
    public void setMusicLevel(float musicLevel)
    {
        masterMixer.SetFloat("musicVolume", musicLevel);
    }

    // Use this for initialization
    void Start()
    {
        //Reset the intro
        currentIntroFrame = 0;

        //Reset the healths and meters
        StoredInfoScript.characterInfo.player1Health[0] = 1f;
        StoredInfoScript.characterInfo.player1Health[1] = 1f;
        StoredInfoScript.characterInfo.player1Health[2] = 1f;
        StoredInfoScript.characterInfo.player2Health[0] = 1f;
        StoredInfoScript.characterInfo.player2Health[1] = 1f;
        StoredInfoScript.characterInfo.player2Health[2] = 1f;

        StoredInfoScript.characterInfo.player1Meter = 100f;
        StoredInfoScript.characterInfo.player2Meter = 100f;

        maxAssist = 70;
        coolDownRate = 0.25f;
        currentAssist1 = 70;
        currentAssist2 = 70;

        //Create the stage
        Instantiate(StoredInfoScript.characterInfo.stages[StoredInfoScript.characterInfo.currentStage], new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)));

        //GameObject char1
        StoredInfoScript.characterInfo.player1CharacterGameObjects[0] = Instantiate(characterObjects[StoredInfoScript.characterInfo.player1Characters[0]], new Vector3(20f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        player1 = StoredInfoScript.characterInfo.player1CharacterGameObjects[0].GetComponent<CharacterScript>();
        player1.gameController = this;
        StoredInfoScript.characterInfo.player1CharacterGameObjects[1] = Instantiate(characterObjects[StoredInfoScript.characterInfo.player1Characters[1]], new Vector3(20f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        StoredInfoScript.characterInfo.player1CharacterGameObjects[1].SetActive(false);
        StoredInfoScript.characterInfo.player1CharacterGameObjects[2] = Instantiate(characterObjects[StoredInfoScript.characterInfo.player1Characters[2]], new Vector3(20f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        StoredInfoScript.characterInfo.player1CharacterGameObjects[2].SetActive(false);


        GameObject char2 = Instantiate(characterObjects[StoredInfoScript.characterInfo.player2Characters[0]], new Vector3(-20f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        player2 = char2.GetComponent<CharacterScript>();
        player2.gameController = this;
        StoredInfoScript.characterInfo.player2CharacterGameObjects[1] = Instantiate(characterObjects[StoredInfoScript.characterInfo.player2Characters[1]], new Vector3(20f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        StoredInfoScript.characterInfo.player2CharacterGameObjects[1].SetActive(false);
        StoredInfoScript.characterInfo.player2CharacterGameObjects[2] = Instantiate(characterObjects[StoredInfoScript.characterInfo.player2Characters[2]], new Vector3(20f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        StoredInfoScript.characterInfo.player2CharacterGameObjects[2].SetActive(false);

        onLeft = true;

        onLeft = true;
        player1.model.transform.localScale = new Vector3(1f, 1f, 1f);
        player1.direction = 1;
        player1.isPlayer1 = true;

        player2.model.transform.localScale = new Vector3(-1f, 1f, 1f);
        player2.grabbox.transform.localPosition = new Vector3(-1 * player2.grabbox.transform.localPosition.x, player2.grabbox.transform.localPosition.y, 0);
        player2.blockbox.transform.localPosition = new Vector3(-1 * player2.blockbox.transform.localPosition.x, player2.blockbox.transform.localPosition.y, 0);
        player2.direction = -1;
        player2.isPlayer1 = false;

     

        name1.text = names[StoredInfoScript.characterInfo.player1Characters[0]];
        name2.text = names[StoredInfoScript.characterInfo.player2Characters[0]];
        name1Assist1.text = names[StoredInfoScript.characterInfo.player1Characters[1]];
        name2Assist1.text = names[StoredInfoScript.characterInfo.player2Characters[1]];
        name1Assist2.text = names[StoredInfoScript.characterInfo.player1Characters[2]];
        name2Assist2.text = names[StoredInfoScript.characterInfo.player2Characters[2]];

        combo1.text = "";
        combo2.text = "";

        cameraPosition = 0;
        cameraSize = 24.33f;

        paused = false;

        assist1Held = 0;
        assist2Held = 0;
        assistSwapTime = 60;

        player1KOswapCount = 0;
        player2KOswapCount = 0;
        autoSwapTime = 180;

        gameWinText.enabled = false;
        returnToSelectCount = 0;
        timeToDisplayWin = 420;
        oneTeamKO = false;

        matchTimer = 99;
        framesForTimeDec = 90;
 
        frameCount = 0;
        timeout = false;
        timerText.text = "99";

        menuOpen = false;
        
        pauseEmpty.SetActive(false);

        unpausedSnapshot.TransitionTo(.01f);


        audioSource.clip = themeMusic[StoredInfoScript.characterInfo.player1Characters[0]];
        audioSource.Play();

        
       
    }

    public void zoomInstructions(Vector3 passedZoomPoint, Vector3 characterPosition, bool direction, float orthoSize)
    {
        startPoint = mainCamera.transform.position;
        lookatPoint = characterPosition;
        zoomPoint = passedZoomPoint;
        currentZoomFrame = 0;
        zoomDirection = direction;
        targetSize = orthoSize;
        startSize = 24.33f;
        zooming = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Intro countdown and fight
        if(currentIntroFrame < frameForIntro)
        {
            currentIntroFrame++;

            if(currentIntroFrame == 1)
            {
                player1.paused = true;
                player2.paused = true;
            }

            if (currentIntroFrame < 105)
            {
                //Decrement the intro bar
                introBar.transform.localScale = new Vector3((105.0f - currentIntroFrame) / 105.0f, 1, 1);
            }

            if(currentIntroFrame == 105)
            {
                introText.text = "FIGHT!";
                introBar.enabled = false;
            }

            if(currentIntroFrame == 150)
            {
                introText.enabled = false;
                player1.paused = false;
                player2.paused = false;
            }
            
            return;
        }

        //Reset the button check if applicable
        if(Input.GetAxisRaw("Start1") == 0 && Input.GetAxisRaw("Start2") == 0)
        {
            startPressed = false;
        }

        //Check if someone is pausing the game float h = Input.GetAxisRaw("Horizontal");
   
       if(!paused && (Input.GetAxisRaw("Start1") == 1 || Input.GetAxisRaw("Start2") == 1))
        {
            if (!startPressed)
            {
                startPressed = true;

                if (!menuOpen)
                {
                    menuOpen = true;
                    player1.paused = true;
                    player2.paused = true;
                    Time.timeScale = 0;

                    player1ControlName.text = names[StoredInfoScript.characterInfo.player1Characters[0]];
                    player2ControlName.text = names[StoredInfoScript.characterInfo.player2Characters[0]];

                    p1S1.text = special1s[StoredInfoScript.characterInfo.player1Characters[0]];
                    p1S2.text = special2s[StoredInfoScript.characterInfo.player1Characters[0]];
                    p1S3.text = special3s[StoredInfoScript.characterInfo.player1Characters[0]];
                    p1SuperName.text = superNames[StoredInfoScript.characterInfo.player1Characters[0]];

                    p2S1.text = special1s[StoredInfoScript.characterInfo.player2Characters[0]];
                    p2S2.text = special2s[StoredInfoScript.characterInfo.player2Characters[0]];
                    p2S3.text = special3s[StoredInfoScript.characterInfo.player2Characters[0]];
                    p2SuperName.text = superNames[StoredInfoScript.characterInfo.player2Characters[0]];

                    move11.material = inputIcons[inputMoves1[StoredInfoScript.characterInfo.player1Characters[0]]];
                    move12.material = inputIcons[inputMoves2[StoredInfoScript.characterInfo.player1Characters[0]]];
                    move13.material = inputIcons[inputMoves3[StoredInfoScript.characterInfo.player1Characters[0]]];
                    move1super.material = inputIcons[inputMovesSuper[StoredInfoScript.characterInfo.player1Characters[0]]];

                    move21.material = inputIcons[inputMoves1[StoredInfoScript.characterInfo.player2Characters[0]]];
                    move22.material = inputIcons[inputMoves2[StoredInfoScript.characterInfo.player2Characters[0]]];
                    move23.material = inputIcons[inputMoves3[StoredInfoScript.characterInfo.player2Characters[0]]];
                    move2super.material = inputIcons[inputMovesSuper[StoredInfoScript.characterInfo.player2Characters[0]]];

                    //Audio
                    pausedSnapshot.TransitionTo(.01f);

                    pauseEmpty.SetActive(true);
                }
                else
                {
                    menuOpen = false;
                    player1.paused = false;
                    player2.paused = false;
                    Time.timeScale = 1;

                    //Audio
                    unpausedSnapshot.TransitionTo(.01f);

                    pauseEmpty.SetActive(false);
                }
            }
        }

        //Check if the game is paused
        if(menuOpen)
        {
            //CHeckif changing volume
   
            if(Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical2") == 1)
            {
                SFXVolumeSlider.value += 1;
            }
       
            if (Input.GetAxisRaw("Vertical") == -1 || Input.GetAxisRaw("Vertical2") == -1)
            {
                SFXVolumeSlider.value -= 1;
            }
        
            if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal2") == 1)
            {
                musicVolumeSlider.value += 1;
            }
      
            if (Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Horizontal2") == -1)
            {
                musicVolumeSlider.value -= 1;
            }

            //Quit and Restart
         
            if((Input.GetAxisRaw("Light1") == 1 && Input.GetAxisRaw("Heavy1") == 1) || (Input.GetAxisRaw("Light2") == 1 && Input.GetAxisRaw("Heavy2") == 1))
            {
                menuOpen = false;
                player1.paused = false;
                player2.paused = false;
                Time.timeScale = 1;

                //Audio
                unpausedSnapshot.TransitionTo(.01f);

                pauseEmpty.SetActive(false);

                SceneManager.LoadScene("characterSelect", LoadSceneMode.Single);
            }
     
            if ((Input.GetAxisRaw("Assist11") == 1 && Input.GetAxisRaw("Assist21") == 1) || (Input.GetAxisRaw("Assist12") == 1 && Input.GetAxisRaw("Assist22") == 1))
            {
                menuOpen = false;
                player1.paused = false;
                player2.paused = false;
                Time.timeScale = 1;

                //Audio
                unpausedSnapshot.TransitionTo(.01f);

                pauseEmpty.SetActive(false);

                SceneManager.LoadScene("fight", LoadSceneMode.Single);
            }

            return;
        }

        //Not Paused but a player inited a camera zoom
        if(zooming)
        {
            if(currentZoomFrame == 0)
            {
                //Pause
                paused = true;
                player1.paused = true;
                player2.paused = true;
                pauseTime = Time.time;
                superSound.Play();
            }

            if (currentZoomFrame <= framesForZoom)
            {
                //Zoom in
               
                mainCamera.transform.position = Vector3.Slerp(startPoint, zoomPoint, (float)currentZoomFrame / (float)framesForZoom);
                mainCamera.orthographicSize = startSize + (targetSize - startSize) * ((float)currentZoomFrame / (float)framesForZoom);

                if (zoomDirection) //On left
                {
                    mainCamera.transform.Rotate(0, (-50.0f) / (float)framesForZoom, 0);
                }
                else
                {
                    mainCamera.transform.Rotate(0, (50.0f) / (float)framesForZoom, 0);
                }
                
            }

            if(currentZoomFrame == (totalZoomFrames - framesForZoom))
            {
                speedUpSound.Play();
            }

            if (currentZoomFrame >= (totalZoomFrames - framesForZoom))
            {
                //Zoom out
                mainCamera.transform.position = Vector3.Slerp(zoomPoint, startPoint, (float)(currentZoomFrame - (totalZoomFrames - framesForZoom)) / (float)framesForZoom);
             
                mainCamera.orthographicSize = targetSize - (targetSize - startSize) * ((float)(currentZoomFrame - (totalZoomFrames - framesForZoom)) / (float)framesForZoom);

                if (zoomDirection) //On left
                {
                    mainCamera.transform.Rotate(0, (50.0f) / (float)framesForZoom, 0);
                }
                else
                {
                    mainCamera.transform.Rotate(0, (-50.0f) / (float)framesForZoom, 0);
                }
            }

            if (currentZoomFrame == totalZoomFrames)
            {
                //UnPause
                paused = false;
                player1.paused = false;
                player2.paused = false;
                player1.startingTime += Time.time - pauseTime;
                player2.startingTime += Time.time - pauseTime;
                zooming = false;
            }

            currentZoomFrame++;
            return;
        }

        //Update the match timer
        frameCount++;
        if (frameCount >= framesForTimeDec && !timeout && !oneTeamKO)
        {
            frameCount = 0;
            matchTimer--;
            timerText.text = matchTimer.ToString();
        }
        
        //Check if time out
        if(matchTimer <= 0 && !timeout)
        {
            timeout = true;
            gameWinText.text = "TIME UP";
            gameWinText.enabled = true;

            float p1TotalHealth = StoredInfoScript.characterInfo.player1Health[0] + StoredInfoScript.characterInfo.player1Health[1] + StoredInfoScript.characterInfo.player1Health[2];
            float p2TotalHealth = StoredInfoScript.characterInfo.player2Health[0] + StoredInfoScript.characterInfo.player2Health[1] + StoredInfoScript.characterInfo.player2Health[2];

            if (p1TotalHealth > p2TotalHealth)
            {
                //Player 1 wins
                StoredInfoScript.characterInfo.player2Health[0] = 0;
                StoredInfoScript.characterInfo.player2Health[1] = 0;
                StoredInfoScript.characterInfo.player2Health[2] = 0;
            }
            else if(p2TotalHealth > p1TotalHealth)
            {
                //Player 2 wins
                StoredInfoScript.characterInfo.player1Health[0] = 0;
                StoredInfoScript.characterInfo.player1Health[1] = 0;
                StoredInfoScript.characterInfo.player1Health[2] = 0;
            }
            else
            {
                //Draw
                gameWinText.text = "DRAW";
                gameWinText.enabled = true;
                if (!oneTeamKO)
                {
                    oneTeamKO = true;
                    returnToSelectCount = timeToDisplayWin;
                }
            }
        }

        //Incrememt the assist meter
        currentAssist1 += coolDownRate;
        currentAssist2 += coolDownRate;

        if (currentAssist1 > maxAssist)
        {
            currentAssist1 = maxAssist;
        }
        if (currentAssist2 > maxAssist)
        {
            currentAssist2 = maxAssist;
        }

        //Fix bug where if call in an assist while on cool down would instantly come up when meter reached max
        if (Input.GetAxisRaw("Assist11") == 0 && p1Assist1Pressed && currentAssist1 != maxAssist)
        {
            p1Assist1Pressed = false;
            assist1Held = 0;
        }
        if (Input.GetAxisRaw("Assist21") == 0 && p1Assist2Pressed && currentAssist1 != maxAssist)
        {
            p1Assist2Pressed = false;
            assist1Held = 0;
        }
        if (Input.GetAxisRaw("Assist12") == 0 && p2Assist1Pressed && currentAssist2 != maxAssist)
        {
            p2Assist1Pressed = false;
            assist2Held = 0;
        }
        if (Input.GetAxisRaw("Assist22") == 0 && p2Assist2Pressed && currentAssist2 != maxAssist)
        {
            p2Assist2Pressed = false;
            assist2Held = 0;
        }

        //If all characters are dead, return to character select if enought time passes
        if (oneTeamKO)
        {
            returnToSelectCount--;

            if(returnToSelectCount <= 0)
            {
                SceneManager.LoadScene("characterSelect", LoadSceneMode.Single);
            }
        }

        //If a character is dead start up the counter
        if(StoredInfoScript.characterInfo.player1Health[0] <= 0 && player1.transform.position.y <= 0)
        {
            player1KOswapCount++;
        }
        if (StoredInfoScript.characterInfo.player2Health[0] <= 0 && player2.transform.position.y <= 0)
        {
            player2KOswapCount++;
        }

        //If enough time has past for the KO animation to play auto switch the the player's next character. If out of characters, game over state
        if (player1KOswapCount >= autoSwapTime)
        {
            player1KOswapCount = 0;

            //Swap in char 1 if can
            if(StoredInfoScript.characterInfo.player1Health[1] > 0)
            {
                StoredInfoScript.characterInfo.swapChar(true, true);

                audioSource.clip = themeMusic[StoredInfoScript.characterInfo.player1Characters[0]];
                audioSource.Play();

                //Delete Current Character
                Vector3 tempVector = new Vector3(player1.transform.position.x, 0, 0);
                Destroy(player1.gameObject);


                //Create new character
                GameObject char1 = Instantiate(characterObjects[StoredInfoScript.characterInfo.player1Characters[0]], tempVector, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                player1 = char1.GetComponent<CharacterScript>();
                player1.gameController = this;

                
                player1.isPlayer1 = true;

                if (onLeft)
                {
                    player1.model.transform.localScale = new Vector3(1f, 1f, 1f);
                    player1.direction = 1;
                }
                else
                {
                    player1.model.transform.localScale = new Vector3(-1f, 1f, 1f);
                    player1.grabbox.transform.localPosition = new Vector3(-1 * player1.grabbox.transform.localPosition.x, player1.grabbox.transform.localPosition.y, 0);
                    player1.blockbox.transform.localPosition = new Vector3(-1 * player1.blockbox.transform.localPosition.x, player1.blockbox.transform.localPosition.y, 0);
                    player1.direction = -1;
                }

              

                //Update UI
                name1.text = names[StoredInfoScript.characterInfo.player1Characters[0]];
                name1Assist1.text = names[StoredInfoScript.characterInfo.player1Characters[1]];
                healthbar1Assist1.transform.localScale = new Vector3(StoredInfoScript.characterInfo.player1Health[1], 1, 1);
            }
            //Swap in char2 if can
            else if(StoredInfoScript.characterInfo.player1Health[2] > 0)
            {
                //Swap
                StoredInfoScript.characterInfo.swapChar(true, false);

                audioSource.clip = themeMusic[StoredInfoScript.characterInfo.player1Characters[0]];
                audioSource.Play();

                //Delete Current Character
                Vector3 tempVector = new Vector3(player1.transform.position.x, 0, 0);
                Destroy(player1.gameObject);

                //Create new character
                GameObject char1 = Instantiate(characterObjects[StoredInfoScript.characterInfo.player1Characters[0]], tempVector, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                player1 = char1.GetComponent<CharacterScript>();
                player1.isPlayer1 = true;
                player1.gameController = this;
                if (onLeft)
                {
                    player1.model.transform.localScale = new Vector3(1f, 1f, 1f);
                    player1.direction = 1;
                }
                else
                {
                    player1.model.transform.localScale = new Vector3(-1f, 1f, 1f);
                    player1.grabbox.transform.localPosition = new Vector3(-1 * player1.grabbox.transform.localPosition.x, player1.grabbox.transform.localPosition.y, 0);
                    player1.blockbox.transform.localPosition = new Vector3(-1 * player1.blockbox.transform.localPosition.x, player1.blockbox.transform.localPosition.y, 0);
                    player1.direction = -1;
                }

              

                //Update UI
                name1.text = names[StoredInfoScript.characterInfo.player1Characters[0]];
                name1Assist2.text = names[StoredInfoScript.characterInfo.player1Characters[2]];
                healthbar1Assist2.transform.localScale = new Vector3(StoredInfoScript.characterInfo.player1Health[2], 1, 1);
            }
            //No characters left game over
            else
            {
                gameWinText.text = "PLAYER 2 WINS";
                gameWinText.enabled = true;
                if (!oneTeamKO)
                {
                    oneTeamKO = true;
                    returnToSelectCount = timeToDisplayWin;
                }
            }
        }
        if (player2KOswapCount >= autoSwapTime)
        {
            player2KOswapCount = 0;

            //Swap in char 1 if can
            if (StoredInfoScript.characterInfo.player2Health[1] > 0)
            {
                //Swap
                StoredInfoScript.characterInfo.swapChar(false, true);

                audioSource.clip = themeMusic[StoredInfoScript.characterInfo.player2Characters[0]];
                audioSource.Play();

                //Delete Current Character
                Vector3 tempVector = new Vector3(player2.transform.position.x, 0, 0);
                Destroy(player2.gameObject);

                //Create new character
                GameObject char1 = Instantiate(characterObjects[StoredInfoScript.characterInfo.player2Characters[0]], tempVector, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                player2 = char1.GetComponent<CharacterScript>();
                player2.gameController = this;

                if (!onLeft)
                {
                    player2.model.transform.localScale = new Vector3(1f, 1f, 1f);
                    player2.direction = 1;
                }
                else
                {
                    player2.model.transform.localScale = new Vector3(-1f, 1f, 1f);
                    player2.grabbox.transform.localPosition = new Vector3(-1 * player2.grabbox.transform.localPosition.x, player2.grabbox.transform.localPosition.y, 0);
                    player2.blockbox.transform.localPosition = new Vector3(-1 * player2.blockbox.transform.localPosition.x, player2.blockbox.transform.localPosition.y, 0);
                    player2.direction = -1;
                }

              

                //Update UI
                name2.text = names[StoredInfoScript.characterInfo.player1Characters[0]];
                name2Assist1.text = names[StoredInfoScript.characterInfo.player2Characters[1]];
                healthbar2Assist1.transform.localScale = new Vector3(StoredInfoScript.characterInfo.player2Health[1], 1, 1);
            }
            //Swap in char2 if can
            else if (StoredInfoScript.characterInfo.player2Health[2] > 0)
            {
                //Swap
                StoredInfoScript.characterInfo.swapChar(false, false);

                audioSource.clip = themeMusic[StoredInfoScript.characterInfo.player2Characters[0]];
                audioSource.Play();

                //Delete Current Character
                Vector3 tempVector = new Vector3(player2.transform.position.x, 0, 0);
                Destroy(player2.gameObject);

                //Create new character
                GameObject char1 = Instantiate(characterObjects[StoredInfoScript.characterInfo.player2Characters[0]], tempVector, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                player2 = char1.GetComponent<CharacterScript>();
                player2.gameController = this;

                if (!onLeft)
                {
                    player2.model.transform.localScale = new Vector3(1f, 1f, 1f);
                    player2.direction = 1;
                }
                else
                {
                    player2.model.transform.localScale = new Vector3(-1f, 1f, 1f);
                    player2.grabbox.transform.localPosition = new Vector3(-1 * player2.grabbox.transform.localPosition.x, player2.grabbox.transform.localPosition.y, 0);
                    player2.blockbox.transform.localPosition = new Vector3(-1 * player2.blockbox.transform.localPosition.x, player2.blockbox.transform.localPosition.y, 0);
                    player2.direction = -1;
                }

               

                //Update UI
                name2.text = names[StoredInfoScript.characterInfo.player1Characters[0]];
                name2Assist2.text = names[StoredInfoScript.characterInfo.player2Characters[2]];
                healthbar2Assist2.transform.localScale = new Vector3(StoredInfoScript.characterInfo.player2Health[2], 1, 1);
            }
            //No characters left game over
            else
            {
                gameWinText.text = "PLAYER 1 WINS";
                gameWinText.enabled = true;
                if (!oneTeamKO)
                {
                    oneTeamKO = true;
                    returnToSelectCount = timeToDisplayWin;
                }
            }
        }

        //Check if Player is holding an assist button to switch characters
      
        if (Input.GetAxisRaw("Assist11") == 1 || Input.GetAxisRaw("Assist21") == 1)
        {
            assist1Held++;

            if(Input.GetAxisRaw("Assist11") == 1)
            {
                p1Assist1Pressed = true;
            }
            if (Input.GetAxisRaw("Assist21") == 1)
            {
                p1Assist2Pressed = true;
            }
        }
        if (Input.GetAxisRaw("Assist12") == 1 || Input.GetAxisRaw("Assist22") == 1)
        {
            assist2Held++;

            if (Input.GetAxisRaw("Assist12") == 1)
            {
                p2Assist1Pressed = true;
            }
            if (Input.GetAxisRaw("Assist22") == 1)
            {
                p2Assist2Pressed = true;
            }
        }

        if (assist1Held == assistSwapTime || assist2Held == assistSwapTime)
        {
            //Swap out the character
            if(assist1Held == assistSwapTime)
            {
                if (Input.GetAxisRaw("Assist11") == 1 && StoredInfoScript.characterInfo.player1Health[1] > 0)
                {
                  

                    //Swap
                    StoredInfoScript.characterInfo.swapChar(true, true);

                    audioSource.clip = themeMusic[StoredInfoScript.characterInfo.player1Characters[0]];
                    audioSource.Play();

                    //Delete Current Character
                    Vector3 tempVector = new Vector3(player1.transform.position.x, 0, 0);
                    Destroy(player1.gameObject);


                    //Create new character
                     GameObject char1 = Instantiate(characterObjects[StoredInfoScript.characterInfo.player1Characters[0]], tempVector, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                    player1 = char1.GetComponent<CharacterScript>();
                    player1.gameController = this;

                  
                    player1.isPlayer1 = true;

                    if (onLeft)
                    {
                        player1.model.transform.localScale = new Vector3(1f, 1f, 1f);
                        player1.direction = 1;
                    }
                    else
                    { 
                        player1.model.transform.localScale = new Vector3(-1f, 1f, 1f);
                        player1.grabbox.transform.localPosition = new Vector3(-1 * player1.grabbox.transform.localPosition.x, player1.grabbox.transform.localPosition.y, 0);
                        player1.blockbox.transform.localPosition = new Vector3(-1 * player1.blockbox.transform.localPosition.x, player1.blockbox.transform.localPosition.y, 0);
                        player1.direction = -1;
                    }

             

                    //Update UI
                    name1.text = names[StoredInfoScript.characterInfo.player1Characters[0]];
                    name1Assist1.text = names[StoredInfoScript.characterInfo.player1Characters[1]];
                    healthbar1Assist1.transform.localScale = new Vector3(StoredInfoScript.characterInfo.player1Health[1], 1, 1);
                }
                else if(StoredInfoScript.characterInfo.player1Health[2] > 0)
                {
                    //Assist 2 Char
       

                    //Swap
                    StoredInfoScript.characterInfo.swapChar(true, false);

                    audioSource.clip = themeMusic[StoredInfoScript.characterInfo.player1Characters[0]];
                    audioSource.Play();

                    //Delete Current Character
                    Vector3 tempVector = new Vector3(player1.transform.position.x, 0, 0);
                    Destroy(player1.gameObject);

                    //Create new character
                     GameObject char1 = Instantiate(characterObjects[StoredInfoScript.characterInfo.player1Characters[0]], tempVector, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                    player1 = char1.GetComponent<CharacterScript>();
                    player1.gameController = this;
                    player1.isPlayer1 = true;
                    if (onLeft)
                    {
                        player1.model.transform.localScale = new Vector3(1f, 1f, 1f);
                        player1.direction = 1;
                    }
                    else
                    {
                        player1.model.transform.localScale = new Vector3(-1f, 1f, 1f);
                        player1.grabbox.transform.localPosition = new Vector3(-1 * player1.grabbox.transform.localPosition.x, player1.grabbox.transform.localPosition.y, 0);
                        player1.blockbox.transform.localPosition = new Vector3(-1 * player1.blockbox.transform.localPosition.x, player1.blockbox.transform.localPosition.y, 0);
                        player1.direction = -1;
                    }


                    //Update UI
                    name1.text = names[StoredInfoScript.characterInfo.player1Characters[0]];
                    name1Assist2.text = names[StoredInfoScript.characterInfo.player1Characters[2]];
                    healthbar1Assist2.transform.localScale = new Vector3(StoredInfoScript.characterInfo.player1Health[2], 1, 1);
                }
            }
            if(assist2Held == assistSwapTime)
            {
             
                if (Input.GetAxisRaw("Assist12") == 1 && StoredInfoScript.characterInfo.player2Health[1] > 0)
                {
                    //Assist 1 Char
                   

                    //Swap
                    StoredInfoScript.characterInfo.swapChar(false, true);

                    audioSource.clip = themeMusic[StoredInfoScript.characterInfo.player2Characters[0]];
                    audioSource.Play();

                    //Delete Current Character
                    Vector3 tempVector = new Vector3(player2.transform.position.x, 0, 0);
                    Destroy(player2.gameObject);

                    //Create new character
                    GameObject char1 = Instantiate(characterObjects[StoredInfoScript.characterInfo.player2Characters[0]], tempVector, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                    player2 = char1.GetComponent<CharacterScript>();
                    player2.gameController = this;

                    if (!onLeft)
                    {
                        player2.model.transform.localScale = new Vector3(1f, 1f, 1f);
                        player2.direction = 1;
                    }
                    else
                    {
                        player2.model.transform.localScale = new Vector3(-1f, 1f, 1f);
                        player2.grabbox.transform.localPosition = new Vector3(-1 * player2.grabbox.transform.localPosition.x, player2.grabbox.transform.localPosition.y, 0);
                        player2.blockbox.transform.localPosition = new Vector3(-1 * player2.blockbox.transform.localPosition.x, player2.blockbox.transform.localPosition.y, 0);
                        player2.direction = -1;
                    }

                  

                    //Update UI
                    name2.text = names[StoredInfoScript.characterInfo.player1Characters[0]];
                    name2Assist1.text = names[StoredInfoScript.characterInfo.player2Characters[1]];
                    healthbar2Assist1.transform.localScale = new Vector3(StoredInfoScript.characterInfo.player2Health[1], 1, 1);
                }
                else if(StoredInfoScript.characterInfo.player2Health[2] > 0)
                {
                    //Assist 2 Char
                    //Update stored healths
                  

                    //Swap
                    StoredInfoScript.characterInfo.swapChar(false, false);

                    audioSource.clip = themeMusic[StoredInfoScript.characterInfo.player2Characters[0]];
                    audioSource.Play();

                    //Delete Current Character
                    Vector3 tempVector = new Vector3(player2.transform.position.x, 0, 0);
                    Destroy(player2.gameObject);

                    //Create new character
                    GameObject char1 = Instantiate(characterObjects[StoredInfoScript.characterInfo.player2Characters[0]], tempVector, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                    player2 = char1.GetComponent<CharacterScript>();
                    player2.gameController = this;

                    if (!onLeft)
                    {
                        player2.model.transform.localScale = new Vector3(1f, 1f, 1f);
                        player2.direction = 1;
                    }
                    else
                    {
                        player2.model.transform.localScale = new Vector3(-1f, 1f, 1f);
                        player2.grabbox.transform.localPosition = new Vector3(-1 * player2.grabbox.transform.localPosition.x, player2.grabbox.transform.localPosition.y, 0);
                        player2.blockbox.transform.localPosition = new Vector3(-1 * player2.blockbox.transform.localPosition.x, player2.blockbox.transform.localPosition.y, 0);
                        player2.direction = -1;
                    }

                
            

                    //Update UI
                    name2.text = names[StoredInfoScript.characterInfo.player1Characters[0]];
                    name2Assist2.text = names[StoredInfoScript.characterInfo.player2Characters[2]];
                    healthbar2Assist2.transform.localScale = new Vector3(StoredInfoScript.characterInfo.player2Health[2], 1, 1);
                }
            }
        }
        else
        {
            //Check if player is calling for an assist
      
            if (Input.GetAxisRaw("Assist11") == 0 && p1Assist1Pressed && currentAssist1 == maxAssist)
            {
                p1Assist1Pressed = false;

                //Dont call assist if swtiching characters
                if (assist1Held > assistSwapTime)
                {
                    assist1Held = 0;
                }
                else
                {
                    //Call in the assist
                    if (StoredInfoScript.characterInfo.player1Health[1] > 0)
                    {
                        if (player1.direction > 0)
                        {
                     
                            GameObject assistObj = Instantiate(assistObjects[StoredInfoScript.characterInfo.player1Characters[1]], new Vector3(player1.transform.position.x - player1.direction * 10, 1f, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                    
                            assistObj.GetComponentInChildren<AssistScript>().isPlayer1Assist = true;
                        }
                        else
                        {
                            GameObject assistObj = Instantiate(assistObjects[StoredInfoScript.characterInfo.player1Characters[1]], new Vector3(player1.transform.position.x - player1.direction * 10, 1f, 0), Quaternion.Euler(new Vector3(0, 180, 0))) as GameObject;
                            assistObj.GetComponentInChildren<AssistScript>().isPlayer1Assist = true;
                        }
                        currentAssist1 = 0;
                    }
                    
                }
            }
         
            if (Input.GetAxis("Assist21") == 0 && p1Assist2Pressed && currentAssist1 == maxAssist)
            {
                p1Assist2Pressed = false;

                if (assist1Held > assistSwapTime)
                {
                    assist1Held = 0;
                }
                else
                {
                    //Call in the assist
                    if (StoredInfoScript.characterInfo.player1Health[2] > 0)
                    {
                        if (player1.direction > 0)
                        {
                        
                            GameObject assistObj = Instantiate(assistObjects[StoredInfoScript.characterInfo.player1Characters[2]], new Vector3(player1.transform.position.x - player1.direction * 10, 1f, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                   
                            assistObj.GetComponentInChildren<AssistScript>().isPlayer1Assist = true;
                        }
                        else
                        {
                            GameObject assistObj = Instantiate(assistObjects[StoredInfoScript.characterInfo.player1Characters[2]], new Vector3(player1.transform.position.x - player1.direction * 10, 1f, 0), Quaternion.Euler(new Vector3(0, 180, 0))) as GameObject;
                            assistObj.GetComponentInChildren<AssistScript>().isPlayer1Assist = true;
                        }
                        currentAssist1 = 0;
                    }
                    
                }
            }
          
            if (Input.GetAxisRaw("Assist12") == 0 && p2Assist1Pressed  && currentAssist2 == maxAssist)
            {
                p2Assist1Pressed = false;

                if (assist2Held > assistSwapTime)
                {
                    assist2Held = 0;
                }
                else
                {
                    //Call in the assist
                    if (StoredInfoScript.characterInfo.player2Health[1] > 0)
                    {
                        if (player2.direction > 0)
                        {
                   
                            GameObject assistObj = Instantiate(assistObjects[StoredInfoScript.characterInfo.player2Characters[1]], new Vector3(player2.transform.position.x - player2.direction * 10, 1f, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                    
                            assistObj.GetComponentInChildren<AssistScript>().isPlayer1Assist = false;
                        }
                        else
                        {
                            GameObject assistObj = Instantiate(assistObjects[StoredInfoScript.characterInfo.player2Characters[1]], new Vector3(player2.transform.position.x - player2.direction * 10, 1f, 0), Quaternion.Euler(new Vector3(0, 180, 0))) as GameObject;
                            assistObj.GetComponentInChildren<AssistScript>().isPlayer1Assist = false;
                        }
                        currentAssist2 = 0;
                    }
                    
                }
            }
     
            if (Input.GetAxisRaw("Assist22") == 0 && p2Assist2Pressed && currentAssist2 == maxAssist)
            {
                p2Assist2Pressed = false;

                if (assist2Held > assistSwapTime)
                {
                    assist2Held = 0;
                }
                else
                {
                    //Call in the assist
                    if (StoredInfoScript.characterInfo.player2Health[2] > 0)
                    {
                        if (player2.direction > 0)
                        {
                            GameObject assistObj = Instantiate(assistObjects[StoredInfoScript.characterInfo.player2Characters[2]], new Vector3(player2.transform.position.x - player2.direction * 10, 1f, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                        
                            assistObj.GetComponentInChildren<AssistScript>().isPlayer1Assist = false;
                        }
                        else
                        {
                            GameObject assistObj = Instantiate(assistObjects[StoredInfoScript.characterInfo.player2Characters[2]], new Vector3(player2.transform.position.x - player2.direction * 10, 1f, 0), Quaternion.Euler(new Vector3(0, 180, 0))) as GameObject;
                            assistObj.GetComponentInChildren<AssistScript>().isPlayer1Assist = false;
                        }
                        currentAssist2 = 0;
                    }
                    
                }
            }
        }

        

        //Reset the ability to move backwards
        player1.backDisabled = false;
        player2.backDisabled = false;

        //Camera movement
        //Facing right
        if (player1.direction > 0 && (player1.transform.position.x > cameraPosition + (cameraSize / 0.6))) 
        {
            if ((player2.transform.position.x > cameraPosition - (cameraSize / 0.6)) && mainCamera.transform.position.x < StoredInfoScript.characterInfo.stageBounds[StoredInfoScript.characterInfo.currentStage])
            {
                cameraPosition += 1;
                mainCamera.transform.position = new Vector3(cameraPosition, mainCamera.transform.position.y, mainCamera.transform.position.z);
            }
            else
            {
                player1.backDisabled = true;
            }

           
        }
        if (player2.direction > 0 && (player2.transform.position.x > cameraPosition + (cameraSize / 0.6)))
        {
           if ((player1.transform.position.x > cameraPosition - (cameraSize / 0.6)) && mainCamera.transform.position.x < StoredInfoScript.characterInfo.stageBounds[StoredInfoScript.characterInfo.currentStage])
            {
                cameraPosition += 1;
              
                mainCamera.transform.position = new Vector3(cameraPosition, mainCamera.transform.position.y, mainCamera.transform.position.z);
            }
            else
            {
                player2.backDisabled = true;
            }
            
        }
        //Facing Left
        if (player1.direction < 0 && (player1.transform.position.x < cameraPosition - (cameraSize / 0.6)))
        {

            if ((player2.transform.position.x < cameraPosition + (cameraSize / 0.6)) && mainCamera.transform.position.x > -1 * StoredInfoScript.characterInfo.stageBounds[StoredInfoScript.characterInfo.currentStage])
            {
                cameraPosition -= 1;
               
                mainCamera.transform.position = new Vector3(cameraPosition, mainCamera.transform.position.y, mainCamera.transform.position.z);
            }
            else
            {
                player1.backDisabled = true;
            }
            
        }
        if (player2.direction < 0 && (player2.transform.position.x < cameraPosition - (cameraSize / 0.6)))
        {
            if ((player1.transform.position.x < cameraPosition + (cameraSize / 0.6)) && mainCamera.transform.position.x > -1 * StoredInfoScript.characterInfo.stageBounds[StoredInfoScript.characterInfo.currentStage])
            {
                cameraPosition -= 1;
              
                mainCamera.transform.position = new Vector3(cameraPosition, mainCamera.transform.position.y, mainCamera.transform.position.z);
            }
            else
            {
                player2.backDisabled = true;
            }
           
        }

        

        //Update combo text
        if(player1.currentCombo >2)
        {
            combo2.text = player1.currentCombo.ToString() + " HITS!";
        }
        else
        {
            combo2.text = "";
        }
        if (player2.currentCombo > 2)
        {
            combo1.text = player2.currentCombo.ToString() + " HITS!";
        }
        else
        {
            combo1.text = "";
        }

        //Update health bars
        name1.text = player1.characterName;
        name2.text = player2.characterName;
        healthbar1.transform.localScale = new Vector3(StoredInfoScript.characterInfo.player1Health[0], healthbar1.transform.localScale.y, healthbar1.transform.localScale.z);
        healthbar2.transform.localScale = new Vector3(StoredInfoScript.characterInfo.player2Health[0], healthbar2.transform.localScale.y, healthbar2.transform.localScale.z);

        
        //The Actual assist bars
        assist1.transform.localScale = new Vector3(currentAssist1 / maxAssist, 1, 1);
        assist2.transform.localScale = new Vector3(currentAssist2 / maxAssist, 1, 1);

        //Update super bars
        if (StoredInfoScript.characterInfo.player1Meter <= StoredInfoScript.characterInfo.maxMeter)
        superBar1.transform.localScale = new Vector3(StoredInfoScript.characterInfo.player1Meter / StoredInfoScript.characterInfo.maxMeter, healthbar1.transform.localScale.y, healthbar1.transform.localScale.z);
        if (StoredInfoScript.characterInfo.player2Meter <= StoredInfoScript.characterInfo.maxMeter)
            superBar2.transform.localScale = new Vector3(StoredInfoScript.characterInfo.player2Meter / StoredInfoScript.characterInfo.maxMeter, healthbar2.transform.localScale.y, healthbar2.transform.localScale.z);
        
        if(StoredInfoScript.characterInfo.player1Meter == StoredInfoScript.characterInfo.maxMeter)
        {
            superBar1.color = new Color(0, 1, 1, 1);
        }
        else if(StoredInfoScript.characterInfo.player1Meter / StoredInfoScript.characterInfo.maxMeter >= 0.75)
        {
            superBar1.color = new Color(0, 0.80f, 0.80f, 1);
        }
        else if (StoredInfoScript.characterInfo.player1Meter / StoredInfoScript.characterInfo.maxMeter >= 0.50)
        {
            superBar1.color = new Color(0, 0.60f, 0.60f, 1);
        }
        else if (StoredInfoScript.characterInfo.player1Meter / StoredInfoScript.characterInfo.maxMeter >= 0.25)
        {
            superBar1.color = new Color(0, 0.4f, 0.4f, 1);
        }
        else
        {
            superBar1.color = new Color(0, 0.2f, 0.2f, 1);
        }

        if (StoredInfoScript.characterInfo.player2Meter == StoredInfoScript.characterInfo.maxMeter)
        {
            superBar2.color = new Color(0, 1, 1, 1);
        }
        else if (StoredInfoScript.characterInfo.player2Meter / StoredInfoScript.characterInfo.maxMeter >= 0.75)
        {
            superBar2.color = new Color(0, 0.80f, 0.80f, 1);
        }
        else if (StoredInfoScript.characterInfo.player2Meter / StoredInfoScript.characterInfo.maxMeter >= 0.50)
        {
            superBar2.color = new Color(0, 0.60f, 0.60f, 1);
        }
        else if (StoredInfoScript.characterInfo.player2Meter / StoredInfoScript.characterInfo.maxMeter >= 0.25)
        {
            superBar2.color = new Color(0, 0.4f, 0.4f, 1);
        }
        else
        {
            superBar2.color = new Color(0, 0.2f, 0.2f, 1);
        }

        //Send the damages and hitstun values to each other
        player1.damageToRecieve = player2.damageToPass;
        player1.hitstunToRecieve = player2.hitstunToPass;
        player1.heightToRecieve = player2.heightToPass;
        player2.damageToRecieve = player1.damageToPass;
        player2.hitstunToRecieve = player1.hitstunToPass;
        player2.heightToRecieve = player1.heightToPass;

        player1.otherCombo = player2.currentCombo;
        player2.otherCombo = player1.currentCombo;

        if (!player1.juggleHitHandled)
        {
            player1.juggleSpeedToRecieve = player2.juggleSpeedToPass;
            player1.juggleHitHandled = true;
        }
        if (!player2.juggleHitHandled)
        {
            player2.juggleSpeedToRecieve = player1.juggleSpeedToPass;
            player2.juggleHitHandled = true;
        }

        //Update the direction for the use of throwing
        player1.throwDirectionToRecieve = player2.throwDirectionToSend;
        player2.throwDirectionToRecieve = player1.throwDirectionToSend;


        if (player1.backToTop)
        {
            player1.backToTop = false;
            player2.hitByBlockBox = false;
            player2.hitByAttackBox = false;
        }
        if (player2.backToTop)
        {
            player2.backToTop = false;
            player1.hitByBlockBox = false;
            player1.hitByAttackBox = false;
        }

        if (onLeft && player1.rb.transform.position.x < player2.rb.transform.position.x)
        {
            onLeft = false;
            player1.model.transform.localScale = new Vector3(-1f, 1f, 1f);
            player1.grabbox.transform.localPosition = new Vector3(-1 * player1.grabbox.transform.localPosition.x, player1.grabbox.transform.localPosition.y, 0);
            player1.blockbox.transform.localPosition = new Vector3(-1 * player1.blockbox.transform.localPosition.x, player1.blockbox.transform.localPosition.y, 0);
            player1.direction = -1;

            player2.model.transform.localScale = new Vector3(1f, 1f, 1f);
            player2.grabbox.transform.localPosition = new Vector3(-1 * player2.grabbox.transform.localPosition.x, player2.grabbox.transform.localPosition.y, 0);
            player2.blockbox.transform.localPosition = new Vector3(-1 * player2.blockbox.transform.localPosition.x, player2.blockbox.transform.localPosition.y, 0);
            player2.direction = 1;
        }
        else if(!onLeft && player1.rb.transform.position.x > player2.rb.transform.position.x)
        {
            onLeft = true;
            player1.model.transform.localScale = new Vector3(1f, 1f, 1f);
            player1.grabbox.transform.localPosition = new Vector3(-1 * player1.grabbox.transform.localPosition.x, player1.grabbox.transform.localPosition.y, 0);
            player1.blockbox.transform.localPosition = new Vector3(-1 * player1.blockbox.transform.localPosition.x, player1.blockbox.transform.localPosition.y, 0);
            player1.direction = 1;

            player2.model.transform.localScale = new Vector3(-1f, 1f, 1f);
            player2.grabbox.transform.localPosition = new Vector3(-1 * player2.grabbox.transform.localPosition.x, player2.grabbox.transform.localPosition.y, 0);
            player2.blockbox.transform.localPosition = new Vector3(-1 * player2.blockbox.transform.localPosition.x, player2.blockbox.transform.localPosition.y, 0);
            player2.direction = -1;
        }
	}
}
