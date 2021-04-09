using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ItemRescue : ItemBase
{
    public int idBallRescue;
    public SpriteRenderer iconResuce;
    public Animator ballRescueAnimator;
    public ParticleSystem efxBallRescue;
    public ParticleSystem efxCollision;
    public GameObject colliderObj;
    // Start is called before the first frame update
    void Start()
    {
        InitBallRescue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    InfoBall infoBallRescue;
    public void InitBallRescue() {
        infoBallRescue = Config.GetInfoBallFromID(idBallRescue);
        ballRescueAnimator.runtimeAnimatorController = infoBallRescue.animatorOverrideController;
        ballRescueAnimator.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
        ballRescueAnimator.transform.DOScale(0f, 0f);
        ballRescueAnimator.transform.DOLocalMoveY(-1f, 0f);
        ballRescueAnimator.gameObject.SetActive(true);
    }

    public override void CollisionWithBall()
    {
        base.CollisionWithBall();
        SetCollier_Enable(false);
        colliderObj.SetActive(false);
        Config.currIDBallRescue = infoBallRescue.id;
        efxCollision.gameObject.SetActive(true);
        efxCollision.Play();
        StartCoroutine(ShowEfxResuce_Success());
    }

    public IEnumerator ShowEfxResuce_Success() {
        yield return new WaitForSeconds(0.1f);
        iconResuce.DOFade(0f, 0.2f);

        yield return new WaitForSeconds(0.15f);
        ballRescueAnimator.gameObject.SetActive(true);
        ballRescueAnimator.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f).SetEase(Ease.OutQuad);
        ballRescueAnimator.transform.DOScale(2.5f, 0.5f).SetEase(Ease.OutBack);
        ballRescueAnimator.transform.DOLocalJump(new Vector3(0f,0.7f,0f),1f,1, 0.5f).SetEase(Ease.InQuad);
        yield return new WaitForSeconds(0.5f);
        ballRescueAnimator.SetTrigger("win2");


        yield return new WaitForSeconds(2f);
        ballRescueAnimator.GetComponent<SpriteRenderer>().DOFade(0f, 1f).SetEase(Ease.InQuad);
        yield return new WaitForSeconds(0.2f);
        efxBallRescue.gameObject.SetActive(true);
        efxBallRescue.Play();

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
