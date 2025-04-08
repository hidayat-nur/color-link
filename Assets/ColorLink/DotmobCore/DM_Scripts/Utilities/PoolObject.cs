/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dotmob
{
	public class PoolObject : MonoBehaviour
	{
		#region Member Variables

		public bool			isInPool;
		public ObjectPool	pool;
		public CanvasGroup	canvasGroup;

		#endregion
	}
}
