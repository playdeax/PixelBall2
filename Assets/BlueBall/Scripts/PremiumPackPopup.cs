﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PremiumPackPopup : MonoBehaviour
{
    public BBUIView popup;
    public BBUIButton btnClose;
    public BBUIButton btnBuy;
    public TextMeshProUGUI txtPrice;
    public List<BBUIView> listPackObjs;
    public ParticleSystem efxPack;
    public GameObject lockGroup;


    public Transform posCenter;
    public Transform posCoin;

    // Start is called before the first frame update
    void Start()
    {
        popup.ShowBehavior.onCallback_Completed.AddListener(Popup_ShowView_Finished);
        popup.HideBehavior.onCallback_Completed.AddListener(Popup_HideView_Finished);

        btnClose.OnPointerClickCallBack_Completed.AddListener(TouchClose);
        btnBuy.OnPointerClickCallBack_Completed.AddListener(TouchBuy);

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
        if (PurchaserManager.instance.IsInitialized())
        {
            txtPrice.text = PurchaserManager.instance.GetLocalizedPriceString(Config.IAP_ID.blueball_starter_pack.ToString());
            btnBuy.Interactable = true;
        }
        else
        {
            txtPrice.text = "";
            btnBuy.Interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenPopup()
    {
        gameObject.SetActive(true);
        efxPack.gameObject.SetActive(false);
        for (int i = 0; i < listPackObjs.Count; i++)
        {
            listPackObjs[i].gameObject.SetActive(false);
        }
        lockGroup.SetActive(true);

        popup.ShowView();
    }

    public void Popup_ShowView_Finished()
    {
        lockGroup.SetActive(false);
        efxPack.gameObject.SetActive(true);
        efxPack.Play();
        for (int i = 0; i < listPackObjs.Count; i++)
        {
            listPackObjs[i].gameObject.SetActive(true);
            listPackObjs[i].ShowView();
        }
    }

    public void Popup_HideView_Finished()
    {
        gameObject.SetActive(false);
    }


    public void TouchClose()
    {
        lockGroup.SetActive(true);
        popup.HideView();
        efxPack.gameObject.SetActive(false);
        for (int i = 0; i < listPackObjs.Count; i++)
        {
            listPackObjs[i].gameObject.SetActive(false);
        }
    }

    public void TouchBuy()
    {
        lockGroup.gameObject.SetActive(true);
        PurchaserManager.instance.BuyConsumable(Config.IAP_ID.premium_pack, (string _iapID, PurchaserManager.IAP_CALLBACK_STATE _state) =>
        {
            if (_state == PurchaserManager.IAP_CALLBACK_STATE.SUCCESS)
            {
                btnBuy.Interactable = false;
                lockGroup.gameObject.SetActive(false);
                if (_iapID.Equals(Config.IAP_ID.premium_pack.ToString()))
                {
                    Config.SetBuyIAP(Config.IAP_ID.premium_pack);
                    if (GamePlayManager.Ins != null)
                    {
                        GamePlayManager.Ins.AddCoin(100000, posCenter.position, posCoin.position, () =>
                        {
                            lockGroup.gameObject.SetActive(false);
                            popup.HideView();
                        });
                    }
                    else if (HomeManager.Ins != null)
                    {
                        HomeManager.Ins.AddCoin(100000, posCenter.position, posCoin.position, () =>
                        {
                            lockGroup.gameObject.SetActive(false);
                            popup.HideView();
                        });
                    }

                    if (ShopPopUp.Ins != null) {
                        ShopPopUp.Ins.SetBuySuccess();
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