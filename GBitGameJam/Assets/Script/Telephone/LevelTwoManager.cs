using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    /// <summary>
    /// 
    /// </summary>
    public class LevelTwoManager  : MonoBehaviour
    {
        public static LevelTwoManager Instance => _instance;
        private static LevelTwoManager _instance;

        public int zoomTime;
        public int currentZoomTime;
        public Animator cameraAnimator;
        private static readonly int Zoom = Animator.StringToHash("Zoom");

        private void Awake()
        {
            if (_instance == null) _instance = this;
            else Destroy(gameObject);
        }

        public void ZoomOut()
        {
            currentZoomTime++;

            if (currentZoomTime >= zoomTime)
            {
                cameraAnimator.SetTrigger(Zoom);
                zoomTime = currentZoomTime;
                zoomTime++;
            }

        }

        public void TimeGoes()
        {
            currentZoomTime--;
        }
    }
}
