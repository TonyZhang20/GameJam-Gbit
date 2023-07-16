using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneDoTween : MonoBehaviour
{
    public float time = 0.8f;
    public float fade = 1f;
    public bool onEnableFade = false;
    private void OnEnable()
    {
        if(onEnableFade) FadeOut();
    }

    public void FadeOut()
    {
        var ig = GetComponent<Image>();
        var color = ig.color;
        
        ig.DOColor(new Color(color.r, color.g, color.b, fade), time);
    }
}
