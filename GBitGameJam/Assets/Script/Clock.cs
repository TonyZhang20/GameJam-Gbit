using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Script
{
    public class Clock : BaseClock
    {
        private GameObject _platform;
        [SerializeField] private Vector2 judgmentArea = new Vector2(30, 90);
        [SerializeField] private float rotateAngle;
        [SerializeField] private Vector2 judgmentLength = new Vector2(5, 20);
        private float _jumpAngle;
        private float _randomAngle;
        private static readonly int Preparing = Animator.StringToHash("Preparing");
        private static readonly int Shake = Animator.StringToHash("Shake");
        private Animator _animator;

        [SerializeField, Header("观察用，用于记录上一次条约的力度")]
        private float forceBeforeJump = 0;
        private float _holdTimeBeforeJump = 0;
        
        protected override void ChildStart()
        {
            GenerateStartJumpPoint();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            EventHandler.AfterJumpFinish += AfterFailJumpFinish;
            EventHandler.AfterJumpFinish += JumpAreaChecker;
            EventHandler.BeforeJumpStart += BeforeJumpStart;
            EventHandler.AfterJumpFail += AfterJumpFail;
            EventHandler.AfterFailJumpFinish += AfterFailJumpFinish;
        }

        private void OnDisable()
        {
            EventHandler.AfterJumpFinish -= AfterFailJumpFinish;
            EventHandler.AfterJumpFinish -= JumpAreaChecker;
            EventHandler.BeforeJumpStart -= BeforeJumpStart;
            EventHandler.AfterJumpFail -= AfterJumpFail;
            EventHandler.AfterFailJumpFinish -= AfterFailJumpFinish;
        }

        private void BeforeJumpStart()
        {
            forceBeforeJump = angle;
            _holdTimeBeforeJump = holdingTime;
        }

        private void AfterJumpFail()
        {
            ableToJump = false;
            rotateScript.RotateAngle(forceBeforeJump, Pointer, _holdTimeBeforeJump / 3,
                EventHandler.CallAfterFailJumpFinish);
        }

        private void AfterFailJumpFinish()
        {
            ableToJump = true;
        }

        protected override void GenerateStartJumpPoint()
        {

            rotateAngle = Random.Range(judgmentArea.x, judgmentArea.y);
            _randomAngle = Random.Range(judgmentLength.x, judgmentLength.y);

            _platform = Instantiate((GameObject)Resources.Load("Prefabs/Platform/Pointer"), transform);

            _platform.GetComponent<SkinnedMeshRenderer>().enabled = false;

            _platform.transform.rotation = Pointer.rotation;

            _platform.transform.Rotate(new Vector3(rotateAngle, 0, 0));

            Vector3 scale = _platform.transform.GetChild(0).localScale;

            _platform.transform.GetChild(0).localScale = new Vector3(scale.x, scale.y * _randomAngle / 2.7f, scale.z);
        }

        //逻辑判断
        protected override void JumpAreaChecker()
        {
            float targetAngle = Quaternion.Angle(_platform.transform.localRotation, Pointer.localRotation);

            //success
            if (targetAngle <= _randomAngle)
            {
                Destroy(_platform);
                GenerateStartJumpPoint();

                rotateScript.totalAngle += forceBeforeJump;
                
            }
            else
            {
                EventHandler.CallAfterJumpFail();
            }
        }

        protected override void BeforeJump()
        {
            
        }

        protected override void Jump()
        {
            EventHandler.CallBeforeJumpStart(); //跳跃开始

            rotateScript.RotateAngle(-angle, Pointer, holdingTime / 5, EventHandler.CallAfterJumpFinish);

            _animator.SetTrigger(Shake);
            _animator.SetBool(Preparing, false);
            
            redAngle.rotation = Pointer.rotation;
            redAngle.Rotate(new Vector3(angle, 0,0));

            angle = 0;
            holdingTime = 0;
            ableToJump = false;
        }

        protected override void PrepareJump()
        {
            angle += force * Time.unscaledDeltaTime;
            if (angle > maxAngle)
            {
                angle = maxAngle;
            }

            holdingTime += Time.unscaledDeltaTime;
            _animator.SetBool(Preparing, true);
        }

        public Transform redAngle;
        public RedAngleType redAngleType = RedAngleType.BeforeJump;
        
        protected override void ChildUpdate()
        {
            base.ChildUpdate();

            switch (redAngleType)
            {
                case RedAngleType.BeforeJump:
                    if (Input.GetKey(KeyCode.Space))
                    {
                        redAngle.rotation = Pointer.rotation;
                        redAngle.Rotate(new Vector3(angle, 0,0));
                    }
                    break;
            }
            
        }
    }
    public enum RedAngleType
    {
        BeforeJump,
        AfterJump
    }
    
}
