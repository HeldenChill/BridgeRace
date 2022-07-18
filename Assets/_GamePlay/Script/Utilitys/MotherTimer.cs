using UnityEngine.Events;
using System;

namespace Utilitys.Timer
{
    public class MotherTimer
    {
        public UnityEvent TimerUpdate;
        private static MotherTimer inst = null;

        public static MotherTimer Inst
        {
            get
            {
                if (inst == null)
                {
                    inst = new MotherTimer();
                }
                return inst;
            }
        }

        private MotherTimer()
        {
            //Debug.Log("Mother Timer Instance");
            TimerUpdate = new UnityEvent();
        }


        public void Update()
        {
            TimerUpdate.Invoke();
        }

        ~MotherTimer()
        {
            TimerUpdate.RemoveAllListeners();
        }

    }
}
