using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Script
{
    public class Clock : BaseClock
    {
        private GameObject _platform;
        [SerializeField] private Transform generatePosition;
        [SerializeField] private Vector2 judgmentArea = new Vector2(30,90);
        [SerializeField] private float rotateAngle;
        private float _judgementLength = 15;
        private float _jumpAngle;

        private Quaternion beforePointer;
        
        private void OnEnable()
        {
            EventHandler.AfterJumpFinish += JumpAreaChecker;
        }

        private void OnDisable()
        {
            EventHandler.AfterJumpFinish -= JumpAreaChecker;
        }
        
        protected override void GenerateStartJumpPoint()
        {
            _platform = Instantiate((GameObject)Resources.Load("Prefabs/Platform/Platform"), transform);
            _platform.transform.position = generatePosition.position;

            rotateAngle = Random.Range(judgmentArea.x, judgmentArea.y);
            _platform.transform.RotateAround( transform.position, new Vector3(0,0,1), rotateAngle);
        }

        protected override void JumpAreaChecker()
        {
            Destroy(_platform);

            float targetAngle = Quaternion.Angle(Pointer.transform.rotation, _platform.transform.rotation);
            float currentAngle = Quaternion.Angle(beforePointer, _platform.transform.rotation);
            Debug.Log(targetAngle + " " + currentAngle);
            
            if (Pointer.eulerAngles.z >= rotateAngle && Pointer.eulerAngles.z <= rotateAngle + _judgementLength)
            {
                //Debug.Log("You Made It!");
            }
            else
            {
                //Debug.Log("You failed");
            }
            
            GenerateStartJumpPoint();
        }

        protected override void ChildStart()
        {
            GenerateStartJumpPoint();
        }

        protected override void ChildUpdate()
        {

        }

        protected override void Jump()
        {
            EventHandler.CallBeforeJumpStart(); //跳跃开始
            CameraShake.Instance.StopShake();   
            //DOTween.KillAll();

            beforePointer = new Quaternion(Pointer.rotation.x, Pointer.rotation.y, Pointer.rotation.z, Pointer.rotation.w);
            
            RotateScript.RotateAngle(-angle, Pointer, holdingTime / 5, true);
            
            
            Debug.Log(angle);
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
