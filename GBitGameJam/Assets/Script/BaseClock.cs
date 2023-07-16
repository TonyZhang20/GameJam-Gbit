using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    [RequireComponent(typeof(RotateScript))]
    public abstract class BaseClock : MonoBehaviour
    {
        [Header("跳跃系数"), SerializeField]protected float force = 2;
        [Header("跳跃角度"), SerializeField] protected float angle = 0;
        [Header("最大跳跃角度")] public float maxAngle = 180;
        [Header("能否跳跃"),SerializeField] protected bool ableToJump = false;
        [SerializeField] protected RotateScript rotateScript;
        protected float holdingTime = 0; //持续按下的时间
        
        protected Transform Pointer;
        
        private void Start()
        {
            rotateScript = GetComponent<RotateScript>();
            Pointer = rotateScript.pointer;

            ChildStart();
        }

        protected abstract void ChildStart();
        protected virtual void ChildUpdate(){}
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space) && ableToJump) BeforeJump();
            if (Input.GetKey(KeyCode.Space) && ableToJump) PrepareJump();
            if (Input.GetKeyUp(KeyCode.Space) && ableToJump) Jump();
            ChildUpdate();
        }

        protected abstract void BeforeJump();
        protected abstract void Jump(); //蓄力 跳跃
        protected abstract void PrepareJump();

        
        protected abstract void GenerateStartJumpPoint(); //生成跳跃起点
        protected abstract void JumpAreaChecker();  //生成跳跃终点
    }
}
