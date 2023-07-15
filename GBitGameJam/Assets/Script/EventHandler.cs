using System;
using UnityEngine;

namespace Script
{
    public static class EventHandler
    {
        public static Action BeforeJumpStart;
        public static void CallBeforeJumpStart()
        {
            BeforeJumpStart?.Invoke();
        }

        public static Action AfterJumpFinish;
        public static void CallAfterJumpFinish()
        {
            AfterJumpFinish?.Invoke();
        }
    }
}
