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
            //LevelManager.Inst.CurrentLevel.SetPlayerPosition();
            LevelManager.Inst.CurrentLevel.ConstructLevel(0);
            canvas.StartGame();
            Close();
        }
    }
}