using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemySpider : EnemyBase
{
    [Header("INFO")]
    public float timeWait = 3f;//Sau thời gian timeWait thì nhện rơi xuống 1 lần
    public float speedMoveBase = 0.2f;//Thời gian di chuyển đc 1f đơn vị
    public float timeStay = 1f;//Thời gian nhện ở lại vị trí đó trc khi quay lại
    public float timeStart = 0f;
    public SpriteRenderer silk;
    public EnemySpider_Spider spider;
    public Transform posEnd;
    //[Header("FORCE ADD BALL")]
    //public Vector2 speedAddBall;


    private Transform posStart;
    private Vector3 vectorStart;
    private float timeMoveToTarget;
    // Start is called before the first frame update
    void Start()
    {
        posStart = spider.transform;
        vectorStart = posStart.localPosition;
        timeMoveToTarget = Vector2.Distance(posEnd.localPosition, posStart.localPosition) * speedMoveBase;
        Sequence sequence = DOTween.Sequence();
        sequence.InsertCallback(timeStart, () => {
            EnemyAutoMove();
        });
       
    }

    // Update is called once per frame
    void Update()
    {
        silk.size = new Vector2(silk.size.x, 1f + Mathf.Abs(spider.transform.localPosition.y - vectorStart.y));
    }

    Sequence sequence;
   
    public void EnemyAutoMove() {
        sequence = DOTween.Sequence();
        sequence.Insert(timeWait, spider.transform.DOLocalMove(posEnd.localPosition,timeMoveToTarget).SetEase(Ease.Linear));
        sequence.Insert(timeWait + timeMoveToTarget + timeStay, spider.transform.DOLocalMove(posStart.localPosition,timeMoveToTarget).SetEase(Ease.Linear));
        sequence.SetLoops(-1, LoopType.Restart);
    }


   

}
