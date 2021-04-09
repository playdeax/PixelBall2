using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemDailyReward : MonoBehaviour
{
    public List<InfoItem> listInfoItems = new List<InfoItem>();
    public Image iconRewarded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowRewarded(bool isRewarded) {
        if (!isRewarded)
        {
            iconRewarded.gameObject.SetActive(false);
        }
        else {
            iconRewarded.gameObject.SetActive(true);
        }
    }
}
