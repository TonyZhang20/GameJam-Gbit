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
        public Canvas canvas;
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
            if(currentZoomTime >= 2)
            {
                canvas.transform.GetChild(1).gameObject.SetActive(true);
            }

        }

        public void TimeGoes()
        {
            currentZoomTime--;
            if(currentZoomTime <= 0)
            {
                canvas.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
