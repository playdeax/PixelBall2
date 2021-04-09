using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackPanel : MonoBehaviour
{
    public static PackPanel Ins;
    public List<ItemShopPack> listItemShopPacks = new List<ItemShopPack>();
    public GameObject lockGroup;
    public Transform posCenter;
    public Transform posCoin;

    private void Awake()
    {
        Ins = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        PurchaserManager.InitializeSucceeded += PurchaserManager_InitializeSucceeded;
        InitIAP();
    }

    private void OnDestroy()
    {
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
            }
            else
            {
                lockGroup.gameObject.SetActive(false);
                NotificationPopup.instance.AddNotification("Buy Fail!");
            }
        });
    }
}
