using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Timeclock : MonoBehaviour
{
    public Transform minPointer;
    public Transform hourPointer;

    public void MoveForward()
    {
        

        minPointer.DORotate(new Vector3(-30,0,0), 0.2f, RotateMode.WorldAxisAdd);
        hourPointer.DORotate(new Vector3(-2.5f,0,0), 0.2f, RotateMode.WorldAxisAdd);
    }

    public void MoveBackward()
    {
        minPointer.DORotate(new Vector3(30,0,0), 0.2f, RotateMode.WorldAxisAdd);
        hourPointer.DORotate(new Vector3(2.5f,0,0), 0.2f, RotateMode.WorldAxisAdd);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveForward();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveBackward();
        }
    }
}
