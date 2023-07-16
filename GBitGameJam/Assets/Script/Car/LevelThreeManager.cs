using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    /// <summary>
    /// 
    /// </summary>
    public class LevelThreeManager  : MonoBehaviour
    {
        public static LevelThreeManager Instance => _instance;
        private static LevelThreeManager _instance;

        public int zoomTime;
        public int currentZoomTime;
        public Animator cameraAnimator;
        private static readonly int Zoom = Animator.StringToHash("Zoom");
        private bool Zoom1 = false;
        private bool Zoom2 = false;

        private void Awake()
        {
            if (_instance == null) _instance = this;
            else Destroy(gameObject);
        }

        public void Zoom1Out()
        {
            if (Zoom1) return;
            Zoom1 = true;

            currentZoomTime++;

            if (currentZoomTime >= zoomTime)
            {
                cameraAnimator.SetTrigger(Zoom);
                zoomTime = currentZoomTime;
                zoomTime++;
            }

        }
        public void Zoom2Out()
        {
            if (Zoom2) return;
            Zoom2 = true;

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
