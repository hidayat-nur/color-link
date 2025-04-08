/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dotmob.ColorLink
{
	public class MainScreen : Screen
	{
		#region Inspector Variables

		[SerializeField] private GameObject	removeAdsButton;

		#endregion

		#region Unity Methods

		protected override void Start()
		{
			base.Start();

		}

		#endregion
	}
}
