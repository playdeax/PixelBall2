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
            gameObject.SetActive(false);
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

    public void TouchBuy() {
        PackPanel.Ins.TouchBuyIAP(this);
    }
}
