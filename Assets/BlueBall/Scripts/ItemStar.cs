using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ItemStar : ItemBase
{
    public SpriteRenderer icon;

    float t;
    Vector3 startPosition;
    Vector3 target;
    public Animator anim;
    public float timeToReachTarget = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        ShowAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingToFinished) {
            //transform.position = Vector3.MoveTowards()
            t += Time.deltaTime / timeToReachTarget;
            transform.position = Vector3.Lerp(startPosition, GamePlayManager.Ins.posStar_Tranform.position, t);
        }
    }

    public void ShowAnimation() {
        
    }


    public override void CollisionWithBall()
    {
        base.CollisionWithBall();

        SetCollier_Enable(false);

        GameLevelManager.Ins.SetAddStar(this);
    }

    bool isMovingToFinished = false;
    public void SetMoveToFinished(Vector3 pos) {
        DOTween.Kill(icon.gameObject.transform);
        Sequence sequence = DOTween.Sequence();
        sequence.InsertCallback(0.1f, () => {
            anim.SetTrigger("Collected");
            SoundManager.instance.SFX_StarCollect();
            isMovingToFinished = true;
        });
        sequence.InsertCallback(1f, () => {
          
        });
        sequence.OnComplete(() =>
        {
            Destroy(gameObject);
        });
        
        startPosition = transform.position;
        timeToReachTarget = 0.5f;
        t = 0;
    }
}
