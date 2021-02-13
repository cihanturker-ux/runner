using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    
    [SerializeField] private float forwardSpeed;
    private int desiredLane = 1;//0:left, 1:middle, 2:right
    public float laneDistance = 3;//The distance between two lanes
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.isGameStarted)
            return;

        direction.z = forwardSpeed;
        if (Input.GetKeyDown(KeyCode.D))
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDistance;
       
        if (transform.position != targetPosition)
        {
            controller.Move(targetPosition);
        }
        //
        //    Vector3 diff = targetPosition - transform.position;
        //    Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        //    if (moveDir.sqrMagnitude < diff.magnitude)
        //        controller.Move(moveDir);
        //    else
        //        controller.Move(diff);
        //}


        //Move Player
        controller.Move(direction * Time.deltaTime);

    }
}
