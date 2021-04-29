using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremiumPanel : MonoBehaviour
{
    public List<int> listBallIDs = new List<int>();
    public ItemShopBall itemShopBallPrefab;
    public Transform ballTranform;
    public PremiumPreview premiumPreview;
    private List<ItemShopBall> listInfoShopBalls = new List<ItemShopBall>();
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
        listInfoShopBalls.Clear();
        for (int i = 0; i < listBallIDs.Count; i++) {
            ItemShopBall itemBall = Instantiate<ItemShopBall>(itemShopBallPrefab, ballTranform);
            itemBall.InitShopBall(Config.GetInfoBallFromID(listBallIDs[i]));
            itemBall.InitCallBack((ItemShopBall _itemShopBall) => {
                ItemShopBall_CallBack(_itemShopBall);
            });
            listInfoShopBalls.Add(itemBall);
        }

        
    }

    public void ItemShopBall_CallBack(ItemShopBall _itemShopBall) {
        Debug.Log("ItemShopBall_CallBack:"+_itemShopBall.infoBall.id);
        premiumPreview.SetInfoBallPreview(Config.GetInfoBallFromID(_itemShopBall.infoBall.id));
    }

    public void UpdateListBall()
    {
        for (int i = 0; i < listInfoShopBalls.Count; i++)
        {
            listInfoShopBalls[i].ShowLockObject();
        }
    }
}
