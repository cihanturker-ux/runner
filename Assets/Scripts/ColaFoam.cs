using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using thirtwo.Scripts.PlayerController;

public class ColaFoam : MonoBehaviour
{
    private NewPlayerMovement playerMovement;
    private int colaMentos;
    public void Start()
    {
        playerMovement = FindObjectOfType<NewPlayerMovement>();
        colaMentos = playerMovement.mentosCount;

    }
    public void ColaFoamer()
    {
        StartCoroutine(ColaFoamCo());
    }

    private IEnumerator ColaFoamCo()
    {
        while(colaMentos > 0)
        {
            transform.localScale +=new Vector3(0, 0.5f, 0);
            colaMentos--;
            GameManager.coin += 50;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
