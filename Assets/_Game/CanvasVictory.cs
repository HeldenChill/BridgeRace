using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BridgeRace.UI
{
    using Manager;
    using Utilitys;
    public class CanvasVictory : UICanvas
    {
        public Text text;

        public void OnInitData(int data)
        {
            text.text = data.ToString();
        }

        public void NextLevelButton()
        {
            int v = LevelManager.Inst.CurrentLevel.CurrentLevelIndex;
            v += 1;
            LevelManager.Inst.CurrentLevel.ConstructLevel(v);            
            CanvasGameplay ui = (CanvasGameplay)UIManager.Inst.OpenUI(UIID.UICGamePlay);
            GameManager.Inst.StopGame();
            ui.StartGame();
            Close();
        }

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

            Close();
        }
    }
}