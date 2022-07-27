using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BridgeRace.Manager;
namespace BridgeRace.UI
{
    using Utilitys;
    public class CanvasSetting : UICanvas
    {
        public void CloseButton()
        {
            GameManager.Inst.StartGame();
            Close();
        }
    }
}