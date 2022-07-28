using TMPro;
using UnityEngine;

namespace BridgeRace.UI
{
    using Manager;
    using Utilitys;
    using Utilitys.Timer;
    public class CanvasGameplay : UICanvas
    {
        [SerializeField]
        private GameObject timerBanner;
        [SerializeField]
        private GameObject optionMenuButton;
        [SerializeField]
        private TMP_Text clockTxt;

        float timeRemaining = 3f;
        bool isShowBanner = true;

        public void StartGame()
        {
            timeRemaining = 3.4f;
            optionMenuButton.SetActive(false);
            timerBanner.SetActive(true);
        }

        private void Update()
        {
            if (isShowBanner)
            {
                if (timeRemaining > -0.5f)
                {
                    clockTxt.text = Mathf.RoundToInt(timeRemaining).ToString();
                    timeRemaining -= Time.unscaledDeltaTime;
                }
                else
                {
                    if (!GameManager.Inst.GameIsRun)
                    {
                        GameManager.Inst.StartGame();
                        timerBanner.SetActive(false);
                        optionMenuButton.SetActive(true);
                        isShowBanner = false;
                    }
                }
            }
            
        }
        public void SettingButton()
        {
            UIManager.Inst.OpenUI(UIID.UICSetting);
            GameManager.Inst.StopGame();
        }
    }
}