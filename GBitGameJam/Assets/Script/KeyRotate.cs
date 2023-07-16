using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    /// <summary>
    /// 
    /// </summary>
    public class KeyRotate : RotateScript
    {
        public float rotateAngle;
        private float _rotateAngle;

        private AudioSource _audioSource;
        
        protected override void RotateFunction()
        {
            base.RotateFunction();

            if (!continueRotate)
            {
                if (counting >= 0 && ableToRotate)
                {
                    counting -= Time.deltaTime;
                }
                else if (ableToRotate)
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
        protected override void ChildUpdate()
        {
            base.ChildUpdate();
            TotalAngleChecker();
        }
        private void TotalAngleChecker()
        {
            if (totalAngle < -120)
            {
                //��תһȦ
                //totalAngle += 58;
                LevelThreeManager.Instance.TimeGoes();
            }
            else if(totalAngle > 40 && totalAngle < 95)
            {
                LevelThreeManager.Instance.Zoom1Out();
            }
            else if(totalAngle > 95 && totalAngle < 152)
            {
                LevelThreeManager.Instance.Zoom2Out();
            }
            else if (totalAngle > 152)
            {
                //success


                //��תһȦ
                //totalAngle -= 360;
                //if (counting >= 1)
                //{
                //    GetComponent<Clock>().redAngleType = RedAngleType.AfterJump;
                //}
            }
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
    }
}
