﻿/*
 * Created on 2025
 *
 * Copyright (c) 2025 bitberrystudio
 * Support : bitberrystudio001@gmail.com
 */
using UnityEngine;
using System.Collections;

	/// <summary>
	/// Gets a static instance of the Component that extends this class and makes it accessible through the Instance property.
	/// </summary>
	public class SingletonComponent<T> : MonoBehaviour where T : Object
	{
		#region Member Variables

		private static T instance;

		#endregion

		#region Properties

		public static T Instance
		{
			get
			{
				// If the instance is null then either Instance was called to early or this object is not active.
				if (instance == null)
				{
					instance = GameObject.FindObjectOfType<T>();
				}

				return instance;
			}
		}

		#endregion

		#region Unity Methods

		protected virtual void Awake()
		{
			SetInstance();
		}

		#endregion

		#region Public Methods

		public static bool Exists()
		{
			return Instance != null;
		}

		public bool SetInstance()
		{
			if (instance != null && instance != gameObject.GetComponent<T>())
			{
				Debug.LogWarning("[SingletonComponent] Instance already set for type " + typeof(T));
				return false;
			}

			instance = gameObject.GetComponent<T>();

			return true;
		}

		#endregion
	}
