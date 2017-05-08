using UnityEngine;
using System.Collections;

public class StoredInfoScript : MonoBehaviour {

    public static StoredInfoScript characterInfo;

    public int[] player1Characters;
    public GameObject[] player1CharacterGameObjects;
    //public int[] objectOrder1;
    public int[] player2Characters;
    public GameObject[] player2CharacterGameObjects;
    //public int[] objectOrder2;

    public GameObject[] stages = new GameObject[8];
    public int currentStage;
    public float[] stageBounds = new float[8]; //For Camera
    public float[] stageBoundsCharacter = new float[8];

    public float[] maxHealths = new float[28];

    public float[] player1Health;
    public float[] player2Health;

    public float maxMeter;
    public float player1Meter;
    public float player2Meter;
    public float meterIncrement;

    void Awake()
    {
        if(characterInfo == null)
        {
            DontDestroyOnLoad(gameObject);
            characterInfo = this;
        }
        else if (characterInfo != this)
        {
            Destroy(characterInfo);
        }
    }

    public void storeCharactersSelected(int p11, int p12, int p13, int p21, int p22, int p23)
    {
        player1Characters[0] = p11;
        player1Characters[1] = p12;
        player1Characters[2] = p13;
        player2Characters[0] = p21;
        player2Characters[1] = p22;
        player2Characters[2] = p23;
    }

    //1-6 are the options
    public void swapChar(bool isplayer1, bool isAssist1)
    {
        if(isplayer1)
        {
            int tempCurrent = player1Characters[0];
            float tempHealth = player1Health[0];
            //GameObject tempObj = player1CharacterGameObjects[0];

            if (isAssist1)
            {
                player1Characters[0] = player1Characters[1];
                player1Characters[1] = tempCurrent;
                player1Health[0] = player1Health[1];
                player1Health[1] = tempHealth;

                //player1CharacterGameObjects[0] = player1CharacterGameObjects[1];
                //player1CharacterGameObjects[1] = tempObj;
                //player1CharacterGameObjects[1].SetActive(false);
                //player1CharacterGameObjects[0].SetActive(true);
            }
            else
            {
                player1Characters[0] = player1Characters[2];
                player1Characters[2] = tempCurrent;
                player1Health[0] = player1Health[2];
                player1Health[2] = tempHealth;

                //player1CharacterGameObjects[0] = player1CharacterGameObjects[2];
                //player1CharacterGameObjects[2] = tempObj;
                //player1CharacterGameObjects[2].SetActive(false);
                //player1CharacterGameObjects[0].SetActive(true);
            }
        }
        else
        {
            int tempCurrent = player2Characters[0];
            float tempHealth = player2Health[0];
           // GameObject tempObj = player2CharacterGameObjects[0];

            if (isAssist1)
            {
                player2Characters[0] = player2Characters[1];
                player2Characters[1] = tempCurrent;
                player2Health[0] = player2Health[1];
                player2Health[1] = tempHealth;

                //player2CharacterGameObjects[0] = player2CharacterGameObjects[1];
                //player2CharacterGameObjects[1] = tempObj;
                //player2CharacterGameObjects[1].SetActive(false);
                //player2CharacterGameObjects[0].SetActive(true);
            }
            else
            {
                player2Characters[0] = player2Characters[2];
                player2Characters[2] = tempCurrent;
                player2Health[0] = player2Health[2];
                player2Health[2] = tempHealth;

                //player2CharacterGameObjects[0] = player2CharacterGameObjects[2];
                //player2CharacterGameObjects[2] = tempObj;
                //player2CharacterGameObjects[2].SetActive(false);
                //player2CharacterGameObjects[0].SetActive(true);
            }
        }
    }

	// Use this for initialization
	void Start ()
    {
        player1Characters = new int[3];
        player2Characters = new int[3];

        player1Health = new float[3];
        player1Health[0] = 1f;
        player1Health[1] = 1f;
        player1Health[2] = 1f;

        player2Health = new float[3];
        player2Health[0] = 1f;
        player2Health[1] = 1f;
        player2Health[2] = 1f;

        player1CharacterGameObjects = new GameObject[3];
        player2CharacterGameObjects = new GameObject[3];

        maxMeter = 400;
        player1Meter = 100;
        player2Meter = 100;
        meterIncrement = 10;
        //maxHealths = new float[28];
        
        currentStage = 0;
    }
}
