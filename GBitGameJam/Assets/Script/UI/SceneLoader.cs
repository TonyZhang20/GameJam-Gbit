using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class SceneLoader  : MonoBehaviour
    {
        public static SceneManager Instance;

        public static void LoadScene(string n)
        {
            SceneManager.LoadSceneAsync(n);
        }
    }
}
