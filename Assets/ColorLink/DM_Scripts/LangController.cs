/*
 * Created on 2025
 *
 * Copyright (c) 2025 bitberrystudio
 * Support : bitberrystudio001@gmail.com
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.Localization;
using UnityEngine.UI;


	public class LangController : SingletonComponent<LangController>
	{
		[SerializeField] private Dropdown dropdownLang;


		private void Start()
		{

			dropdownLang.onValueChanged.AddListener(delegate
			{
				DropdownValueChanged(dropdownLang);
			});

			// Load saved value
			int savedValue = PlayerPrefs.GetInt("SelectedDropdownValue_colorlink", 0); // default value is 0
			dropdownLang.value = savedValue;

		}


		private void RefreshText()
		{
		//Debug.Log("......RefreshText ALL TEXT........ ");


		Dotmob.ColorLink.GameManager.Instance.UpdateTextUI();
	
			//UIController.Instance.UpdateUI();
			//UIController.Instance.OnNewLevelStarted();
			//RewardAdGrantedPopup.In
		}

		void DropdownValueChanged(Dropdown change)
		{
			// Save selected value
			PlayerPrefs.SetInt("SelectedDropdownValue_colorlink", change.value);
			PlayerPrefs.Save();

			// You can perform other actions based on the selected value if needed
			Debug.Log("Selected Dropdown Value: " + change.value);

			//Debug.Log("Current Scene :"+ ScreenManager.Instance.CurrentScreenId)
		}

		public void GetDropDowValue()
		{
			//int indexDropDow = dropdownLang.value;

			Debug.Log("DROPDOW LANG :" + dropdownLang.value);

			switch (dropdownLang.value)
			{
				case 0:
					Gley.Localization.API.SetCurrentLanguage(SupportedLanguages.English);
					RefreshText();
					break;
				case 1:
					Gley.Localization.API.SetCurrentLanguage(SupportedLanguages.French);
					RefreshText();
					break;

				case 2:
					Gley.Localization.API.SetCurrentLanguage(SupportedLanguages.Spanish);
					RefreshText();
					break;
            case 3:
                Gley.Localization.API.SetCurrentLanguage(SupportedLanguages.Turkish);
                RefreshText();
                break;
            case 4:
                Gley.Localization.API.SetCurrentLanguage(SupportedLanguages.Korean);
                RefreshText();
                break;
			case 5:
				Gley.Localization.API.SetCurrentLanguage(SupportedLanguages.Chinese);
				RefreshText();
				break;
			case 6:
				Gley.Localization.API.SetCurrentLanguage(SupportedLanguages.Russian);
				RefreshText();
				break;
			case 7:
				Gley.Localization.API.SetCurrentLanguage(SupportedLanguages.Japanese);
				RefreshText();
				break;
		}
		}
	}