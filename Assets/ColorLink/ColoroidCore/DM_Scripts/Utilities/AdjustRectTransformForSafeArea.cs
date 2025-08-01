﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bitberry
{
	public class AdjustRectTransformForSafeArea : UIMonoBehaviour
	{
		#region Inspector Variables

		[SerializeField] protected bool adjustForBannerAd;

		#endregion

		#region Unity Methods

		protected virtual void Start()
		{
			AdjustScreen();

			
		}

		#endregion

		#region Private Methods

		protected void AdjustScreen()
		{
			Rect safeArea = UnityEngine.Screen.safeArea;

			float yMin = safeArea.yMin;
			float yMax = safeArea.yMax;

			#if UNITY_EDITOR

			// In editor, if the screen width/height is set to iPhoneX then set the offsets as they would be on the iPhoneX
			if (UnityEngine.Screen.width == 1125f && UnityEngine.Screen.height == 2436f)
			{
				yMin = 102;
				yMax = 2304;
			}

			#endif

			float topAreaHeightInPixels		= yMin;
			float bottomAreaHeightInPixels	= UnityEngine.Screen.height - yMax;

			

			float scale			= 1f / Utilities.GetCanvas(transform).scaleFactor;
			float topOffset		= topAreaHeightInPixels * scale;
			float bottomOffset	= bottomAreaHeightInPixels * scale;

			RectT.offsetMax = new Vector2(RectT.offsetMax.x, -topOffset);
			RectT.offsetMin = new Vector2(RectT.offsetMin.x, bottomOffset);
		}

		private void OnAdManagerInitialized()
		{

			AdjustScreen();
		}

		#endregion
	}
}
