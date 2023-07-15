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
            
            //DOTween.KillAll();
            
            RotateScript.RotateAngle(angle, holdingTime/2);
            
            angle = 0;
            holdingTime = 0;

            //transform.DOScale(new Vector3(0.39638f, 2.4065f,1f), 0.2f).OnComplete(EventHandler.CallAfterJumpFinish);
        }

        protected override void PrepareJump()
        {
            angle += force * Time.deltaTime;
            holdingTime += Time.deltaTime;
            //transform.DOScale(new Vector3(0.4818395f, 0.7325628f,1f), 3f);
        }
        

    }
}
