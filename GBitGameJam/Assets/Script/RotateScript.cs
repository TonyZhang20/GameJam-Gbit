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
        public float angle = 0;

        protected float counting = 1f;
        protected TweenerCore<Quaternion, Vector3, QuaternionOptions> _tweenerCore;
        protected abstract void RotateFunction();
        public abstract void RotateAngle(float angle, float time = 0.2f, RotateMode rotateMode = RotateMode.Fast);

        private void Update()
        {
            RotateFunction();
        }
        
        public float GetCurrentAngle()
        {
            return angle;
        }
    }
}
