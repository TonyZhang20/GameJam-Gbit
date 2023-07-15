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
        

        protected override void RotateFunction()
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

        /// <summary>
        /// 顺时针输入负数角度，逆时针输入正数
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="target"></param>
        /// <param name="time"></param>
        public override void RotateAngle(float angle,  Transform target, float time = 0.2f, bool callEvent = false)
        {
            DOTween.Kill(_tweenerCore);

            _tweenerCore = target.DOLocalRotate(new Vector3(-angle, 0, 0), time, RotateMode.WorldAxisAdd);

            if (callEvent)
                _tweenerCore.OnComplete(EventHandler.CallAfterJumpFinish);
        }
        
    }
}
