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
        [Header("旋转速度")] public float rotateSpeed = 5; //旋转速度
        public Transform pointer;
        protected float counting = 1f;
        protected TweenerCore<Quaternion, Vector3, QuaternionOptions> _tweenerCore;
        protected abstract void RotateFunction();
        /// <summary>
        /// 顺时针输入负数角度，逆时针输入正数
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="target"></param>
        /// <param name="time"></param>
        public abstract void RotateAngle(float angle, Transform target, float time = 0.2f ,bool callEvent = false);

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
