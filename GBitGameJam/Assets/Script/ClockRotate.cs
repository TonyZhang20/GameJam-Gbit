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
        private bool _ableToRotate = true;
        [SerializeField] private bool continueRotate;

        protected override void RotateFunction()
        {
            if (continueRotate)
            {
                
            }
            else
            {
                if (counting >= 0 && _ableToRotate)
                {
                    counting -= Time.deltaTime;
                }
                else if(_ableToRotate)
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
            _ableToRotate = false;
        }

        private void FinishJump()
        {
            _rotateAngle = rotateAngle;
            _ableToRotate = true;
        }
    }
}
