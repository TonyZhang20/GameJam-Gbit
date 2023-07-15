using System;
using DG.Tweening;
using UnityEngine;

namespace Script
{
    public class ClockRotate : RotateScript
    {
        private float _rotateAngle = -30f;
        private bool _ableToRotate = true;
        protected override void RotateFunction()
        {
            if (counting >= 0 && _ableToRotate)
            {
                counting -= Time.deltaTime;
            }
            else if(_ableToRotate)
            {
                RotateAngle(_rotateAngle);
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
            _rotateAngle = -30;
            _ableToRotate = true;
        }

        /// <summary>
        /// 顺时针输入负数角度，逆时针输入正数
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="time"></param>
        public override void RotateAngle(float angle, float time = 0.2f)
        {
            DOTween.KillAll();
            
            this.angle += angle;
            DOTween.Kill(_tweenerCore);
            
            _tweenerCore = transform.DORotateQuaternion(Quaternion.AngleAxis(this.angle, new Vector3(0, 0, 1)), time).OnComplete(EventHandler.CallAfterJumpFinish);
            //transform.Rotate(new Vector3(0,0,1), angle);
        }
    }
}
