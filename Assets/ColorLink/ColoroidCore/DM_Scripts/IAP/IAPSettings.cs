/*
 * Created on 2025
 *
 * Copyright (c) 2025 bitberrystudio
 * Support : bitberrystudio001@gmail.com
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dotmob
{
	public class IAPSettings : SingletonScriptableObject<IAPSettings>
	{
		#region Classes

		[System.Serializable]
		public class ProductInfo
		{
			public string	productId;
			public bool		consumable;
		}

		#endregion

		#region Member Variables

		public List<ProductInfo> productInfos;

		#endregion

		#region Properties

		public static bool IsIAPEnabled
		{
			get
			{
				#if DM_IAP
				return true;
				#else
				return false;
				#endif
			}
		}

		#endregion
	}
}
