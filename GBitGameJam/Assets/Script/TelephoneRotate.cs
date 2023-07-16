using Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class TelephoneRotate : RotateScript
    {
        public Transform Finger;
        public float rotateAngle;
        private float _rotateAngle;
        private Quaternion _fingerWorldRotation;

        public float range = 90;
        private AudioSource _audioSource;
        protected override void RotateFunction()
        {
            base.RotateFunction();

            Finger.rotation = _fingerWorldRotation;

            if (!continueRotate)
            {
                if (counting >= 0 && ableToRotate)
                {
                    counting -= Time.deltaTime;
                }
                else if (ableToRotate)
                {
                    RotateAngle(_rotateAngle, transform);
                    counting = 1;
                }

            }
            

            if (_audioSource == null || !_audioSource.isPlaying)
            {
                if(_audioSource!= null) _audioSource.Stop();
                _audioSource = AudioManager.Instance.PlayAudio(Sound.PhoneRotate);
            }

        }

        private void OnEnable()
        {
            _rotateAngle = rotateAngle;
            EventHandler.BeforeJumpStart += DuringJump;
            EventHandler.AfterJumpFinish += FinishJump;
            EventHandler.BeforeJumpStart += StopAudio;

            _fingerWorldRotation = Finger.rotation;
        }

        private void OnDisable()
        {
            EventHandler.BeforeJumpStart -= DuringJump;
            EventHandler.AfterJumpFinish -= FinishJump;
            EventHandler.BeforeJumpStart -= StopAudio;
        }
        protected override void ChildUpdate()
        {
            base.ChildUpdate();
            TotalAngleChecker();
        }
        private void TotalAngleChecker()
        {
            if (totalAngle < -190)
            {
                totalAngle += 360;
                LevelTwoManager.Instance.TimeGoes();
            }
            else if (totalAngle > 170)
            {
                Debug.Log(1);
                LevelTwoManager.Instance.ZoomOut();
                totalAngle -= 360;
            }

        }

        private void DuringJump()
        {
            _rotateAngle = 0;
            ableToRotate = false;
        }

        private void FinishJump()
        {
            _rotateAngle = rotateAngle;
            ableToRotate = true;
            _audioSource.Play();
        }

        private void StopAudio()
        {
            _audioSource.Stop();
        }
        
    }
}

