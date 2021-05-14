using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PixelPlayerAnimation : PlayerAnimation
{

    public Animator animator;
    public ParticleSystem efxBehit;
    public ParticleSystem efxMagic;
    public ParticleSystem efxBallDead;

    private GameObject efx;
    // Start is called before the first frame update
    void Start()
    {
        //animator.runtimeAnimatorController = Config.currInfoBall.animatorOverrideController;
        if (Config.currInfoBall_Try != null)
        {
            animator.runtimeAnimatorController = Config.currInfoBall_Try.animatorOverrideController;
        }
        else
        {
            animator.runtimeAnimatorController = Config.currInfoBall.animatorOverrideController;
        }
        UpdateEfxBall();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void SetAnimIdle()
    {
        base.SetAnimIdle();
    }

    public override void SetAnimHit()
    {
        base.SetAnimHit();
        animator.SetTrigger("behit");
        efxBehit.gameObject.SetActive(true);
        efxBehit.Play();
    }


    public override void SetEfxBeHit_End()
    {
        base.SetEfxBeHit_End();
        efxBehit.Stop();
    }



    public override void SetBallDead()
    {
        base.SetBallDead();
        if (efxBehit.gameObject.activeSelf) {
            efxBehit.gameObject.SetActive(false);
        }

        efxBallDead.gameObject.SetActive(true);
        efxBallDead.Play();
        StopAutoRotate();
        animator.gameObject.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
        animator.gameObject.transform.DOLocalRotate(new Vector3(0f,0f,0f),0.5f).SetEase(Ease.OutQuad);
        
        if (efx != null)
        {
            efx.SetActive(false);
        }
    }


    public override void SetBallRevive() {
        Debug.Log("SetBallRevive");
        DOTween.Kill(animator.gameObject.transform);
        animator.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        animator.gameObject.GetComponent<SpriteRenderer>().DOFade(1f, 0.5f);
        animator.gameObject.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.2f);
        efxBehit.gameObject.SetActive(true);
        efxBehit.Play();
        
        if (efx != null)
        {
            efx.SetActive(true);
        }
    }



    public override void SetAutoRotate(bool _isRight) {
        base.SetAutoRotate(_isRight);
        if (_isRight)
        {
            animator.gameObject.transform.DORotate(new Vector3(0f, 0f, -360f), 1f).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
        }
        else {
            animator.gameObject.transform.DORotate(new Vector3(0f, 0f, 360f), 1f).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
        }
    }

    public override void StopAutoRotate() {
        base.StopAutoRotate();
        DOTween.Kill(animator.gameObject.transform);
    }

    public override void SetUpdateTrySkinInGame()
    {
        if (Config.currInfoBall_Try != null)
        {
            animator.runtimeAnimatorController = Config.currInfoBall_Try.animatorOverrideController;
            UpdateEfxBall();
        }
    }

    private void UpdateEfxBall()
    {
        if (efx != null)
        {
            Destroy(efx.gameObject);   
        }
        
        if (Config.currInfoBall_Try != null)
        {
            if (Config.currInfoBall_Try.efxBall != null)
            {
                efx = Instantiate(Config.currInfoBall_Try.efxBall, animator.transform);
            }
        }
        else
        {
            if (Config.currInfoBall.efxBall != null)
            {
                 efx = Instantiate(Config.currInfoBall.efxBall, animator.transform);
            }
        }        
    }
}
