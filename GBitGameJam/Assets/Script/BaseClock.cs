using System;
using UnityEngine;

namespace Script
{
    [RequireComponent(typeof(RotateScript))]
    public abstract class BaseClock : MonoBehaviour
    {
        [Header("跳跃系数"), SerializeField]protected float force = 2;
        protected abstract void GenerateStartJumpPoint(); //生成跳跃起点
        protected abstract void GenerateEndJumpPoint();  //生成跳跃终点
        [Header("跳跃角度"), SerializeField] protected float angle = 0;
        [Header("能否跳跃"),SerializeField] protected bool ableToJump = false;
        [HideInInspector] protected RotateScript RotateScript;

        protected float holdingTime = 0; //持续按下的时间

        private bool scaling;
        public Vector3 targetScale;
        public Vector3 originalScale;
        public float scaleSpeed = 3;
        public float originalSpeed = 10;

        private void Start()
        {
            RotateScript = GetComponent<RotateScript>();
        }
        
        private void Update()
        {
            
            if (Input.GetKey(KeyCode.Space) && ableToJump) PrepareJump();
            if (Input.GetKeyUp(KeyCode.Space) && ableToJump) Jump();
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                scaling = true;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                scaling = false;
            }

            if (scaling)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, originalScale, originalSpeed * Time.deltaTime);
            }
        }

        protected abstract void Jump(); //蓄力 跳跃
        protected abstract void PrepareJump();

    }
}
