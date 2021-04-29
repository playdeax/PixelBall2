using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : ItemBase
{
    public Animator anim;

    public bool isAd = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void CollisionWithBall()
    {
        base.CollisionWithBall();

        SetCollier_Enable(false);

        SoundManager.instance.SFX_CoinCollect();

        if (AdmobManager.instance.isRewardAds_Avaiable() && isAd)
        {
            Time.timeScale = 0f;
            AdmobManager.instance.ShowRewardAd_CallBack((AdmobManager.ADS_CALLBACK_STATE state) =>
            {
                if (state == AdmobManager.ADS_CALLBACK_STATE.SUCCESS)
                {
                    ShowEfx();
                }
                Time.timeScale = 1f;
            });
        }
        else
        {
            ShowEfx();
        }
       
    }

    public void ShowEfx()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.InsertCallback(0.1f, () => {
            anim.SetTrigger("Collected");
            BallManager.Ins.magnetCollider.SetActive(true);
        });
        sequence.InsertCallback(0.8f, () => {
            
        });
      
        sequence.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
