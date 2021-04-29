using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemShopPack : MonoBehaviour
{
    public Config.IAP_ID packID = Config.IAP_ID.remove_ad;
    public BBUIButton btnBuy;
    public TextMeshProUGUI txtPrice;
    // Start is called before the first frame update
    void Start()
    {
        btnBuy.OnPointerClickCallBack_Completed.AddListener(TouchBuy);

        if (Config.GetBuyIAP(packID)) {
            btnBuy.Interactable = false;
        }
    }

    private void OnDestroy()
    {
        btnBuy.OnPointerClickCallBack_Completed.RemoveAllListeners();
        
    }

    public void InitIAP() {
        if (PurchaserManager.instance.IsInitialized())
        {
            txtPrice.text = PurchaserManager.instance.GetLocalizedPriceString(packID.ToString());
        }
        else
        {
            txtPrice.text = "";
        }

        if (packID == Config.IAP_ID.remove_ad)
        {
            if (Config.GetBuyIAP(Config.IAP_ID.premium_pack) || Config.GetBuyIAP(Config.IAP_ID.remove_ad_coin) ||
                Config.GetBuyIAP(Config.IAP_ID.remove_ad_heart))
            {
                btnBuy.Interactable = false;
            }
        }
        else if (Config.GetBuyIAP(packID))
        {
            btnBuy.Interactable = false;
        }
        else
        {
            btnBuy.Interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TouchBuy() {
        if (PurchaserManager.instance.IsInitialized())
        {
            ShopCoinPopup.Ins.TouchBuyIAP(this);
        }
        else
        {
            NotificationPopup.instance.AddNotification("Init IAP Fail!");
        }
    }
}
