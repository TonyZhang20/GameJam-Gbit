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
        [SerializeField] private Vector2 judgmentArea = new Vector2(30,90);
        [SerializeField] private float rotateAngle;
        [SerializeField] private Vector2 judgmentLength = new Vector2(5, 20);
        private float _jumpAngle;
        private float _randomAngle;
        
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
            
            rotateAngle = Random.Range(judgmentArea.x, judgmentArea.y);
            _randomAngle = Random.Range(judgmentLength.x, judgmentLength.y);

            _platform = Instantiate((GameObject)Resources.Load("Prefabs/Platform/Pointer"), transform);

            _platform.GetComponent<SkinnedMeshRenderer>().enabled = false;
            
            _platform.transform.rotation = Pointer.rotation;
            
            _platform.transform.Rotate(new Vector3(rotateAngle,0,0));

            Vector3 scale = _platform.transform.GetChild(0).localScale;
            _platform.transform.GetChild(0).localScale = new Vector3(scale.x, scale.y * _randomAngle / 2.7f, scale.z);
        }

        protected override void JumpAreaChecker()
        {
            // float targetAngle = Quaternion.Angle(_platform.transform.localRotation, Quaternion.identity);
            // float currentAngle = Quaternion.Angle(Pointer.localRotation, Quaternion.identity);
            // float dot = Quaternion.Dot(_platform.transform.localRotation, Pointer.localRotation);

           
            float targetAngle = Quaternion.Angle(_platform.transform.localRotation, Pointer.localRotation);

            if (targetAngle <= _randomAngle)
            {
                Destroy(_platform);
                GenerateStartJumpPoint();
                Debug.Log("You Made It");
            }
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

            RotateScript.RotateAngle(-angle, Pointer, holdingTime / 5, true);
            
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
