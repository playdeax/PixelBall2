using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ItemSwitch : MonoBehaviour
{
    public List<GameObject> switchObjs;
    public List<Transform> pos_Start;
    public List<Transform> post_End;
    public float timeMove = 1f;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < switchObjs.Count; i++)
        {
            switchObjs[i].transform.localPosition = pos_Start[i].localPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetSwitch_ON() {
        for (int i = 0; i < switchObjs.Count; i++)
        {
            DOTween.Kill(switchObjs[i].transform);
            float _time = Vector3.Distance(switchObjs[i].transform.localPosition, post_End[i].localPosition) / Vector3.Distance(pos_Start[i].localPosition, post_End[i].localPosition) * timeMove;
            switchObjs[i].transform.DOLocalMove(post_End[i].localPosition, _time).SetEase(Ease.Linear);
        }
    }

    public void SetSwitch_OFF() {
        for (int i = 0; i < switchObjs.Count; i++)
        {
            DOTween.Kill(switchObjs[i].transform);
            float _time = Vector3.Distance(switchObjs[i].transform.localPosition, pos_Start[i].localPosition) / Vector3.Distance(pos_Start[i].localPosition, post_End[i].localPosition) * timeMove;
            switchObjs[i].transform.DOLocalMove(pos_Start[i].localPosition, _time).SetEase(Ease.Linear);
        }

    }
}
