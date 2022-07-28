using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeRace.UI
{
    using Manager;
    using Utilitys;
    public class CanvasMainMenu : UICanvas
    {
        public void PlayGameButton()
        {
            CanvasGameplay canvas = (CanvasGameplay)UIManager.Inst.OpenUI(UIID.UICGamePlay);
            canvas.StartGame();
            Close();
        }
    }
}