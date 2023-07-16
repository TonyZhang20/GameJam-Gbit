using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTogether : MonoBehaviour
{
    public Animator cameraAnimator;
    public GameObject target;
    private static readonly int Play = Animator.StringToHash("Play");

    public void BackgroundOnEnAble()
    {
        target.SetActive(true);
    }
    
}
