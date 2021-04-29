using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LosePopup : MonoBehaviour
{
    public BBUIView bg;
    public BBUIView popup;
    public BBUIButton btnShop;
    public BBUIButton btnShopSkin;
    public ShopNewPopup shopPopup;
    public ShopCoinPopup shopCoinPopup;
    
    
    public BBUIButton btnReplay;
    public BBUIButton btnSkip;
    
    public BBUIButton btnFreeCoin;
    public BBUIButton btnDailyReward;
    public BBUIButton btnChest;
    public BBUIButton btnFreeHeart;
    public BBUIButton btnShopCoin;
    public BBUIButton btnShopHeart;
    
    public Animator ballAnimator;



    public GameObject lockPopup;
   
    public TextMeshProUGUI txtLevel;
    public TextMeshProUGUI txtFailed;

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
        btnReplay.OnPointerClickCallBack_Completed.AddListener(TouchReplay);
        btnSkip.OnPointerClickCallBack_Completed.AddListener(TouchSkip);
        
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

        ShowBallPreview();
    }

    private void OnDestroy()
    {
        btnReplay.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnSkip.OnPointerClickCallBack_Completed.RemoveAllListeners();
        
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
    public void ShowPopup(int _level) {
        lockPopup.SetActive(true);
        gameObject.SetActive(true);

        txtLevel.text = "LEVEL " + Config.GetLevel();
        if(Config.GetLevel() == 0)
        {
            txtLevel.text = "TUTORIAL";
        }

        FirebaseManager.instance.LogLevelWin(_level);

        StartCoroutine(ShowPopup_IEnumerator());
        
    }

    public IEnumerator ShowPopup_IEnumerator()
    {
        SoundManager.instance.SFX_GameOver();
        bg.gameObject.SetActive(false);
        btnReplay.gameObject.SetActive(false);
        btnSkip.gameObject.SetActive(false);
        btnFreeCoin.gameObject.SetActive(false);
        btnChest.gameObject.SetActive(false);
        btnDailyReward.gameObject.SetActive(false);
        btnFreeHeart.gameObject.SetActive(false);
        btnShopCoin.gameObject.SetActive(false);
        btnShopSkin.gameObject.SetActive(false);
        ballAnimator.gameObject.SetActive(false);
        txtLevel.gameObject.SetActive(false);
        txtFailed.gameObject.SetActive(false);
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
        txtFailed.gameObject.SetActive(true);
        txtFailed.gameObject.GetComponent<BBUIView>().ShowView();

        yield return new WaitForSeconds(0.5f);

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

        yield return new WaitForSeconds(0.5f);
        
        btnSkip.gameObject.SetActive(true);
        btnSkip.GetComponent<BBUIView>().ShowView();

        yield return new WaitForSeconds(1f);
        lockPopup.SetActive(false);

        yield return new WaitForSeconds(3f);
        btnReplay.gameObject.SetActive(true);
        btnReplay.GetComponent<BBUIView>().ShowView();

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
    
    public void TouchReplay() {

        if(!Config.isFinished_AddCoin) return;
        
        if (Config.interstitialAd_countWin % 2 == 0 && AdmobManager.instance.isInterstititalAds_Avaiable())
        {
            AdmobManager.instance.ShowInterstitialAd_CallBack((AdmobManager.ADS_CALLBACK_STATE state) =>
            {
                SetReplayLevel();
            });
        }
        else
        {
            SetReplayLevel();
        }
        Config.interstitialAd_countWin++;
    }

    public void TouchSkip() {
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
                    
                    SetNextLevel();

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
    
    public void SetReplayLevel() {
        GamePlayManager.Ins.SetLoading_In(() =>
        {
            SceneManager.LoadScene("Level" + Config.GetLevel());
            Config.currInfoBall_Try = null;
        });
    }

    public void TouchFreeCoin() {
        if (AdmobManager.instance.isRewardAds_Avaiable())
        {
            lockPopup.gameObject.SetActive(true);
            AdmobManager.instance.ShowRewardAd_CallBack((AdmobManager.ADS_CALLBACK_STATE state) =>
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
            ballAnimator.runtimeAnimatorController = Config.GetInfoBallFromID(Config.GetBallActive()).animatorImgOverrideController;
        }


    }

    private int indexBallPreview;
    private InfoBall infoBallPreview;

    private void InitBallPreview()
    {
        for (int i = 0; i < listIDBallPreviews.Count; i++)
        {
            if (listIDBallPreviews[i] == Config.GetBallActive())
            {
                indexBallPreview = i;
                return;
            }
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
        Config.currInfoBall_Try = Config.GetInfoBallFromID(listIDBallPreviews[indexBallPreview]);
        Config.SetLevel(Config.GetLevel() + 1);
        GamePlayManager.Ins.ShowLoadingNextGame();
    }
    private void TouchActiveBallPreview()
    {
        Config.SetBallActive(listIDBallPreviews[indexBallPreview]);
        
        ShopNewPopup.Ins.SetUpdateListBalls();
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
}
