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
        public float waitTine = 12;
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

            if (MovedAngle >= waitTine)
            {
                MovedAngle = 0;
                AudioManager.Instance.PlayAudio(Sound.SecondPoint);
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

        // ReSharper disable Unity.PerformanceAnalysis
        protected override void ChildUpdate()
        {
            base.ChildUpdate();
            TotalAngleChecker();
        }
        
        private void TotalAngleChecker()
        {
            if (totalAngle < -360)
            {
                //正转一圈
                totalAngle += 360;
                LevelOneManager.Instance.TimeGoes();
                FindObjectOfType<Timeclock>().MoveForward();
            }
            else if(totalAngle > 0)
            {
                //反转一圈
                LevelOneManager.Instance.ZoomOut();
                FindObjectOfType<Timeclock>().MoveBackward();
                totalAngle -= 360;
                if (counting >= 1)
                {
                    GetComponent<Clock>().redAngleType = RedAngleType.AfterJump;
                }
            }
        }

        public override void RotateAngle(float angle, Transform target, float time = 0.2f, Action action = null)
        {
            base.RotateAngle(angle, target, time, action);
            
        }
    }
}
