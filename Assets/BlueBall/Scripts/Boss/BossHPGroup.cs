using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlueBall.Scripts.Boss
{
    public class BossHPGroup : MonoBehaviour
    {
        [SerializeField] private Image hpBar;

        private int _maxHP;
        private int _currentHP;
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
        
    }
}