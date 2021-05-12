using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FinishPoint_Key : MonoBehaviour
{
    public FinishPoint_Finish finishPoint;
    public SpriteRenderer icon;
    public FinishPoint finishPoint2;
    // Start is called before the first frame update
    void Start()
    {
        icon.gameObject.transform.DOMoveY(0.5f, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).SetRelative(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball")) {
            finishPoint.SetOpenFinishOpen();
            //finishPoint2.SetOpenDoor();
            gameObject.SetActive(false);
        }
    }
}
