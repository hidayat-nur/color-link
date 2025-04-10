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
using Dotmob;
using UnityEngine.UI;

    public class NotEnoughCoinsPopup : Popup
    {

        [SerializeField] private Text Header, Des, OpenStoreText, WatchAd;
        // Start is called before the first frame update
        void Start()
        {
            RefreshText();
        }

         public override void OnShowing(object[] inData)
        {
            base.OnShowing(inData);

            RefreshText();
        }

        void RefreshText()
        {
        if (Header != null && OpenStoreText != null && Des != null && WatchAd != null)
        {
            Header.text = API.GetText(WordIDs.OutofhintPop_title);
            OpenStoreText.text = API.GetText(WordIDs.OutofhintPop_store);
            Des.text = API.GetText(WordIDs.OutofhintPop_des);
            WatchAd.text = API.GetText(WordIDs.OutofhintPop_ad);

        }
    }

    }
