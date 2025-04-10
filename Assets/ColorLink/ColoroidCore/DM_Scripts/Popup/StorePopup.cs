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

public class StorePopup : Popup
{
    [SerializeField] private Text Header;
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
        if (Header != null )
        {
            Header.text = API.GetText(WordIDs.StorePop_title);
         

        }
    }
}
