using System;
using UnityEngine;
using UnityEngine.UI;

namespace BlueBall.Scripts.Boss
{
    public class BossHPGroup : MonoBehaviour
    {
        [SerializeField] private Image hpBar;
        [SerializeField] private GameObject coinGroup, starGroup;

        private int _maxHP;
        private int _currentHP;

        private static BossHPGroup _instance;

        public static BossHPGroup Instance => _instance;

        private void Awake()
        {
            _instance = this;
        }

        public void Init(int maxHP)
        {
            _maxHP = maxHP;
            _currentHP = _maxHP;
            hpBar.fillAmount = 1f;
        }

        public void DecreaseHP(int decrease = 1)
        {
            _currentHP -= decrease;
            hpBar.fillAmount = _currentHP * 1f / _maxHP;
        }

        public void OnBossDie()
        {
            coinGroup.SetActive(true);
            starGroup.SetActive(true);
        }
        
    }
}