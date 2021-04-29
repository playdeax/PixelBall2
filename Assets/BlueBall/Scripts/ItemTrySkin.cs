using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrySkin : ItemBase
{
    public List<int> listIDBallTrys = new List<int>();
    public Animator ballAnimator;

    private int idBallTry = -1;
    // Start is called before the first frame update
    void Start()
    {
        InitIDBallTry();

        if (Config.GetBuyIAP(Config.IAP_ID.premium_pack))
        {
            gameObject.SetActive(false);
        }
        else if(Config.currInfoBall_Try != null)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitIDBallTry()
    {
        idBallTry = listIDBallTrys[Random.Range(0, listIDBallTrys.Count)];
        ballAnimator.runtimeAnimatorController = Config.GetInfoBallFromID(idBallTry).animatorOverrideController;
    }
    
    public override void CollisionWithBall()
    {
        base.CollisionWithBall();

        SetCollier_Enable(false);

        SoundManager.instance.SFX_CoinCollect();
        
        if (AdmobManager.instance.isRewardAds_Avaiable())
        {
            Time.timeScale = 0f;
            AdmobManager.instance.ShowRewardAd_CallBack((AdmobManager.ADS_CALLBACK_STATE state) =>
            {
                if (state == AdmobManager.ADS_CALLBACK_STATE.SUCCESS)
                {
                    
                    Config.currInfoBall_Try = Config.GetInfoBallFromID(idBallTry);
                    
                    GameLevelManager.Ins.UpdateTrySkin();
                }
                Time.timeScale = 1f;
            });
        }
        
        Destroy(gameObject);
    }
    
}
