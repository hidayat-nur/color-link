/*
 * Created on 2025
 *
 * Copyright (c) 2025 bitberrystudio
 * Support : bitberrystudio001@gmail.com
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gley.MobileAds;
using System;
using EasyUI.Toast;

namespace Bitberry.ColorLink
{
	[RequireComponent(typeof(Button))]
	public class RewardAdButton : MonoBehaviour
	{
		#region Inspector Variables

		[SerializeField] private int hintsToReward;

		#endregion

		#region Properties

		public Button Button { get { return gameObject.GetComponent<Button>(); } }

		#endregion

		#region Unity Methods

		private void Awake()
		{
			Button.onClick.AddListener(OnClick);
		}

		#endregion

		#region Private Methods

		private void OnClick()
		{

            if (API.IsRewardedVideoAvailable())
            {
				API.ShowRewardedVideo(completeMethod);
            }
            else
            {
				Toast.Show("Reward Ads not avaiable now, please try again later!", ToastColor.Black, ToastPosition.MiddleCenter);
            }
			
		}

        private void completeMethod(bool s)
        {
            if (s)
            {
				OnRewardAdGranted();
            }
        }

        private void OnRewardAdLoaded()
		{
			gameObject.SetActive(true);
		}

		private void OnRewardAdClosed()
		{
			gameObject.SetActive(false);
		}

		private void OnRewardAdGranted()
		{
			// Give the hints
			GameManager.Instance.GiveHints(hintsToReward);

			// Show the popup to the user so they know they got the hint
			PopupManager.Instance.Show("reward_ad_granted");
		}

		private void OnAdsRemoved()
		{
			//MobileAdsManager.Instance.OnRewardAdLoaded -= OnRewardAdLoaded;
			gameObject.SetActive(false);
		}

		#endregion
	}
}
