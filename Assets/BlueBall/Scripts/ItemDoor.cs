using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ItemDoor : MonoBehaviour
{
    public GameObject objUp, objDown;
    public Transform posDoor_Start;
    public Transform posDoor_End;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOpenDoor() {
        objUp.transform.DOLocalMove(posDoor_Start.localPosition, 0.5f).SetEase(Ease.OutQuad);
        objDown.transform.DOLocalMove(posDoor_End.localPosition, 0.5f).SetEase(Ease.OutQuad);
    }
}
