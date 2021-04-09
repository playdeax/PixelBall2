using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class StarGroup : MonoBehaviour
{
    public Image starCollected;
    public List<Image> iconStars;

    public int countStar;
    public int maxStar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitStarGroup(int _maxStar) {
        maxStar = _maxStar;
        countStar = 0; 
    }

    public void AddStar() {

        StartCoroutine(AddStar_IEnumerator());
    }

    public IEnumerator AddStar_IEnumerator() {
        yield return new WaitForSeconds(0.6f);
        iconStars[countStar].sprite = starCollected.sprite;
        countStar = countStar + 1;
        if (countStar > maxStar) countStar = maxStar;
    }
}
