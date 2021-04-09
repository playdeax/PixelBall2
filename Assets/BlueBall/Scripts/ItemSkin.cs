using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemSkin : MonoBehaviour
{
    public Sprite unselect;
    public Sprite selected;

    private Image img;
    private Button btn;

    public int index = 0;

    ItemSkin[] listItems;

    public Animator premiumSkin;
    private Animator animator;
    void Start()
    {
        img = GetComponent<Image>();
        btn = GetComponent<Button>();
        animator = GetComponentInChildren<Animator>();
        btn.onClick.AddListener(() => Selected());
        listItems = transform.parent.GetComponentsInChildren<ItemSkin>();
    }

    public void UnSelect()
    {
        img.sprite = unselect;
    }
    public void Selected() {
        img.sprite = selected;
        premiumSkin.runtimeAnimatorController = animator.runtimeAnimatorController;
        for (int i = 0; i < listItems.Length; i++)
        {
            if (i != index)
            {
                listItems[i].UnSelect();
            }
        }
    }
}
