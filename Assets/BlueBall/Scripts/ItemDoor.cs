using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ItemDoor : MonoBehaviour
{
    public GameObject objUp, objDown;
    public Transform up_start;
    public Transform up_end;

    public Transform down_start;
    public Transform down_end;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetOpenDoor()
    {
        SoundManager.instance.SFX_OpenDoor();
        objUp.transform.DOLocalMove(up_end.localPosition, 0.5f).SetEase(Ease.OutQuad);
        objDown.transform.DOLocalMove(down_end.localPosition, 0.5f).SetEase(Ease.OutQuad);
    }
    public void SetCloseDoor()
    {
        SoundManager.instance.SFX_OpenDoor();
        objUp.transform.DOLocalMove(up_start.localPosition, 0.5f).SetEase(Ease.OutQuad);
        objDown.transform.DOLocalMove(down_start.localPosition, 0.5f).SetEase(Ease.OutQuad);
    }
}
