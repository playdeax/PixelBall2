using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PremiumPreview : MonoBehaviour
{
    public Animator animator;
    public BBUIButton btnTry;
    public BBUIButton btnBuy;
    public BBUIButton btnBuyPremium;
    public BBUIButton btnActive;
    public BBUIButton btnVideo;
    public TextMeshProUGUI txtActived;
    public TextMeshProUGUI txtUnlockLevel;
    public TextMeshProUGUI txtPrice;
    public GameObject groupLock;
    public GameObject groupUnLock;

    public GameObject lockPopup;
    // Start is called before the first frame update
    void Start()
    {
        btnTry.OnPointerClickCallBack_Completed.AddListener(TouchTry);
        btnBuy.OnPointerClickCallBack_Completed.AddListener(TouchBuy);
        btnBuyPremium.OnPointerClickCallBack_Completed.AddListener(TouchBuyPremium);
        btnActive.OnPointerClickCallBack_Completed.AddListener(TouchActive);
        btnVideo.OnPointerClickCallBack_Completed.AddListener(TouchVideo);

        SetInfoBallPreview(Config.GetInfoBallFromID(Config.GetBallActive()));
    }


    private void OnDestroy()
    {
        btnTry.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnBuy.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnBuyPremium.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnActive.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnVideo.OnPointerClickCallBack_Completed.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    bool isShowVideo = false;
    public InfoBall currPreviewInfoBall;
    public Transform posCenter;
    public Transform posCoin;
    public void SetInfoBallPreview(InfoBall _infoBall) {
        Debug.Log("SetInfoBallPreview:"+_infoBall.id);
        currPreviewInfoBall = _infoBall;

        animator.runtimeAnimatorController = _infoBall.animatorImgOverrideController;

        ShowInfo();
    }

    public void ShowInfo() {
        groupLock.gameObject.SetActive(false);
        groupUnLock.gameObject.SetActive(false);
        btnVideo.gameObject.SetActive(false);

        //if (shopType == ShopPopUp.SHOP_TYPE.PREMIUM && Config.GetBuyIAP(Config.IAP_ID.premium_pack)) {
        //    groupLock.gameObject.SetActive(false);
        //    groupUnLock.gameObject.SetActive(true);

        //    txtActived.gameObject.SetActive(true);
        //    txtUnlockLevel.gameObject.SetActive(false);
        //    btnActive.gameObject.SetActive(false);

        //    btnVideo.gameObject.SetActive(true);
        //    return;
        //}

        if (Config.GetBallActive() == currPreviewInfoBall.id)
        {
            Debug.Log("111111111111111111");
            groupLock.gameObject.SetActive(false);
            groupUnLock.gameObject.SetActive(true);

            txtActived.gameObject.SetActive(true);
            txtUnlockLevel.gameObject.SetActive(false);
            btnActive.gameObject.SetActive(false);

            btnVideo.gameObject.SetActive(true);
        }
        else {
            if (Config.GetInfoBallUnlock(currPreviewInfoBall.id) || currPreviewInfoBall.price == 0 || (currPreviewInfoBall.ballType == Config.BALL_TYPE.PREMIUM && Config.GetBuyIAP(Config.IAP_ID.premium_pack)))
            {
                Debug.Log("2222222222222222222222");
                groupLock.gameObject.SetActive(false);
                groupUnLock.gameObject.SetActive(true);

                txtActived.gameObject.SetActive(false);
                txtUnlockLevel.gameObject.SetActive(false);
                btnActive.gameObject.SetActive(true);
                btnVideo.gameObject.SetActive(true);
            }
            else {
                btnVideo.gameObject.SetActive(false);
                if (currPreviewInfoBall.ballType == Config.BALL_TYPE.RESCUE)
                {
                    Debug.Log("333333333333333");
                    groupLock.gameObject.SetActive(false);
                    groupUnLock.gameObject.SetActive(true);

                    txtActived.gameObject.SetActive(false);
                    btnActive.gameObject.SetActive(false);
                    btnVideo.gameObject.SetActive(true);
                    txtUnlockLevel.gameObject.SetActive(true);
                    txtUnlockLevel.text = "Unlock Level " + currPreviewInfoBall.levelUnlock;
                }
                else if (currPreviewInfoBall.ballType == Config.BALL_TYPE.PREMIUM)
                {
                    Debug.Log("44444444444444");
                    groupLock.gameObject.SetActive(true);
                    groupUnLock.gameObject.SetActive(false);

                    btnBuyPremium.gameObject.SetActive(true);
                    btnBuy.gameObject.SetActive(false);
                    btnTry.gameObject.SetActive(true);
                    txtPrice.text = "" + currPreviewInfoBall.price;
                    txtPrice.gameObject.SetActive(false);
                }
                else if (currPreviewInfoBall.ballType == Config.BALL_TYPE.COIN) {
                    Debug.Log("5555555555555555555555");
                    groupLock.gameObject.SetActive(true);
                    groupUnLock.gameObject.SetActive(false);

                    btnBuy.gameObject.SetActive(true);
                    btnBuyPremium.gameObject.SetActive(false);
                    btnTry.gameObject.SetActive(true);
                    txtUnlockLevel.gameObject.SetActive(false);
                    txtActived.gameObject.SetActive(false);
                    txtPrice.text = "" + currPreviewInfoBall.price;
                    txtPrice.gameObject.SetActive(true);
                }
            }
        }
    }

    public void TouchTry() {
        
        
        if (AdmobManager.instance.isRewardAds_Avaiable())
        {
            lockPopup.gameObject.SetActive(true);
            AdmobManager.instance.ShowRewardAd_CallBack((AdmobManager.ADS_CALLBACK_STATE state) =>
            {
                if (state == AdmobManager.ADS_CALLBACK_STATE.SUCCESS)
                {
                    Debug.Log("AddHeartAddHeartAddHeartAddHeartAddHeart");
                    lockPopup.gameObject.SetActive(false);
                    Config.currInfoBall_Try = currPreviewInfoBall;

                    if (HomeManager.Ins != null)
                    {
                        HomeManager.Ins.ShowLoadingToGame();
                    }
                    else if (GamePlayManager.Ins != null)
                    {
                        GamePlayManager.Ins.ShowLoadingNextGame();
                    }
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

    public void TouchBuy()
    {
        if (currPreviewInfoBall.ballType == Config.BALL_TYPE.COIN)
        {
            if (Config.currCoin >= currPreviewInfoBall.price)
            {
                Config.SetCoin(Config.currCoin - currPreviewInfoBall.price);
                Config.SetInfoBallUnlock(currPreviewInfoBall.id);
                if (ShopNewPopup.Ins != null && ShopNewPopup.Ins.isActiveAndEnabled)
                {
                    ShopNewPopup.Ins.SetUpdateListBalls();
                }

                ShowInfo();
            }
            else
            {
                NotificationPopup.instance.AddNotification("Not enough Coin!");
            }
        }
    }

    public void TouchBuyPremium()
    {
        if (currPreviewInfoBall.ballType == Config.BALL_TYPE.PREMIUM)
        {
            if (HomeManager.Ins != null)
            {
                HomeManager.Ins.OpenPremiumPackPopup();
            }
            else if (GamePlayManager.Ins != null)
            {
                GamePlayManager.Ins.OpenPremiumPackPopup();
            }
        }
    }

    public void TouchActive() {
        Config.SetBallActive(currPreviewInfoBall.id);
        Config.currBallID = Config.GetBallActive();
        Config.currInfoBall = Config.GetInfoBallFromID(Config.currBallID);
        if (ShopNewPopup.Ins != null && ShopNewPopup.Ins.isActiveAndEnabled)
        {
            ShopNewPopup.Ins.SetUpdateListBalls();
        }

        ShowInfo();

        if (HomeManager.Ins != null)
        {
            HomeManager.Ins.ShowBallPreview();
        }
    }

    public void TouchVideo() {
        if (AdmobManager.instance.isRewardAds_Avaiable())
        {
            lockPopup.gameObject.SetActive(true);
            AdmobManager.instance.ShowRewardAd_CallBack((AdmobManager.ADS_CALLBACK_STATE state) =>
            {
                if (state == AdmobManager.ADS_CALLBACK_STATE.SUCCESS)
                {
                    Debug.Log("AddHeartAddHeartAddHeartAddHeartAddHeart");
                    lockPopup.gameObject.SetActive(false);
                    btnVideo.Interactable = false;
                    isShowVideo = true;
                    if(GamePlayManager.Ins != null)
                    {
                        GamePlayManager.Ins.AddCoin(Config.FREE_COIN_REWARD2, posCenter.position, posCoin.position, () => {
                            
                        });
                    }else if(HomeManager.Ins != null){
                        HomeManager.Ins.AddCoin(Config.FREE_COIN_REWARD2, posCenter.position, posCoin.position, () => {

                        });
                    }
                    

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
}
