using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremiumPanel : MonoBehaviour
{
    public List<int> listBallIDs = new List<int>();
    public ItemShopBall itemShopBallPrefab;
    public Transform ballTranform;
    public PremiumPreview premiumPreview;
    public ShopPopUp.SHOP_TYPE shopType;
    // Start is called before the first frame update
    void Start()
    {
        InitPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void InitPanel() {
        for (int i = 0; i < listBallIDs.Count; i++) {
            ItemShopBall itemBall = Instantiate<ItemShopBall>(itemShopBallPrefab, ballTranform);
            itemBall.InitShopBall(Config.GetInfoBallFromID(listBallIDs[i]));
            itemBall.InitCallBack((ItemShopBall _itemShopBall) => {
                ItemShopBall_CallBack(_itemShopBall);
            });
        }

        
    }

    public void ItemShopBall_CallBack(ItemShopBall _itemShopBall) {
        premiumPreview.SetInfoBallPreview(Config.GetInfoBallFromID(_itemShopBall.infoBall.id), shopType);
    }
}
