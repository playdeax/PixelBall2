using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class StartPackButton : MonoBehaviour
{
    public Image iconChest;
    public TextMeshProUGUI txtTime;
    // Start is called before the first frame update
    void Start()
    {
        ShowAnimation();
        StartCoroutine(UpdateTime_IEnumerator());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator UpdateTime_IEnumerator() {
        while (true) {
            int time = Mathf.FloorToInt(Config.TIME_START_PACK - Time.realtimeSinceStartup);
            if (time <= 0) {
                gameObject.SetActive(false);
            }
            txtTime.text = Config.FormatTime(time);
            yield return new WaitForSecondsRealtime(1f);
        }
    }

    public void ShowAnimation() {
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(0f, iconChest.transform.DOScale(Vector3.one * 1.1f, 0.4f).SetEase(Ease.Linear).SetLoops(5, LoopType.Yoyo));
        sequence.Insert(2f, iconChest.transform.DOPunchRotation(new Vector3(0f, 0f, 20f), 1f, 6, 2).SetEase(Ease.OutQuart));
        sequence.Insert(2f, iconChest.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 1f, 2, 1).SetEase(Ease.OutQuart));
        //sequence.Insert(2f,btnGift.gameObject.GetComponent<RectTransform>().DOAnchorPosY(-10f, 1f).SetRelative(true).SetEase(Ease.Linear));
        sequence.SetLoops(-1, LoopType.Yoyo);
    }
}
