using Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class Telephone : BaseClock
    {
        private GameObject _platform;
        [SerializeField] private Vector2 judgmentArea = new Vector2(30, 90);
        [SerializeField] private float rotateAngle;
        [SerializeField] private Vector2 judgmentLength = new Vector2(5, 20);
        private float _jumpAngle;
        private float _randomAngle;
        private static readonly int Preparing = Animator.StringToHash("Empty");
        private static readonly int In = Animator.StringToHash("PhoneCall");
        private Animator _animator;

        [SerializeField] private float _forceBeforeJump = 0;
        private float _holdTimeBeforeJump = 0;

        protected override void ChildStart()
        {
            _animator = GetComponent<Animator>();
            rotateScript.SetAbleToRotate(false);
            StartCoroutine(StartCall());
        }
        private IEnumerator CallNum5()
        {
            yield return new WaitForSeconds(0.1f);
            rotateScript.RotateAngle(-304, Pointer, 1f);
            yield return new WaitForSeconds(1f);
        }
        private IEnumerator StartCall()
        {
            yield return StartCoroutine(CallNum5());
            rotateScript.RotateAngle(179, Pointer, 0.6f);
            yield return new WaitForSeconds(0.6f);
            EnableRotate();
            GenerateStartJumpPoint();
        }
        private void OnEnable()
        {
            //EventHandler.AfterJumpFinish += AfterJumpFinish;
            EventHandler.AfterJumpFinish += JumpAreaChecker;
            EventHandler.BeforeJumpStart += BeforeJumpStart;
            EventHandler.AfterJumpFail += AfterJumpFail;
            EventHandler.AfterFailJumpFinish += AfterJumpFailFinish;
        }

        private void OnDisable()
        {
            //EventHandler.AfterJumpFinish -= AfterJumpFinish;
            EventHandler.AfterJumpFinish -= JumpAreaChecker;
            EventHandler.BeforeJumpStart -= BeforeJumpStart;
            EventHandler.AfterJumpFail -= AfterJumpFail;
            EventHandler.AfterFailJumpFinish -= AfterJumpFailFinish;
        }
        public void EnableRotate()                            
        {
            rotateScript.SetAbleToRotate(true);
            //_animator.enabled = false;
            //Pointer.localRotation = Quaternion.Euler(0, 0, 132);
        }
        private void BeforeJumpStart()
        {
            _forceBeforeJump = angle;
            _holdTimeBeforeJump = holdingTime;
        }

        private void AfterJumpFail()
        {
            rotateScript.RotateAngle(_forceBeforeJump, Pointer, _holdTimeBeforeJump / 2, EventHandler.CallAfterFailJumpFinish);
            ableToJump = false;
        }

        private void AfterJumpFailFinish()
        {
            ableToJump = true;
        }
        
        

        protected override void GenerateStartJumpPoint()
        {
            rotateAngle = Random.Range(judgmentArea.x, judgmentArea.y);
            _randomAngle = Random.Range(judgmentLength.x, judgmentLength.y);

            _platform = Instantiate((GameObject)Resources.Load("Prefabs/Platform/PhonePlat"), transform);

            _platform.GetComponent<MeshRenderer>().enabled = false;

            _platform.transform.rotation = Pointer.rotation;

            _platform.transform.Rotate(new Vector3(0, 0 ,rotateAngle));

            Vector3 scale = _platform.transform.GetChild(0).localScale;
            _platform.transform.GetChild(0).localScale = new Vector3(scale.x, scale.y * _randomAngle / 5f, scale.z);
        }

        protected override void JumpAreaChecker()
        {
            float targetAngle = Quaternion.Angle(_platform.transform.localRotation, Pointer.localRotation);

            if (targetAngle <= _randomAngle)
            {
                Destroy(_platform);
                GenerateStartJumpPoint();

                rotateScript.totalAngle += _forceBeforeJump;
            }
            else
            {
                EventHandler.CallAfterJumpFail();
            }
        }

        protected override void BeforeJump()
        {
            //_animator.SetBool(Preparing, true);
        }

        protected override void Jump()
        {
            EventHandler.CallBeforeJumpStart(); //

            rotateScript.RotateAngle(-angle, Pointer, holdingTime / 5, EventHandler.CallAfterJumpFinish);
            //_animator.SetTrigger(Shake);
            //_animator.SetBool(Preparing, false);

            angle = 0;
            holdingTime = 0;

            _animator.SetBool("Holding", false);
        }

        protected override void PrepareJump()
        {

            //var clipName = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            //_animator.Play(clipName, 0, 0);

            _animator.SetBool("Holding", true);

            angle += force * Time.unscaledDeltaTime;
            holdingTime += Time.unscaledDeltaTime;
        }

    }
}
