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
        public void PlayAgainButton()
        {
            int v = LevelManager.Inst.CurrentLevel.CurrentLevelIndex;
            LevelManager.Inst.CurrentLevel.ConstructLevel(v);
            CanvasGameplay ui = (CanvasGameplay)UIManager.Inst.OpenUI(UIID.UICGamePlay);
            GameManager.Inst.StopGame();
            ui.StartGame();
            Close();
        }
        public void MainMenuButton()
        {
            UIManager.Inst.OpenUI(UIID.UICMainMenu);
            GameManager.Inst.StopGame();
            Close();
        }
    }
}