using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
public class HomeManager : MonoBehaviour
{
    public static HomeManager Ins;
    private void Awake()
    {
        Ins = this;
    }
    public BBUIButton btnPlay;
    public BBUIButton btnShop;
    public ShopPopUp shopPopup;
    public Animator ballAnimator;

    public BBUIButton btnFreeCoin;
    public BBUIButton btnDailyReward;
    public BBUIButton btnChest;
    public BBUIButton btnBall;
    public BBUIButton btnAddHeart;
    public BBUIButton btnShopCoin;
    public Transform posAddCoin;

    public TextMeshProUGUI txtLevel;


    public GameObject lockObj;

    // Start is called before the first frame update


    void Start()
    {
        if(Config.GetLevel() == 1)
        {
            SceneManager.LoadScene("Level1");
        }

        btnPlay.OnPointerClickCallBack_Completed.AddListener(TouchPlay);
        btnShop.OnPointerClickCallBack_Completed.AddListener(TouchOpenShopPopup);
        txtLevel.text = "LEVEL " + Config.GetLevel();
        btnChest.OnPointerClickCallBack_Completed.AddListener(TouchChest);
        btnDailyReward.OnPointerClickCallBack_Completed.AddListener(TouchDailyReward);
        btnFreeCoin.OnPointerClickCallBack_Completed.AddListener(TouchFreeCoin);
        btnAddHeart.OnPointerClickCallBack_Completed.AddListener(TouchFreeHeart);

        ShowBallPreview();
        StartCoroutine(Start_IEnumerator());
    }

    public IEnumerator Start_IEnumerator() {
        btnPlay.gameObject.SetActive(false);
        btnShop.gameObject.SetActive(false);
        ballAnimator.gameObject.SetActive(false);
        btnFreeCoin.gameObject.SetActive(false);
        btnDailyReward.gameObject.SetActive(false);
        btnChest.gameObject.SetActive(false);
        btnBall.gameObject.SetActive(false);
        btnAddHeart.gameObject.SetActive(false);
        btnShopCoin.gameObject.SetActive(false);
        txtLevel.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.1f);
        txtLevel.gameObject.SetActive(true);
        txtLevel.gameObject.GetComponent<BBUIView>().ShowView();

        yield return new WaitForSeconds(0.5f);
        btnBall.gameObject.SetActive(true);
        btnBall.GetComponent<BBUIView>().ShowView();
        btnFreeCoin.gameObject.SetActive(true);
        btnFreeCoin.GetComponent<BBUIView>().ShowView();
        if (Config.CheckShowStartPack())
        {
            btnChest.gameObject.SetActive(true);
            btnChest.GetComponent<BBUIView>().ShowView();
        }
        btnShopCoin.gameObject.SetActive(true);
        btnShopCoin.GetComponent<BBUIView>().ShowView();
        btnBall.gameObject.SetActive(true);
        btnBall.GetComponent<BBUIView>().ShowView();
        btnAddHeart.gameObject.SetActive(true);
        btnAddHeart.GetComponent<BBUIView>().ShowView();
        btnShop.gameObject.SetActive(true);
        btnShop.GetComponent<BBUIView>().ShowView();

        yield return new WaitForSeconds(0.2f);
        ballAnimator.gameObject.SetActive(true);
        ballAnimator.GetComponent<BBUIView>().ShowView();
        /*btnDailyReward.gameObject.SetActive(true);
        btnDailyReward.GetComponent<BBUIView>().ShowView();*/
        btnPlay.gameObject.SetActive(true);
        btnPlay.GetComponent<BBUIView>().ShowView();

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
    public void TouchPlay() {
        if (forceLevel > 0)
        {
            SceneManager.LoadScene("Level" + forceLevel);
        }
        else
        {
            SceneManager.LoadScene("Level" + Config.GetLevel());
        }
        
    }


    public void TouchOpenShopPopup()
    {
        shopPopup.OpenShop();
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
                    btnAddHeart.Interactable = false;
                    AddHeart(Config.FREE_HEART_REWARD, ballAnimator.transform.position, btnBall.transform.position, () => {
                        btnAddHeart.Interactable = false;
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
        lockObj.SetActive(true);
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
}
