using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UIElements;

namespace Script
{
    public abstract class RotateScript : MonoBehaviour
    {
        [Header("父物体旋转一圈时间")] public float rotateSpeed = 5; //本体旋转速度
        [Header("是否连续旋转")] public bool continueRotate = false;
        public Transform pointer;
        protected float counting = 1f; //倒计时时间
        protected TweenerCore<Quaternion, Vector3, QuaternionOptions> _tweenerCore;
        protected bool ableToRotate = true;


        protected virtual void RotateFunction()
        {
            if (continueRotate && ableToRotate)
            {
                transform.Rotate(new Vector3(1, 0, 0), rotateSpeed * Time.deltaTime);
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

            _tweenerCore = target.DOLocalRotate(new Vector3(-angle, 0, 0), time, RotateMode.WorldAxisAdd);

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
