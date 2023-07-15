using System;
using UnityEngine;

namespace Script
{
    [RequireComponent(typeof(RotateScript))]
    public abstract class BaseClock : MonoBehaviour
    {
        [Header("跳跃系数"), SerializeField]protected float force = 2;
        [Header("跳跃角度"), SerializeField] protected float angle = 0;
        [Header("能否跳跃"),SerializeField] protected bool ableToJump = false;
        protected RotateScript RotateScript;
        protected float holdingTime = 0; //持续按下的时间
        
        protected Transform Pointer;

        private void Start()
        {
            RotateScript = GetComponent<RotateScript>();
            Pointer = RotateScript.pointer;

            ChildStart();
        }

        protected abstract void ChildStart();
        
        private void Update()
        {
            
            if (Input.GetKey(KeyCode.Space) && ableToJump) PrepareJump();
            if (Input.GetKeyUp(KeyCode.Space) && ableToJump) Jump();
            
        }
        
        protected abstract void ChildUpdate();

        protected abstract void Jump(); //蓄力 跳跃
        protected abstract void PrepareJump();

        
        protected abstract void GenerateStartJumpPoint(); //生成跳跃起点
        protected abstract void JumpAreaChecker();  //生成跳跃终点
    }
}
