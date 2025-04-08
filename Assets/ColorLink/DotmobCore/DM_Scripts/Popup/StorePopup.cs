/*Created on 2024
*
*Copyright(c) 2024 dotmobstudio
* Support : dotmobstudio @gmail.com
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.Localization;
using Dotmob;
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
