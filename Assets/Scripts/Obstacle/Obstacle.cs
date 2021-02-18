using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Tooltip("You can arrange rotation speed")]
    [SerializeField] private float rotationTime;
    [SerializeField] private float rotationSpeed;

    void Start()
    {
        StartCoroutine(RotateObstacle());

    }

    private IEnumerator RotateObstacle()
    {
        while (true)
        {
            transform.Rotate(new Vector3(transform.eulerAngles.x, rotationSpeed * Time.deltaTime, transform.eulerAngles.z));
            yield return new WaitForSeconds(rotationTime);
        }
    }
}
