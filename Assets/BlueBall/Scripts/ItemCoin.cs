using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class ItemCoin : ItemBase
{
    public SpriteRenderer icon;
    public TextMeshPro txtValue;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        txtValue.gameObject.SetActive(false);
        ShowAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShowAnimation()
    {
        
    }


    public override void CollisionWithBall()
    {
        base.CollisionWithBall();
        Collected();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.CompareTag("Magnet"))
        {
            Sequence sequence = DOTween.Sequence();
            sequence.InsertCallback(0.1f, () => {
                transform.DOMove(collision.transform.position, 0.3f);
            });
            sequence.InsertCallback(0.5f, () => {
                Collected();
            });
          
        }
    }
    bool collected = false;
    public void Collected()
    {
        if (!collected)
        {
            collected = true;
            SetCollier_Enable(false);
            ShowEfx_AddCoin();

            SoundManager.instance.SFX_CoinCollect();
            GameLevelManager.Ins.SetAddCoin();
        }
    }

    public void ShowEfx_AddCoin() {
        Sequence sequence = DOTween.Sequence();
        sequence.InsertCallback(0.1f, () => {
            anim.SetTrigger("Collected");
        });
        sequence.InsertCallback(0.3f, ()=> {
            txtValue.gameObject.SetActive(true);
        });
        sequence.Insert(0.35f, txtValue.transform.DOScaleX(1.2f,0.2f));
        sequence.Insert(0.35f, txtValue.DOFade(1f, 0.6f).SetEase(Ease.InBack));
        sequence.Insert(0.65f, txtValue.DOFade(0f, 0.4f).SetEase(Ease.InQuart));
        sequence.Insert(0.35f, txtValue.DOFade(0f, 0.6f).SetEase(Ease.InBack));
        sequence.Insert(0.35f, txtValue.transform.DOMoveY(Random.Range(2f,3f), 0.8f).SetEase(Ease.OutExpo).SetRelative(true));
        sequence.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
