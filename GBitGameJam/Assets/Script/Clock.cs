using DG.Tweening;
using UnityEngine;

namespace Script
{
    public class Clock : BaseClock
    {
        // Update is called once per frame
        protected override void GenerateStartJumpPoint()
        {
            
        }

        protected override void GenerateEndJumpPoint()
        {
         
        }

        protected override void Jump()
        {
            EventHandler.CallBeforeJumpStart(); //跳跃开始
            CameraShake.Instance.StopShake();   
            //DOTween.KillAll();
            
            RotateScript.RotateAngle(-angle, Pointer, 0.1f, RotateMode.FastBeyond360,false);
            
            angle = 0;
            holdingTime = 0;
        }

        protected override void PrepareJump()
        {
            angle += force * Time.deltaTime;
            holdingTime += Time.deltaTime;
            
            if(holdingTime >= 1.5f && !CameraShake.shaking) CameraShake.Instance.StartShake();
        }
        

    }
}
