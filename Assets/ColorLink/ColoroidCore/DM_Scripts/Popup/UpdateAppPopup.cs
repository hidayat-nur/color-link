using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.Localization;
using Bitberry;
using UnityEngine.UI;

public class UpdateAppPopup : Popup
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
            Header.text = API.GetText(WordIDs.update_required);
            Des.text = API.GetText(WordIDs.update_required_desc);
            Continue.text = API.GetText(WordIDs.update_now);

        }
    }
}
