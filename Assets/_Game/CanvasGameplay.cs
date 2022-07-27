using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.UI
{
    using Manager;
    using Utilitys;
    public class CanvasGameplay : UICanvas
    {
        public void SettingButton()
        {
            UIManager.Inst.OpenUI(UIID.UICSetting);
            GameManager.Inst.StopGame();
        }
    }
}