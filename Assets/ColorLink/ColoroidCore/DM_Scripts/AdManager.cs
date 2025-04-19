/*
 * Created on 2025
 *
 * Copyright (c) 2025 bitberrystudio
 * Support : bitberrystudio001@gmail.com
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.MobileAds;
using Firebase.RemoteConfig;


public class AdManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(WaitAndInitAds());

    }

    private IEnumerator WaitAndInitAds()
    {
        yield return new WaitForSeconds(1f);

        API.Initialize(OnInitialized);
    }

    private void OnInitialized()
    {
        bool showBanner = FirebaseRemoteConfig.DefaultInstance.GetValue("showAdBanner").BooleanValue;
        Debug.Log("showAdBanner from Remote Config: " + showBanner);

        if (showBanner)
        {
            API.ShowBanner(BannerPosition.Bottom, BannerType.Adaptive);
        }

        if (!API.GDPRConsentWasSet())
        {
            API.ShowBuiltInConsentPopup(PopupCloseds);
        }
    }

    private void PopupCloseds()
    {

    }
}
