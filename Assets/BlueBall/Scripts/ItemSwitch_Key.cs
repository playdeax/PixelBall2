using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ItemSwitch_Key : ItemBase
{
    public ItemSwitch itemSwitch;
    public List<string> listTagsCheck = new List<string>();
    public GameObject iconSwitch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    bool isBoxOn = false;
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        //base.OnTriggerEnter2D(collision);
        if (CheckTagCollision(collision.tag)) {

            if(collision.tag == "Box")
            {
                isBoxOn = true;
            }
            SetKey_Open();

            itemSwitch.SetSwitch_ON();
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (CheckTagCollision(collision.tag))
        {
            if (collision.tag == "Box")
            {
                isBoxOn = false;
            }

            if (collision.tag == "Ball" && isBoxOn)
            {
                //do nothing
            }
            else
            {
                SetKey_Close();
                itemSwitch.SetSwitch_OFF();
            }

        }
    }

    private bool CheckTagCollision(string _tag) {
        if (listTagsCheck.Contains(_tag)) {
            return true;
        }
        return false;
    }

    public float openY, closeY;
    public void SetKey_Open() {
        DOTween.Kill(iconSwitch.transform);
      //  iconSwitch.transform.DOLocalMoveY(0f, 0.3f).SetEase(Ease.OutQuad);
        transform.DOLocalMoveY(openY, 0.3f).SetEase(Ease.OutQuad);
    }

    public void SetKey_Close() {
        DOTween.Kill(iconSwitch.transform);
     //   iconSwitch.transform.DOLocalMoveY(0.37f, 0.3f).SetEase(Ease.OutQuad);
        transform.DOLocalMoveY(closeY, 0.3f).SetEase(Ease.OutQuad);
    }
}
