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
        btnActive.OnPointerClickCallBack_Completed.AddListener(TouchActive);
        btnVideo.OnPointerClickCallBack_Completed.AddListener(TouchVideo);
    }


    private void OnDestroy()
    {
        btnTry.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnBuy.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnActive.OnPointerClickCallBack_Completed.RemoveAllListeners();
        btnVideo.OnPointerClickCallBack_Completed.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    bool isShowVideo = false;
    public InfoBall currPreviewInfoBall;
    ShopPopUp.SHOP_TYPE shopType;
    public Transform posCenter;
    public Transform posCoin;
    public void SetInfoBallPreview(InfoBall _infoBall,ShopPopUp.SHOP_TYPE _typeShop) {
        shopType = _typeShop;
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
            if (Config.GetInfoBallUnlock(currPreviewInfoBall.id) || currPreviewInfoBall.price == 0 || (shopType == ShopPopUp.SHOP_TYPE.PREMIUM && Config.GetBuyIAP(Config.IAP_ID.premium_pack)))
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
                if (shopType == ShopPopUp.SHOP_TYPE.RESCUE)
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
                else if (shopType == ShopPopUp.SHOP_TYPE.PREMIUM)
                {
                    Debug.Log("44444444444444");
                    groupLock.gameObject.SetActive(true);
                    groupUnLock.gameObject.SetActive(false);

                    btnBuy.gameObject.SetActive(true);
                    btnTry.gameObject.SetActive(true);
                    txtPrice.text = "" + currPreviewInfoBall.price;
                    txtPrice.gameObject.SetActive(false);
                }
                else if (shopType == ShopPopUp.SHOP_TYPE.COIN) {
                    Debug.Log("5555555555555555555555");
                    groupLock.gameObject.SetActive(true);
                    groupUnLock.gameObject.SetActive(false);

                    btnBuy.gameObject.SetActive(true);
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
                    ShopPopUp.Ins.CloseShop();

                    if (HomeManager.Ins != null) {
                        HomeManager.Ins.ShowBallPreview();
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
        Debug.Log("TouchBuyTouchBuyTouchBuy");
        //Config.SetInfoBallUnlock(currPreviewInfoBall.id);
        //ShowInfo();

        if (HomeManager.Ins != null)
        {
            HomeManager.Ins.OpenPremiumPackPopup();
        }
        else if (GamePlayManager.Ins != null) {
            GamePlayManager.Ins.OpenPremiumPackPopup();
        }
    }

    public void TouchActive() {
        Config.SetBallActive(currPreviewInfoBall.id);
        Config.currBallID = Config.GetBallActive();
        Config.currInfoBall = Config.GetInfoBallFromID(Config.currBallID);

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
