using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackGroundWaitForLoadScene : MonoBehaviour
{
    public int levelIndex;
    
    
    private void OnEnable()
    {
        StartCoroutine(WaitForNextLevel());
    }

    IEnumerator WaitForNextLevel()
    {
        yield return new WaitForSeconds(12f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
