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
        protected override void RotateFunction()
        {
            transform.localEulerAngles += new Vector3(0, 0, rotateSpeed * Time.deltaTime);
        }

        public override void RotateAngle(float angle, Transform target, float time = 0.2f, bool callEvent = false)
        {
            DOTween.Kill(_tweenerCore);

            _tweenerCore = target.DORotate(new Vector3(0, 0, -angle), time, RotateMode.WorldAxisAdd);
            if(callEvent) _tweenerCore.OnComplete(EventHandler.CallAfterJumpFinish);
            
        }
    }
}
