/*Created on 2025
*
*Copyright(c) 2025 bitberrystudio
* Support : bitberrystudio001@gmail.com
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.Localization;
using Bitberry;
using UnityEngine.UI;

public class RewardAdGranted : Popup
{
    [SerializeField] private Text Header, Des, Continue;
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
        if (Header != null && Des != null && Continue != null)
        {
            Header.text = API.GetText(WordIDs.RewardAdPop_title);
            Des.text = API.GetText(WordIDs.RewardAdPop_des);
            Continue.text = API.GetText(WordIDs.RewardAdPop_continue);

        }
    }
}