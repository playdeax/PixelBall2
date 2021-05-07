using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource audioSound;
    public AudioSource audioSoundLoop;
    private void Awake()
    {
        instance = this;
        audioSound = transform.GetComponent<AudioSource>();
        DontDestroyOnLoad(this);

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StopPlay()
    {
        if (Config.isSound)
        {
            audioSound.Stop();
        }
    }

    public void StopSoundLoop()
    {
        if (Config.isSound)
        {
            audioSoundLoop.Stop();
        }
    }

    [Header("JumpLoxo")]
    public AudioClip jumpLoxo;


    public void SFX_Loxo()
    {
        if (Config.isSound)
        {
            audioSound.PlayOneShot(jumpLoxo);
        }
    }

    [Header("OpenDoor")]
    public AudioClip openDoor;


    public void SFX_OpenDoor()
    {
        if (Config.isSound)
        {
            audioSound.PlayOneShot(openDoor);
        }
    }
    [Header("Enemy Pock")]
    public AudioClip enemyPock;


    public void SFX_EnemyPock()
    {
        if (Config.isSound)
        {
            audioSound.PlayOneShot(enemyPock);
        }
    }

    [Header("Jump")]
    public AudioClip jump;


    public void SFX_Jump()
    {
        if (Config.isSound)
        {
            audioSound.PlayOneShot(jump);
        }
    }

    [Header("Bite")]
    public AudioClip bite;


    public void SFX_Bite()
    {
        if (Config.isSound)
        {
            audioSound.PlayOneShot(bite);
        }
    }


    [Header("Hurt")]
    public AudioClip hurt;


    public void SFX_Hurt()
    {
        if (Config.isSound)
        {
            audioSound.PlayOneShot(hurt);
        }
    }

    [Header("StarCollect")]
    public AudioClip starCollect;


    public void SFX_StarCollect()
    {
        if (Config.isSound)
        {
            audioSound.PlayOneShot(starCollect);
        }
    }

    [Header("CheckPoint")]
    public AudioClip checkPoint;

    public void SFX_CheckPoint()
    {
        if (Config.isSound)
        {
            audioSound.PlayOneShot(checkPoint);
        }
    }

    [Header("CoinCollect")]
    public AudioClip coinCollect;


    public void SFX_CoinCollect()
    {
        if (Config.isSound)
        {
            audioSound.PlayOneShot(coinCollect);
        }
    }

    [Header("CoinCollect2")]
    public AudioClip coinCollect2;


    public void SFX_CoinCollect2()
    {
        if (Config.isSound)
        {
            audioSound.PlayOneShot(coinCollect2);
        }
    }


    [Header("Phao Hoa")]
    public AudioClip phaoHoa;
    public void SFX_PhaoHoa()
    {
        if (Config.isSound)
        {
            audioSound.PlayOneShot(phaoHoa);
        }
    }

    [Header("Win")]
    public AudioClip win;
    public void SFX_Win()
    {
        if (Config.isSound)
        {
            audioSound.PlayOneShot(win);
        }
    }

    [Header("Win")]
    public AudioClip cash;
    public void SFX_Cash()
    {
        if (Config.isSound)
        {
            audioSound.PlayOneShot(cash);
        }
    }

    [Header("Notification")]
    public AudioClip notification;
    public void SFX_Notification()
    {
        if (Config.isSound)
        {
            audioSound.PlayOneShot(notification);
        }
    }

    [Header("Popup")]
    public AudioClip openPopup;
    public void SFX_OpenPopup()
    {
        if (Config.isSound)
        {
            audioSound.PlayOneShot(openPopup);
        }
    }

    [Header("Click")]
    public AudioClip click;


    public void PlaySound_Click()
    {
        if (Config.isSound)
        {
            audioSound.PlayOneShot(click);
        }
    }

    [Header("Game Over")]
    public AudioClip gameOver;


    public void SFX_GameOver()
    {
        if (Config.isSound)
        {
            audioSound.PlayOneShot(gameOver);
        }
    }

    [Header("BALL Explosion")]
    public AudioClip ballExplosion;
    public void SFX_BallExplosion()
    {
        if (Config.isSound)
        {
            audioSound.PlayOneShot(ballExplosion);
        }
    }
    [Header("Boss 1")]
    public AudioClip boss1Attack;
    public void SFX_Boss1_Attack()
    {
        if (Config.isSound)
        {
            audioSound.PlayOneShot(boss1Attack);
        }
    }

}
