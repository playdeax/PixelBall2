using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCoinPopup : MonoBehaviour
{
    public static ShopCoinPopup Ins;

    private void Awake()
    {
        Ins = this;
    }
    public BBUIButton btnBack;
    public GameObject lockGroup;
    public Transform posCenter;
    public Transform posCoin;
    public Transform posHeart;
    public List<ItemShopPack> listItemShopPacks = new List<ItemShopPack>();
    // Start is called before the first frame update
    void Start()
    {
        btnBack.OnPointerClickCallBack_Completed.AddListener(TouchBack);
        PurchaserManager.InitializeSucceeded += PurchaserManager_InitializeSucceeded;
    }
    
    private void OnDestroy()
    {
        btnBack.OnPointerClickCallBack_Completed.RemoveAllListeners();
        PurchaserManager.InitializeSucceeded -= PurchaserManager_InitializeSucceeded;
    }
    
    public void PurchaserManager_InitializeSucceeded()
    {
        InitIAP();
    }
    
    public void InitIAP()
    {
        for (int i = 0; i < listItemShopPacks.Count; i++) {
            listItemShopPacks[i].InitIAP();
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ShowPopup()
    {
        gameObject.SetActive(true);
        btnBack.GetComponent<BBUIView>().ShowView();
    }
    
    private void TouchBack()
    {
        gameObject.SetActive(false);
    }
    
    public void TouchBuyIAP(ItemShopPack itemShopPack) {
        lockGroup.gameObject.SetActive(true);
        PurchaserManager.instance.BuyConsumable(itemShopPack.packID, (string _iapID, PurchaserManager.IAP_CALLBACK_STATE _state) =>
        {
            if (_state == PurchaserManager.IAP_CALLBACK_STATE.SUCCESS)
            {
                NotificationPopup.instance.AddNotification("Buy Success!");
                itemShopPack.btnBuy.Interactable = false;
                lockGroup.gameObject.SetActive(false);
                if (_iapID.Equals(Config.IAP_ID.remove_ad.ToString()))
                {
                    Config.SetBuyIAP(Config.IAP_ID.remove_ad);
                    Config.SetRemoveAd();
                    if (GamePlayManager.Ins != null)
                    {
                        GamePlayManager.Ins.AddCoin(100000, posCenter.position, posCoin.position, () =>
                        {
                            lockGroup.gameObject.SetActive(false);
                        });
                    }
                    else if (HomeManager.Ins != null)
                    {
                        HomeManager.Ins.AddCoin(100000, posCenter.position, posCoin.position, () =>
                        {
                            lockGroup.gameObject.SetActive(false);
                        });
                    }

                }
                else if (_iapID.Equals(Config.IAP_ID.premium_pack.ToString()))
                {
                    Config.SetBuyIAP(Config.IAP_ID.premium_pack);
                    if (GamePlayManager.Ins != null)
                    {
                        GamePlayManager.Ins.AddCoin(100000, posCenter.position, posCoin.position, () =>
                        {
                            lockGroup.gameObject.SetActive(false);
                        });
                    }
                    else if (HomeManager.Ins != null)
                    {
                        HomeManager.Ins.AddCoin(100000, posCenter.position, posCoin.position, () =>
                        {
                            lockGroup.gameObject.SetActive(false);
                        });
                    }
                }
                else if (_iapID.Equals(Config.IAP_ID.remove_ad_coin.ToString()))
                {
                    Config.SetBuyIAP(Config.IAP_ID.premium_pack);
                    if (GamePlayManager.Ins != null)
                    {
                        GamePlayManager.Ins.AddCoin(70000, posCenter.position, posCoin.position, () =>
                        {
                            lockGroup.gameObject.SetActive(false);
                        });
                    }
                    else if (HomeManager.Ins != null)
                    {
                        HomeManager.Ins.AddCoin(70000, posCenter.position, posCoin.position, () =>
                        {
                            lockGroup.gameObject.SetActive(false);
                        });
                    }
                }
                else if (_iapID.Equals(Config.IAP_ID.remove_ad_heart.ToString()))
                {
                    Config.SetBuyIAP(Config.IAP_ID.remove_ad_heart);
                    if (GamePlayManager.Ins != null)
                    {
                        GamePlayManager.Ins.AddHeart(5, posCenter.position, posHeart.position, () =>
                        {
                            lockGroup.gameObject.SetActive(false);
                        });
                    }
                    else if (HomeManager.Ins != null)
                    {
                        HomeManager.Ins.AddHeart(5, posCenter.position, posHeart.position, () =>
                        {
                            lockGroup.gameObject.SetActive(false);
                        });
                    }
                }
            }
            else
            {
                lockGroup.gameObject.SetActive(false);
                NotificationPopup.instance.AddNotification("Buy Fail!");
            }
        });
    }
}
