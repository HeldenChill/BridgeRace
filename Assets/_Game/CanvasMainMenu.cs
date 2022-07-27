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
            UIManager.Inst.OpenUI(UIID.UICGamePlay);
            GameManager.Inst.StartGame();
            Close();
        }
    }
}