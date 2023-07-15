using System;
using DG.Tweening;
using UnityEngine;

namespace Script
{
    public class ClockRotate : RotateScript
    {
        private float _rotateAngle = 30f;
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
            _rotateAngle = 30;
            _ableToRotate = true;
        }

        /// <summary>
        /// 顺时针输入负数角度，逆时针输入正数
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="target"></param>
        /// <param name="time"></param>
        /// <param name="rotateMode"></param>
        /// <param name="replaceAngle"></param>
        public override void RotateAngle(float angle,  Transform target, float time = 0.2f, RotateMode rotateMode = RotateMode.Fast, bool replaceAngle = true)
        {
            if (angle >= 120) rotateMode = RotateMode.FastBeyond360;

            float targetAngle = target.eulerAngles.z + angle;

            if (targetAngle >= 180)
            {
                targetAngle -= 360;
            }
            else if(targetAngle < -180)
            {
                targetAngle += 360;
            }
            
            DOTween.Kill(_tweenerCore);

            //_tweenerCore = target.DORotate(new Vector3(0, 180, targetAngle), time, rotateMode).OnComplete(EventHandler.CallAfterJumpFinish);
            _tweenerCore = target.DORotate(new Vector3(0, 180, targetAngle), time, rotateMode).OnComplete(EventHandler.CallAfterJumpFinish);
        }
        
    }
}
