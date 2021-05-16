using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ItemDoor_Key : ItemBase
{
    public ItemDoor itemDoor;
    public GameObject key;
    public bool oneTime = false;
    [SerializeField]private int count;
    // Start is called before the first frame update
    void Start()
    {
        key.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
         if(count>0 && oneTime) return;
         count++;
        base.OnTriggerEnter2D(collision);
        Debug.Log("" + collision.name);
        // SetCollier_Enable(false);
        if (collision.GetComponent<PlayerMovement>().isMoveLeft)
        {
            key.transform.DOLocalRotate(new Vector3(0f, 0f, 35f), 0.3f).SetEase(Ease.OutQuart);
            itemDoor.SetCloseDoor();
        }
        else
        {
            key.transform.DOLocalRotate(new Vector3(0f, 0f, -35f), 0.3f).SetEase(Ease.OutQuart);
            itemDoor.SetOpenDoor();
        }

    }


}
