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
        public override void RotateAngle(float angle, Transform target, float time = 0.2F, RotateMode rotateMode = RotateMode.Fast, bool replaceAngle = true)
        {
            DOTween.Kill(_tweenerCore);

            _tweenerCore = target.DORotate(new Vector3(0, 0, -angle), time, RotateMode.WorldAxisAdd).OnComplete(EventHandler.CallAfterJumpFinish);
        }

        protected override void RotateFunction()
        {
            transform.localEulerAngles += new Vector3(0, 0, rotateSpeed * Time.deltaTime);
        }
    }
}
