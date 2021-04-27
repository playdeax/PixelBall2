using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigInfoBalls", menuName = "ScriptableObjects/ConfigInfoBalls")]
public class ConfigInfoBalls : ScriptableObject
{
    public InfoBall[] infoBalls;
}

[Serializable]
public class InfoBall
{
    public Config.BALL_TYPE ballType;
    public int id;
    public string name;
    public AnimatorOverrideController animatorOverrideController;
    public AnimatorOverrideController animatorImgOverrideController;
    public int price;
    public int levelUnlock = 2;
}