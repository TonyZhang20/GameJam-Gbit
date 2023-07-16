using System;
using DG.Tweening;
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

        public static Action AfterJumpFail;
        public static void CallAfterJumpFail()
        {
            AfterJumpFail?.Invoke();
        }

        public static Action AfterFailJumpFinish;
        public static void CallAfterFailJumpFinish()
        {
            AfterFailJumpFinish?.Invoke();
        }
    }
}
