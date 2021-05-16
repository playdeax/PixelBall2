using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Serialization;

public class WinPopup : MonoBehaviour
{
    public BBUIView bg;
    public BBUIView popup;
    public BBUIButton btnShop;
    public BBUIButton btnShopSkin;
    public ShopNewPopup shopPopup;
    public ShopCoinPopup shopCoinPopup;
    
    
    public BBUIButton btnNoThank;
    public BBUIButton btnReward;
    public BBUIButton btnNextLevel;
    public TextMeshProUGUI txtReward;
    public BBUIButton btnFreeCoin;
    public BBUIButton btnDailyReward;
    public BBUIButton btnChest;
    public BBUIButton btnFreeHeart;
    public BBUIButton btnShopCoin;
    public BBUIButton btnShopHeart;
    
    public Animator ballAnimator;
    public ParticleSystem efxWin;



    public GameObject lockPopup;
   
    public TextMeshProUGUI txtLevel;
    public TextMeshProUGUI txtCompeted;

    [Header("NEW SKIN POPUP")]
    public RescuePopup rescuePopup;
    [Header("DAILY REWARD POPUP")]
    public DailyRewardPopup dailyRewardPopup;
    
    public List<int> listIDBallPreviews = new List<int>();
    public BBUIButton btnNextBallPreview;
    public BBUIButton btnBackBallPreview;
    public BBUIButton btnTryBallPreview;
    public BBUIButton btnActiveBallPreview;

    public Transform posCenter;
    public Transform posCoin;
    public Transform posHeart;
    // Start is called before the first frame update
    void Start()
    {
        btnNoThank.OnPointerClickCallBack_Completed.AddListener(TouchNoThank);
        btnReward.OnPointerClickCallBack_Completed.AddListener(TouchReward);
        btnNextLevel.OnPointerClickCallBack_Completed.AddListener(TouchNextLevel);
        
        btnShop.OnPointerClickCallBack_Completed.AddListener(TouchOpenShopPopup);
        btnShopSkin.OnPointerClickCallBack_Completed.AddListener(TouchOpenShopSkin);
        
        btnChest.OnPointerClickCallBack_Completed.AddListener(TouchChest);
        btnDailyReward.OnPointerClickCallBack_Completed.AddListener(TouchDailyReward);
        btnFreeCoin.OnPointerClickCallBack_Completed.AddListener(TouchFreeCoin);
        btnFreeHeart.OnPointerClickCallBack_Completed.AddListener(TouchFreeHeart);
        btnShopCoin.OnPointerClickCallBack_Completed.AddListener(TouchShopCoin);
        btnShopHeart.OnPointerClickCallBack_Completed.AddListener(TouchShopHeart);
        btnNextBallPreview.OnPointerClickCallBack_Completed.AddListener(TouchNextBallPreview);
        btnBackBallPreview.OnPointerClickCallBack_Completed.AddListener(TouchBackBallPreview);
        btnTryBallPreview.OnPointerClickCallBack_Completed.AddListener(TouchTryBallPreview);
        btnActiveBallPreview.OnPointerClickCallBack_Completed.AddListener(TouchActiveBallPreview);
        InitBallPreview();
        ShowBallPreview();
    }

    private void OnDestroy()
    {
        btnNoThank.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnReward.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnNextLevel.OnPointerClickCallBack_Completed.RemoveAllListeners();
        
        btnShop.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnShopSkin.OnPointerClickCallBack_Completed.RemoveAllListeners();
        
        btnChest.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnDailyReward.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnFreeCoin.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnFreeHeart.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnShopCoin.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnShopHeart.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnNextBallPreview.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnBackBallPreview.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnTryBallPreview.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnActiveBallPreview.OnPointerClickCallBack_Completed.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private int coinReward;
    public void ShowPopup(int _coinReward, int _level) {
        lockPopup.SetActive(true);
        coinReward = _coinReward;
        gameObject.SetActive(true);

        txtReward.text = "+" + coinReward;
        txtLevel.text = "LEVEL " + Config.GetLevel();
        if(Config.GetLevel() == 0)
        {
            txtLevel.text = "TUTORIAL";
        }

        FirebaseManager.instance.LogLevelWin(_level);

        StartCoroutine(ShowPopup_IEnumerator(coinReward));
        InitBallPreview();
    }

    public IEnumerator ShowPopup_IEnumerator(int coinReward)
    {
        SoundManager.instance.SFX_Win();
        bg.gameObject.SetActive(false);
        btnNoThank.gameObject.SetActive(false);
        btnReward.gameObject.SetActive(false);
        btnNextLevel.gameObject.SetActive(false);
        btnFreeCoin.gameObject.SetActive(false);
        btnChest.gameObject.SetActive(false);
        btnDailyReward.gameObject.SetActive(false);
        btnFreeHeart.gameObject.SetActive(false);
        btnShopCoin.gameObject.SetActive(false);
        btnShopSkin.gameObject.SetActive(false);
        ballAnimator.gameObject.SetActive(false);
        efxWin.gameObject.SetActive(false);
        txtLevel.gameObject.SetActive(false);
        txtCompeted.gameObject.SetActive(false);
        btnNextBallPreview.gameObject.SetActive(false);
        btnBackBallPreview.gameObject.SetActive(false);
        btnTryBallPreview.gameObject.SetActive(false);
        btnActiveBallPreview.gameObject.SetActive(false);
        btnShop.gameObject.SetActive(false);
        btnShopHeart.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.1f);
        bg.gameObject.SetActive(true);
        bg.ShowView();

        yield return new WaitForSeconds(0.2f);
        txtLevel.gameObject.SetActive(true);
        txtLevel.gameObject.GetComponent<BBUIView>().ShowView();
        txtCompeted.gameObject.SetActive(true);
        txtCompeted.gameObject.GetComponent<BBUIView>().ShowView();

        yield return new WaitForSeconds(0.5f);
        
        if (Config.GetLevel() > Config.MIN_LEVEL_SHOW_REWARD_BUTTON){
            
            btnReward.gameObject.SetActive(true);
            btnReward.GetComponent<BBUIView>().ShowView();
        }

        btnFreeCoin.gameObject.SetActive(true);
        btnFreeCoin.GetComponent<BBUIView>().ShowView();
        if (Config.CheckShowStartPack())
        {
            btnChest.gameObject.SetActive(true);
            btnChest.GetComponent<BBUIView>().ShowView();
        }
        btnFreeHeart.gameObject.SetActive(true);
        btnFreeHeart.GetComponent<BBUIView>().ShowView();
        btnShopSkin.gameObject.SetActive(true);
        btnShopSkin.GetComponent<BBUIView>().ShowView();
        
        btnShopCoin.gameObject.SetActive(true);
        btnShopCoin.GetComponent<BBUIView>().ShowView();
        btnShopHeart.gameObject.SetActive(true);
        btnShopHeart.GetComponent<BBUIView>().ShowView();
        
        yield return new WaitForSeconds(0.2f);
        btnShop.gameObject.SetActive(true);
        btnShop.GetComponent<BBUIView>().ShowView();
       

        yield return new WaitForSeconds(0.2f);
        ballAnimator.gameObject.SetActive(true);
        ballAnimator.GetComponent<BBUIView>().ShowView();
        btnDailyReward.gameObject.SetActive(true);
        btnDailyReward.GetComponent<BBUIView>().ShowView();
        
        btnNextBallPreview.gameObject.SetActive(true);
        btnNextBallPreview.GetComponent<BBUIView>().ShowView();
        
        btnBackBallPreview.gameObject.SetActive(true);
        btnBackBallPreview.GetComponent<BBUIView>().ShowView();
        if (Config.GetBallActive() == listIDBallPreviews[indexBallPreview])
        {
            btnTryBallPreview.gameObject.SetActive(false);
            btnActiveBallPreview.gameObject.SetActive(false);
        }
        else if (Config.GetInfoBallFromID(listIDBallPreviews[indexBallPreview]).ballType == Config.BALL_TYPE.PREMIUM && Config.GetBuyIAP(Config.IAP_ID.premium_pack))
        {
            btnTryBallPreview.gameObject.SetActive(false);
            btnActiveBallPreview.gameObject.SetActive(true);
            btnActiveBallPreview.GetComponent<BBUIView>().ShowView();
        }
        else if (Config.GetInfoBallUnlock(listIDBallPreviews[indexBallPreview]))
        {
            btnTryBallPreview.gameObject.SetActive(false);
            btnActiveBallPreview.gameObject.SetActive(true);
            btnActiveBallPreview.GetComponent<BBUIView>().ShowView();
        }
        else
        {
            btnActiveBallPreview.gameObject.SetActive(false);
            btnTryBallPreview.gameObject.SetActive(true);
            btnTryBallPreview.GetComponent<BBUIView>().ShowView();
        }
        yield return new WaitForSeconds(0.5f);
        SoundManager.instance.SFX_PhaoHoa();
        efxWin.gameObject.SetActive(true);
        efxWin.Play();

        yield return new WaitForSeconds(1f);
        lockPopup.SetActive(false);
        GamePlayManager.Ins.AddCoin(coinReward, posCenter.position, posCoin.position,()=> { 
            
        });

        yield return new WaitForSeconds(3f);
        if (Config.GetLevel() <= Config.MIN_LEVEL_SHOW_INTERSTITIAL)
        {
            btnNextLevel.gameObject.SetActive(true);
            btnNextLevel.GetComponent<BBUIView>().ShowView();
        }
        else
        {
             btnNoThank.gameObject.SetActive(true);
             btnNoThank.GetComponent<BBUIView>().ShowView();
        }

        if (Config.currIDBallRescue != -1)
        {
            rescuePopup.OpenRescuePopup();
        }
        else if (Config.CheckDailyReward()) {
          //  dailyRewardPopup.OpenPopup();
        }
    }


    private void TouchNextLevel()
    {
        SetNextLevel();
    }
    
    public void TouchNoThank() {

        if(!Config.isFinished_AddCoin) return;
        //
        // if (AdmobManager.instance.isInterstititalAds_Avaiable())
        // {
        //     AdmobManager.instance.ShowInterstitialAd_CallBack((AdmobManager.ADS_CALLBACK_STATE state) =>
        //     {
        //         SetNextLevel();
        //     });
        // }
        // else
        // {
            SetNextLevel();
        // }
        // Config.interstitialAd_countWin++;
    }

    public void TouchReward() {
        //SceneManager.LoadScene("Level1");
        if(!Config.isFinished_AddCoin) return;
        if (AdmobManager.instance.isRewardAds_Avaiable())
        {
            lockPopup.gameObject.SetActive(true);
            AdmobManager.instance.ShowRewardAd_CallBack((AdmobManager.ADS_CALLBACK_STATE state) =>
            {
                if (state == AdmobManager.ADS_CALLBACK_STATE.SUCCESS)
                {
                    lockPopup.gameObject.SetActive(false);
                    
                    GamePlayManager.Ins.AddCoin(coinReward, posCenter.position, posCoin.position, () => {
                        btnReward.Interactable = false;
                        SetNextLevel();
                    });

                }
                else
                {
                    lockPopup.gameObject.SetActive(false);
                }
            });
        }
        else
        {
            NotificationPopup.instance.AddNotification("No Video Available!");
        }
        
    }

    public void SetNextLevel() {
        GamePlayManager.Ins.SetLoading_In(() =>
        {
            Config.SetLevel(Config.GetLevel() + 1);
            SceneManager.LoadScene("Level" + Config.GetLevel());
            Config.currInfoBall_Try = null;
        });
    }

    public void TouchFreeCoin() {
        if (AdmobManager.instance.isRewardAds_Avaiable())
        {
            lockPopup.gameObject.SetActive(true);
            AdmobManager.instance.ShowRewardAd_CallBack(state =>
            {
                if (state == AdmobManager.ADS_CALLBACK_STATE.SUCCESS)
                {
                    lockPopup.gameObject.SetActive(false);
                    btnFreeCoin.Interactable = false;
                    GamePlayManager.Ins.AddCoin(Config.FREE_COIN_REWARD, posCenter.position, posCoin.position, () => {
                        btnFreeCoin.Interactable = false;
                    });

                }
                else
                {
                    lockPopup.gameObject.SetActive(false);
                }
            });
        }
        else
        {
            NotificationPopup.instance.AddNotification("No Video Available!");
        }
    }

    public void TouchShop() {
        GamePlayManager.Ins.ShowShopPopup();
    }


    public void TouchDailyReward() {
        Debug.Log("TouchDailyReward");
        dailyRewardPopup.OpenPopup();
    }

    public void TouchChest() {
        GamePlayManager.Ins.OpenStarterPackPopup();
    }

    public void TouchFreeHeart() {
        if (AdmobManager.instance.isRewardAds_Avaiable())
        {
            lockPopup.gameObject.SetActive(true);
            AdmobManager.instance.ShowRewardAd_CallBack((AdmobManager.ADS_CALLBACK_STATE state) =>
            {
                if (state == AdmobManager.ADS_CALLBACK_STATE.SUCCESS)
                {
                    lockPopup.gameObject.SetActive(false);
                    btnFreeHeart.Interactable = false;
                    GamePlayManager.Ins.AddHeart(Config.FREE_HEART_REWARD, posCenter.position, posHeart.position, () => {
                        btnFreeHeart.Interactable = false;
                    });

                }
                else
                {
                    lockPopup.gameObject.SetActive(false);
                }
            });
        }
        else
        {
            NotificationPopup.instance.AddNotification("No Video Available!");
        }
    }
    
    
    
    #region BALL PREVIEW
    public void ShowBallPreview() {
        if (Config.currInfoBall_Try != null)
        {
            ballAnimator.runtimeAnimatorController = Config.currInfoBall_Try.animatorImgOverrideController;
        }
        else {
//            ballAnimator.runtimeAnimatorController = Config.GetInfoBallFromID(Config.GetBallActive()).animatorImgOverrideController;
            ballAnimator.runtimeAnimatorController = Config.GetInfoBallFromID(listIDBallPreviews[indexBallPreview]).animatorImgOverrideController;
        }
    }

    private int indexBallPreview;
    private InfoBall infoBallPreview;

    private void InitBallPreview()
    {
        var lockedIds = new List<int>();
        for (int i = 0; i < listIDBallPreviews.Count; i++)
        {
            if (!Config.GetInfoBallUnlock(listIDBallPreviews[i]))
            {
                lockedIds.Add(listIDBallPreviews[i]);
            }
        }
        
        Debug.Log("lockedIds.Count = "+lockedIds.Count);
        var percent = Random.Range(0, 100f);
        if (lockedIds.Count > 0)
        {
            var randomId = Random.Range(0, lockedIds.Count);
            var index = listIDBallPreviews.IndexOf(lockedIds[randomId]);
            indexBallPreview = index;
        }
        else
        {
            var index = listIDBallPreviews.IndexOf(Config.GetBallActive());
            indexBallPreview = index;
        }
    }
    private void TouchNextBallPreview()
    {
        indexBallPreview++;
        if (indexBallPreview == listIDBallPreviews.Count) indexBallPreview = 0;
        
        StartCoroutine(SetChangeBallPreview(listIDBallPreviews[indexBallPreview]));
    }

    private void TouchBackBallPreview()
    {
        indexBallPreview--;
        if (indexBallPreview == -1) indexBallPreview = listIDBallPreviews.Count -1;
        
        StartCoroutine(SetChangeBallPreview(listIDBallPreviews[indexBallPreview]));
    }

    private IEnumerator SetChangeBallPreview(int idBall)
    {
        ballAnimator.gameObject.GetComponent<BBUIView>().HideView();
        yield return new WaitForSeconds(0.3f);
        ShowBallPreview_ID(idBall);
        ballAnimator.gameObject.GetComponent<BBUIView>().ShowView();

        if (Config.GetBallActive() == idBall)
        {
            btnTryBallPreview.gameObject.SetActive(false);
            btnActiveBallPreview.gameObject.SetActive(false);
        }
        else if (Config.GetInfoBallFromID(idBall).ballType == Config.BALL_TYPE.PREMIUM && Config.GetBuyIAP(Config.IAP_ID.premium_pack))
        {
            btnTryBallPreview.gameObject.SetActive(false);
            btnActiveBallPreview.gameObject.SetActive(true);
            btnActiveBallPreview.GetComponent<BBUIView>().ShowView();
        }
        else if (Config.GetInfoBallUnlock(idBall))
        {
            btnTryBallPreview.gameObject.SetActive(false);
            btnActiveBallPreview.gameObject.SetActive(true);
            btnActiveBallPreview.GetComponent<BBUIView>().ShowView();
        }
        else
        {
            btnActiveBallPreview.gameObject.SetActive(false);
            btnTryBallPreview.gameObject.SetActive(true);
            btnTryBallPreview.GetComponent<BBUIView>().ShowView();
        }
    }

    private void ShowBallPreview_ID(int idBall)
    {
        ballAnimator.runtimeAnimatorController = Config.GetInfoBallFromID(idBall).animatorImgOverrideController;
    }

    private void TouchTryBallPreview()
    {
        if (AdmobManager.instance.isRewardAds_Avaiable())
        {
            lockPopup.gameObject.SetActive(true);
            AdmobManager.instance.ShowRewardAd_CallBack((AdmobManager.ADS_CALLBACK_STATE state) =>
            {
                if (state == AdmobManager.ADS_CALLBACK_STATE.SUCCESS)
                {
                    lockPopup.gameObject.SetActive(false);
                    Config.currInfoBall_Try = Config.GetInfoBallFromID(listIDBallPreviews[indexBallPreview]);
                    Config.SetLevel(Config.GetLevel() + 1);
                    GamePlayManager.Ins.ShowLoadingNextGame();

                }
                else
                {
                    lockPopup.gameObject.SetActive(false);
                }
            });
        }
        else
        {
            NotificationPopup.instance.AddNotification("No Video Available!");
        }
        
    }
    private void TouchActiveBallPreview()
    {
        btnActiveBallPreview.gameObject.SetActive(false);
        Config.SetBallActive(listIDBallPreviews[indexBallPreview]);
        
        if (ShopNewPopup.Ins != null && ShopNewPopup.Ins.isActiveAndEnabled)
        {
            ShopNewPopup.Ins.SetUpdateListBalls();
        }
    }

    #endregion
    
    private void TouchOpenShopPopup()
    {
        shopCoinPopup.ShowPopup();
    }
    
    private void TouchOpenShopSkin()
    {
        shopPopup.ShowPopup();
    }
    
    private void TouchShopCoin()
    {
        shopCoinPopup.ShowPopup();
    }

    private void TouchShopHeart()
    {
        shopCoinPopup.ShowPopup();
    }
    public void ReInitBallPreview()
    {
        InitBallPreview();
        ShowBallPreview();
    }
}
