using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeart : ItemBase
{
   public bool needViewAd;
   public int healthPoint;

   public override void CollisionWithBall()
   {
      if (needViewAd)
      {
         if (AdmobManager.instance.isRewardAds_Avaiable())
         {
            Time.timeScale = 0f;
            AdmobManager.instance.ShowRewardAd_CallBack(state =>
            {
               if (state == AdmobManager.ADS_CALLBACK_STATE.SUCCESS)
               {

                  IncreaseHealthPoint();
               }
               Time.timeScale = 1f;
            });
         }
         else
         {
            IncreaseHealthPoint();
         }
      }
      else
      {
         IncreaseHealthPoint();
      }
      Destroy(gameObject);
   }

   private void IncreaseHealthPoint()
   {
      Config.AddHeart(healthPoint);
   }
}
