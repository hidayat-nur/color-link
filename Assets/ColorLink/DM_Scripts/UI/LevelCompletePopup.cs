﻿/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gley.Localization;
namespace Dotmob.ColorLink
{
	public class LevelCompletePopup : Popup
	{
		#region Inspector Variables

		[Space]
		[SerializeField] private Text			numMovesText		= null;
		[SerializeField] private Color			numMovesColor		= Color.white;
		[Space]
		[SerializeField] private CanvasGroup	starEarnedImage		= null;
	//	[SerializeField] private Text			starInfoText		= null;
		[SerializeField] private AnimationCurve	starEarnedAnimCurve	= null;
		[Space]
		[SerializeField] private GameObject		nextLevelButton		= null;
		[SerializeField] private GameObject		backToMenuButton	= null;
		[Space]
		[SerializeField] private ProgressBar	giftProgressBar		= null;
		[SerializeField] private Text			giftProgressText	= null;
		[SerializeField] private GiftBox		giftBox				= null;
		[SerializeField] private CanvasGroup	giftAnimContainer	= null;
		[SerializeField] private CanvasGroup	giftAnimBkgFade		= null;

		[SerializeField] private Sprite[] spriteImage = null;
		[SerializeField] private Image imgWin = null;


		#endregion

		#region Member Variables

		private const float StarEarnedAnimDuration = 0.75f;
		private const float GiftProgressAnimDuration = 0.5f;

		private IEnumerator giveGiftAnimRoutine;
		private IEnumerator giftBoxAnimRoutine;

		#endregion

		#region Public Methods

		public override void OnShowing(object[] inData)
		{
			base.OnShowing(inData);

			bool	isLastLevel			= (bool)inData[0];
			int		numMoves			= (int)inData[1];
			int		numMoveForStar		= (int)inData[2];
			bool	earnedStar			= (bool)inData[3];
			bool	alreadyEarnedStar	= (bool)inData[4];
			bool	giftProgressed		= (bool)inData[5];
			bool	giftAwarded			= (bool)inData[6];
			int		fromGiftProgress	= (int)inData[7];
			int		toGiftProgress		= (int)inData[8];
			int		numLevelsForGift	= (int)inData[9];

			ResetUI();

			int ran = Random.Range(0, 5);
			if(imgWin != null)
            {
				switch (ran)
                {
					case 0:
						imgWin.sprite = spriteImage[0];
						break;
					case 1:
						imgWin.sprite = spriteImage[1];
						break;
					case 2:
						imgWin.sprite = spriteImage[2];
						break;
					case 3:
						imgWin.sprite = spriteImage[3];
						break;
					case 4:
						imgWin.sprite = spriteImage[4];
						break;
					case 5:
						imgWin.sprite = spriteImage[5];
						break;
				}

			 }

			numMovesText.text = string.Format(API.GetText(WordIDs.winpop_des), ColorUtility.ToHtmlStringRGB(numMovesColor), numMoves);
			

			SetupStarImage(earnedStar, alreadyEarnedStar, numMoveForStar);

			nextLevelButton.SetActive(!isLastLevel);
			backToMenuButton.SetActive(isLastLevel);

			int giftFromAmt	= (fromGiftProgress % numLevelsForGift);
			int giftToAmt	= (giftAwarded ? numLevelsForGift : toGiftProgress % numLevelsForGift);

			if (giftProgressed)
			{
				giftProgressText.text = string.Format("{0} / {1}", giftToAmt, numLevelsForGift);

				float fromProgress	= (float)giftFromAmt / (float)numLevelsForGift;
				float toProgress	= (float)giftToAmt / (float)numLevelsForGift;

				float giftProgressStartDelay = animDuration + 0.25f + (earnedStar ? StarEarnedAnimDuration : 0);

				giftProgressBar.SetProgressAnimated(fromProgress, toProgress, GiftProgressAnimDuration, giftProgressStartDelay);

				if (giftAwarded)
				{
					StartCoroutine(giveGiftAnimRoutine = PlayGiftAwardedAnimation(giftProgressStartDelay + GiftProgressAnimDuration + 0.25f));
				}
			}
			else
			{
				giftProgressText.text = string.Format("{0} / {1}", giftFromAmt, numLevelsForGift);

				giftProgressBar.SetProgress((float)giftFromAmt / (float)numLevelsForGift);
			}
		}

		#endregion

		#region Private Methods

		private void SetupStarImage(bool earnedStar, bool alreadyEarnedStar, int numMoveForStar)
		{
			if (alreadyEarnedStar)
			{
				// If the star was already earned then just show the star
				starEarnedImage.alpha = 1f;

				//starInfoText.gameObject.SetActive(false);
			}
			else if (earnedStar)
			{
				starEarnedImage.alpha = 1f;

				//starInfoText.gameObject.SetActive(true);

				//starInfoText.text = "You earned +1 star!";

				PlayStarEarnedAnimation();
			}
			else
			{
				starEarnedImage.alpha = 0f;

				//starInfoText.gameObject.SetActive(true);

				//starInfoText.text = string.Format("Complete the level in <color=#{0}>{1}</color> moves to earn a star!", ColorUtility.ToHtmlStringRGB(numMovesColor), numMoveForStar);
			}
		}

		private void PlayStarEarnedAnimation()
		{
			UIAnimation anim;

			anim					= UIAnimation.ScaleX(starEarnedImage.transform as RectTransform, 1.5f, 1f, StarEarnedAnimDuration);
			anim.style				= UIAnimation.Style.Custom;
			anim.startOnFirstFrame	= true;
			anim.startDelay			= animDuration;
			anim.animationCurve		= starEarnedAnimCurve;
			anim.Play();

			anim					= UIAnimation.ScaleY(starEarnedImage.transform as RectTransform, 1.5f, 1f, StarEarnedAnimDuration);
			anim.style				= UIAnimation.Style.Custom;
			anim.startOnFirstFrame	= true;
			anim.startDelay			= animDuration;
			anim.animationCurve		= starEarnedAnimCurve;
			anim.Play();

			anim					= UIAnimation.Alpha(starEarnedImage, 0f, 1f, StarEarnedAnimDuration);
			anim.startOnFirstFrame	= true;
			anim.startDelay			= animDuration;
			anim.style	= UIAnimation.Style.EaseIn;
			anim.Play();
		}

		private IEnumerator PlayGiftAwardedAnimation(float startDelay)
		{
			// Wait before starting the gift animations
			yield return new WaitForSeconds(startDelay);

			SoundManager.Instance.Play("gift-awarded");

			// Set the gift to the gift animation container so it will appear ontop of everything
			giftBox.transform.SetParent(giftAnimContainer.transform);

			// Set the gift animation container as active
			giftAnimContainer.gameObject.SetActive(true);

			UIAnimation anim;

			// Fade in the gift container background
			anim = UIAnimation.Alpha(giftAnimBkgFade, 0f, 1f, 0.5f);
			anim.startOnFirstFrame = true;
			anim.Play();

			giftBoxAnimRoutine = giftBox.PlayOpenAnimation();

			// Play the gift open animations and wait for them to finish
			yield return giftBoxAnimRoutine;

			giftBoxAnimRoutine = null;

			// Wait a bit so the player can see what they got
			yield return new WaitForSeconds(1f);

			// Fade out the whole gift container so the popup is now visible again
			anim = UIAnimation.Alpha(giftAnimContainer, 1f, 0f, 0.5f);
			anim.startOnFirstFrame = true;
			anim.Play();

			while (anim.IsPlaying)
			{
				yield return null;
			}

			// Set the gift container to de-active so the player can click buttons on the popup again
			giftAnimContainer.gameObject.SetActive(false);
			giftAnimContainer.alpha = 1f;

			giveGiftAnimRoutine = null;
		}

		private void ResetUI()
		{
			if (giveGiftAnimRoutine != null)
			{
				StopCoroutine(giveGiftAnimRoutine);
				giveGiftAnimRoutine = null;
			}

			if (giftBoxAnimRoutine != null)
			{
				StopCoroutine(giftBoxAnimRoutine);
				giftBoxAnimRoutine = null;
			}

			giftBox.ResetUI();

			UIAnimation.DestroyAllAnimations(starEarnedImage.gameObject);
			UIAnimation.DestroyAllAnimations(giftAnimBkgFade.gameObject);
			UIAnimation.DestroyAllAnimations(giftAnimContainer.gameObject);
		}

		#endregion
	}
}
