using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    
    public static AudioClip buildSound, buttonSound, buttonClickSound, coinSound, insufficientSound, lvlUpSound, 
        loseSound, stepSound, teleportSound, turretShootSound, zombieSound, zombieAttackSound, zombieDeathSound,
        exp_coinSound, appleSound, stoneSound, woodSound, backgroundSound;
    public static AudioSource audioSrc;

    void Start()
    {
        buildSound = Resources.Load<AudioClip>("Sound/build");
        buttonSound = Resources.Load<AudioClip>("Sound/button");
        buttonClickSound = Resources.Load<AudioClip>("Sound/button_click");
        coinSound = Resources.Load<AudioClip>("Sound/coin");
        insufficientSound = Resources.Load<AudioClip>("Sound/insufficient_materials");
        lvlUpSound = Resources.Load<AudioClip>("Sound/level_up");
        loseSound = Resources.Load<AudioClip>("Sound/lose");
        stepSound = Resources.Load<AudioClip>("Sound/step");
        teleportSound = Resources.Load<AudioClip>("Sound/teleport");
        turretShootSound = Resources.Load<AudioClip>("Sound/turret_shoot");
        zombieSound = Resources.Load<AudioClip>("Sound/zombie");
        zombieAttackSound = Resources.Load<AudioClip>("Sound/zombie_attack");
        zombieDeathSound = Resources.Load<AudioClip>("Sound/zombie_death");
        exp_coinSound = Resources.Load<AudioClip>("Sound/exp_coin");
        appleSound = Resources.Load<AudioClip>("Sound/apple");
        stoneSound = Resources.Load<AudioClip>("Sound/stone");
        woodSound = Resources.Load<AudioClip>("Sound/wood");
        backgroundSound = Resources.Load<AudioClip>("Sound/background");
        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "build":
                audioSrc.PlayOneShot(buildSound);
                break;
            case "button":
                audioSrc.PlayOneShot(buttonSound);
                break;
            case "button_click":
                audioSrc.PlayOneShot(buttonClickSound);
                break;
            case "coin":
                audioSrc.PlayOneShot(coinSound);
                break;
            case "insufficient_materials":
                audioSrc.PlayOneShot(insufficientSound);
                break;
            case "level_up":
                audioSrc.PlayOneShot(lvlUpSound);
                break;
            case "lose":
                audioSrc.PlayOneShot(loseSound);
                break;
            case "step":
                audioSrc.PlayOneShot(stepSound);
                break;
            case "teleport":
                audioSrc.PlayOneShot(teleportSound);
                break;
            case "turret_shoot":
                audioSrc.PlayOneShot(turretShootSound);
                break;
            case "zombie":
                audioSrc.PlayOneShot(zombieSound);
                break;
            case "zombie_attack":
                audioSrc.PlayOneShot(zombieAttackSound);
                break;
            case "zombie_death":
                audioSrc.PlayOneShot(zombieDeathSound);
                break;
            case "exp_coin":
                audioSrc.PlayOneShot(exp_coinSound);
                break;
            case "apple":
                audioSrc.PlayOneShot(appleSound);
                break;
            case "stone":
                audioSrc.PlayOneShot(stoneSound);
                break;
            case "wood":
                audioSrc.PlayOneShot(woodSound);
                break;
            case "background":
                audioSrc.PlayOneShot(backgroundSound);
                break;
            default:
                Debug.Log("Sound clip not found: " + clip);
                break;
        }
    }
    public static void StopMusic()
    {
        audioSrc.Stop();
    }
}
