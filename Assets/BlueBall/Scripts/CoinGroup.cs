using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class CoinGroup : MonoBehaviour
{
    public TextMeshProUGUI txtCoin;
    // Start is called before the first frame update
    void Start()
    {
        Config.OnChangeCoin += OnChangeCoin;
        ShowCoin();
    }

    private void OnDestroy()
    {
        Config.OnChangeCoin -= OnChangeCoin;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnChangeCoin(float coinValue)
    {
        ShowCoin();
    }

    public void ShowCoin()
    {
        DOTween.Kill(txtCoin.transform);
        txtCoin.transform.localScale = Vector3.one;
        txtCoin.transform.DOPunchScale(Vector3.one * 0.3f, 0.2f, 10, 2f).SetEase(Ease.InOutBack).SetRelative(true).SetLoops(3,LoopType.Restart);
        //txtCoin.transform.DOShakeScale(0.3f,0.5f,10).SetEase(Ease.InOutBack).SetRelative(true);
        txtCoin.text = $"{Mathf.FloorToInt(Config.currCoin)}";
    }
}
