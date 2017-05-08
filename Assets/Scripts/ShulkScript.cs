using UnityEngine;
using System.Collections;
using System;

public class ShulkScript : CharacterScript
{
    

    public GameObject tornado;
    public AudioClip[] shulkAudioClips = new AudioClip[8];

    public override void characterSpecificInit()
    {
        characterName = "Shulk";
        speed = 1800.0F;
        maxHealth = 1000;
        currentHealth = 1000;
        //throw new NotImplementedException();
    }

    public override void crouchChanges()
    {
        characterbox.transform.localPosition = new Vector3(0f, 1.9f, 0);
        characterbox.transform.localScale = new Vector3(2f, 3.53f, 3f);

        hitbox.transform.localPosition = new Vector3(-0.29f, 2f, 0);
        hitbox.transform.localScale = new Vector3(3.33f, 4.47f, 5f);
    }

    public override void standChanges()
    {
        characterbox.transform.localPosition = new Vector3(0, 3f, 0);
        characterbox.transform.localScale = new Vector3(1.72f, 3.66f, 3f);

        hitbox.transform.localPosition = new Vector3(0f, 3f, 0);
        hitbox.transform.localScale = new Vector3(3.33f, 6f, 5f);
    }

    public override void alwaysCheck()
    {

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
        return lastDirection4 == 6 && lastDirection2 == 2 && lastDirection1 == 3;
        //return lastDirection3 == 2 && lastDirection2 == 1 && lastDirection1 == 4;
        //throw new NotImplementedException();
    }

    public override bool special13InputCheck()
    {
        return lastDirection3 == 2 && lastDirection2 == 1 && lastDirection1 == 4;
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
        //recovery = 310;
        recovery = 190;
        specialType = 'U';
        activeFrame = 170;
        deactiveFrame = 20;
        juggleSpeedToPass = 45;

        damageToPass = 25;
        hitstunToPass = 1000;

        backToTop = false;
        

        attackbox.transform.localPosition = new Vector3(0f, 0f, 0);
        attackbox.transform.localScale = new Vector3(10f, 40f, 5f);

        anim.Play("Armature|Super", -1, 0f);

        audioSource.clip = shulkAudioClips[0];
        audioSource.Play();

        //throw new NotImplementedException();
    }

    public override void special1Ex()
    {
        //Burn meter
        //currentMeter -= 100;

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
        anim.Play("Armature|StreamEdge", -1, 0f);
        v0 = transform.position;
        startingTime = Time.time;
        activeFrame = 20;
        deactiveFrame = 10;

        backToTop = false;

        //currentMeter += 10;

        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -3.5f, 3f, 0);
        attackbox.transform.localScale = new Vector3(4f, 2f, 5f);

        audioSource.clip = shulkAudioClips[1];
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
        recovery = 45;
        specialType = 'T';
        anim.Play("Armature|ShadowEye", -1, 0f);
        v0 = transform.position;
        startingTime = Time.time;
        activeFrame = 15;
        deactiveFrame = 14;

        backToTop = false;

        //currentMeter += 10;

        //Teleport

        blockbox.SetActive(true);
        //hitbox.transform.localPosition = new Vector3(-0.29f, 2f, 0);
        //hitbox.transform.localScale = new Vector3(3.33f, 4.47f, 5f);
        attackbox.transform.localPosition = new Vector3(-0.29f, 2f, 0);
        attackbox.transform.localScale = new Vector3(3.33f, 4.47f, 5f);
        //inputH = Input.GetAxisRaw("Horizontal");
        //throw new NotImplementedException();
        Instantiate(switchEffect, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));

        audioSource.clip = shulkAudioClips[2];
        audioSource.Play();
    }

    public override void special3Ex()
    {
        //currentMeter -= 100;

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

        //currentHealth += 200;
        //if(currentHealth > maxHealth)
        //{
        //    currentHealth = maxHealth;
        //}

        //StoredInfoScript.characterInfo.player1Meter += 30;
        if (isPlayer1)
        {
            StoredInfoScript.characterInfo.player1Health[0] += 0.1f;
            if(StoredInfoScript.characterInfo.player1Health[0] > 1)
            {
                StoredInfoScript.characterInfo.player1Health[0] = 1;
            }
        }
        else
        {
            StoredInfoScript.characterInfo.player2Health[0] += 0.1f;
            if (StoredInfoScript.characterInfo.player2Health[0] > 1)
            {
                StoredInfoScript.characterInfo.player2Health[0] = 1;
            }
        }

        //currentMeter += 10;

        //TODO Blocking? Probably do it in the hadouken itself

        anim.Play("Armature|BattleSoul", -1, 0f);

        audioSource.clip = shulkAudioClips[3];
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
        recovery = 40;
        specialType = 'S';
        anim.Play("Armature|StreamEdge", -1, 0f);
        v0 = transform.position;
        startingTime = Time.time;
        activeFrame = 20;
        deactiveFrame = 10;

        backToTop = false;

        //currentMeter += 10;

        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -3.5f, 3f, 0);
        attackbox.transform.localScale = new Vector3(4f, 2f, 5f);
        //inputH = Input.GetAxisRaw("Horizontal");
        //throw new NotImplementedException();

        audioSource.clip = shulkAudioClips[4];
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
        recovery = 53;
        specialType = 'T';
        anim.Play("Armature|ShadowEye", -1, 0f);
        v0 = transform.position;
        startingTime = Time.time;
        activeFrame = -1;
        deactiveFrame = -1;

        backToTop = false;
        Instantiate(switchEffect, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));

        //currentMeter += 10;

        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(0, 9.73f, 0);
        attackbox.transform.localScale = new Vector3(8f, 1.83f, 5f);

        audioSource.clip = shulkAudioClips[2];
        audioSource.Play();
        //inputH = Input.GetAxisRaw("Horizontal");
        //throw new NotImplementedException();
    }

    public override void special3()
    {
        //Prevent repeat
        lastDirection1 = 5;
        lastDirection2 = 5;
        lastDirection3 = 5;

        //if(currentHealth <= 100)
        //{
        //    return;
        //}
        if((isPlayer1 && StoredInfoScript.characterInfo.player1Health[0] <= 0.1) || (!isPlayer1 && StoredInfoScript.characterInfo.player2Health[0] <= 0.1))
        {
            return;
        }

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 45;
        specialType = 'H';
        activeFrame = -1;
        deactiveFrame = -1;
        juggleSpeedToPass = 120;
        //Move attack box

        backToTop = false;

        //currentMeter += 100;

        //currentHealth -= 100;
        if(isPlayer1)
        {
            StoredInfoScript.characterInfo.player1Health[0] -= 0.1f;
            StoredInfoScript.characterInfo.player1Meter += 80;
        }
        else
        {
            StoredInfoScript.characterInfo.player2Health[0] -= 0.1f;
            StoredInfoScript.characterInfo.player2Meter += 80;
        }
        


        //TODO Blocking? Probably do it in the hadouken itself

        audioSource.clip = shulkAudioClips[5];
        audioSource.Play();

        anim.Play("Armature|BattleSoul", -1, 0f);
        //throw new NotImplementedException();
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
        activeFrame = 9;
        deactiveFrame = 4;

        backToTop = false;

        //currentMeter += 10;

        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -1.5f, 4f, 0);
        attackbox.transform.localScale = new Vector3(2f, 1f, 5f);

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

        //currentMeter += 10;

        activeFrame = 10;
        deactiveFrame = 5;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -2.5f, 1f, 0);
        attackbox.transform.localScale = new Vector3(2f, 2f, 5f);

        anim.Play("Armature|CrouchLight", -1, 0f);
        //throw new NotImplementedException();
    }

    public override void standMedium()
    {
        damageToPass = 35;
        hitstunToPass = 35;
        heightToPass = 'm';
        juggleSpeedToPass = 80;

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 30;

        backToTop = false;

        //currentMeter += 10;

        activeFrame = 24;
        deactiveFrame = 14;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -3f, 3.48f, 0);
        attackbox.transform.localScale = new Vector3(4.34f, 1f, 5f);

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

        //currentMeter += 10;

        activeFrame = 22;
        deactiveFrame = 15;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -1.69f, 1.54f, 0);
        attackbox.transform.localScale = new Vector3(5f, 1f, 5f);

        anim.Play("Armature|CrouchMedium", -1, 0f);
        //throw new NotImplementedException();
    }

    public override void standHeavy()
    {
        damageToPass = 50;
        hitstunToPass = 53;
        heightToPass = 'h';
        juggleSpeedToPass = 100;

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 36;

        backToTop = false;

       // currentMeter += 10;

        activeFrame = 19;
        deactiveFrame = 14;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -5f, 6.79f, 0);
        attackbox.transform.localScale = new Vector3(4.89f, 12.88f, 5f);

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

        //currentMeter += 10;

        activeFrame = 20;
        deactiveFrame = 19;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -7.45f, 1.29f, 0);
        attackbox.transform.localScale = new Vector3(4.42f, 2.48f, 5f);

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

        //currentMeter += 10;

        activeFrame = 18;
        deactiveFrame = 14;
        blockbox.SetActive(true);
        //attackbox.transform.localPosition = new Vector3(direction * -2.6f, 6.21f, 0);
        //attackbox.transform.localPosition = new Vector3(direction * -2.6f, 3.81f, 0);
        //attackbox.transform.localScale = new Vector3(1.83f, 3.56f, 5f);
        attackbox.transform.localPosition = new Vector3(direction * -1.59f, 3.61f, 0);
        attackbox.transform.localScale = new Vector3(1.13f, 1.31f, 5f);

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

        activeFrame = 11;
        deactiveFrame = 6;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -1.53f, 2.75f, 0);
        attackbox.transform.localScale = new Vector3(1f, 1.86f, 5f);

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

        activeFrame = 22;
        deactiveFrame = 14;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -2f, 4.39f, 0);
        attackbox.transform.localScale = new Vector3(3.33f, 2.33f, 5f);

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

        activeFrame = 30;
        deactiveFrame = 20;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -5.09f, 1.03f, 0);
        attackbox.transform.localScale = new Vector3(5.15f, 2f, 5f);

        anim.Play("Armature|AirHeavy", -1, 0f);
        //throw new NotImplementedException();
    }

    public override void special1Effects()
    {
       // float resulty = (float)(shoryukenJumpSpeed * (Time.time - startingTime) + 0.5 * gravity * (Time.time - startingTime) * (Time.time - startingTime));
        //float resultx = v0.x + -1 * jumpSideSpeed * (Time.time - startingTime);
        //rb.position = new Vector3(v0.x, resulty, v0.z);



        //throw new NotImplementedException();
    }

    public override void special2Effects()
    {
        //float resulty = (float)(jumpSpeed * (Time.time - startingTime) + 0.5 * gravity * (Time.time - startingTime) * (Time.time - startingTime));
        // float resultx = (v0.x + -1 * direction * jumpSideSpeed * (Time.time - startingTime));
        //rb.position = new Vector3(resultx, v0.y, v0.z);

        if (recovery == 15)
        {
            transform.localPosition = new Vector3(transform.localPosition.x + (-1 * direction * 50), 0, 0);
            Instantiate(switchEffect, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
            //Instantiate(hadouken, new Vector3((float)(transform.position.x - (8.63 * direction)), (float)(transform.position.y + 11.96), 0), fireballDirection);
        }

        //throw new NotImplementedException();
    }

    public override void special3Effects()
    {
        
        //throw new NotImplementedException();
    }

    public override void superEffects()
    {
        if (recovery == 170)
        {
            v0 = transform.position;
            startingTime = Time.time;
            Instantiate(tornado, new Vector3((transform.position.x), 0, 0), Quaternion.Euler(new Vector3(90, 0, 0)));
        }

        if(recovery < 170 && recovery%8 == 0)
        {
            attackbox.SetActive(false);
            attackbox.SetActive(true);
        }

        if(recovery < 80)
        {
            juggleSpeedToPass = 360;
        }

        if (recovery <= 34)
        {
            //float resulty = (float)(shoryukenJumpSpeed * (Time.time - startingTime) + 0.5 * gravity * (Time.time - startingTime) * (Time.time - startingTime));
            //float resultx = v0.x + -1 * jumpSideSpeed * (Time.time - startingTime);
            //rb.position = new Vector3(v0.x, resulty, v0.z);
        }
        //throw new NotImplementedException();
    }

    public override Vector3 getDeathScale()
    {
        audioSource.clip = shulkAudioClips[6];
        audioSource.Play();

        return new Vector3(direction * model.transform.localScale.x, model.transform.localScale.y, model.transform.localScale.z);

        //throw new NotImplementedException();
    }


}

