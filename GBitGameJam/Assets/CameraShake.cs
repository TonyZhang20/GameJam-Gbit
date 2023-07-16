using System;
using EZCameraShake;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CameraShakeInstance shake;
    
    
    public static bool shaking = false;

    public float magnitude;
    public float roughness;
    public float fadeInTime;

    public CameraShaker cameraShaker;

    private void Start()
    {
        if(cameraShaker == null)
            cameraShaker = GetComponentInParent<CameraShaker>();
        
        StartShake();
    }

    public void StartShake()    
    {
        shaking = true;
        cameraShaker.ableToShake = true;
        shake = cameraShaker.StartShake(magnitude, roughness, fadeInTime);
    }

    public void StopShake()
    {
        shaking = false;
        cameraShaker.ableToShake = false;
        if(shake != null)
            shake.StartFadeOut(0.5f);
    }
}