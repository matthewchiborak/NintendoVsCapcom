using UnityEngine;
using System.Collections;
using System;

public class RyuScript : CharacterScript
{
    public GameObject hadouken;
    public float shoryukenJumpSpeed;
    public GameObject tatsuWind;
    public GameObject superParticle;

    public AudioClip[] ryuAudioClips = new AudioClip[7];

    public override void characterSpecificInit()
    {
        characterName = "Ryu";
        shoryukenJumpSpeed = 90.0f;
        speed = 1800.0F;
        maxHealth = 1000;
        currentHealth = 1000;
    }

    public override void crouchChanges()
    {
        characterbox.transform.localPosition = new Vector3(-1.15f, 5.70f, 0);
        characterbox.transform.localScale = new Vector3(6f, 10.6f, 5f);

        hitbox.transform.localPosition = new Vector3(-0.88f, 6.02f, 0);
        hitbox.transform.localScale = new Vector3(10f, 13.4f, 5f);
    }

    public override void standChanges()
    {
        characterbox.transform.localPosition = new Vector3(0, 8.95f, 0);
        characterbox.transform.localScale = new Vector3(6f, 14.65f, 5f);

        hitbox.transform.localPosition = new Vector3(0f, 9.19f, 0);
        hitbox.transform.localScale = new Vector3(10f, 18.01f, 5f);
    }

    public override void alwaysCheck()
    {

    }

    public override bool superInputCheck()
    {
        return lastDirection4 == 6 && lastDirection2 == 2 && lastDirection1 == 3;
        
    }

    public override bool special11InputCheck()
    {
        return lastDirection4 == 6 && lastDirection2 == 2 && lastDirection1 == 3;
       
    }

    public override bool special12InputCheck()
    {
        return lastDirection3 == 2 && lastDirection2 == 1 && lastDirection1 == 4;
      
    }

    public override bool special13InputCheck()
    {
        return lastDirection3 == 2 && lastDirection2 == 3 && lastDirection1 == 6;
     
    }

    public override void super()
    {
      

        //Prevent repeat
        lastDirection1 = 5;
        lastDirection2 = 5;
        lastDirection3 = 5;

        rb.velocity = new Vector3(0, 0, 0);
       
        recovery = 300;
        specialType = 'U';
        activeFrame = 283;
        deactiveFrame = 280;

        damageToPass = 50;
        hitstunToPass = 90;

        juggleSpeedToPass = 360;

        backToTop = false;

        audioSource.clip = ryuAudioClips[4];
        audioSource.Play();

        attackbox.transform.localPosition = new Vector3(direction * -7.25f, 12.72f, 0);
        attackbox.transform.localScale = new Vector3(5f, 4f, 5f);

        anim.Play("Armature|Super", -1, 0f);

        
    }

    public override void special1Ex()
    {
        

        //Prevent repeat
        lastDirection1 = 5;
        lastDirection2 = 5;
        lastDirection3 = 5;

        damageToPass = 50;
        hitstunToPass = 53;
        heightToPass = 'm';
        juggleSpeedToPass = 240;

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 33;
        specialType = 'S';
        anim.Play("Armature|Shoryuken", -1, 0f);
        v0 = transform.position;
        startingTime = Time.time;
        activeFrame = 29;
        deactiveFrame = 15;

        backToTop = false;

        

        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -5.85f, 15.11f, 0);
        attackbox.transform.localScale = new Vector3(5.98f, 17.72f, 5f);

        //ryuAudioClips
        audioSource.clip = ryuAudioClips[0];
        audioSource.Play();

        
    }
    public override void special2Ex()
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
        anim.Play("Armature|Tatsu", -1, 0f);
        v0 = transform.position;
        startingTime = Time.time;
        activeFrame = 40;
        deactiveFrame = 21;

        backToTop = false;

       

        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(0, 9.73f, 0);
        attackbox.transform.localScale = new Vector3(23.9f, 5.5f, 5f);

        audioSource.clip = ryuAudioClips[1];
        audioSource.Play();
       
    }

    public override void special3Ex()
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
        //Move attack box

        juggleSpeedToPass = 120;

        backToTop = false;

        

        anim.Play("Armature|HadoukenLight", -1, 0f);

        audioSource.clip = ryuAudioClips[2];
        audioSource.Play();
      
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
        recovery = 33;
        specialType = 'S';
        anim.Play("Armature|Shoryuken", -1, 0f);
        v0 = transform.position;
        startingTime = Time.time;
        activeFrame = 29;
        deactiveFrame = 15;

        backToTop = false;

      

        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -5.85f, 15.11f, 0);
        attackbox.transform.localScale = new Vector3(5.98f, 17.72f, 5f);
     

        audioSource.clip = ryuAudioClips[0];
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
        anim.Play("Armature|Tatsu", -1, 0f);
        v0 = transform.position;
        startingTime = Time.time;
        activeFrame = 40;
        deactiveFrame = 21;

        backToTop = false;

   

        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(0, 9.73f, 0);
        attackbox.transform.localScale = new Vector3(23.9f, 5.5f, 5f);
       

        audioSource.clip = ryuAudioClips[1];
        audioSource.Play();
    }

    public override void special3()
    {
        //Prevent repeat
        lastDirection1 = 5;
        lastDirection2 = 5;
        lastDirection3 = 5;

        juggleSpeedToPass = 120;

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 45;
        specialType = 'H';
        activeFrame = -1;
        deactiveFrame = -1;
        //Move attack box

        backToTop = false;

       

        anim.Play("Armature|HadoukenLight", -1, 0f);
        

        audioSource.clip = ryuAudioClips[2];
        audioSource.Play();
    }

    public override void standLight()
    {
        damageToPass = 20;
      
        hitstunToPass = 70;
        heightToPass = 'm';

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 18;
        activeFrame = 13;
        deactiveFrame = 10;

        backToTop = false;
        juggleSpeedToPass = 60;

        

        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -7.25f, 12.72f, 0);
        attackbox.transform.localScale = new Vector3(5.72f, 2.53f, 5f);

        anim.Play("Armature|StandLight", -1, 0f);
        
    }

    public override void crouchLight()
    {
        damageToPass = 20;
        hitstunToPass = 20;
        heightToPass = 'l';

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 18;
        juggleSpeedToPass = 60;

        backToTop = false;

     

        activeFrame = 13;
        deactiveFrame = 11;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -8.31f, 1.54f, 0);
        attackbox.transform.localScale = new Vector3(5.72f, 2.53f, 5f);

        anim.Play("Armature|CrouchLight", -1, 0f);
        
    }

    public override void standMedium()
    {
        damageToPass = 35;
        hitstunToPass = 35;
        heightToPass = 'm';

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 32;
        juggleSpeedToPass = 80;

        backToTop = false;

       

        activeFrame = 24;
        deactiveFrame = 21;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -5.8f, 11.04f, 0);
        attackbox.transform.localScale = new Vector3(5.72f, 2.53f, 5f);

        anim.Play("Armature|StandMedium", -1, 0f);
        
    }

    public override void crouchMedium()
    {
        damageToPass = 35;
        hitstunToPass = 34;
        heightToPass = 'l';

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 30;
        juggleSpeedToPass = 80;

        backToTop = false;

      

        activeFrame = 22;
        deactiveFrame = 19;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -9.07f, 1.54f, 0);
        attackbox.transform.localScale = new Vector3(13.27f, 2.53f, 5f);

        anim.Play("Armature|CrouchMedium", -1, 0f);
      
    }

    public override void standHeavy()
    {
        damageToPass = 50;
        hitstunToPass = 53;
        heightToPass = 'm';

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 34;
        juggleSpeedToPass = 100;

        backToTop = false;

    

        activeFrame = 24;
        deactiveFrame = 21;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -8f, 14.73f, 0);
        attackbox.transform.localScale = new Vector3(5.72f, 2.53f, 5f);

        anim.Play("Armature|StandHeavy", -1, 0f);
       
    }

    public override void crouchHeavy()
    {
        damageToPass = 50;
        hitstunToPass = 53;
        heightToPass = 'l';

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 40;
        juggleSpeedToPass = 100;

        backToTop = false;

     

        activeFrame = 30;
        deactiveFrame = 26;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -9.07f, 2.66f, 0);
        attackbox.transform.localScale = new Vector3(13.27f, 4.91f, 5f);

        anim.Play("Armature|CrouchHeavy", -1, 0f);
      
    }

    public override void launcher()
    {
        damageToPass = 50;
        hitstunToPass = 10000;
        heightToPass = 'm';

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 36;
        juggleSpeedToPass = 120;

        backToTop = false;

      

        activeFrame = 27;
        deactiveFrame = 22;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -5.73f, 3.16f, 0);
        attackbox.transform.localScale = new Vector3(5.5f, 1.31f, 5f);

        anim.Play("Armature|Shoryuken", -1, 0f);
    
    }

    public override void airLight()
    {
        damageToPass = 20;
        hitstunToPass = 30;
        heightToPass = 'h';
        juggleSpeedToPass = 60;

        airAttackRecovery = 24;

        backToTop = false;
   

        activeFrame = 18;
        deactiveFrame = 5;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -7.25f, 12.72f, 0);
        attackbox.transform.localScale = new Vector3(5.72f, 2.53f, 5f);

        anim.Play("Armature|AirLight", -1, 0f);
      
    }

    public override void airMedium()
    {
        damageToPass = 35;
        hitstunToPass = 34;
        heightToPass = 'h';
        juggleSpeedToPass = 80;

        airAttackRecovery = 32;

        backToTop = false;
        

        activeFrame = 23;
        deactiveFrame = 19;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -7.15f, 3.72f, 0);
        attackbox.transform.localScale = new Vector3(10f, 7f, 5f);

        anim.Play("Armature|AirMedium", -1, 0f);
        
    }

    public override void airHeavy()
    {
        damageToPass = 50;
        hitstunToPass = 38;
        heightToPass = 'h';
        juggleSpeedToPass = 100;

        airAttackRecovery = 36;

        backToTop = false;
      

        activeFrame = 27;
        deactiveFrame = 23;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -5.51f, 8.65f, 0);
        attackbox.transform.localScale = new Vector3(5.98f, 7f, 5f);

        anim.Play("Armature|AirHeavy", -1, 0f);
        
    }

    public override void special1Effects()
    {
        float resulty = (float)(shoryukenJumpSpeed * (Time.time - startingTime) + 0.5 * gravity * (Time.time - startingTime) * (Time.time - startingTime));
       
        rb.position = new Vector3(v0.x, resulty, v0.z);
        
    }

    public override void special2Effects()
    {
        if(recovery == 40)
        {
            Quaternion fireballDirection = Quaternion.identity;

            if (direction < 0)
            {
                fireballDirection = Quaternion.Euler(new Vector3(0, 180, 0));
            }

           
            Instantiate(tatsuWind, new Vector3((float)(transform.position.x ), (float)(transform.position.y), 0), fireballDirection);
        }

      
        float resultx = (v0.x + -1 * direction * jumpSideSpeed * (Time.time - startingTime));
        rb.position = new Vector3(resultx, v0.y, v0.z);
      
    }

    public override void special3Effects()
    {
        if (recovery == 34)
        {
            Quaternion fireballDirection = Quaternion.identity;

            if (direction < 0)
            {
                fireballDirection = Quaternion.Euler(new Vector3(0, 180, 0));
            }

            Instantiate(hadouken, new Vector3((float)(transform.position.x - (8.63 * direction)), (float)(transform.position.y + 11.96), 0), fireballDirection);
        }
       
    }

    public override void superEffects()
    {
        if (recovery == 34)
        {
            damageToPass = 1;
            hitstunToPass = 1000;
            attackbox.SetActive(false);
            attackbox.SetActive(true);
            v0 = transform.position;
            startingTime = Time.time;
        }

        if(recovery == 260)
        {
            audioSource.clip = ryuAudioClips[5];
            audioSource.Play();
        }

        if(recovery == 220)
        {
            damageToPass = 50;
            hitstunToPass = 90;
            attackbox.SetActive(false);
            attackbox.SetActive(true);
            audioSource.clip = ryuAudioClips[6];
            audioSource.Play();
        }

        if (recovery == 160)
        {
            damageToPass = 100;
            hitstunToPass = 500;
            attackbox.SetActive(false);
            attackbox.SetActive(true);
            audioSource.clip = ryuAudioClips[0];
            audioSource.Play();
            Instantiate(superParticle, new Vector3((float)(transform.position.x), (float)(transform.position.y + 11.96), 0), Quaternion.identity);
        }

        if (recovery <= 34)
        {
            float resulty = (float)(shoryukenJumpSpeed * (Time.time - startingTime) + 0.5 * gravity * (Time.time - startingTime) * (Time.time - startingTime));
         
            rb.position = new Vector3(v0.x, resulty, v0.z);
        }
       
    }

    public override Vector3 getDeathScale()
    {
        audioSource.clip = ryuAudioClips[3];
        audioSource.Play();

        return new Vector3(direction * model.transform.localScale.x, model.transform.localScale.y, model.transform.localScale.z);

       
    }
}
