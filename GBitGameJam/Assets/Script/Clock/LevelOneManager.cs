using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Script
{
    public class LevelOneManager : MonoBehaviour
    {
        public static LevelOneManager Instance => _instance;
        private static LevelOneManager _instance;
        public int index;

        public int zoomTime;
        public int currentZoomTime;
        public Animator cameraAnimator;
        private static readonly int Zoom = Animator.StringToHash("Zoom");
        public GameObject target;

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
            if (currentZoomTime <= -2)
            {
                FindObjectOfType<CameraShake>().StartShake();
                SetTargetActive();
            }
        }

        public void LoadNextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        public void LoadNextLevel(int i)
        {
            SceneManager.LoadScene(i);
        }

        public void SetTargetActive()
        {
            target.SetActive(true);
        }
    }
}

