using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.Serialization;

public class HomeManager : MonoBehaviour
{
    public static HomeManager Ins;
    private void Awake()
    {
        Ins = this;
    }
    public BBUIButton btnPlay;
    public BBUIButton btnShop;
    public BBUIButton btnShopSkin;
    public ShopNewPopup shopPopup;
    public Animator ballAnimator;

    public BBUIButton btnFreeCoin;
    public BBUIButton btnDailyReward;
    public BBUIButton btnChest;
    // public BBUIButton btnBall;
    public BBUIButton btnFreeHeart;
    public BBUIButton btnShopCoin;
    public BBUIButton btnShopHeart;
    public Transform posAddCoin;
    public Transform posAddHeart;

    public TextMeshProUGUI txtLevel;
    
    public List<int> listIDBallPreviews = new List<int>();
    public BBUIButton btnNextBallPreview;
    public BBUIButton btnBackBallPreview;
    public BBUIButton btnTryBallPreview;
    public BBUIButton btnActiveBallPreview;

    public GameObject lockObj;
    public SceneTransitionController sceneTransitionController;
    // Start is called before the first frame update


    void Start()
    {
        sceneTransitionController.gameObject.SetActive(false);
        btnPlay.OnPointerClickCallBack_Completed.AddListener(TouchPlay);
        btnShop.OnPointerClickCallBack_Completed.AddListener(TouchOpenShopPopup);
        btnShopSkin.OnPointerClickCallBack_Completed.AddListener(TouchOpenShopSkin);
        txtLevel.text = "LEVEL " + Config.GetLevel();
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
        StartCoroutine(Start_IEnumerator());
        if (Config.currLevel == 0)
        {
            SceneManager.LoadSceneAsync("Level0");
        }
    }

    public IEnumerator Start_IEnumerator() {
        btnPlay.gameObject.SetActive(false);
        btnShop.gameObject.SetActive(false);
        btnShopSkin.gameObject.SetActive(false);
        ballAnimator.gameObject.SetActive(false);
        btnFreeCoin.gameObject.SetActive(false);
        btnDailyReward.gameObject.SetActive(false);
        btnChest.gameObject.SetActive(false);
        btnShopHeart.gameObject.SetActive(false);
        btnFreeHeart.gameObject.SetActive(false);
        btnShopCoin.gameObject.SetActive(false);
        txtLevel.gameObject.SetActive(false);
        btnNextBallPreview.gameObject.SetActive(false);
        btnBackBallPreview.gameObject.SetActive(false);
        btnTryBallPreview.gameObject.SetActive(false);
        btnActiveBallPreview.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.1f);
        txtLevel.gameObject.SetActive(true);
        txtLevel.gameObject.GetComponent<BBUIView>().ShowView();

        yield return new WaitForSeconds(0.5f);
        btnFreeCoin.gameObject.SetActive(true);
        btnFreeCoin.GetComponent<BBUIView>().ShowView();
        if (Config.CheckShowStartPack())
        {
            btnChest.gameObject.SetActive(true);
            btnChest.GetComponent<BBUIView>().ShowView();
        }
        btnShopCoin.gameObject.SetActive(true);
        btnShopCoin.GetComponent<BBUIView>().ShowView();
        btnShopHeart.gameObject.SetActive(true);
        btnShopHeart.GetComponent<BBUIView>().ShowView();
        btnFreeHeart.gameObject.SetActive(true);
        btnFreeHeart.GetComponent<BBUIView>().ShowView();
        btnShopSkin.gameObject.SetActive(true);
        btnShopSkin.GetComponent<BBUIView>().ShowView();

        yield return new WaitForSeconds(0.2f);
        btnShop.gameObject.SetActive(true);
        btnShop.GetComponent<BBUIView>().ShowView();
        ballAnimator.gameObject.SetActive(true);
        ballAnimator.GetComponent<BBUIView>().ShowView();
        /*btnDailyReward.gameObject.SetActive(true);
        btnDailyReward.GetComponent<BBUIView>().ShowView();*/
        btnPlay.gameObject.SetActive(true);
        btnPlay.GetComponent<BBUIView>().ShowView();
        
        btnNextBallPreview.gameObject.SetActive(true);
        btnNextBallPreview.GetComponent<BBUIView>().ShowView();
        
        btnBackBallPreview.gameObject.SetActive(true);
        btnBackBallPreview.GetComponent<BBUIView>().ShowView();


        if (Config.CheckDailyReward())
        {
            OpenDailyRewardPopup();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public int forceLevel = 0;
    private void TouchPlay() {
        if(!Config.isFinished_AddCoin) return;
        
        ShowLoadingToGame();

    }


    private void ShowLoadingToGame()
    {
        sceneTransitionController.gameObject.SetActive(true);
        sceneTransitionController.ShowLoading_In(() =>
        {
            if (forceLevel > 0)
            {
                SceneManager.LoadScene("Level" + forceLevel);
            }
            else
            {
                SceneManager.LoadScene("Level" + Config.GetLevel());
            }
        });
    }


    private void TouchOpenShopPopup()
    {
        
    }
    
    private void TouchOpenShopSkin()
    {
        shopPopup.ShowPopup();
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
        ShowLoadingToGame();
    }
    private void TouchActiveBallPreview()
    {
        Config.SetBallActive(listIDBallPreviews[indexBallPreview]);
        
        ShopNewPopup.Ins.SetUpdateListBalls();
    }

    #endregion

    public void TouchChest() {
        OpenStarterPackPopup();
    }

    public void TouchDailyReward() {
        OpenDailyRewardPopup();
    }


    public void TouchFreeCoin() {
        if (AdmobManager.instance.isRewardAds_Avaiable())
        {
            lockObj.gameObject.SetActive(true);
            AdmobManager.instance.ShowRewardAd_CallBack((AdmobManager.ADS_CALLBACK_STATE state) =>
            {
                if (state == AdmobManager.ADS_CALLBACK_STATE.SUCCESS)
                {
                    lockObj.gameObject.SetActive(false);
                    btnFreeCoin.Interactable = false;
                    AddCoin(Config.FREE_COIN_REWARD, ballAnimator.transform.position, posAddCoin.position, () => {
                        btnFreeCoin.Interactable = false;
                    });

                }
                else
                {
                    lockObj.gameObject.SetActive(false);
                }
            });
        }
        else
        {
            NotificationPopup.instance.AddNotification("No Video Avaiable!");
        }
    }

    public void TouchFreeHeart() {
        Debug.Log("TouchFreeHeart");
        if (AdmobManager.instance.isRewardAds_Avaiable())
        {
            lockObj.gameObject.SetActive(true);
            AdmobManager.instance.ShowRewardAd_CallBack((AdmobManager.ADS_CALLBACK_STATE state) =>
            {
                if (state == AdmobManager.ADS_CALLBACK_STATE.SUCCESS)
                {
                    Debug.Log("AddHeartAddHeartAddHeartAddHeartAddHeart");
                    lockObj.gameObject.SetActive(false);
                    btnFreeHeart.Interactable = false;
                    AddHeart(Config.FREE_HEART_REWARD, ballAnimator.transform.position, posAddHeart.transform.position, () => {
                        btnFreeHeart.Interactable = false;
                    });

                }
                else
                {
                    lockObj.gameObject.SetActive(false);
                }
            });
        }
        else
        {
            NotificationPopup.instance.AddNotification("No Video Avaiable!");
        }
    }


    #region ADD COIN
    public ItemCoinReward itemCoinRewardPrefab;
    public Transform coinTranfromParent;

    int indexAddCoin = 0;
    int countIndexAddCoin = 0;

    public Action AddCoin_CallBack = delegate () { };
    public void AddCoin(int _countAddCoin, Vector3 posStart, Vector3 posEnd, Action _addCoin_CallBack)
    {
        AddCoin_CallBack = _addCoin_CallBack;
        indexAddCoin = 0;
        float valueReward = 100f;
        countIndexAddCoin = Mathf.RoundToInt(_countAddCoin / valueReward);
        if (countIndexAddCoin > 25)
        {
            countIndexAddCoin = 25;
            valueReward = _countAddCoin / 25f;
        }
        // lockObj.SetActive(true);
        Config.isFinished_AddCoin = false;
        StartCoroutine(AddCoin_IEnumerator(posStart, posEnd, valueReward));
    }

    public IEnumerator AddCoin_IEnumerator(Vector3 posStart, Vector3 posEnd, float valueReward)
    {
        float timeWait = 0.5f / countIndexAddCoin;
        if (timeWait > 0.05f) timeWait = 0.05f;
        while (indexAddCoin < countIndexAddCoin)
        {
            ItemCoinReward itemCoinReward = Instantiate<ItemCoinReward>(itemCoinRewardPrefab, coinTranfromParent);
            itemCoinReward.ShowCoin(posStart, posEnd, valueReward);
            indexAddCoin++;
            yield return new WaitForSeconds(timeWait);
        }

        yield return new WaitForSeconds(3f);
        lockObj.SetActive(false);
        Config.isFinished_AddCoin = true;
        AddCoin_CallBack.Invoke();
    }
    #endregion


    #region ADD HEART
    public ItemHeartReward itemHeartRewardPrefab;

    int indexAddHeart = 0;
    int countIndexAddHeart = 0;

    public Action AddHeart_CallBack = delegate () { };
    public void AddHeart(int _countAddHeart, Vector3 posStart, Vector3 posEnd, Action _addHeart_CallBack)
    {
        AddHeart_CallBack = _addHeart_CallBack;
        indexAddHeart = 0;
        int valueReward = 1;
        countIndexAddHeart = _countAddHeart;
        lockObj.SetActive(true);
        StartCoroutine(AddHeart_IEnumerator(posStart, posEnd, valueReward));
    }

    public IEnumerator AddHeart_IEnumerator(Vector3 posStart, Vector3 posEnd, int valueReward)
    {
        float timeWait = 0.5f / countIndexAddCoin;
        if (timeWait > 0.05f) timeWait = 0.05f;
        while (indexAddHeart < countIndexAddHeart)
        {
            Debug.Log("AddHeart_IEnumerator  AAAAAAAAAAAAA");
            ItemHeartReward itemHeartReward = Instantiate<ItemHeartReward>(itemHeartRewardPrefab, coinTranfromParent);
            itemHeartReward.ShowHeart(posStart, posEnd, valueReward);
            indexAddHeart++;
            yield return new WaitForSeconds(timeWait);
        }

        yield return new WaitForSeconds(3f);
        lockObj.SetActive(false);
        AddHeart_CallBack.Invoke();
    }
    #endregion

    #region NEWSKIN
    [Header("NEW SKIN POPUP")]
    public RescuePopup rescuePopup;

    public void OpenNewSkinPopup(int _idBallNewSkin)
    {
        rescuePopup.OpenNewSkin(_idBallNewSkin);
    }

    public void CloseRescuePopup()
    {
        if (dailyRewardPopup.isActiveAndEnabled)
        {
            ShowDailyReward_Day7();
        }
    }
    #endregion


    #region DAILY_REWARD
    [Header("DAILY_REWARD POPUP")]
    public DailyRewardPopup dailyRewardPopup;
    public void ShowDailyReward_Day7()
    {
        dailyRewardPopup.ShowReward_Day7();
    }

    public void OpenDailyRewardPopup()
    {
        dailyRewardPopup.OpenPopup();
    }
    #endregion

    #region STARTER_PACK
    [Header("STARTER_PACK POPUP")]
    public StarterPackPopup starterPackPopup;

    public void OpenStarterPackPopup()
    {
        starterPackPopup.OpenPopup();
    }
    #endregion

    #region PREMIUM_PACK
    [Header("PREMIUM_PACK POPUP")]
    public PremiumPackPopup premiumPackPopup;

    public void OpenPremiumPackPopup()
    {
        premiumPackPopup.OpenPopup();
    }
    #endregion


    private void TouchShopCoin()
    {
        
    }

    private void TouchShopHeart()
    {
        
    }
}
