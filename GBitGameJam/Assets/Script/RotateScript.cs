using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UIElements;

namespace Script
{
    public enum RotateAxis
    {
        X,
        Y,
        Z
    }
    public abstract class RotateScript : MonoBehaviour
    {
        [Header("父物体旋转一圈时间")] public float rotateSpeed = 5; //本体旋转速度
        [Header("是否连续旋转")] public bool continueRotate = false;
        public RotateAxis Axis;
        public Transform pointer;
        protected float counting = 1f; //倒计时时间
        protected TweenerCore<Quaternion, Vector3, QuaternionOptions> _tweenerCore;
        protected bool ableToRotate = true;
        protected Vector3 rotateAxis;

        public float totalAngle;
        
        private void Start()
        {
            switch (Axis)
            {
                case RotateAxis.X:
                    rotateAxis = new Vector3(1, 0, 0);
                    break;
                case RotateAxis.Y:
                    rotateAxis = new Vector3(0, 1, 0);
                    break;
                case RotateAxis.Z:
                    rotateAxis = new Vector3(0, 0, 1);
                    break;
            }
        }
        protected virtual void RotateFunction()
        {
            if (continueRotate && ableToRotate)
            {
                totalAngle += rotateSpeed * Time.deltaTime;
                transform.Rotate(rotateAxis, rotateSpeed * Time.deltaTime);
            }
        }   

        /// <summary>
        /// 顺时针输入负数角度，逆时针输入正数
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="target"></param>
        /// <param name="time"></param>
        /// <param name="action"></param>
        public virtual void RotateAngle(float angle, Transform target, float time = 0.2f, Action action = null)
        {
            DOTween.Kill(_tweenerCore);

            _tweenerCore = target.DOLocalRotate(-rotateAxis * angle, time, RotateMode.WorldAxisAdd);
            
            if(action == null) return;
            
            _tweenerCore.OnComplete(action.Invoke);
        }

        private void Update()
        {
            RotateFunction();
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
            }
        }
        
        public float GetCurrentAngle()
        {
            return transform.rotation.z;
        }
    }
}
