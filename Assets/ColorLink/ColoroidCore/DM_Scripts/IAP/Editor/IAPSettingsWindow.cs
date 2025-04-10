﻿/*
 * Created on 2025
 *
 * Copyright (c) 2025 bitberrystudio
 * Support : bitberrystudio001@gmail.com
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Bitberry
{
	public class IAPSettingsWindow : CustomEditorWindow
	{
		#region Inspector Variables

		private SerializedObject settingsSerializedObject;

		private bool showNoPluginError;
		private bool showBadPluginError;

		#endregion

		#region Properties

		private SerializedObject SettingsSerializedObject
		{
			get
			{
				if (settingsSerializedObject == null)
				{
					settingsSerializedObject = new SerializedObject(IAPSettings.Instance);
				}

				return settingsSerializedObject;
			}
		}

		#endregion

		#region Public Methods

		[MenuItem ("Dotmob/IAP Settings")]
		public static void Open()
		{
			EditorWindow.GetWindow<IAPSettingsWindow>("IAP Settings ©DotmobStudio");
		}

		#endregion

		#region Draw Methods

		public override void DoGUI()
		{
			SettingsSerializedObject.Update();

			DrawIAPSettings();

			GUI.enabled = true;

			SettingsSerializedObject.ApplyModifiedProperties();
		}

		private void DrawIAPSettings()
		{
			BeginBox("IAP Settings ©DotmobStudio");

			DrawEnableDisableButtons();

			Space();

			SerializedProperty productInfosProp = SettingsSerializedObject.FindProperty("productInfos");

			EditorGUILayout.PropertyField(productInfosProp, true);

			if (productInfosProp.isExpanded)
			{
				Space();
			}

			EndBox();
		}

		private void DrawEnableDisableButtons()
		{
			if (!IAPSettings.IsIAPEnabled)
			{
				EditorGUILayout.HelpBox("IAP is not enabled, please import the IAP plugin using the Services window then click the button below.", MessageType.Info);

				if (DrawButton("Enable IAP"))
				{
					if (!EditorUtilities.CheckNamespacesExists("UnityEngine.Purchasing"))
					{
						showNoPluginError = true;
					}
					else if (!EditorUtilities.CheckClassExists("UnityEngine.Purchasing", "StandardPurchasingModule"))
					{
						showBadPluginError = true;
					}
					else
					{
						showNoPluginError	= false;
						showBadPluginError	= false;

						EditorUtilities.SyncScriptingDefineSymbols("DM_IAP", true);
					}
				}

				if (showNoPluginError)
				{
					EditorGUILayout.HelpBox("The Unity IAP plugin was not been detected. Please import the Unity IAP plugin using the Services window and make sure there are no compiler errors in your project. Consult the documentation for more information.", MessageType.Error);
				}
				else if (showBadPluginError)
				{
					EditorGUILayout.HelpBox("The Unity IAP plugin has not been imported correctly. Please click the re-import button in the IAP section of the Services window.", MessageType.Error);
				}
			}
			else
			{
				if (DrawButton("Disable IAP"))
				{
					// Remove DM_IAP from scripting define symbols
					EditorUtilities.SyncScriptingDefineSymbols("DM_IAP", false);
				}
			}

			GUI.enabled = IAPSettings.IsIAPEnabled;
		}

		#endregion
	}
}
