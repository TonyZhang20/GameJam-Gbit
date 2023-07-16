using Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class TelephoneRotate : RotateScript
    {
        public Transform Finger;
        public float rotateAngle;
        private float _rotateAngle;
        private Quaternion _fingerWorldRotation;

        protected override void RotateFunction()
        {
            base.RotateFunction();

            Finger.rotation = _fingerWorldRotation;

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
            _fingerWorldRotation = Finger.rotation;
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
            if (totalAngle < -190)
            {
                //正转一圈
                totalAngle += 360;
                LevelTwoManager.Instance.TimeGoes();
            }
            else if (totalAngle > 170)
            {
                Debug.Log(1);
                //反转一圈
                LevelTwoManager.Instance.ZoomOut();
                totalAngle -= 360;
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

