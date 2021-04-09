using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class ItemHeartReward : MonoBehaviour
{
    public Image icon;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector3 posStart;
    Vector3 posEnd;
    int valueReward = 1;
    public void ShowHeart(Vector3 _posStart, Vector3 _posEnd, int _valueReward)
    {
        valueReward = _valueReward;
        posStart = _posStart;
        posEnd = _posEnd;
        gameObject.transform.position = new Vector3(0f, -10f, 0f);
        MoveHeart();
    }


    Sequence sequenceMove;

    public void MoveHeart()
    {
        Vector3 pos1 = Vector3.zero;
        pos1.y = posStart.y - 10f;
        pos1.x = posStart.x + Random.Range(-3f, 3f);
        Debug.Log("pos1:" + pos1);
        Vector3 pos2 = Vector3.zero;
        pos2.x = posStart.x + Random.Range(-3f, 3f);
        pos2.y = posStart.y + Random.Range(-3f, 3f);

        gameObject.transform.position = pos1;
        gameObject.transform.DOScale(0f, 0f);
        icon.DOFade(0f, 0f);

        sequenceMove = DOTween.Sequence();
        sequenceMove.Insert(0.1f, gameObject.transform.DOMove(pos2, 1f).SetEase(Ease.OutQuad));
        sequenceMove.Insert(0.1f, gameObject.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack));
        sequenceMove.Insert(0.1f, icon.DOFade(1f, 0.8f).SetEase(Ease.OutBack));
        sequenceMove.InsertCallback(0.5f, () => {
            SoundManager.instance.SFX_CoinCollect2();
        });
        sequenceMove.Insert(1.5f, gameObject.transform.DOMove(posEnd, 1f).SetEase(Ease.InQuad));
        sequenceMove.Insert(1.5f, gameObject.transform.DOScale(Vector3.one * 0.6f, 1f).SetEase(Ease.Linear));
        sequenceMove.Insert(1.5f, icon.DOFade(0.5f, 1f).SetEase(Ease.Linear));
        sequenceMove.OnComplete(() =>
        {
            SoundManager.instance.SFX_Cash();
            Destroy(gameObject);
            Config.AddHeart(valueReward);
        });
    }
}
