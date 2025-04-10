/*
 * Created on 2025
 *
 * Copyright (c) 2025 bitberrystudio
 * Support : bitberrystudio001@gmail.com
 */
using UnityEngine;
using UnityEngine.UI;
using Dotmob;
using Gley.Localization;
public class TimeoutPopup : Popup
{
    [SerializeField] private Text Header, Des, Continue;
    public override void OnShowing(object[] inData)
    {
        base.OnShowing(inData);

        RefreshText();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    

    public void OnClickContinue()
    {
        Dotmob.ColorLink.GameManager.Instance.ResetBoard();

    }

    void RefreshText()
    {
        if (Header != null && Des != null && Continue != null)
        {
            Header.text = API.GetText(WordIDs.Timeout_title);
            Des.text = API.GetText(WordIDs.Timeout_des);
            Continue.text = API.GetText(WordIDs.Timeout_continue);

        }
    }
}
