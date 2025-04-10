/*
 * Created on 2025
 *
 * Copyright (c) 2025 bitberrystudio
 * Support : bitberrystudio001@gmail.com
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.DailyRewards;
using Gley.DailyRewards.API;
using EasyUI.Toast;


namespace Bitberry.ColorLink
{


    public class DailyReward : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Calendar.AddClickListener(CalendarButtonClicked);
        }

        private void CalendarButtonClicked(int dayNumber, int rewardValue, Sprite rewardSprite)
        {

            

            // Get the current amount of coins
            //int animateFromCoins = GameController.Instance.Coins;

            // Give the amount of coins
            //   GameController.Instance.GiveCoins(rewardValue, false);
            GameManager.Instance.GiveHints(rewardValue);

            // Get the amount of coins now after giving them
          //  int animateToCoins = GameController.Instance.Coins;

            Toast.Show("Successful Reward " + rewardValue + " hints!", 3f, ToastColor.Green, ToastPosition.MiddleCenter);

            // Show the popup to the user so they know they got the coins
             //PopupManager.Instance.Show("reward_ad_granted", new object[] { rewardValue, animateFromCoins, animateToCoins });

        }

        public void ShowRewardPannel()
        {
           // Debug.Log("Chay vaof day");
            Calendar.Show();
        }
    }

}