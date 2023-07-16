using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Script
{
    public class ClockRotate : RotateScript
    {
        public float rotateAngle;
        private float _rotateAngle;
        
        protected override void RotateFunction()
        {
            base.RotateFunction();

            if (!continueRotate)
            {
                if (counting >= 0 && ableToRotate)
                {
                    counting -= Time.deltaTime;
                }
                else if(ableToRotate) 
                {
                    RotateAngle(_rotateAngle, transform);
                    counting = 1;
                }
            }

        }

        private void OnEnable()
        {
            _rotateAngle = rotateAngle;
            EventHandler.BeforeJumpStart += DuringJump;
            EventHandler.AfterJumpFinish += FinishJump;
        }

        private void OnDisable()
        {
            EventHandler.BeforeJumpStart -= DuringJump;
            EventHandler.AfterJumpFinish -= FinishJump;
        }

        private void DuringJump()
        {
            _rotateAngle = 0;
            ableToRotate = false;
        }

        private void FinishJump()
        {
            _rotateAngle = rotateAngle;
            ableToRotate = true;
        }

        public override void RotateAngle(float angle, Transform target, float time = 0.2f, Action action = null)
        {
            base.RotateAngle(angle, target, time, action);
            
        }
    }
}
