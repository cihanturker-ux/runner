using UnityEngine.UI;
using UnityEngine;
using thirtwo.Scripts.PlayerController;

public class LevelProgressUI : MonoBehaviour
{
    [Header("UI")]
    public Image fillImage;
    public Text startText;

    [Header("Player & Endline")]
    public Transform player;
    public Transform endLine;
    [Header("Confetti")]
    [SerializeField] private GameObject confettiEffect;
    private Vector3 endLinePos;
    private float distance;
    private bool playOnce = false;
    public bool levelFinish = false;

    private void Start()
    {
        endLinePos = endLine.position;
        distance = GetDistance();
    }
    public void LevelTexts(int level)
    {
        startText.text = level.ToString();
        startText.text = (level + 1).ToString();
    }

    float GetDistance()
    {
        //return Vector3.Distance(player.position, endLinePos);
        return (endLinePos - player.position).sqrMagnitude;
    }

    void FillProgress(float value)
    {
        fillImage.fillAmount = value;
    }

    private void Update()
    {
        if (player.position.z < endLine.position.z)
        {
            float newDistance = GetDistance();
            float progressValue = Mathf.InverseLerp(distance, 0f, newDistance);
            FillProgress(progressValue);
        }
        else if(player.position.z >= endLine.position.z && !playOnce )
        {
            playOnce = true;
            Instantiate(confettiEffect, endLinePos, Quaternion.identity);
            player.gameObject.GetComponent<NewPlayerMovement>().ConfettiSound();
            levelFinish = true;
        }
    }
}
