using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public bool isPlayer1;

    public Animator anim;
    public Rigidbody rb;

    private float inputH;
    private bool crouch;
    private bool jump;

    private int recovery;
    private int airAttackRecovery;

    public int direction;

    public GameObject hadouken;

    public GameObject characterbox;
    public GameObject throwbox;
    public GameObject hitbox;
    public GameObject attackbox;
    public GameObject grabbox;
    public GameObject blockbox;

    public float speed;
    public float jumpSpeed;
    public float jumpSideSpeed;
    public float shoryukenJumpSpeed;
    public float gravity;
    public float maxHeight;
    private Vector3 v0;
    private float startingTime;
    private char specialType;

    private int activeFrame;
    private int deactiveFrame;
    

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        crouch = false;
        jump = false;
        gravity = -360.0F;
        jumpSpeed = 120.0F;
        jumpSideSpeed = 25.0f;
        shoryukenJumpSpeed = 90.0f;
        speed = 1800.0F;
        maxHeight = 60.0f;
        v0 = Vector3.zero;
        startingTime = 0;
        airAttackRecovery = 0;
        specialType = 'N';
        activeFrame = 0;
        deactiveFrame = 0;
       // direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        /* if(Input.GetKeyDown(KeyCode.A))
         {
             //Name of animation, the layer (-1 goes to base layer), then how far into animation to start playing at. 0-1. 1 is end of animtion
             anim.Play("Armature|WalkBack", -1, 0f);
         }
         if (Input.GetKeyDown(KeyCode.D))
         {
             anim.Play("Armature|WalkForward", -1, 0f);
         }*/
        if (recovery<=0)
        {
            specialType = 'N';
            blockbox.SetActive(false);

            if (Input.GetKey(KeyCode.S) && !jump)
            {
                crouch = true;
                characterbox.transform.localPosition = new Vector3(-1.15f, 5.70f, 0);
                characterbox.transform.localScale = new Vector3(6f, 10.6f, 5f);

                hitbox.transform.localPosition = new Vector3(-0.88f, 6.02f, 0);
                hitbox.transform.localScale = new Vector3(10f, 13.4f, 5f);
            }
            else
            {
                crouch = false;
                characterbox.transform.localPosition = new Vector3(0, 8.95f, 0);
                characterbox.transform.localScale = new Vector3(6f, 14.65f, 5f);

                hitbox.transform.localPosition = new Vector3(0f, 9.19f, 0);
                hitbox.transform.localScale = new Vector3(10f, 18.01f, 5f);
            }
            if (Input.GetKey(KeyCode.W))
            {
                jump = true;
                anim.SetBool("jump", jump);
                recovery = 40;
                v0 = transform.position;
                startingTime = Time.time;
                inputH = Input.GetAxisRaw("Horizontal");
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
            anim.SetFloat("inputH", inputH);

            if (!crouch)
            {
                float moveX = -1 * inputH * speed * Time.deltaTime;
                rb.velocity = new Vector3(moveX, 0, 0);
            }
            else
            {
                rb.velocity = new Vector3(0, 0, 0);
            }

            //Dashing
            if (!crouch && Input.GetKeyDown(KeyCode.E) && inputH != 0)
            {
                recovery = 10;
                specialType = 'D';
                return;
            }

            //Attacks
            if (Input.GetKeyDown(KeyCode.P))
            {
                rb.velocity = new Vector3(0, 0, 0);
                recovery = 33;
                specialType = 'S';
                anim.Play("Armature|Shoryuken", -1, 0f);
                v0 = transform.position;
                startingTime = Time.time;
                activeFrame = 29;
                deactiveFrame = 15;

                blockbox.SetActive(true);
                attackbox.transform.localPosition = new Vector3(-5.85f, 15.11f, 0);
                attackbox.transform.localScale = new Vector3(5.98f, 17.72f, 5f);
                //inputH = Input.GetAxisRaw("Horizontal");
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                rb.velocity = new Vector3(0, 0, 0);
                recovery = 53;
                specialType = 'T';
                anim.Play("Armature|Tatsu", -1, 0f);
                v0 = transform.position;
                startingTime = Time.time;
                activeFrame = 40;
                deactiveFrame = 21;

                blockbox.SetActive(true);
                attackbox.transform.localPosition = new Vector3(0, 9.73f, 0);
                attackbox.transform.localScale = new Vector3(23.9f, 5.5f, 5f);
                //inputH = Input.GetAxisRaw("Horizontal");
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                rb.velocity = new Vector3(0, 0, 0);
                recovery = 45;
                specialType = 'H';
                activeFrame = -1;
                deactiveFrame = -1;
                //Move attack box

                //TODO Blocking? Probably do it in the hadouken itself

                anim.Play("Armature|HadoukenLight", -1, 0f);
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                rb.velocity = new Vector3(0, 0, 0);
                recovery = 40;
                specialType = 'G';
                activeFrame = 30;
                deactiveFrame = 10;

                anim.Play("Armature|Grab", -1, 0f);
            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                rb.velocity = new Vector3(0, 0, 0);
                recovery = 420;
                specialType = 'U';
                activeFrame = 283;
                deactiveFrame = 280;

                //If mis swithc to regular shoryuken

                attackbox.transform.localPosition = new Vector3(-7.25f, 12.72f, 0);
                attackbox.transform.localScale = new Vector3(5f, 4f, 5f);

                anim.Play("Armature|Super", -1, 0f);
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                if (!crouch)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                    recovery = 18;
                    activeFrame = 13;
                    deactiveFrame = 10;

                    blockbox.SetActive(true);
                    attackbox.transform.localPosition = new Vector3(-7.25f, 12.72f, 0);
                    attackbox.transform.localScale = new Vector3(5.72f, 2.53f, 5f);

                    anim.Play("Armature|StandLight", -1, 0f);
                }
                else
                {
                    rb.velocity = new Vector3(0, 0, 0);
                    recovery = 18;

                    activeFrame = 13;
                    deactiveFrame = 11;
                    blockbox.SetActive(true);
                    attackbox.transform.localPosition = new Vector3(-8.31f, 1.54f, 0);
                    attackbox.transform.localScale = new Vector3(5.72f, 2.53f, 5f);

                    anim.Play("Armature|CrouchLight", -1, 0f);
                }
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                if (!crouch)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                    recovery = 32;

                    activeFrame = 24;
                    deactiveFrame = 21;
                    blockbox.SetActive(true);
                    attackbox.transform.localPosition = new Vector3(-5.8f, 11.04f, 0);
                    attackbox.transform.localScale = new Vector3(5.72f, 2.53f, 5f);

                    anim.Play("Armature|StandMedium", -1, 0f);
                }
                else
                {
                    rb.velocity = new Vector3(0, 0, 0);
                    recovery = 30;

                    activeFrame = 22;
                    deactiveFrame = 19;
                    blockbox.SetActive(true);
                    attackbox.transform.localPosition = new Vector3(-9.07f, 1.54f, 0);
                    attackbox.transform.localScale = new Vector3(13.27f, 2.53f, 5f);

                    anim.Play("Armature|CrouchMedium", -1, 0f);
                }
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                if (!crouch)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                    recovery = 34;

                    activeFrame = 24;
                    deactiveFrame = 21;
                    blockbox.SetActive(true);
                    attackbox.transform.localPosition = new Vector3(-8f, 14.73f, 0);
                    attackbox.transform.localScale = new Vector3(5.72f, 2.53f, 5f);

                    anim.Play("Armature|StandHeavy", -1, 0f);
                }
                else
                {
                    rb.velocity = new Vector3(0, 0, 0);
                    recovery = 40;

                    activeFrame = 30;
                    deactiveFrame = 26;
                    blockbox.SetActive(true);
                    attackbox.transform.localPosition = new Vector3(-9.07f, 2.66f, 0);
                    attackbox.transform.localScale = new Vector3(13.27f, 4.91f, 5f);

                    anim.Play("Armature|CrouchHeavy", -1, 0f);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Semicolon))
            {
                rb.velocity = new Vector3(0, 0, 0);
                recovery = 36;

                activeFrame = 27;
                deactiveFrame = 22;
                blockbox.SetActive(true);
                attackbox.transform.localPosition = new Vector3(-5.73f, 13.58f, 0);
                attackbox.transform.localScale = new Vector3(5.5f, 19.69f, 5f);

                anim.Play("Armature|Shoryuken", -1, 0f);
            }
        }
        else if(jump && specialType == 'N')
        {
            recovery--;

            //rb.velocity = new Vector3(moveX, 0, 0);
            //moveDirection.y += (-1 * gravity * Time.deltaTime + jumpSpeed);
            //controller.Move(moveDirection * Time.deltaTime);

            if (airAttackRecovery <= 0)
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    airAttackRecovery = 24;

                    activeFrame = 18;
                    deactiveFrame = 5;
                    blockbox.SetActive(true);
                    attackbox.transform.localPosition = new Vector3(-7.25f, 12.72f, 0);
                    attackbox.transform.localScale = new Vector3(5.72f, 2.53f, 5f);

                    anim.Play("Armature|AirLight", -1, 0f);
                }
                else if (Input.GetKeyDown(KeyCode.K))
                {
                    airAttackRecovery = 32;

                    activeFrame = 23;
                    deactiveFrame = 19;
                    blockbox.SetActive(true);
                    attackbox.transform.localPosition = new Vector3(-7.15f, 3.72f, 0);
                    attackbox.transform.localScale = new Vector3(10f, 7f, 5f);

                    anim.Play("Armature|AirMedium", -1, 0f);
                }
                else if (Input.GetKeyDown(KeyCode.L))
                {
                    airAttackRecovery = 36;

                    activeFrame = 27;
                    deactiveFrame = 23;
                    blockbox.SetActive(true);
                    attackbox.transform.localPosition = new Vector3(-5.51f, 8.65f, 0);
                    attackbox.transform.localScale = new Vector3(5.98f, 7f, 5f);

                    anim.Play("Armature|AirHeavy", -1, 0f);
                }
                else if (Input.GetKeyDown(KeyCode.Semicolon))
                {
                    airAttackRecovery = 36;

                    activeFrame = 27;
                    deactiveFrame = 19;
                    blockbox.SetActive(true);
                    attackbox.transform.localPosition = new Vector3(-5.51f, 8.65f, 0);
                    attackbox.transform.localScale = new Vector3(5.98f, 7f, 5f);

                    anim.Play("Armature|AirHeavy", -1, 0f);
                }
            }
            else
            {
                airAttackRecovery--;

                //Check hit boxes
                if(airAttackRecovery == activeFrame)
                {
                    attackbox.SetActive(true);
                }
                if(airAttackRecovery == deactiveFrame)
                {
                    attackbox.SetActive(false);
                }
            }
       

            float resulty = (float) (jumpSpeed * (Time.time - startingTime) + 0.5 * gravity * (Time.time - startingTime) * (Time.time - startingTime));
            float resultx = v0.x + -1 * inputH * jumpSideSpeed * (Time.time - startingTime);
            rb.position = new Vector3(resultx, resulty, v0.z);
           
            //rb.MovePosition(moveDirection);
        }
        else if(specialType == 'S')
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
            }

            float resulty = (float)(shoryukenJumpSpeed * (Time.time - startingTime) + 0.5 * gravity * (Time.time - startingTime) * (Time.time - startingTime));
            //float resultx = v0.x + -1 * jumpSideSpeed * (Time.time - startingTime);
            rb.position = new Vector3(v0.x, resulty, v0.z);
        }
        else if(specialType == 'T')
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
            }

            //float resulty = (float)(jumpSpeed * (Time.time - startingTime) + 0.5 * gravity * (Time.time - startingTime) * (Time.time - startingTime));
            float resultx = (v0.x + -1 * direction * jumpSideSpeed * (Time.time - startingTime));
            rb.position = new Vector3(resultx, v0.y, v0.z);
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
        }
        else if(specialType == 'H')
        {
            recovery--;

            if(recovery == 34)
            {
                Quaternion fireballDirection = Quaternion.identity;
                
                if(direction < 0)
                {
                    fireballDirection = Quaternion.Euler(new Vector3(0, 180, 0));
                }

                Instantiate(hadouken, new Vector3((float)(transform.position.x - (8.63 * direction)) , (float)(transform.position.y + 11.96), 0), fireballDirection);
            }
        }
        else if (specialType == 'D')
        {
            recovery--;

            //float resulty = (float)(jumpSpeed * (Time.time - startingTime) + 0.5 * gravity * (Time.time - startingTime) * (Time.time - startingTime));
            float moveX = -3 * inputH * speed * Time.deltaTime;
            rb.velocity = new Vector3(moveX, 0, 0);
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

            if (recovery == 34)
            {
                v0 = transform.position;
                startingTime = Time.time;
            }

            if (recovery <= 34)
            {
                float resulty = (float)(shoryukenJumpSpeed * (Time.time - startingTime) + 0.5 * gravity * (Time.time - startingTime) * (Time.time - startingTime));
                //float resultx = v0.x + -1 * jumpSideSpeed * (Time.time - startingTime);
                rb.position = new Vector3(v0.x, resulty, v0.z);
            }
        }
        else
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
            }
        }
    }



    /*void OnTriggerEnter(Collider other)
    {
        //If the player hits the final room, trigger the name entry
        if (other.gameObject.CompareTag("ThrowBox"))
        {

        }
        if (other.gameObject.CompareTag("HitBox"))
        {

        }
        if (other.gameObject.CompareTag("AttackBox"))
        {

        }
        if (other.gameObject.CompareTag("GrabBox"))
        {

        }
        if (other.gameObject.CompareTag("BlockBox"))
        {

        }
    }*/

}
