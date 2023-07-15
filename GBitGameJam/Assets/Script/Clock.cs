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
        private static readonly int Preparing = Animator.StringToHash("Preparing");
        private static readonly int Shake = Animator.StringToHash("Shake");
        private Animator _animator;
        
        private float _forceBeforeJump = 0;
        private float _holdTimeBeforeJump = 0;
        
        protected override void ChildStart()
        {
            GenerateStartJumpPoint();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            EventHandler.BeforeJumpStart += BeforeJumpStart;
            EventHandler.AfterJumpFinish += JumpAreaChecker;
            EventHandler.AfterJumpFail += AfterJumpFail;
        }

        private void OnDisable()
        {
            EventHandler.AfterJumpFinish -= JumpAreaChecker;
            EventHandler.BeforeJumpStart -= BeforeJumpStart;
            EventHandler.AfterJumpFail -= AfterJumpFail;
        }

        private void BeforeJumpStart()
        {
            _forceBeforeJump = angle;
            _holdTimeBeforeJump = holdingTime;
        }

        private void AfterJumpFail()
        {
            rotateScript.RotateAngle(-_forceBeforeJump, Pointer, _holdTimeBeforeJump);
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
            float targetAngle = Quaternion.Angle(_platform.transform.localRotation, Pointer.localRotation);

            if (targetAngle <= _randomAngle)
            {
                Destroy(_platform);
                GenerateStartJumpPoint();
            }
            else
            {
                EventHandler.CallAfterJumpFail();
            }
        }   

        protected override void BeforeJump()
        {
            _animator.SetBool(Preparing, true);
        }

        protected override void Jump()
        {
            EventHandler.CallBeforeJumpStart(); //跳跃开始
            //GetComponent<CameraShake>().StopShake();   
            //DOTween.KillAll();

            rotateScript.RotateAngle(-angle, Pointer, holdingTime / 5, true);
            _animator.SetTrigger(Shake);
            _animator.SetBool(Preparing, false);
            
            angle = 0;
            holdingTime = 0;
        }

        protected override void PrepareJump()
        {
            angle += force * Time.deltaTime;
            holdingTime += Time.deltaTime;
        }
        

    }
}
