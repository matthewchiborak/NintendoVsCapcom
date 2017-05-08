using UnityEngine;
using System.Collections;

public abstract class CharacterScript : MonoBehaviour {

    public bool isPlayer1;

    public GameControllerScript gameController;

    public GameObject exEffect;
    public GameObject switchEffect;

    public Animator anim;
    public Rigidbody rb;

    public AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip swingSound;
    public AudioClip blockSound;


    protected float inputH;
    protected float inputH2;
    protected bool crouch;
    protected bool jump;

    public int recovery;
    protected int airAttackRecovery;

    public int direction; //Positive = looksing right (on left).

    //public GameObject hadouken;

    public GameObject characterbox;
    public GameObject throwbox;
    public GameObject hitbox;
    public GameObject attackbox;
    public GameObject grabbox;
    public GameObject blockbox;
    public GameObject model;

    public bool backToTop;

    public float speed;
    public float jumpSpeed;
    public float jumpSideSpeed;
    //public float shoryukenJumpSpeed;
    public float gravity;
    public float maxHeight;
    public Vector3 v0;
    public float startingTime;
    protected char specialType;

    protected int activeFrame;
    protected int deactiveFrame;

    public float maxHealth;
    public float currentHealth;
    public float maxMeter;
    public float currentMeter;

    /*bool lightPressed;
    bool mediumPressed;
    bool heavyPressed;
    bool launchPressed;*/

    public bool hitByBlockBox;
    public bool blocking;
    public bool hitByAttackBox;
    public bool hit;
    public int hitstunToPass;
    public float damageToPass;
    public int hitstunToRecieve;
    public float damageToRecieve;
    public char heightToPass;
    public char heightToRecieve;

    public bool juggleToPass;
    public bool juggleToRecieve;

    public string characterName;

    public bool juggle;
    public bool backDisabled;
    public float lastJumpTime;

    public float projectileDamageToRecieve;
    public int projectileHitStunToRecieve;
    public bool hitByProjectile;

    public float knockback;

    public bool hitByGrabBox;
    public bool throwDirectionToRecieve;
    public bool throwDirectionToSend;
    public bool throwSuccessful;
    public bool localThrowDirection;
    public float throwSpeed;

    //7 8 9
    //4 5 6
    //1 2 3
    public int lastDirection1; //Most recent direction
    public int lastDirection2;
    public int lastDirection3;
    public int lastDirection4;

    public float tempx;
    public float tempy;
    public int frameSinceLastDirectionChange;

    public int currentCombo;

    public bool paused;

    public bool isDead;
    public bool alreadyDied;
    public Vector3 deathScale;

    //Bools to ensure single button presses
    private bool lightDown = false;
    private bool mediumDown = false;
    private bool heavyDown = false;
    private bool specialDown = false;

    //private float juggleSpeed = 60.0f;
    public float juggleSpeedToPass;
    public float juggleSpeedToRecieve;
    public bool juggleHitHandled;

    //Make it so you can't use the launcher over and over again to make an infinite combo. (Only use once per combo)
    private bool launcherUsed;
    public int otherCombo;
    

    // Use this for initialization
    /*void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}*/

    //Variable and functions for the character's specific attacks. Will be redifined in by the classes inheriting from this
    public abstract void standLight();
    public abstract void standMedium();
    public abstract void standHeavy();
    public abstract void crouchLight();
    public abstract void crouchMedium();
    public abstract void crouchHeavy();
    public abstract void airLight();
    public abstract void airMedium();
    public abstract void airHeavy();
    public abstract void launcher();

    public abstract void crouchChanges();
    public abstract void standChanges();

    //See line 370
    public abstract void special1Ex();
    public abstract void special2Ex();
    public abstract void special3Ex();
    public abstract void special1();
    public abstract void special2();
    public abstract void special3();
    public abstract void super();

    //Special move additional effect aka movement etc
    public abstract void special1Effects();
    public abstract void special2Effects();
    public abstract void special3Effects();
    public abstract void superEffects();

    public abstract bool special11InputCheck();
    public abstract bool special12InputCheck();
    public abstract bool special13InputCheck();
    public abstract bool superInputCheck();

    public abstract void characterSpecificInit();
    public abstract Vector3 getDeathScale();

    public abstract void alwaysCheck();


    public void cameraPush()
    {
        rb.transform.position = new Vector3(rb.transform.position.x - (direction), rb.transform.position.y, rb.transform.position.z);
    }

    public void playerPush()
    {
        rb.transform.position = new Vector3(rb.transform.position.x + (direction), rb.transform.position.y, rb.transform.position.z);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        crouch = false;
        jump = false;
        gravity = -360.0F;
        jumpSpeed = 120.0F;
        jumpSideSpeed = 25.0f;

        maxHeight = 60.0f;
        v0 = Vector3.zero;
        startingTime = 0;
        airAttackRecovery = 0;
        specialType = 'N';
        activeFrame = 0;
        deactiveFrame = 0;
        hitByBlockBox = false;
        blocking = false;
        backToTop = false;
        backDisabled = false;

        damageToRecieve = 0;
        hitstunToRecieve = 0;
        heightToRecieve = 'm';

        juggle = false;
        juggleToPass = false;
        juggleToRecieve = false;
        lastJumpTime = 0;

        
        maxMeter = 400;
        currentMeter = 100;

        projectileDamageToRecieve = 0;
        projectileHitStunToRecieve = 0;
        hitByProjectile = false;

        knockback = 1;

        hitByGrabBox = false;
        localThrowDirection = true;
        throwSuccessful = false;
        throwSpeed = 120f;

        lastDirection1 = 5;
        lastDirection2 = 5;
        lastDirection3 = 5;
        lastDirection4 = 5;

        frameSinceLastDirectionChange = 0;

        currentCombo = 0;

        paused = false;
        isDead = false;
        alreadyDied = false;
        deathScale = Vector3.zero;

        juggleSpeedToPass = 120f;
        juggleSpeedToRecieve = 120f;
        juggleHitHandled = true;

        launcherUsed = false;
        otherCombo = 0;

        Instantiate(switchEffect, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        characterSpecificInit();
    }

    // Update is called once per frame
    void Update()
    {
        /* if (isPlayer1)
         {
             lightPressed = Input.GetKey()
             mediumPressed;
             heavyPressed;
             launchPressed;
         }*/
        //Set up juggle
        /*if (juggleToRecieve && hitByAttackBox)
        {
            juggleToRecieve = false;
            jump = true;
            hitByAttackBox = true;
            recovery = 10000;
        }*/

        alwaysCheck();

        //Reset the launcher
        if(otherCombo <= 0)
        {
            launcherUsed = false;
        }

        //Reset button presses
        if(isPlayer1)
        {
            if(Input.GetAxisRaw("Light1") == 0)
            {
                lightDown = false;
            }
            if (Input.GetAxisRaw("Medium1") == 0)
            {
                mediumDown = false;
            }
            if (Input.GetAxisRaw("Heavy1") == 0)
            {
                heavyDown = false;
            }
            if (Input.GetAxisRaw("Special1") == 0)
            {
                specialDown = false;
            }
        }
        else
        {
            if (Input.GetAxisRaw("Light2") == 0)
            {
                lightDown = false;
            }
            if (Input.GetAxisRaw("Medium2") == 0)
            {
                mediumDown = false;
            }
            if (Input.GetAxisRaw("Heavy2") == 0)
            {
                heavyDown = false;
            }
            if (Input.GetAxisRaw("Special2") == 0)
            {
                specialDown = false;
            }
        }

        //Prevent falling through the floor
        if(transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }

        //Prevent from going offscreen
        if (transform.position.x > StoredInfoScript.characterInfo.stageBoundsCharacter[StoredInfoScript.characterInfo.currentStage])
        {
            transform.position = new Vector3(StoredInfoScript.characterInfo.stageBoundsCharacter[StoredInfoScript.characterInfo.currentStage], transform.position.y, 0);
        }
        if (transform.position.x < -1 * StoredInfoScript.characterInfo.stageBoundsCharacter[StoredInfoScript.characterInfo.currentStage])
        {
            transform.position = new Vector3(-1 * StoredInfoScript.characterInfo.stageBoundsCharacter[StoredInfoScript.characterInfo.currentStage], transform.position.y, 0);
        }

        //Dead if character is dead
        //if(currentHealth <= 0)
        if ((isPlayer1 && StoredInfoScript.characterInfo.player1Health[0] <=0) || (!isPlayer1 && StoredInfoScript.characterInfo.player2Health[0] <= 0))
        {
            /*if (!isDead && !jump)
            {
                isDead = true;
                anim.Play("Armature|HardDown", -1, 0f);
                v0 = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
                deathScale = new Vector3(model.transform.localScale.x, model.transform.localScale.y, model.transform.localScale.z);
                anim.Play("Armature|HardDown", -1, 0f);
                //anim.SetBool("isDead", isDead);
            }
            else if(!jump)
            {
                transform.position = v0;
                transform.localScale = deathScale;
            }
            return;*/

            if(!jump)
            {
                if (!isDead)
                {
                    isDead = true;
                    anim.Play("Armature|HardDown", -1, 0f);
                    v0 = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
                    //deathScale = new Vector3(direction * model.transform.localScale.x, model.transform.localScale.y, model.transform.localScale.z);
                    deathScale = getDeathScale();
                    anim.Play("Armature|HardDown", -1, 0f);
                    //anim.SetBool("isDead", isDead);
                }

                transform.localPosition = v0;
                transform.localScale = deathScale;
                return;
            }
        }

        if (!paused && !isDead)
        {

            //Check for changes in the movement input direction for special moves
            if (isPlayer1)
            {
                tempx = Input.GetAxisRaw("Horizontal");
                tempy = Input.GetAxisRaw("Vertical");
            }
            else
            {
                tempx = Input.GetAxisRaw("Horizontal2");
                tempy = Input.GetAxisRaw("Vertical2");
            }

            //Account for if on right side
            if (direction < 0)
            {
                tempx *= -1;
            }

            bool newDirection = false;

            switch (lastDirection1)
            {
                case 1:
                    if (tempx != -1 || tempy != -1)
                    {
                        newDirection = true;
                    }
                    break;
                case 2:
                    if (tempx != 0 || tempy != -1)
                    {
                        newDirection = true;
                    }
                    break;
                case 3:
                    if (tempx != 1 || tempy != -1)
                    {
                        newDirection = true;
                    }
                    break;
                case 4:
                    if (tempx != -1 || tempy != 0)
                    {
                        newDirection = true;
                    }
                    break;
                case 5:
                    if (tempx != 0 || tempy != 0)
                    {
                        newDirection = true;
                    }
                    break;
                case 6:
                    if (tempx != 1 || tempy != 0)
                    {
                        newDirection = true;
                    }
                    break;
                case 7:
                    if (tempx != -1 || tempy != 1)
                    {
                        newDirection = true;
                    }
                    break;
                case 8:
                    if (tempx != 0 || tempy != 1)
                    {
                        newDirection = true;
                    }
                    break;
                case 9:
                    if (tempx != 1 || tempy != 1)
                    {
                        newDirection = true;
                    }
                    break;
            }

            //Set the new direction
            if (newDirection)
            {
                lastDirection4 = lastDirection3;
                lastDirection3 = lastDirection2;
                lastDirection2 = lastDirection1;
                frameSinceLastDirectionChange = 0;

                if (tempx == -1 && tempy == -1)
                {
                    lastDirection1 = 1;
                }
                else if (tempx == 0 && tempy == -1)
                {
                    lastDirection1 = 2;
                }
                else if (tempx == 1 && tempy == -1)
                {
                    lastDirection1 = 3;
                }
                else if (tempx == -1 && tempy == 0)
                {
                    lastDirection1 = 4;
                }
                else if (tempx == 0 && tempy == 0)
                {
                    lastDirection1 = 5;
                }
                else if (tempx == 1 && tempy == 0)
                {
                    lastDirection1 = 6;
                }
                else if (tempx == -1 && tempy == 1)
                {
                    lastDirection1 = 7;
                }
                else if (tempx == 0 && tempy == 1)
                {
                    lastDirection1 = 8;
                }
                else if (tempx == 1 && tempy == 1)
                {
                    lastDirection1 = 9;
                }
            }
            else
            {
                frameSinceLastDirectionChange++;
            }

            //If not change direction recently for speical moves
            if (frameSinceLastDirectionChange > 10)
            {
                lastDirection1 = 5;
                lastDirection2 = 5;
                lastDirection3 = 5;
                frameSinceLastDirectionChange = 0;
            }


            //Check for over and underflow of current stats
            //if (currentHealth < 0)
            //{
            //    currentHealth = 0;
            //}
            //if (currentHealth > maxHealth)
            //{
            //    currentHealth = maxHealth;
            //}
            if (isPlayer1)
            {
                if (StoredInfoScript.characterInfo.player1Health[0] < 0)
                {
                    StoredInfoScript.characterInfo.player1Health[0] = 0;
                }
                if (StoredInfoScript.characterInfo.player1Health[0] > 1)
                {
                    StoredInfoScript.characterInfo.player1Health[0] = 1;
                }
                if (StoredInfoScript.characterInfo.player1Meter < 0)
                {
                    StoredInfoScript.characterInfo.player1Meter = 0;
                }
                if (StoredInfoScript.characterInfo.player1Meter > StoredInfoScript.characterInfo.maxMeter)
                {
                    StoredInfoScript.characterInfo.player1Meter = StoredInfoScript.characterInfo.maxMeter;
                }
                //if (currentMeter < 0)
                //{
                //    currentMeter = 0;
                //}
                //if (currentMeter > maxMeter)
                //{
                //    currentMeter = maxMeter;
                //}
            }
            else
            {
                if (StoredInfoScript.characterInfo.player2Health[0] < 0)
                {
                    StoredInfoScript.characterInfo.player2Health[0] = 0;
                }
                if (StoredInfoScript.characterInfo.player2Health[0] > 1)
                {
                    StoredInfoScript.characterInfo.player2Health[0] = 1;
                }
                if (StoredInfoScript.characterInfo.player2Meter < 0)
                {
                    StoredInfoScript.characterInfo.player2Meter = 0;
                }
                if (StoredInfoScript.characterInfo.player2Meter > StoredInfoScript.characterInfo.maxMeter)
                {
                    StoredInfoScript.characterInfo.player2Meter = StoredInfoScript.characterInfo.maxMeter;
                }
            }


            if (recovery <= 0)
            {
                currentCombo = 0;
                backToTop = true;
                hit = false;
                juggle = false;
                anim.SetBool("juggle", juggle);
                anim.SetBool("hit", hit);
                specialType = 'N';
                blockbox.SetActive(false);
                attackbox.SetActive(false);
                throwSuccessful = false;
                hitByGrabBox = false;
                rb.transform.localPosition = new Vector3(rb.transform.localPosition.x, 0, 0);
                rb.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

                if (hitByProjectile)
                {
                    hitByAttackBox = true;
                }

                //if ((isPlayer1 && Input.GetKey(KeyCode.S) && !jump) || (!isPlayer1 && Input.GetKey(KeyCode.DownArrow) && !jump))
                if ((isPlayer1 && Input.GetAxisRaw("Vertical") == -1 && !jump) || (!isPlayer1 && Input.GetAxisRaw("Vertical2") == -1 && !jump))
                {
                    crouch = true;
                    /*characterbox.transform.localPosition = new Vector3(-1.15f, 5.70f, 0);
                    characterbox.transform.localScale = new Vector3(6f, 10.6f, 5f);

                    hitbox.transform.localPosition = new Vector3(-0.88f, 6.02f, 0);
                    hitbox.transform.localScale = new Vector3(10f, 13.4f, 5f);*/
                    crouchChanges();
                }
                else
                {
                    crouch = false;
                    /*characterbox.transform.localPosition = new Vector3(0, 8.95f, 0);
                    characterbox.transform.localScale = new Vector3(6f, 14.65f, 5f);

                    hitbox.transform.localPosition = new Vector3(0f, 9.19f, 0);
                    hitbox.transform.localScale = new Vector3(10f, 18.01f, 5f);*/
                    standChanges();
                }
                //if ((isPlayer1 && Input.GetKey(KeyCode.W)) || (!isPlayer1 && Input.GetKey(KeyCode.UpArrow)))
                if ((isPlayer1 && Input.GetAxisRaw("Vertical") == 1) || (!isPlayer1 && Input.GetAxisRaw("Vertical2") == 1))
                {
                    jump = true;
                    anim.SetBool("jump", jump);
                    //Universal jump recovery. Change this to a abstract function if want to change it in future
                    recovery = 40;
                    v0 = transform.position;
                    startingTime = Time.time;
                    inputH = Input.GetAxisRaw("Horizontal");
                    inputH2 = Input.GetAxisRaw("Horizontal2");
                    throwbox.SetActive(false);
                    return;
                }
                else
                {
                    jump = false;
                    throwbox.SetActive(true);
                    rb.position = new Vector3(rb.position.x, 0, rb.position.z);
                }

                anim.SetBool("jump", jump);
                anim.SetBool("crouch", crouch);

                inputH = Input.GetAxisRaw("Horizontal");
                inputH2 = Input.GetAxisRaw("Horizontal2");
                if (isPlayer1)
                {
                    anim.SetFloat("inputH", inputH);
                }
                else
                {
                    anim.SetFloat("inputH", inputH2);
                }

                if (hitByAttackBox)
                {
                    if (isPlayer1)
                    {
                        if ((inputH * direction < 0 && crouch && heightToRecieve != 'h') || (inputH * direction < 0 && !crouch && heightToRecieve != 'l'))
                        {
                            //Check if its a projectile
                            if (projectileDamageToRecieve != 0)
                            {
                                //Chip damage
                                projectileDamageToRecieve = 0;
                                projectileHitStunToRecieve = 0;
                                hitByProjectile = false;
                            }

                            //The player should be blocking
                            blocking = true;
                            anim.SetBool("blocking", blocking);
                            
                            
                            audioSource.clip = blockSound;
                            audioSource.Play();
                            

                            //hitByBlockBox = false;
                            rb.velocity = new Vector3(0, 0, 0);

                            //Play animation

                            return;
                        }
                        else
                        {
                            //Take damage and hit stun
                            activeFrame = -1;
                            deactiveFrame = -1;

                            if (!backDisabled)
                                rb.transform.position = new Vector3(rb.transform.position.x + knockback * direction, rb.transform.position.y, rb.transform.position.z);

                            currentCombo++;

                            //If launcher
                            if (hitstunToRecieve >= 1000 || projectileHitStunToRecieve >=1000)
                            {
                                recovery = 1000;
                                airAttackRecovery = 1000;
                                jump = true;
                                v0 = rb.transform.position;
                                startingTime = Time.time;
                                projectileHitStunToRecieve = 0;
                                return;
                            }

                            hitByAttackBox = false;
                            hitByProjectile = false;
                            hit = true;

                            activeFrame = -1;
                            deactiveFrame = -1;

                            if (projectileDamageToRecieve == 0)
                            {
                                //currentHealth -= damageToRecieve;
                                if(isPlayer1)
                                {
                                    StoredInfoScript.characterInfo.player1Health[0] -= (damageToRecieve / StoredInfoScript.characterInfo.maxHealths[StoredInfoScript.characterInfo.player1Characters[0]]);
                                }
                                else
                                {
                                    StoredInfoScript.characterInfo.player2Health[0] -= (damageToRecieve / StoredInfoScript.characterInfo.maxHealths[StoredInfoScript.characterInfo.player2Characters[0]]);
                                }
                            }
                            else
                            {
                                //currentHealth -= projectileDamageToRecieve;
                                if (isPlayer1)
                                {
                                    StoredInfoScript.characterInfo.player1Health[0] -= (projectileDamageToRecieve / StoredInfoScript.characterInfo.maxHealths[StoredInfoScript.characterInfo.player1Characters[0]]);
                                }
                                else
                                {
                                    StoredInfoScript.characterInfo.player2Health[0] -= (projectileDamageToRecieve / StoredInfoScript.characterInfo.maxHealths[StoredInfoScript.characterInfo.player2Characters[0]]);
                                }

                                projectileDamageToRecieve = 0;
                                hitByProjectile = false;
                            }
                            audioSource.clip = hitSound;
                            audioSource.Play();

                            //if (currentHealth < 0)
                            //{
                            //    currentHealth = 0;
                            //}
                            if (isPlayer1)
                            {
                                if(StoredInfoScript.characterInfo.player1Health[0] < 0)
                                StoredInfoScript.characterInfo.player1Health[0] = 0;
                            }
                            else
                            {
                                if (StoredInfoScript.characterInfo.player2Health[0] < 0)
                                    StoredInfoScript.characterInfo.player2Health[0] = 0;
                            }

                            if (projectileHitStunToRecieve == 0)
                            {
                                recovery = hitstunToRecieve;
                            }
                            else
                            {
                                recovery = projectileHitStunToRecieve;
                                projectileHitStunToRecieve = 0;
                            }

                            anim.SetBool("hit", hit);

                            //hitByBlockBox = false;
                            rb.velocity = new Vector3(0, 0, 0);

                            //Play animation
                            if (heightToRecieve != 'l')
                            {
                                anim.Play("Armature|StandHit", -1, 0f);
                            }
                            else
                            {
                                anim.Play("Armature|CrouchHit", -1, 0f);
                            }

                            return;
                        }
                    }
                    else
                    {
                        if ((inputH2 * direction < 0 && crouch && heightToRecieve != 'h') || (inputH2 * direction < 0 && !crouch && heightToRecieve != 'l'))
                        {

                            //Check if its a projectile
                            if (projectileDamageToRecieve != 0)
                            {
                                //Chip damage
                                projectileDamageToRecieve = 0;
                                projectileHitStunToRecieve = 0;
                                hitByProjectile = false;
                            }

                            blocking = true;
                            anim.SetBool("blocking", blocking);
                            audioSource.clip = blockSound;
                            audioSource.Play();
                            //hitByBlockBox = false;
                            rb.velocity = new Vector3(0, 0, 0);
                           
                            //Play animation

                            return;
                        }
                        else
                        {
                            //Take damage and hit stun
                            activeFrame = -1;
                            deactiveFrame = -1;

                            if (!backDisabled)
                                rb.transform.position = new Vector3(rb.transform.position.x + knockback * direction, rb.transform.position.y, rb.transform.position.z);

                            currentCombo++;

                            //If launcher
                            if (hitstunToRecieve >= 1000 || projectileHitStunToRecieve >= 1000)
                            {
                                recovery = 1000;
                                airAttackRecovery = 1000;
                                jump = true;
                                v0 = rb.transform.position;
                                startingTime = Time.time;
                                projectileHitStunToRecieve = 0;
                                return;
                            }

                            hitByAttackBox = false;
                            hitByProjectile = false;
                            hit = true;

                            activeFrame = -1;
                            deactiveFrame = -1;

                            if (projectileDamageToRecieve == 0)
                            {
                                //currentHealth -= damageToRecieve;
                                if (isPlayer1)
                                {
                                    StoredInfoScript.characterInfo.player1Health[0] -= (damageToRecieve / StoredInfoScript.characterInfo.maxHealths[StoredInfoScript.characterInfo.player1Characters[0]]);
                                }
                                else
                                {
                                    StoredInfoScript.characterInfo.player2Health[0] -= (damageToRecieve / StoredInfoScript.characterInfo.maxHealths[StoredInfoScript.characterInfo.player2Characters[0]]);
                                }
                            }
                            else
                            {
                                //currentHealth -= projectileDamageToRecieve;
                                if (isPlayer1)
                                {
                                    StoredInfoScript.characterInfo.player1Health[0] -= (projectileDamageToRecieve / StoredInfoScript.characterInfo.maxHealths[StoredInfoScript.characterInfo.player1Characters[0]]);
                                }
                                else
                                {
                                    StoredInfoScript.characterInfo.player2Health[0] -= (projectileDamageToRecieve / StoredInfoScript.characterInfo.maxHealths[StoredInfoScript.characterInfo.player2Characters[0]]);
                                }

                                projectileDamageToRecieve = 0;
                                hitByProjectile = false;
                            }
                            audioSource.clip = hitSound;
                            audioSource.Play();

                            //if (currentHealth < 0)
                            //{
                            //    currentHealth = 0;
                            //}
                            if (isPlayer1)
                            {
                                if (StoredInfoScript.characterInfo.player1Health[0] < 0)
                                    StoredInfoScript.characterInfo.player1Health[0] = 0;
                            }
                            else
                            {
                                if (StoredInfoScript.characterInfo.player2Health[0] < 0)
                                    StoredInfoScript.characterInfo.player2Health[0] = 0;
                            }


                            if (projectileHitStunToRecieve == 0)
                            {
                                recovery = hitstunToRecieve;
                            }
                            else
                            {
                                recovery = projectileHitStunToRecieve;
                                projectileHitStunToRecieve = 0;
                            }


                            anim.SetBool("hit", hit);

                            //hitByBlockBox = false;
                            rb.velocity = new Vector3(0, 0, 0);

                            //Play animation
                            if (heightToRecieve != 'l')
                            {
                                anim.Play("Armature|StandHit", -1, 0f);
                            }
                            else
                            {
                                anim.Play("Armature|CrouchHit", -1, 0f);
                            }

                            return;
                        }
                    }
                }

                if (hitByBlockBox)
                {
                    if (isPlayer1)
                    {
                        if (inputH * direction < 0)
                        {
                            //The player should be blocking
                            blocking = true;
                            anim.SetBool("blocking", blocking);

                            //hitByBlockBox = false;
                            rb.velocity = new Vector3(0, 0, 0);

                            //Play animation

                            return;
                        }
                    }
                    else
                    {
                        if (inputH2 * direction < 0)
                        {
                            blocking = true;
                            anim.SetBool("blocking", blocking);
                            //hitByBlockBox = false;
                            rb.velocity = new Vector3(0, 0, 0);
                           
                            //Play animation

                            return;
                        }
                    }
                }
                else //Maybe this the problem with animation?
                {
                    blocking = false;
                    anim.SetBool("blocking", blocking);
                }

                if (!crouch)
                {
                    //float moveX = -1 * inputH * speed * Time.deltaTime;
                    float moveX = 0;

                    if (isPlayer1)
                    {
                        //Make sure dont walk off camera
                        if (inputH * direction < 0 && backDisabled)
                        {
                            moveX = 0 * Time.deltaTime;
                        }
                        else
                        {
                            moveX = -1 * inputH * speed * Time.deltaTime;
                        }
                    }
                    else
                    {
                        //Make sure dont walk off camera
                        if (inputH2 * direction < 0 && backDisabled)
                        {
                            moveX = 0 * Time.deltaTime;
                        }
                        else
                        {
                            moveX = -1 * inputH2 * speed * Time.deltaTime;
                        }

                        //moveX = -1 * inputH2 * speed * Time.deltaTime;
                    }


                    rb.velocity = new Vector3(moveX, 0, 0);

                }
                else
                {
                    rb.velocity = new Vector3(0, 0, 0);
                }

                //Dashing
                if ((!crouch && isPlayer1 && (lastDirection1 == 6 && lastDirection2 == 5 && lastDirection3 == 6 || lastDirection1 == 4 && lastDirection2 == 5 && lastDirection3 == 4) && inputH != 0)
                    || (!crouch && !isPlayer1 && (lastDirection1 == 6 && lastDirection2 == 5 && lastDirection3 == 6 || lastDirection1 == 4 && lastDirection2 == 5 && lastDirection3 == 4) && inputH2 != 0))
                {
                    //Prevent repeat
                    lastDirection1 = 5;
                    lastDirection2 = 5;
                    lastDirection3 = 5;

                    //Universal Dashing recovery. Abstract this if you want different values
                    recovery = 10;
                    specialType = 'D';
                    return;
                }

                //Attacks
                //Super
                //if ((isPlayer1 && StoredInfoScript.characterInfo.player1Meter >= 400 && (Input.GetKeyDown(KeyCode.K) && Input.GetKeyDown(KeyCode.L)) && superInputCheck())
                //   || (!isPlayer1 && StoredInfoScript.characterInfo.player2Meter >= 400 && (Input.GetKeyDown(KeyCode.B) && Input.GetKeyDown(KeyCode.N)) && superInputCheck()))
                if ((isPlayer1 && StoredInfoScript.characterInfo.player1Meter >= 400 && (Input.GetAxisRaw("Medium1") == 1 && !mediumDown && Input.GetAxisRaw("Heavy1") == 1 && !heavyDown) && superInputCheck())
                   || (!isPlayer1 && StoredInfoScript.characterInfo.player2Meter >= 400 && (Input.GetAxisRaw("Medium2") == 1 && !mediumDown && Input.GetAxisRaw("Heavy2") == 1 && !heavyDown) && superInputCheck()))
                {
                    heavyDown = true;
                    mediumDown = true;

                    if (isPlayer1)
                    {
                        StoredInfoScript.characterInfo.player1Meter = 0;
                    }
                    else
                    {
                        StoredInfoScript.characterInfo.player2Meter = 0;
                    }
                    super();

                    //gameController.zoomInstructions(transform.position, transform.position);
                    if(direction > 0) //On Left
                    {
                        Vector3 zoomPoint = new Vector3(transform.position.x - 33.0f, 14.0f, 27.55f);
                        gameController.zoomInstructions(zoomPoint, transform.position, true, 5.0f);
                    }
                    else //On Right
                    {
                        
                        Vector3 zoomPoint = new Vector3(transform.position.x + 33.0f, 14.0f, 27.55f);
                        gameController.zoomInstructions(zoomPoint, transform.position, false, 5.0f);
                    }
                    

                   /* currentMeter = 0;

                    //Prevent repeat
                    lastDirection1 = 5;
                    lastDirection2 = 5;
                    lastDirection3 = 5;

                    rb.velocity = new Vector3(0, 0, 0);
                    recovery = 420;
                    specialType = 'U';
                    activeFrame = 283;
                    deactiveFrame = 280;

                    backToTop = false;

                    //If mis swithc to regular shoryuken

                    attackbox.transform.localPosition = new Vector3(direction * -7.25f, 12.72f, 0);
                    attackbox.transform.localScale = new Vector3(5f, 4f, 5f);

                    anim.Play("Armature|Super", -1, 0f);*/
                }
                //else if ((isPlayer1 && StoredInfoScript.characterInfo.player1Meter >= 100 && (Input.GetKeyDown(KeyCode.Semicolon)) && special11InputCheck())
                //   || (!isPlayer1 && StoredInfoScript.characterInfo.player2Meter >= 100 && (Input.GetKeyDown(KeyCode.M)) && special11InputCheck()))
                else if ((isPlayer1 && StoredInfoScript.characterInfo.player1Meter >= 100 && (Input.GetAxisRaw("Special1") == 1 && !specialDown) && special11InputCheck())
                   || (!isPlayer1 && StoredInfoScript.characterInfo.player2Meter >= 100 && (Input.GetAxisRaw("Special2") == 1 && !specialDown) && special11InputCheck()))
                {
                    specialDown = true;

                    if (isPlayer1)
                    {
                        StoredInfoScript.characterInfo.player1Meter -= 100;
                    }
                    else
                    {
                        StoredInfoScript.characterInfo.player2Meter -= 100;
                    }

                    Instantiate(exEffect, new Vector3((float)(transform.position.x), (float)(transform.position.y), 0), Quaternion.identity);
                    special1Ex();
                    ////Burn meter
                    //currentMeter -= 100;

                    ////Prevent repeat
                    //lastDirection1 = 5;
                    //lastDirection2 = 5;
                    //lastDirection3 = 5;

                    //damageToPass = 50;
                    //hitstunToPass = 53;
                    //heightToPass = 'm';

                    //rb.velocity = new Vector3(0, 0, 0);
                    //recovery = 33;
                    //specialType = 'S';
                    //anim.Play("Armature|Shoryuken", -1, 0f);
                    //v0 = transform.position;
                    //startingTime = Time.time;
                    //activeFrame = 29;
                    //deactiveFrame = 15;

                    //backToTop = false;

                    ////currentMeter += 10;

                    //blockbox.SetActive(true);
                    //attackbox.transform.localPosition = new Vector3(direction * -5.85f, 15.11f, 0);
                    //attackbox.transform.localScale = new Vector3(5.98f, 17.72f, 5f);
                    //inputH = Input.GetAxisRaw("Horizontal");
                }
                //else if ((isPlayer1 && StoredInfoScript.characterInfo.player1Meter >= 100 && (Input.GetKeyDown(KeyCode.Semicolon)) && special12InputCheck())
                //    || (!isPlayer1 && StoredInfoScript.characterInfo.player2Meter >= 100 && (Input.GetKeyDown(KeyCode.M)) && special12InputCheck()))
                else if ((isPlayer1 && StoredInfoScript.characterInfo.player1Meter >= 100 && (Input.GetAxisRaw("Special1") == 1 && !specialDown) && special12InputCheck())
                   || (!isPlayer1 && StoredInfoScript.characterInfo.player2Meter >= 100 && (Input.GetAxisRaw("Special2") == 1 && !specialDown) && special12InputCheck()))
                {
                    specialDown = true;

                    if (isPlayer1)
                    {
                        StoredInfoScript.characterInfo.player1Meter -= 100;
                    }
                    else
                    {
                        StoredInfoScript.characterInfo.player2Meter -= 100;
                    }
                    Instantiate(exEffect, new Vector3((float)(transform.position.x), (float)(transform.position.y), 0), Quaternion.identity);
                    special2Ex();
                    //currentMeter -= 100;

                    ////Prevent repeat
                    //lastDirection1 = 5;
                    //lastDirection2 = 5;
                    //lastDirection3 = 5;

                    //damageToPass = 50;
                    //hitstunToPass = 53;
                    //heightToPass = 'm';

                    //rb.velocity = new Vector3(0, 0, 0);
                    //recovery = 53;
                    //specialType = 'T';
                    //anim.Play("Armature|Tatsu", -1, 0f);
                    //v0 = transform.position;
                    //startingTime = Time.time;
                    //activeFrame = 40;
                    //deactiveFrame = 21;

                    //backToTop = false;

                    ////currentMeter += 10;

                    //blockbox.SetActive(true);
                    //attackbox.transform.localPosition = new Vector3(0, 9.73f, 0);
                    //attackbox.transform.localScale = new Vector3(23.9f, 5.5f, 5f);
                    ////inputH = Input.GetAxisRaw("Horizontal");
                }
                //else if ((isPlayer1 && StoredInfoScript.characterInfo.player1Meter >= 100 && (Input.GetKeyDown(KeyCode.Semicolon)) && special13InputCheck())
                //    || (!isPlayer1 && StoredInfoScript.characterInfo.player2Meter >= 100 && (Input.GetKeyDown(KeyCode.M)) && special13InputCheck()))
                else if ((isPlayer1 && StoredInfoScript.characterInfo.player1Meter >= 100 && (Input.GetAxisRaw("Special1") == 1 && !specialDown) && special13InputCheck())
                    || (!isPlayer1 && StoredInfoScript.characterInfo.player2Meter >= 100 && (Input.GetAxisRaw("Special2") == 1 & !specialDown) && special13InputCheck()))
                {
                    specialDown = true;

                    if (isPlayer1)
                    {
                        StoredInfoScript.characterInfo.player1Meter -= 100;
                    }
                    else
                    {
                        StoredInfoScript.characterInfo.player2Meter -= 100;
                    }
                    Instantiate(exEffect, new Vector3((float)(transform.position.x), (float)(transform.position.y), 0), Quaternion.identity);
                    special3Ex();
                    //currentMeter -= 100;

                    ////Prevent repeat
                    //lastDirection1 = 5;
                    //lastDirection2 = 5;
                    //lastDirection3 = 5;

                    //rb.velocity = new Vector3(0, 0, 0);
                    //recovery = 45;
                    //specialType = 'H';
                    //activeFrame = -1;
                    //deactiveFrame = -1;
                    ////Move attack box

                    //backToTop = false;

                    ////currentMeter += 10;

                    ////TODO Blocking? Probably do it in the hadouken itself

                    //anim.Play("Armature|HadoukenLight", -1, 0f);
                }
                //else if ((isPlayer1 && (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L)) && special11InputCheck())
                //    || (!isPlayer1 && (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.N)) && special11InputCheck()))
                else if ((isPlayer1 && ((Input.GetAxisRaw("Light1") == 1 && !lightDown) || (Input.GetAxisRaw("Medium1") == 1 && !mediumDown) || (Input.GetAxisRaw("Heavy1") == 1 && !heavyDown)) && special11InputCheck())
                    || (!isPlayer1 && ((Input.GetAxisRaw("Light2") == 1 && !lightDown) || (Input.GetAxisRaw("Medium2") == 1 && !mediumDown) || (Input.GetAxisRaw("Heavy2") == 1 && !heavyDown)) && special11InputCheck()))
                {
                    lightDown = true;
                    mediumDown = true;
                    heavyDown = true;

                    special1();
                    if (isPlayer1)
                    {
                        StoredInfoScript.characterInfo.player1Meter += StoredInfoScript.characterInfo.meterIncrement;
                    }
                    else
                    {
                        StoredInfoScript.characterInfo.player2Meter += StoredInfoScript.characterInfo.meterIncrement;
                    }

                    ////Prevent repeat
                    //lastDirection1 = 5;
                    //lastDirection2 = 5;
                    //lastDirection3 = 5;

                    //damageToPass = 50;
                    //hitstunToPass = 53;
                    //heightToPass = 'm';

                    //rb.velocity = new Vector3(0, 0, 0);
                    //recovery = 33;
                    //specialType = 'S';
                    //anim.Play("Armature|Shoryuken", -1, 0f);
                    //v0 = transform.position;
                    //startingTime = Time.time;
                    //activeFrame = 29;
                    //deactiveFrame = 15;

                    //backToTop = false;

                    //currentMeter += 10;

                    //blockbox.SetActive(true);
                    //attackbox.transform.localPosition = new Vector3(direction * -5.85f, 15.11f, 0);
                    //attackbox.transform.localScale = new Vector3(5.98f, 17.72f, 5f);
                    ////inputH = Input.GetAxisRaw("Horizontal");
                }
                //else if ((isPlayer1 && (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L)) && special12InputCheck())
                //    || (!isPlayer1 && (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.N)) && special12InputCheck()))
                else if ((isPlayer1 && ((Input.GetAxisRaw("Light1") == 1 && !lightDown) || (Input.GetAxisRaw("Medium1") == 1 && !mediumDown) || (Input.GetAxisRaw("Heavy1") == 1 && !heavyDown)) && special12InputCheck())
                    || (!isPlayer1 && ((Input.GetAxisRaw("Light2") == 1 && !lightDown) || (Input.GetAxisRaw("Medium2") == 1 && !mediumDown) || (Input.GetAxisRaw("Heavy2") == 1 && !heavyDown)) && special12InputCheck()))
                {
                    lightDown = true;
                    mediumDown = true;
                    heavyDown = true;

                    special2();
                    if (isPlayer1)
                    {
                        StoredInfoScript.characterInfo.player1Meter += StoredInfoScript.characterInfo.meterIncrement;
                    }
                    else
                    {
                        StoredInfoScript.characterInfo.player2Meter += StoredInfoScript.characterInfo.meterIncrement;
                    }
                    ////Prevent repeat
                    //lastDirection1 = 5;
                    //lastDirection2 = 5;
                    //lastDirection3 = 5;

                    //damageToPass = 50;
                    //hitstunToPass = 53;
                    //heightToPass = 'm';

                    //rb.velocity = new Vector3(0, 0, 0);
                    //recovery = 53;
                    //specialType = 'T';
                    //anim.Play("Armature|Tatsu", -1, 0f);
                    //v0 = transform.position;
                    //startingTime = Time.time;
                    //activeFrame = 40;
                    //deactiveFrame = 21;

                    //backToTop = false;

                    //currentMeter += 10;

                    //blockbox.SetActive(true);
                    //attackbox.transform.localPosition = new Vector3(0, 9.73f, 0);
                    //attackbox.transform.localScale = new Vector3(23.9f, 5.5f, 5f);
                    ////inputH = Input.GetAxisRaw("Horizontal");
                }
                //else if ((isPlayer1 && (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L)) && special13InputCheck())
                //    || (!isPlayer1 && (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.N)) && special13InputCheck()))
                else if ((isPlayer1 && ((Input.GetAxisRaw("Light1") == 1 && !lightDown) || (Input.GetAxisRaw("Medium1") == 1 && !mediumDown) || (Input.GetAxisRaw("Heavy1") == 1 && !heavyDown)) && special13InputCheck())
                    || (!isPlayer1 && ((Input.GetAxisRaw("Light2") == 1 && !lightDown) || (Input.GetAxisRaw("Medium2") == 1 && !mediumDown) || (Input.GetAxisRaw("Heavy2") == 1 && !heavyDown)) && special13InputCheck()))
                {
                    lightDown = true;
                    mediumDown = true;
                    heavyDown = true;

                    special3();
                    if (isPlayer1)
                    {
                        StoredInfoScript.characterInfo.player1Meter += StoredInfoScript.characterInfo.meterIncrement;
                    }
                    else
                    {
                        StoredInfoScript.characterInfo.player2Meter += StoredInfoScript.characterInfo.meterIncrement;
                    }
                    ////Prevent repeat
                    //lastDirection1 = 5;
                    //lastDirection2 = 5;
                    //lastDirection3 = 5;

                    //rb.velocity = new Vector3(0, 0, 0);
                    //recovery = 45;
                    //specialType = 'H';
                    //activeFrame = -1;
                    //deactiveFrame = -1;
                    ////Move attack box

                    //backToTop = false;

                    //currentMeter += 10;

                    ////TODO Blocking? Probably do it in the hadouken itself

                    //anim.Play("Armature|HadoukenLight", -1, 0f);
                }
                //else if ((isPlayer1 && Input.GetKeyDown(KeyCode.U)))
                //else if ((isPlayer1 && Input.GetKeyDown(KeyCode.J) && Input.GetKeyDown(KeyCode.K))
                //    || (!isPlayer1 && Input.GetKeyDown(KeyCode.V) && Input.GetKeyDown(KeyCode.B)))
                else if ((isPlayer1 && Input.GetAxisRaw("Light1") == 1 && !lightDown && Input.GetAxisRaw("Medium1") == 1 && !mediumDown)
                   || (!isPlayer1 && Input.GetAxisRaw("Light2") == 1 && !lightDown && Input.GetAxisRaw("Medium2") == 1 && !mediumDown))
                {
                    lightDown = true;
                    mediumDown = true;

                    rb.velocity = new Vector3(0, 0, 0);
                    recovery = 40;
                    specialType = 'G';
                    activeFrame = 30;
                    deactiveFrame = 10;
                    damageToPass = 50;

                    hitstunToPass = 50;

                    if (isPlayer1)
                    {
                        if (Input.GetAxisRaw("Horizontal") == 0)
                        {
                            if (direction < 0)
                            {
                                throwDirectionToSend = true;
                            }
                            else
                            {
                                throwDirectionToSend = false;
                            }
                        }
                        else if (Input.GetAxisRaw("Horizontal") > 0)
                        {
                            throwDirectionToSend = false;
                        }
                        else
                        {
                            throwDirectionToSend = true;
                        }
                    }
                    else
                    {
                        if (Input.GetAxisRaw("Horizontal2") == 0)
                        {
                            if (direction < 0)
                            {
                                throwDirectionToSend = true;
                            }
                            else
                            {
                                throwDirectionToSend = false;
                            }
                        }
                        else if (Input.GetAxisRaw("Horizontal2") > 0)
                        {
                            throwDirectionToSend = false;
                        }
                        else
                        {
                            throwDirectionToSend = true;
                        }
                    }

                    //backToTop = false;

                    anim.Play("Armature|Grab", -1, 0f);
                }
                //else if ((isPlayer1 && Input.GetKeyDown(KeyCode.J)) || (!isPlayer1 && Input.GetKeyDown(KeyCode.V)))
                else if ((isPlayer1 && Input.GetAxisRaw("Light1") == 1 && !lightDown) || (!isPlayer1 && Input.GetAxisRaw("Light2") == 1 && !lightDown))
                {
                    lightDown = true;

                    if (!crouch)
                    {
                        standLight();
                        if (isPlayer1)
                        {
                            StoredInfoScript.characterInfo.player1Meter += StoredInfoScript.characterInfo.meterIncrement;
                        }
                        else
                        {
                            StoredInfoScript.characterInfo.player2Meter += StoredInfoScript.characterInfo.meterIncrement;
                        }
                        audioSource.clip = swingSound;
                        audioSource.Play();

                        //damageToPass = 50;
                        ////hitstunToPass = 20;
                        //hitstunToPass = 70;
                        //heightToPass = 'm';

                        //rb.velocity = new Vector3(0, 0, 0);
                        //recovery = 18;
                        //activeFrame = 13;
                        //deactiveFrame = 10;

                        //backToTop = false;

                        //currentMeter += 10;

                        //blockbox.SetActive(true);
                        //attackbox.transform.localPosition = new Vector3(direction * -7.25f, 12.72f, 0);
                        //attackbox.transform.localScale = new Vector3(5.72f, 2.53f, 5f);

                        //anim.Play("Armature|StandLight", -1, 0f);
                    }
                    else
                    {
                        crouchLight();
                        if (isPlayer1)
                        {
                            StoredInfoScript.characterInfo.player1Meter += StoredInfoScript.characterInfo.meterIncrement;
                        }
                        else
                        {
                            StoredInfoScript.characterInfo.player2Meter += StoredInfoScript.characterInfo.meterIncrement;
                        }
                        audioSource.clip = swingSound;
                        audioSource.Play();
                        //damageToPass = 50;
                        //hitstunToPass = 20;
                        //heightToPass = 'l';

                        //rb.velocity = new Vector3(0, 0, 0);
                        //recovery = 18;

                        //backToTop = false;

                        //currentMeter += 10;

                        //activeFrame = 13;
                        //deactiveFrame = 11;
                        //blockbox.SetActive(true);
                        //attackbox.transform.localPosition = new Vector3(direction * -8.31f, 1.54f, 0);
                        //attackbox.transform.localScale = new Vector3(5.72f, 2.53f, 5f);

                        //anim.Play("Armature|CrouchLight", -1, 0f);
                    }
                }
                else if ((isPlayer1 && Input.GetAxisRaw("Medium1") == 1 && !mediumDown) || (!isPlayer1 && Input.GetAxisRaw("Medium2") == 1 & !mediumDown))
                {
                    mediumDown = true;

                    if (!crouch)
                    {
                        standMedium();
                        if (isPlayer1)
                        {
                            StoredInfoScript.characterInfo.player1Meter += StoredInfoScript.characterInfo.meterIncrement;
                        }
                        else
                        {
                            StoredInfoScript.characterInfo.player2Meter += StoredInfoScript.characterInfo.meterIncrement;
                        }
                        audioSource.clip = swingSound;
                        audioSource.Play();
                        //damageToPass = 50;
                        //hitstunToPass = 35;
                        //heightToPass = 'm';

                        //rb.velocity = new Vector3(0, 0, 0);
                        //recovery = 32;

                        //backToTop = false;

                        //currentMeter += 10;

                        //activeFrame = 24;
                        //deactiveFrame = 21;
                        //blockbox.SetActive(true);
                        //attackbox.transform.localPosition = new Vector3(direction * -5.8f, 11.04f, 0);
                        //attackbox.transform.localScale = new Vector3(5.72f, 2.53f, 5f);

                        //anim.Play("Armature|StandMedium", -1, 0f);
                    }
                    else
                    {
                        crouchMedium();
                        if (isPlayer1)
                        {
                            StoredInfoScript.characterInfo.player1Meter += StoredInfoScript.characterInfo.meterIncrement;
                        }
                        else
                        {
                            StoredInfoScript.characterInfo.player2Meter += StoredInfoScript.characterInfo.meterIncrement;
                        }
                        audioSource.clip = swingSound;
                        audioSource.Play();
                        //damageToPass = 50;
                        //hitstunToPass = 34;
                        //heightToPass = 'l';

                        //rb.velocity = new Vector3(0, 0, 0);
                        //recovery = 30;

                        //backToTop = false;

                        //currentMeter += 10;

                        //activeFrame = 22;
                        //deactiveFrame = 19;
                        //blockbox.SetActive(true);
                        //attackbox.transform.localPosition = new Vector3(direction * -9.07f, 1.54f, 0);
                        //attackbox.transform.localScale = new Vector3(13.27f, 2.53f, 5f);

                        //anim.Play("Armature|CrouchMedium", -1, 0f);
                    }
                }
                //else if ((isPlayer1 && Input.GetKeyDown(KeyCode.L)) || (!isPlayer1 && Input.GetKeyDown(KeyCode.N)))
                else if ((isPlayer1 && Input.GetAxisRaw("Heavy1") == 1 && !heavyDown) || (!isPlayer1 && Input.GetAxisRaw("Heavy2") == 1 & !heavyDown))
                {
                    heavyDown = true;

                    if (!crouch)
                    {
                        standHeavy();
                        if (isPlayer1)
                        {
                            StoredInfoScript.characterInfo.player1Meter += StoredInfoScript.characterInfo.meterIncrement;
                        }
                        else
                        {
                            StoredInfoScript.characterInfo.player2Meter += StoredInfoScript.characterInfo.meterIncrement;
                        }
                        audioSource.clip = swingSound;
                        audioSource.Play();
                        //damageToPass = 50;
                        //hitstunToPass = 53;
                        //heightToPass = 'm';

                        //rb.velocity = new Vector3(0, 0, 0);
                        //recovery = 34;

                        //backToTop = false;

                        //currentMeter += 10;

                        //activeFrame = 24;
                        //deactiveFrame = 21;
                        //blockbox.SetActive(true);
                        //attackbox.transform.localPosition = new Vector3(direction * -8f, 14.73f, 0);
                        //attackbox.transform.localScale = new Vector3(5.72f, 2.53f, 5f);

                        //anim.Play("Armature|StandHeavy", -1, 0f);
                    }
                    else
                    {
                        crouchHeavy();
                        if (isPlayer1)
                        {
                            StoredInfoScript.characterInfo.player1Meter += StoredInfoScript.characterInfo.meterIncrement;
                        }
                        else
                        {
                            StoredInfoScript.characterInfo.player2Meter += StoredInfoScript.characterInfo.meterIncrement;
                        }
                        audioSource.clip = swingSound;
                        audioSource.Play();
                        //damageToPass = 50;
                        //hitstunToPass = 53;
                        //heightToPass = 'l';

                        //rb.velocity = new Vector3(0, 0, 0);
                        //recovery = 40;

                        //backToTop = false;

                        //currentMeter += 10;

                        //activeFrame = 30;
                        //deactiveFrame = 26;
                        //blockbox.SetActive(true);
                        //attackbox.transform.localPosition = new Vector3(direction * -9.07f, 2.66f, 0);
                        //attackbox.transform.localScale = new Vector3(13.27f, 4.91f, 5f);

                        //anim.Play("Armature|CrouchHeavy", -1, 0f);
                    }

                }
                // else if ((isPlayer1 && Input.GetKeyDown(KeyCode.Semicolon)) || (!isPlayer1 && Input.GetKeyDown(KeyCode.M)))
                else if (!launcherUsed && ((isPlayer1 && Input.GetAxisRaw("Special1") == 1 && !specialDown) || (!isPlayer1 && Input.GetAxisRaw("Special2") == 1 & !specialDown)))
                {
                    specialDown = true;

                    launcher();
                    launcherUsed = true;

                    if (isPlayer1)
                    {
                        StoredInfoScript.characterInfo.player1Meter += StoredInfoScript.characterInfo.meterIncrement;
                    }
                    else
                    {
                        StoredInfoScript.characterInfo.player2Meter += StoredInfoScript.characterInfo.meterIncrement;
                    }
                    audioSource.clip = swingSound;
                    audioSource.Play();
                    //damageToPass = 50;
                    //hitstunToPass = 10000;
                    //heightToPass = 'm';

                    //rb.velocity = new Vector3(0, 0, 0);
                    //recovery = 36;

                    //backToTop = false;

                    //currentMeter += 10;

                    //activeFrame = 27;
                    //deactiveFrame = 22;
                    //blockbox.SetActive(true);
                    //attackbox.transform.localPosition = new Vector3(direction * -5.73f, 13.58f, 0);
                    //attackbox.transform.localScale = new Vector3(5.5f, 19.69f, 5f);

                    //anim.Play("Armature|Shoryuken", -1, 0f);
                }

            }
            else if (jump && specialType == 'N')
            {
                recovery--;

                //rb.velocity = new Vector3(moveX, 0, 0);
                //moveDirection.y += (-1 * gravity * Time.deltaTime + jumpSpeed);
                //controller.Move(moveDirection * Time.deltaTime);

                if (hitByProjectile)
                {
                    hitByProjectile = false;
                    hitByAttackBox = true;
                }

                if (hitByAttackBox)
                {
                    //Take damage and hit stun
                    attackbox.SetActive(false);
                    hitByAttackBox = false;
                    hitByProjectile = false;
                    juggle = true;
                    currentCombo++;

                    if (!backDisabled)
                        rb.transform.position = new Vector3(rb.transform.position.x + knockback * direction, rb.transform.position.y, rb.transform.position.z);
                    v0 = new Vector3(rb.transform.position.x, v0.y, v0.z);

                    activeFrame = -1;
                    deactiveFrame = -1;

                    if (projectileDamageToRecieve == 0)
                    {
                        //currentHealth -= damageToRecieve;
                        if (isPlayer1)
                        {
                            StoredInfoScript.characterInfo.player1Health[0] -= (damageToRecieve / StoredInfoScript.characterInfo.maxHealths[StoredInfoScript.characterInfo.player1Characters[0]]);
                        }
                        else
                        {
                            StoredInfoScript.characterInfo.player2Health[0] -= (damageToRecieve / StoredInfoScript.characterInfo.maxHealths[StoredInfoScript.characterInfo.player2Characters[0]]);
                        }
                    }
                    else
                    {
                        //currentHealth -= projectileDamageToRecieve;
                        if (isPlayer1)
                        {
                            StoredInfoScript.characterInfo.player1Health[0] -= (projectileDamageToRecieve / StoredInfoScript.characterInfo.maxHealths[StoredInfoScript.characterInfo.player1Characters[0]]);
                        }
                        else
                        {
                            StoredInfoScript.characterInfo.player2Health[0] -= (projectileDamageToRecieve / StoredInfoScript.characterInfo.maxHealths[StoredInfoScript.characterInfo.player2Characters[0]]);
                        }

                        projectileDamageToRecieve = 0;
                        hitByProjectile = false;
                    }
                    audioSource.clip = hitSound;
                    audioSource.Play();

                    //if (currentHealth < 0)
                    //{
                    //    currentHealth = 0;
                    //}
                    if (isPlayer1)
                    {
                        if (StoredInfoScript.characterInfo.player1Health[0] < 0)
                            StoredInfoScript.characterInfo.player1Health[0] = 0;
                    }
                    else
                    {
                        if (StoredInfoScript.characterInfo.player2Health[0] < 0)
                            StoredInfoScript.characterInfo.player2Health[0] = 0;
                    }

                    airAttackRecovery = 10000;
                    recovery = 10000;
                    startingTime = Time.time;

                    anim.SetBool("juggle", juggle);

                    //Horizontal knockback
                    //transform.position = new Vector3(transform.position.x + (direction * 6), transform.position.y, transform.position.z);
                    //v0.x = rb.position.x;

                    //v0 = rb.position;
                    v0.y = rb.position.y;
                
                    
                    //hitByBlockBox = false;
                    rb.velocity = new Vector3(0, 0, 0);

                    //Play animation
                    if (characterName != "C.Falcon")
                    {
                        anim.Play("Armature|Juggle", -1, 0.1f);
                    }
                    else
                    {
                        anim.Play("Armature|Idle", -1, 0.1f);
                    }

                    // return;
                }

                if (airAttackRecovery <= 0)
                {
                    blockbox.SetActive(false);
                    attackbox.SetActive(false);

                    //if ((isPlayer1 && Input.GetKeyDown(KeyCode.J)) || (!isPlayer1 && Input.GetKeyDown(KeyCode.V)))
                    if ((isPlayer1 && Input.GetAxisRaw("Light1") == 1 && !lightDown) || (!isPlayer1 && Input.GetAxisRaw("Light2") == 1 & !lightDown))
                    {
                        lightDown = true;

                        airLight();
                        if (isPlayer1)
                        {
                            StoredInfoScript.characterInfo.player1Meter += StoredInfoScript.characterInfo.meterIncrement;
                        }
                        else
                        {
                            StoredInfoScript.characterInfo.player2Meter += StoredInfoScript.characterInfo.meterIncrement;
                        }
                        audioSource.clip = swingSound;
                        audioSource.Play();
                        //damageToPass = 50;
                        //hitstunToPass = 30;
                        //heightToPass = 'h';

                        //airAttackRecovery = 24;

                        //backToTop = false;
                        //currentMeter += 10;

                        //activeFrame = 18;
                        //deactiveFrame = 5;
                        //blockbox.SetActive(true);
                        //attackbox.transform.localPosition = new Vector3(direction * -7.25f, 12.72f, 0);
                        //attackbox.transform.localScale = new Vector3(5.72f, 2.53f, 5f);

                        //anim.Play("Armature|AirLight", -1, 0f);
                    }
                    //else if ((isPlayer1 && Input.GetKeyDown(KeyCode.K)) || (!isPlayer1 && Input.GetKeyDown(KeyCode.B)))
                    else if ((isPlayer1 && Input.GetAxisRaw("Medium1") == 1 && !mediumDown) || (!isPlayer1 && Input.GetAxisRaw("Medium2") == 1 & !mediumDown))
                    {
                        mediumDown = true;

                        airMedium();
                        if (isPlayer1)
                        {
                            StoredInfoScript.characterInfo.player1Meter += StoredInfoScript.characterInfo.meterIncrement;
                        }
                        else
                        {
                            StoredInfoScript.characterInfo.player2Meter += StoredInfoScript.characterInfo.meterIncrement;
                        }
                        audioSource.clip = swingSound;
                        audioSource.Play();
                        //damageToPass = 50;
                        //hitstunToPass = 34;
                        //heightToPass = 'h';

                        //airAttackRecovery = 32;

                        //backToTop = false;
                        //currentMeter += 10;

                        //activeFrame = 23;
                        //deactiveFrame = 19;
                        //blockbox.SetActive(true);
                        //attackbox.transform.localPosition = new Vector3(direction * -7.15f, 3.72f, 0);
                        //attackbox.transform.localScale = new Vector3(10f, 7f, 5f);

                        //anim.Play("Armature|AirMedium", -1, 0f);
                    }
                    //else if ((isPlayer1 && Input.GetKeyDown(KeyCode.L)) || (!isPlayer1 && Input.GetKeyDown(KeyCode.N)))
                    else if ((isPlayer1 && Input.GetAxisRaw("Heavy1") == 1 && !heavyDown) || (!isPlayer1 && Input.GetAxisRaw("Heavy2") == 1 & !heavyDown))
                    {
                        heavyDown = true;

                        airHeavy();
                        if (isPlayer1)
                        {
                            StoredInfoScript.characterInfo.player1Meter += StoredInfoScript.characterInfo.meterIncrement;
                        }
                        else
                        {
                            StoredInfoScript.characterInfo.player2Meter += StoredInfoScript.characterInfo.meterIncrement;
                        }
                        audioSource.clip = swingSound;
                        audioSource.Play();
                        //damageToPass = 50;
                        //hitstunToPass = 38;
                        //heightToPass = 'h';

                        //airAttackRecovery = 36;

                        //backToTop = false;
                        //currentMeter += 10;

                        //activeFrame = 27;
                        //deactiveFrame = 23;
                        //blockbox.SetActive(true);
                        //attackbox.transform.localPosition = new Vector3(direction * -5.51f, 8.65f, 0);
                        //attackbox.transform.localScale = new Vector3(5.98f, 7f, 5f);

                        //anim.Play("Armature|AirHeavy", -1, 0f);
                    }
                    
                }
                else
                {
                    airAttackRecovery--;

                    //Check hit boxes
                    if (airAttackRecovery == activeFrame)
                    {
                        attackbox.SetActive(true);
                    }
                    if (airAttackRecovery == deactiveFrame)
                    {
                        attackbox.SetActive(false);
                        blockbox.SetActive(false);
                        backToTop = true;
                    }
                }

                float resulty = 0;

                if(juggle)
                {
                    resulty = (float)(v0.y + (juggleSpeedToRecieve * (Time.time - startingTime) + 0.5 * gravity * (Time.time - startingTime) * (Time.time - startingTime)));
                }
                else
                {
                    resulty = (float)(v0.y + (jumpSpeed * (Time.time - startingTime) + 0.5 * gravity * (Time.time - startingTime) * (Time.time - startingTime)));
                }

                //float resulty = (float)(v0.y + (jumpSpeed * (Time.time - startingTime) + 0.5 * gravity * (Time.time - startingTime) * (Time.time - startingTime)));
                float resultx = 0; //v0.x + -1 * inputH * jumpSideSpeed * (Time.time - startingTime);

                if (isPlayer1)
                {
                    if (backDisabled && inputH * direction < 0)
                    {
                        if (lastJumpTime <= 0)
                        {
                            lastJumpTime = (Time.time - startingTime);
                        }

                        resultx = v0.x + -1 * inputH * jumpSideSpeed * lastJumpTime;
                    }
                    else
                    {
                        lastJumpTime = 0;
                        resultx = v0.x + -1 * inputH * jumpSideSpeed * (Time.time - startingTime);
                    }
                }
                else
                {
                    if (backDisabled && inputH2 * direction < 0)
                    {
                        if (lastJumpTime <= 0)
                        {
                            lastJumpTime = (Time.time - startingTime);
                        }

                        resultx = v0.x + -1 * inputH2 * jumpSideSpeed * lastJumpTime;
                    }
                    else
                    {
                        lastJumpTime = 0;
                        resultx = v0.x + -1 * inputH2 * jumpSideSpeed * (Time.time - startingTime);
                    }

                    //resultx = v0.x + -1 * inputH2 * jumpSideSpeed * (Time.time - startingTime);
                }

                rb.position = new Vector3(resultx, resulty, v0.z);

               
                //Check if hit the ground? And stop if juggled?
                if (juggle && rb.position.y < 0)
                {
                    recovery = 0;
                    airAttackRecovery = 0;
                    rb.position = new Vector3(resultx, 0, v0.z);
                }

                //rb.MovePosition(moveDirection);
            }
            else if (specialType == 'S')
            {
                recovery--;

                //Chekc hit boxes
                if (recovery == activeFrame)
                {
                    attackbox.SetActive(true);
                }
                if (recovery == deactiveFrame)
                {
                    attackbox.SetActive(false);
                    blockbox.SetActive(false);
                    backToTop = true;
                }

                special1Effects();
            }
            else if (specialType == 'T')
            {
                recovery--;

                //Check hitboxes
                if (recovery == activeFrame)
                {
                    attackbox.SetActive(true);
                }
                if (recovery == deactiveFrame)
                {
                    attackbox.SetActive(false);
                    blockbox.SetActive(false);
                    backToTop = true;
                }

                special2Effects();
            }
            else if (specialType == 'G')
            {
                recovery--;

                //Check hitboxes
                if (recovery == activeFrame)
                {
                    grabbox.SetActive(true);
                }
                if (recovery == deactiveFrame)
                {
                    grabbox.SetActive(false);
                }

                //Grab successful
                if (throwSuccessful)
                {
                    grabbox.SetActive(false);

                    if (throwDirectionToSend)
                    {
                        if (direction < 0)
                        {
                            anim.Play("Armature|ForwardThrow", -1, 0f);
                        }
                        else
                        {
                            anim.Play("Armature|BackThrow", -1, 0f);
                        }
                    }
                    else
                    {
                        if (direction < 0)
                        {
                            anim.Play("Armature|BackThrow", -1, 0f);
                        }
                        else
                        {
                            anim.Play("Armature|ForwardThrow", -1, 0f);
                        }
                    }

                    

                    throwSuccessful = false;
                }
            }
            else if (specialType == 'H')
            {
                recovery--;

                if (recovery == activeFrame)
                {
                    attackbox.SetActive(true);
                }
                if (recovery == deactiveFrame)
                {
                    attackbox.SetActive(false);
                    blockbox.SetActive(false);
                    backToTop = true;
                }

                special3Effects();

               
            }
            else if (specialType == 'D')
            {
                recovery--;
                
                float moveX = 0;

                if (isPlayer1)
                {
                    moveX = -3 * inputH * speed * Time.deltaTime;
                }
                else
                {
                    moveX = -3 * inputH2 * speed * Time.deltaTime;
                }

                if (backDisabled && moveX * direction > 0)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                }
                else
                {
                    rb.velocity = new Vector3(moveX, 0, 0);
                }
            }
            else if (specialType == 'U')
            {
                recovery--;

                if (recovery == activeFrame)
                {
                    attackbox.SetActive(true);
                    blockbox.SetActive(true);
                }
                if (recovery == deactiveFrame)
                {
                    attackbox.SetActive(false);
                }

                superEffects();

              
            }
            else
            {
                recovery--;

                if (hitByGrabBox)
                {
                    activeFrame = -1;
                    deactiveFrame = -1;

                    //Rot around character point. Hey
                    if (throwDirectionToRecieve)
                    {
                        //throw forward
                        //Increment the rotation and move the character accordingly using v0
                        //rb.transform.Rotate(0, 0, 5);
                        rb.transform.Rotate(new Vector3(0, 0, 1), -2);
                        //rb.transform.R
                        float resulty = (float)(throwSpeed * (Time.time - startingTime) + 0.5 * gravity * (Time.time - startingTime) * (Time.time - startingTime));
                        if (resulty < 0)
                        {
                            resulty = 0;
                        }
                        rb.transform.position = new Vector3(rb.transform.position.x + 0.5f, resulty, 0);
                    }
                    else
                    {
                        //Throw backwards
                        //rb.transform.Rotate(0, 0, 5);
                        rb.transform.Rotate(new Vector3(0, 0, 1), 2);
                        float resulty = (float)(throwSpeed * (Time.time - startingTime) + 0.5 * gravity * (Time.time - startingTime) * (Time.time - startingTime));
                        if (resulty < 0)
                        {
                            resulty = 0;
                        }
                        rb.transform.position = new Vector3(rb.transform.position.x - 0.5f, resulty, 0);
                    }
                }

                if (hitByAttackBox)
                {
                    //Take damage and hit stun
                    attackbox.SetActive(false);
                    hitByAttackBox = false;
                    hitByProjectile = false;
                    hit = true;
                    currentCombo++;

                    if (!backDisabled)
                        rb.transform.position = new Vector3(rb.transform.position.x + knockback * direction, rb.transform.position.y, rb.transform.position.z);

                    activeFrame = -1;
                    deactiveFrame = -1;

                    //If launcher
                    if (hitstunToRecieve >= 1000 || projectileHitStunToRecieve >= 1000)
                    {
                        recovery = 1000;
                        airAttackRecovery = 1000;
                        hitByAttackBox = true;
                        jump = true;
                        v0 = rb.transform.position;
                        startingTime = Time.time;
                        projectileHitStunToRecieve = 0;
                        return;
                    }

                    if (projectileDamageToRecieve == 0)
                    {
                        //currentHealth -= damageToRecieve;
                        if (isPlayer1)
                        {
                            StoredInfoScript.characterInfo.player1Health[0] -= (damageToRecieve / StoredInfoScript.characterInfo.maxHealths[StoredInfoScript.characterInfo.player1Characters[0]]);
                        }
                        else
                        {
                            StoredInfoScript.characterInfo.player2Health[0] -= (damageToRecieve / StoredInfoScript.characterInfo.maxHealths[StoredInfoScript.characterInfo.player2Characters[0]]);
                        }
                    }
                    else
                    {
                        //currentHealth -= projectileDamageToRecieve;
                        if (isPlayer1)
                        {
                            StoredInfoScript.characterInfo.player1Health[0] -= (projectileDamageToRecieve / StoredInfoScript.characterInfo.maxHealths[StoredInfoScript.characterInfo.player1Characters[0]]);
                        }
                        else
                        {
                            StoredInfoScript.characterInfo.player2Health[0] -= (projectileDamageToRecieve / StoredInfoScript.characterInfo.maxHealths[StoredInfoScript.characterInfo.player2Characters[0]]);
                        }

                        projectileDamageToRecieve = 0;
                        hitByProjectile = false;
                    }
                    audioSource.clip = hitSound;
                    audioSource.Play();

                    //if (currentHealth < 0)
                    //{
                    //    currentHealth = 0;
                    //}
                    if (isPlayer1)
                    {
                        if (StoredInfoScript.characterInfo.player1Health[0] < 0)
                            StoredInfoScript.characterInfo.player1Health[0] = 0;
                    }
                    else
                    {
                        if (StoredInfoScript.characterInfo.player2Health[0] < 0)
                            StoredInfoScript.characterInfo.player2Health[0] = 0;
                    }

                    if (projectileHitStunToRecieve == 0)
                    {
                        recovery = hitstunToRecieve;
                    }
                    else
                    {
                        recovery = projectileHitStunToRecieve;
                        projectileHitStunToRecieve = 0;
                    }

                    anim.SetBool("hit", hit);

                    //hitByBlockBox = false;
                    rb.velocity = new Vector3(0, 0, 0);

                    //Play animation
                    if (heightToRecieve != 'l')
                    {
                        anim.Play("Armature|StandHit", -1, 0f);
                    }
                    else
                    {
                        anim.Play("Armature|CrouchHit", -1, 0f);
                    }

                    return;
                }

                //Check hitboxes
                if (recovery == activeFrame)
                {
                    attackbox.SetActive(true);
                }
                if (recovery == deactiveFrame)
                {
                    attackbox.SetActive(false);
                    blockbox.SetActive(false);
                    backToTop = true;
                }
            }
        }
    }
}
