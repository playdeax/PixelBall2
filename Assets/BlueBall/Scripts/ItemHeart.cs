using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeart : ItemBase
{
   public bool needViewAd;
   public int healthPoint;
   public ItemHeartReward itemHeartRewardPrefab;
   public override void CollisionWithBall()
   {
      if (needViewAd)
      {
         if (AdmobManager.instance.isRewardAds_Avaiable())
         {
            //Time.timeScale = 0f;
            AdmobManager.instance.ShowRewardAd_CallBack(state =>
            {
               if (state == AdmobManager.ADS_CALLBACK_STATE.SUCCESS)
               {

                  IncreaseHealthPoint();
               }
               //Time.timeScale = 1f;
            });
         }
         else
         {
            NotificationPopup.instance.AddNotification("No Video Available!");
         }
      }
      else
      {
         IncreaseHealthPoint();
      }
      gameObject.SetActive(false);
   }

   private void IncreaseHealthPoint()
   {
      //Config.AddHeart(healthPoint);
      // Config.currGameState = Config.GAMESTATE.PAUSE;
      var pos = transform.position;
      GamePlayManager.Ins.AddHeart(healthPoint,pos,GamePlayManager.Ins.heartGroup.txtHeart.transform.position, ()=>{
         // Config.currGameState = Config.GAMESTATE.PLAYING;
         GamePlayManager.Ins.ClearMove();
         Destroy(gameObject);});
     
   }
   
}
