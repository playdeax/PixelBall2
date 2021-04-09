using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Animator anim;
    bool isCheck = false;
    void Start()
    {
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (!isCheck)
            {
                anim.enabled = true;
                anim.Play("End");
                SoundManager.instance.SFX_CheckPoint();
                GameLevelManager.Ins.SetCheckPoint(this);
                isCheck = true;
            }
        }
    }
}
