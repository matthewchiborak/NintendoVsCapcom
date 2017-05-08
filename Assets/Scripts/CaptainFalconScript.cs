using UnityEngine;
using System.Collections;
using System;

public class CaptainFalconScript : CharacterScript
{
    //public GameObject hadouken;
    //public float shoryukenJumpSpeed;

    public float blueFalconJumpSpeed;
    public GameObject blueFalcon;
    public GameObject falconPunchProjectile;
    public GameObject falconPunchNormal;
    public GameObject falconKick;
    
    public AudioClip[] CFAudioClips = new AudioClip[10];

    public override void characterSpecificInit()
    {
        characterName = "C.Falcon";
        speed = 1800.0F;
        maxHealth = 1000;
        currentHealth = 1000;
        blueFalconJumpSpeed = 90f;
        //throw new NotImplementedException();
    }

    public override void crouchChanges()
    {
        characterbox.transform.localPosition = new Vector3(0f, 0.18f, 0);
        characterbox.transform.localScale = new Vector3(0.15f, 0.36f, 0.24f);

        hitbox.transform.localPosition = new Vector3(0f, 0.17f, 0);
        hitbox.transform.localScale = new Vector3(0.28f, 0.45f, 0.3f);
    }

    public override void standChanges()
    {
        characterbox.transform.localPosition = new Vector3(0, 0.3f, 0);
        characterbox.transform.localScale = new Vector3(0.15f, 0.36f, 0.24f);

        hitbox.transform.localPosition = new Vector3(0f, 0.27f, 0);
        hitbox.transform.localScale = new Vector3(0.22f, 0.55f, 0.3f);
    }

    public override void alwaysCheck()
    {
        //Is continuously checked so can be used to decrement the speed bonus
        if (speed > 1800.0f)
        {
            speed -= 6.00f;
        }
        if (speed < 1800.0f)
        {
            speed = 1800.0f;
        }
    }

    public override bool superInputCheck()
    {
        

        //return lastDirection4 == 6 && lastDirection2 == 2 && lastDirection1 == 3;
        return lastDirection3 == 2 && lastDirection2 == 3 && lastDirection1 == 6;
        //throw new NotImplementedException();
    }

    public override bool special11InputCheck()
    {
        //return lastDirection4 == 6 && lastDirection2 == 2 && lastDirection1 == 3;
        return lastDirection3 == 2 && lastDirection2 == 3 && lastDirection1 == 6;
        //throw new NotImplementedException();
    }

    public override bool special12InputCheck()
    {
        //return lastDirection4 == 6 && lastDirection2 == 2 && lastDirection1 == 3;
        return lastDirection3 == 2 && lastDirection2 == 1 && lastDirection1 == 4;
        //return lastDirection3 == 2 && lastDirection2 == 1 && lastDirection1 == 4;
        //throw new NotImplementedException();
    }

    public override bool special13InputCheck()
    {
        return lastDirection4 == 6 && lastDirection2 == 2 && lastDirection1 == 3;
        //return lastDirection3 == 2 && lastDirection2 == 1 && lastDirection1 == 4;
        //return lastDirection3 == 2 && lastDirection2 == 3 && lastDirection1 == 6;
        //throw new NotImplementedException();
    }

    public override void super()
    {
        //currentMeter = 0;
        

        //Prevent repeat
        lastDirection1 = 5;
        lastDirection2 = 5;
        lastDirection3 = 5;

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 120;
        specialType = 'U';
        activeFrame = -1;
        deactiveFrame = -1;
        juggleSpeedToPass = 360;

        backToTop = false;

        audioSource.clip = CFAudioClips[6];
        audioSource.Play();

        attackbox.transform.localPosition = new Vector3(0f, 0f, 0);
        attackbox.transform.localScale = new Vector3(10f, 40f, 5f);

        anim.Play("Armature|Super", -1, 0f);

        //throw new NotImplementedException();
    }

    public override void special1Ex()
    {
        //Burn meter
       // currentMeter -= 100;

        //Prevent repeat
        lastDirection1 = 5;
        lastDirection2 = 5;
        lastDirection3 = 5;

        damageToPass = 50;
        hitstunToPass = 53;
        heightToPass = 'm';
        juggleSpeedToPass = 120;

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 40;
        specialType = 'S';
        anim.Play("Armature|FalconPunch", -1, 0f);
        v0 = transform.position;
        startingTime = Time.time;
        activeFrame = -1;
        deactiveFrame = -1;

        backToTop = false;

        //currentMeter += 10;

        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -3.5f, 3f, 0);
        attackbox.transform.localScale = new Vector3(4f, 2f, 5f);

        audioSource.clip = CFAudioClips[0];
        audioSource.Play();

        //throw new NotImplementedException();
    }
    public override void special2Ex()
    {
       // currentMeter -= 100;

        //Prevent repeat
        lastDirection1 = 5;
        lastDirection2 = 5;
        lastDirection3 = 5;

        damageToPass = 50;
        hitstunToPass = 53;
        heightToPass = 'm';
        juggleSpeedToPass = 120;

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 55;
        specialType = 'T';
        anim.Play("Armature|FalconKick", -1, 0f);
        v0 = transform.position;
        startingTime = Time.time;
        activeFrame = 45;
        deactiveFrame = 10;

        backToTop = false;

        //currentMeter += 10;

        //Teleport

        blockbox.SetActive(true);
        //hitbox.transform.localPosition = new Vector3(-0.29f, 2f, 0);
        //hitbox.transform.localScale = new Vector3(3.33f, 4.47f, 5f);
        attackbox.transform.localPosition = new Vector3(direction * -0.15f, 0.13f, 0);
        attackbox.transform.localScale = new Vector3(0.32f, 0.18f, 0.51f);

        audioSource.clip = CFAudioClips[2];
        audioSource.Play();

        //inputH = Input.GetAxisRaw("Horizontal");
        //throw new NotImplementedException();
    }

    public override void special3Ex()
    {
       // currentMeter -= 100;

        //Prevent repeat
        lastDirection1 = 5;
        lastDirection2 = 5;
        lastDirection3 = 5;

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 41;
        specialType = 'H';
        activeFrame = -1;
        deactiveFrame = -1;
        juggleSpeedToPass = 120;
        //Move attack box

        backToTop = false;

        speed = 7200.0f;

        //currentMeter += 10;

        //TODO Blocking? Probably do it in the hadouken itself

        anim.Play("Armature|SpeedPower", -1, 0f);

        audioSource.clip = CFAudioClips[3];
        audioSource.Play();
        //throw new NotImplementedException();
    }

    public override void special1()
    {
        //Prevent repeat
        lastDirection1 = 5;
        lastDirection2 = 5;
        lastDirection3 = 5;

        damageToPass = 50;
        hitstunToPass = 53;
        heightToPass = 'm';
        juggleSpeedToPass = 120;

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 45;
        specialType = 'S';
        anim.Play("Armature|FalconPunch", -1, 0f);
        v0 = transform.position;
        startingTime = Time.time;
        activeFrame = 12;
        deactiveFrame = 5;

        backToTop = false;

        //currentMeter += 10;

        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -0.26f, 0.28f, 0);
        attackbox.transform.localScale = new Vector3(0.45f, 0.3f, 0.46f);
        //inputH = Input.GetAxisRaw("Horizontal");
        //throw new NotImplementedException();

        audioSource.clip = CFAudioClips[0];
        audioSource.Play();
    }

    public override void special2()
    {
        //Prevent repeat
        lastDirection1 = 5;
        lastDirection2 = 5;
        lastDirection3 = 5;

        damageToPass = 50;
        hitstunToPass = 53;
        heightToPass = 'm';
        juggleSpeedToPass = 120;

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 55;
        specialType = 'T';
        anim.Play("Armature|FalconKick", -1, 0f);
        v0 = transform.position;
        startingTime = Time.time;
        activeFrame = 45;
        deactiveFrame = 10;

        backToTop = false;

        //currentMeter += 10;

        blockbox.SetActive(true);

        attackbox.transform.localPosition = new Vector3(direction * -0.15f, 0.13f, 0);
        attackbox.transform.localScale = new Vector3(0.32f, 0.18f, 0.51f);
        //inputH = Input.GetAxisRaw("Horizontal");
        //throw new NotImplementedException();

        audioSource.clip = CFAudioClips[2];
        audioSource.Play();
    }

    public override void special3()
    {
        //Prevent repeat
        lastDirection1 = 5;
        lastDirection2 = 5;
        lastDirection3 = 5;

        

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 45;
        specialType = 'H';
        activeFrame = -1;
        deactiveFrame = -1;
        juggleSpeedToPass = 120;
        //Move attack box

        backToTop = false;

        if (isPlayer1)
        {
            StoredInfoScript.characterInfo.player1Meter += 30;
        }
        else
        {
            StoredInfoScript.characterInfo.player2Meter += 30;
        }


        //TODO Blocking? Probably do it in the hadouken itself

        anim.Play("Armature|SpeedPower", -1, 0f);
        //throw new NotImplementedException();

        audioSource.clip = CFAudioClips[4];
        audioSource.Play();
    }

    public override void standLight()
    {
        damageToPass = 20;
        //hitstunToPass = 20;
        hitstunToPass = 70;
        heightToPass = 'm';
        juggleSpeedToPass = 60;

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 20;
        activeFrame = 13;
        deactiveFrame = 7;

        backToTop = false;

       // currentMeter += 10;

        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -0.12f, 0.37f, 0);
        attackbox.transform.localScale = new Vector3(0.16f, 0.11f, 0.43f);

        anim.Play("Armature|StandLight", -1, 0f);
        //throw new NotImplementedException();
    }

    public override void crouchLight()
    {
        damageToPass = 20;
        hitstunToPass = 20;
        heightToPass = 'l';
        juggleSpeedToPass = 60;

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 20;

        backToTop = false;

       // currentMeter += 10;

        activeFrame = 9;
        deactiveFrame = 5;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -0.19f, 0.04f, 0);
        attackbox.transform.localScale = new Vector3(0.19f, 0.18f, 0.46f);

        anim.Play("Armature|CrouchLight", -1, 0f);
        //throw new NotImplementedException();
    }

    public override void standMedium()
    {
        damageToPass = 35;
        hitstunToPass = 35;
        heightToPass = 'h';
        juggleSpeedToPass = 80;

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 30;

        backToTop = false;

      //  currentMeter += 10;

        activeFrame = 18;
        deactiveFrame = 11;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -0.22f, 0.2f, 0);
        attackbox.transform.localScale = new Vector3(0.18f, 0.36f, 0.89f);

        anim.Play("Armature|StandMedium", -1, 0f);
        //throw new NotImplementedException();
    }

    public override void crouchMedium()
    {
        damageToPass = 35;
        hitstunToPass = 34;
        heightToPass = 'l';
        juggleSpeedToPass = 80;

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 30;

        backToTop = false;

       // currentMeter += 10;

        activeFrame = 23;
        deactiveFrame = 15;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -0.11f, 0.3f, 0);
        attackbox.transform.localScale = new Vector3(0.34f, 0.18f, 1.01f);

        anim.Play("Armature|CrouchMedium", -1, 0f);
        //throw new NotImplementedException();
    }

    public override void standHeavy()
    {
        damageToPass = 50;
        hitstunToPass = 53;
        heightToPass = 'm';
        juggleSpeedToPass = 100;

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 35;

        backToTop = false;

        //currentMeter += 10;

        activeFrame = 21;
        deactiveFrame = 16;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -0.21f, 0.34f, 0);
        attackbox.transform.localScale = new Vector3(0.23f, 0.2f, 0.3f);

        anim.Play("Armature|StandHeavy", -1, 0f);
        //throw new NotImplementedException();
    }

    public override void crouchHeavy()
    {
        damageToPass = 50;
        hitstunToPass = 53;
        heightToPass = 'l';
        juggleSpeedToPass = 100;

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 40;

        backToTop = false;

       // currentMeter += 10;

        activeFrame = 25;
        deactiveFrame = 16;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -0.25f, 0.1f, 0);
        attackbox.transform.localScale = new Vector3(0.5f, 0.18f, 0.26f);

        anim.Play("Armature|CrouchHeavy", -1, 0f);
        //throw new NotImplementedException();
    }

    public override void launcher()
    {
        damageToPass = 50;
        hitstunToPass = 10000;
        heightToPass = 'm';
        juggleSpeedToPass = 120;

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 30;

        backToTop = false;

       // currentMeter += 10;

        activeFrame = 18;
        deactiveFrame = 14;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -0.1f, 0.3f, 0);
        attackbox.transform.localScale = new Vector3(0.13f, 0.14f, 0.65f);

        anim.Play("Armature|Launcher", -1, 0f);
        //throw new NotImplementedException();
    }

    public override void airLight()
    {
        damageToPass = 20;
        hitstunToPass = 30;
        heightToPass = 'h';
        juggleSpeedToPass = 60;

        airAttackRecovery = 20;

        backToTop = false;
       // currentMeter += 10;

        activeFrame = 10;
        deactiveFrame = 5;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -0.19f, 0.14f, 0);
        attackbox.transform.localScale = new Vector3(0.13f, 0.2f, 0.64f);

        anim.Play("Armature|AirLight", -1, 0f);
        //throw new NotImplementedException();
    }

    public override void airMedium()
    {
        damageToPass = 35;
        hitstunToPass = 34;
        heightToPass = 'h';
        juggleSpeedToPass = 80;

        airAttackRecovery = 30;

        backToTop = false;
      //  currentMeter += 10;

        activeFrame = 21;
        deactiveFrame = 14;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -0.07f, 0.31f, 0);
        attackbox.transform.localScale = new Vector3(0.28f, 0.12f, 0.27f);

        anim.Play("Armature|AirMedium", -1, 0f);
        //throw new NotImplementedException();
    }

    public override void airHeavy()
    {
        damageToPass = 50;
        hitstunToPass = 38;
        heightToPass = 'h';
        juggleSpeedToPass = 100;

        airAttackRecovery = 40;

        backToTop = false;
       // currentMeter += 10;

        activeFrame = 26;
        deactiveFrame = 14;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -0.18f, 0.1f, 0);
        attackbox.transform.localScale = new Vector3(0.3f, 0.26f, 0.19f);

        anim.Play("Armature|AirHeavy", -1, 0f);
        //throw new NotImplementedException();
    }

    public override void special1Effects()
    {
        // float resulty = (float)(shoryukenJumpSpeed * (Time.time - startingTime) + 0.5 * gravity * (Time.time - startingTime) * (Time.time - startingTime));
        //float resultx = v0.x + -1 * jumpSideSpeed * (Time.time - startingTime);
        //rb.position = new Vector3(v0.x, resulty, v0.z);

        if (recovery == 1)
        {
            Quaternion fireballDirection = Quaternion.identity;

            if (direction < 0)
            {
                fireballDirection = Quaternion.Euler(new Vector3(0, 180, 0));
            }

            Instantiate(falconPunchProjectile, new Vector3((float)(transform.position.x - (25.63 * direction)), (float)(transform.position.y + 11.96), 0), fireballDirection);
           
        }

        if (recovery == 1)
        {
            audioSource.clip = CFAudioClips[1];
            audioSource.Play();
        }

        //throw new NotImplementedException();
    }

    public override void special2Effects()
    {
        //float resulty = (float)(jumpSpeed * (Time.time - startingTime) + 0.5 * gravity * (Time.time - startingTime) * (Time.time - startingTime));
        // float resultx = (v0.x + -1 * direction * jumpSideSpeed * (Time.time - startingTime));
        //rb.position = new Vector3(resultx, v0.y, v0.z);

        if (recovery == 45)
        {
            Quaternion fireballDirection = Quaternion.identity;

            if (direction < 0)
            {
                fireballDirection = Quaternion.Euler(new Vector3(0, 180, 0));
            }

            if (direction > 0)
            {
                Instantiate(falconKick, new Vector3((float)(transform.position.x + 17.526 - (25.63 * direction)), (float)(transform.position.y - 5.66 + 11.96), 0), fireballDirection);
            }
            else
            {
                Instantiate(falconKick, new Vector3((float)(transform.position.x + 9.59 + 17.526 - (-1 * 25.63 * direction)), (float)(transform.position.y - 5.66 + 11.96), 0), fireballDirection);
            }
        }

        float resultx = (v0.x + -1 * direction * jumpSideSpeed * (Time.time - startingTime));
        rb.position = new Vector3(resultx, v0.y, v0.z);

        //throw new NotImplementedException();
    }

    public override void special3Effects()
    {
       
        //throw new NotImplementedException();
    }

    public override void superEffects()
    {
        if (recovery == 115)
        {
            audioSource.clip = CFAudioClips[7];
            audioSource.Play();

            v0 = transform.position;
            startingTime = Time.time;

            Quaternion fireballDirection = Quaternion.identity;

            if (direction < 0)
            {
                fireballDirection = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            else
            {
                fireballDirection = Quaternion.Euler(new Vector3(0, -90, 0));
            }
            
            Instantiate(blueFalcon, new Vector3((float)(transform.position.x + (100 * direction)), 0, 0), fireballDirection);
        }
        

        if (recovery <= 115 && recovery >= 30)
        {
            float resulty = (float)(blueFalconJumpSpeed * (Time.time - startingTime) + 0.5 * gravity/3.0 * (Time.time - startingTime) * (Time.time - startingTime));
            //float resultx = v0.x + -1 * jumpSideSpeed * (Time.time - startingTime);
            rb.position = new Vector3(v0.x, resulty, v0.z);

            if(resulty >=0)
            {
                //rb.position = new Vector3(v0.x, resulty, v0.z);
                rb.position = new Vector3(rb.position.x, resulty, v0.z);
            }
            else
            {
                //rb.position = new Vector3(v0.x, 0, v0.z);
                rb.position = new Vector3(rb.position.x, 0, v0.z);
            }

            //float resulty = (float)(shoryukenJumpSpeed * (Time.time - startingTime) + 0.5 * gravity * (Time.time - startingTime) * (Time.time - startingTime));
            //float resultx = v0.x + -1 * jumpSideSpeed * (Time.time - startingTime);
            //rb.position = new Vector3(v0.x, resulty, v0.z);
        }
        else
        {
            //rb.position = new Vector3(v0.x, 0, v0.z);
            rb.position = new Vector3(rb.position.x, 0, v0.z);
        }

        //throw new NotImplementedException();
    }

    public override Vector3 getDeathScale()
    {

        //model.transform.localScale = new Vector3(direction * model.transform.localScale.x, model.transform.localScale.y, model.transform.localScale.z);
        audioSource.clip = CFAudioClips[5];
        audioSource.Play();

        return new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);

        //throw new NotImplementedException();
    }
}


