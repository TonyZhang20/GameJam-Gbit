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
                transform.Rotate(rotateAxis, rotateSpeed * Time.deltaTime);
            }
        }

        /// <summary>
        /// 顺时针输入负数角度，逆时针输入正数
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="target"></param>
        /// <param name="time"></param>
        public void RotateAngle(float angle, Transform target, float time = 0.2f, bool callEvent = false)
        {
            DOTween.Kill(_tweenerCore);

            _tweenerCore = target.DOLocalRotate(-rotateAxis * angle, time, RotateMode.WorldAxisAdd);

            if (callEvent)
                _tweenerCore.OnComplete(EventHandler.CallAfterJumpFinish);
        }

        private void Update()
        {
            RotateFunction();
        }
        
        public float GetCurrentAngle()
        {
            return transform.rotation.z;
        }
    }
}
