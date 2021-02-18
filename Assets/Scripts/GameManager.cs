using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isGameStarted = false;
    private bool settingsActive = false;
    [SerializeField] private GameObject settingsPanel;
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
    public void Settings()
    {

        settingsPanel.SetActive(!settingsActive); //if settings panel inactive it will be active. if settings panel active it will be inactive.
        settingsActive = !settingsActive;
    }

}

