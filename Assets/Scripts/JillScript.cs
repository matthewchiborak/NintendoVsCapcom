using UnityEngine;
using System.Collections;
using System;

public class JillScript : CharacterScript
{
    //public GameObject hadouken;
    //public float shoryukenJumpSpeed;
    //public GameObject tatsuWind;
    //public GameObject superParticle;

    public AudioClip[] jillAudioClips = new AudioClip[7];
    public GameObject standingGun;
    public GameObject airGun;
    public GameObject crouchGun;
    public GameObject knife;
    public GameObject grenadeLaucher;
    public GameObject rocketLauncher;
    public GameObject rocket;

    public GameObject grenadeShell;
    public GameObject zombie;
    private int zombieCooldown;
    public GameObject explosion;

    public GameObject kerosene;

    public GameObject lighterBox;
    public GameObject thrownKerosene;
    

    public override void characterSpecificInit()
    {
        characterName = "Jill";
        //shoryukenJumpSpeed = 90.0f;
        speed = 1800.0F;
        maxHealth = 1000;
        currentHealth = 1000;
        zombieCooldown = 0;
    }

    public override void crouchChanges()
    {
        characterbox.transform.localPosition = new Vector3(0f, 1.76f, 0);
        characterbox.transform.localScale = new Vector3(1.18f, 1.69f, 9f);

        hitbox.transform.localPosition = new Vector3(0f, 1.82f, 0);
        hitbox.transform.localScale = new Vector3(1.95f, 2.75f, 9f);
    }

    public override void standChanges()
    {
        characterbox.transform.localPosition = new Vector3(-0f, 2f, 0);
        characterbox.transform.localScale = new Vector3(0.68f, 2.74f, 9f);

        hitbox.transform.localPosition = new Vector3(-0.08f, 1.9f, 0);
        hitbox.transform.localScale = new Vector3(1.74f, 3.9f, 9f);
    }

    public override void alwaysCheck()
    {
        //Zombie cool down
        if(zombieCooldown > 0)
        {
            zombieCooldown--;
        }

        //If Recover is zero, turn off all weapons
        if (recovery <= 0)
        {
            standingGun.SetActive(false);
            airGun.SetActive(false);
            crouchGun.SetActive(false);
            knife.SetActive(false);
            grenadeLaucher.SetActive(false);
            rocketLauncher.SetActive(false);
        }
    }

    public override bool superInputCheck()
    {
        return lastDirection3 == 2 && lastDirection2 == 3 && lastDirection1 == 6;

    }

    public override bool special11InputCheck()
    {
        return lastDirection3 == 2 && lastDirection2 == 3 && lastDirection1 == 6;

    }

    public override bool special12InputCheck()
    {
        return lastDirection3 == 2 && lastDirection2 == 1 && lastDirection1 == 4;

    }

    public override bool special13InputCheck()
    {
        return lastDirection4 == 6 && lastDirection2 == 2 && lastDirection1 == 3;

    }

    public override void super()
    {


        //Prevent repeat
        lastDirection1 = 5;
        lastDirection2 = 5;
        lastDirection3 = 5;

        rb.velocity = new Vector3(0, 0, 0);

        recovery = 190;
        specialType = 'U';
        activeFrame = -1;
        deactiveFrame = -1;
        heightToPass = 'm';

        damageToPass = 75;
        hitstunToPass = 100;

        juggleSpeedToPass = 100;

        backToTop = false;

        audioSource.clip = jillAudioClips[4];
        audioSource.Play();

        rocketLauncher.SetActive(true);

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
        recovery = 80;
        specialType = 'S';
        anim.Play("Armature|Grenade", -1, 0f);
        v0 = transform.position;
        startingTime = Time.time;
        activeFrame = -1;
        deactiveFrame = -1;

        backToTop = false;

        grenadeLaucher.SetActive(true);


        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -5.85f, 15.11f, 0);
        attackbox.transform.localScale = new Vector3(5.98f, 17.72f, 5f);

       
        //audioSource.clip = jillAudioClips[3];
        //audioSource.Play();


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
        recovery = 40;
        specialType = 'T';
        anim.Play("Armature|Zombie", -1, 0f);
        v0 = transform.position;
        startingTime = Time.time;
        activeFrame = -1;
        deactiveFrame = -1;

        backToTop = false;



        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(0, 9.73f, 0);
        attackbox.transform.localScale = new Vector3(23.9f, 5.5f, 5f);

        audioSource.clip = jillAudioClips[2];
        audioSource.Play();

    }

    public override void special3Ex()
    {


        //Prevent repeat
        lastDirection1 = 5;
        lastDirection2 = 5;
        lastDirection3 = 5;

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 40;
        specialType = 'H';
        activeFrame = -1;
        deactiveFrame = -1;
        //Move attack box

        juggleSpeedToPass = 120;

        backToTop = false;



        anim.Play("Armature|Gas", -1, 0f);

        //audioSource.clip = jillAudioClips[2];
        //audioSource.Play();

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
        recovery = 80;
        specialType = 'S';
        anim.Play("Armature|Grenade", -1, 0f);
        v0 = transform.position;
        startingTime = Time.time;
        activeFrame = -1;
        deactiveFrame = -1;

        backToTop = false;

        grenadeLaucher.SetActive(true);

        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -5.85f, 15.11f, 0);
        attackbox.transform.localScale = new Vector3(5.98f, 17.72f, 5f);


        //audioSource.clip = jillAudioClips[3];
        //audioSource.Play();
    }

    public override void special2()
    {
        //Prevent repeat
        lastDirection1 = 5;
        lastDirection2 = 5;
        lastDirection3 = 5;

        if(zombieCooldown > 0)
        {
            return;
        }

        zombieCooldown = 300;

        damageToPass = 50;
        hitstunToPass = 53;
        heightToPass = 'm';

        juggleSpeedToPass = 120;

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 40;
        specialType = 'T';
        anim.Play("Armature|Zombie", -1, 0f);
        v0 = transform.position;
        startingTime = Time.time;
        activeFrame = -1;
        deactiveFrame = -1;

        backToTop = false;



        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(0, 9.73f, 0);
        attackbox.transform.localScale = new Vector3(23.9f, 5.5f, 5f);


        audioSource.clip = jillAudioClips[2];
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



        anim.Play("Armature|Gas", -1, 0f);


        //audioSource.clip = jillAudioClips[2];
        //audioSource.Play();
    }

    public override void standLight()
    {
        damageToPass = 20;

        hitstunToPass = 70;
        heightToPass = 'm';

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 20;
        activeFrame = 13;
        deactiveFrame = 10;

        backToTop = false;
        juggleSpeedToPass = 60;



        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -0.72f, 2.94f, 0);
        attackbox.transform.localScale = new Vector3(1f, 1f, 5f);

        anim.Play("Armature|StandLight", -1, 0f);

    }

    public override void crouchLight()
    {
        damageToPass = 20;
        hitstunToPass = 20;
        heightToPass = 'l';

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 20;
        juggleSpeedToPass = 60;

        backToTop = false;

        crouchGun.SetActive(true);

        activeFrame = 13;
        deactiveFrame = 11;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -1.56f, 0.63f, 0);
        attackbox.transform.localScale = new Vector3(1f, 1f, 5f);

        anim.Play("Armature|CrouchLight", -1, 0f);

    }

    public override void standMedium()
    {
        damageToPass = 35;
        hitstunToPass = 35;
        heightToPass = 'm';

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 30;
        juggleSpeedToPass = 80;

        backToTop = false;

        knife.SetActive(true);

        activeFrame = 24;
        deactiveFrame = 21;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -1.55f, 2.81f, 0);
        attackbox.transform.localScale = new Vector3(1.87f, 0.83f, 5f);

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
        attackbox.transform.localPosition = new Vector3(direction * -1.39f, 1.39f, 0);
        attackbox.transform.localScale = new Vector3(2.95f, 0.64f, 5f);

        anim.Play("Armature|CrouchMedium", -1, 0f);

    }

    public override void standHeavy()
    {
        damageToPass = 50;
        hitstunToPass = 53;
        heightToPass = 'm';

        rb.velocity = new Vector3(0, 0, 0);
        recovery = 40;
        juggleSpeedToPass = 100;

        backToTop = false;

        standingGun.SetActive(true);

        activeFrame = 30;
        deactiveFrame = 25;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -0.3f, 3.97f, 0);
        attackbox.transform.localScale = new Vector3(1.68f, 2.533f, 5f);

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
        attackbox.transform.localPosition = new Vector3(direction * -1.12f, -0.26f, 0);
        attackbox.transform.localScale = new Vector3(0.5f, 2.16f, 5f);

        Instantiate(lighterBox, new Vector3(transform.position.x + direction * -5.19f, -0.26f, 0), Quaternion.identity);
        //Instantiate(lighterBox, new Vector3(0, -0.26f, 0), Quaternion.identity);

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

        grenadeLaucher.SetActive(true);

        activeFrame = 27;
        deactiveFrame = 22;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -2.54f, 0.65f, 0);
        attackbox.transform.localScale = new Vector3(1.48f, 1.13f, 5f);

        Instantiate(explosion, new Vector3((float)(transform.position.x - (8.54 * direction)), (float)(transform.position.y), 0), Quaternion.identity);


        anim.Play("Armature|Launcher", -1, 0f);

    }

    public override void airLight()
    {
        damageToPass = 20;
        hitstunToPass = 30;
        heightToPass = 'h';
        juggleSpeedToPass = 60;

        airAttackRecovery = 20;

        backToTop = false;


        activeFrame = 18;
        deactiveFrame = 5;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -1.1f, 2.91f, 0);
        attackbox.transform.localScale = new Vector3(1.32f, 0.71f, 5f);

        anim.Play("Armature|AirLight", -1, 0f);

    }

    public override void airMedium()
    {
        damageToPass = 35;
        hitstunToPass = 34;
        heightToPass = 'h';
        juggleSpeedToPass = 80;

        airAttackRecovery = 30;

        backToTop = false;


        activeFrame = 23;
        deactiveFrame = 19;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -1.31f, 1.57f, 0);
        attackbox.transform.localScale = new Vector3(1.2f, 1.45f, 5f);

        anim.Play("Armature|AirMedium", -1, 0f);

    }

    public override void airHeavy()
    {
        damageToPass = 50;
        hitstunToPass = 38;
        heightToPass = 'h';
        juggleSpeedToPass = 100;

        airAttackRecovery = 40;

        backToTop = false;

        airGun.SetActive(true);

        activeFrame = 27;
        deactiveFrame = 23;
        blockbox.SetActive(true);
        attackbox.transform.localPosition = new Vector3(direction * -0.97f, 1.23f, 0);
        attackbox.transform.localScale = new Vector3(1.38f, 2.39f, 5f);

        anim.Play("Armature|AirHeavy", -1, 0f);

    }

    public override void special1Effects()
    {

        if (recovery == 70)
        {
            Quaternion fireballDirection = Quaternion.identity;

            if (direction < 0)
            {
                fireballDirection = Quaternion.Euler(new Vector3(0, 180, 0));
            }

            Instantiate(grenadeShell, new Vector3((float)(transform.position.x - (8.63 * direction)), (float)(transform.position.y + 11.96), 0), fireballDirection);
        }

    }

    public override void special2Effects()
    {

        if (recovery == 30)
        {
            Quaternion fireballDirection = Quaternion.identity;
      
            Instantiate(zombie, new Vector3((float)(transform.position.x - (30.63 * direction)), (float)(transform.position.y), 0), fireballDirection);
        }

    }

    public override void special3Effects()
    {
        if(recovery == 30)
        {
            if (direction > 0)
            {
                Instantiate(thrownKerosene, new Vector3((float)(transform.position.x), (float)(transform.position.y + 11f), 0), Quaternion.Euler(new Vector3(0, 270, 0)));
            }
            else
            {
                Instantiate(thrownKerosene, new Vector3((float)(transform.position.x), (float)(transform.position.y + 11f), 0), Quaternion.Euler(new Vector3(0, 90, 0)));
            }
        }

        if(recovery == 10)
        {
            Instantiate(kerosene, new Vector3((float)(transform.position.x - (8.63 * direction)), (float)(transform.position.y), 0), Quaternion.identity);
        }

    }

    public override void superEffects()
    {
        if (recovery == 189)
        {
            Quaternion fireballDirection = Quaternion.identity;

            if (direction < 0)
            {
                fireballDirection = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            
            //Spawn rocket
            Instantiate(rocket, new Vector3((float)(transform.position.x - (12.63 * direction)), (float)(transform.position.y + 11.96), 0), fireballDirection);

            audioSource.clip = jillAudioClips[1];
            audioSource.Play();
        }
        if (recovery == 160)
        {
            Quaternion fireballDirection = Quaternion.identity;

            if (direction < 0)
            {
                fireballDirection = Quaternion.Euler(new Vector3(0, 180, 0));
            }

            //Spawn rocket
            Instantiate(rocket, new Vector3((float)(transform.position.x - (12.63 * direction)), (float)(transform.position.y + 11.96), 0), fireballDirection);


            audioSource.clip = jillAudioClips[1];
            audioSource.Play();
        }
        if (recovery == 130)
        {
            Quaternion fireballDirection = Quaternion.identity;

            if (direction < 0)
            {
                fireballDirection = Quaternion.Euler(new Vector3(0, 180, 0));
            }

            //Spawn rocket
            Instantiate(rocket, new Vector3((float)(transform.position.x - (12.63 * direction)), (float)(transform.position.y + 11.96), 0), fireballDirection);


            audioSource.clip = jillAudioClips[1];
            audioSource.Play();
        }
        if (recovery == 100)
        {
            Quaternion fireballDirection = Quaternion.identity;

            if (direction < 0)
            {
                fireballDirection = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            

            //Spawn rocket
            Instantiate(rocket, new Vector3((float)(transform.position.x - (12.63 * direction)), (float)(transform.position.y + 11.96), 0), fireballDirection);


            audioSource.clip = jillAudioClips[1];
            audioSource.Play();
        }

        //Get Rid of launcher
        if(recovery == 50)
        {
            rocketLauncher.SetActive(false);
        }

        //Instantiate(superParticle, new Vector3((float)(transform.position.x), (float)(transform.position.y + 11.96), 0), Quaternion.identity);

        

    }

    public override Vector3 getDeathScale()
    {
        audioSource.clip = jillAudioClips[5];
        audioSource.Play();

        return new Vector3(4.5f, 4.5f, 4.5f);


    }
}
