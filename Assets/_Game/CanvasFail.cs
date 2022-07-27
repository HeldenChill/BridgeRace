using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BridgeRace.UI
{
    using Manager;
    using Utilitys;
    public class CanvasFail : UICanvas
    {
        public void MainMenuButton()
        {
            UIManager.Inst.OpenUI(UIID.UICMainMenu);

            Close();
        }
    }
}