using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ItemDoor_Key : ItemBase
{
    public ItemDoor itemDoor;
    public GameObject key;

    // Start is called before the first frame update
    void Start()
    {
        key.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        SetCollier_Enable(false);
        if (collision.GetComponent<PlayerMovement>().isMoveLeft)
        {
            key.transform.DOLocalRotate(new Vector3(0f, 0f, 35f), 0.3f).SetEase(Ease.OutQuart);
        }
        else {
            key.transform.DOLocalRotate(new Vector3(0f, 0f, -35f), 0.3f).SetEase(Ease.OutQuart);
        }
        itemDoor.SetOpenDoor();
    }


}
