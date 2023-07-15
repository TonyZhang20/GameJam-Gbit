using System;
using EZCameraShake;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CameraShakeInstance shake;

    public static CameraShake Instance => instance;
    private static CameraShake instance;
    
    public static bool shaking = false;

    public float magnitude;
    public float roughness;
    public float fadeInTime;
    
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {

    }

    public void StartShake()
    {
        shaking = true;
        shake = CameraShaker.Instance.StartShake(magnitude, roughness, fadeInTime);
    }

    public void StopShake()
    {
        shaking = false;
        if(shake != null)
            shake.StartFadeOut(0.5f);
    }
}