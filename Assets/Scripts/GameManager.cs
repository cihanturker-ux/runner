﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isGameStarted = false;
    public static int coin = 0;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isGameStarted = true;
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

}
