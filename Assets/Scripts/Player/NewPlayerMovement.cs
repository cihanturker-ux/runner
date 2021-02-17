using UnityEngine;

namespace thirtwo.Scripts.PlayerController
{
    [RequireComponent(typeof(Rigidbody))]
    public class NewPlayerMovement : MonoBehaviour
    {
        [Header("Components")]
        private Rigidbody rb;
        [Header("Character Info")]
        [SerializeField] private float forwardSpeed = 5f;
        [SerializeField] private float horizontalSpeed = 5f;
        [SerializeField] private float deadZone = 0.1f;
        Vector3 movementDelta;
        private float mouseStart;
        public int mentosCount;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();

        }
        private void Update()
        {
            if (!GameManager.isGameStarted)// if game not started it will return
                return;  

            movementDelta = Vector3.forward * forwardSpeed;
            if (Input.GetMouseButtonDown(0))
            {
                mouseStart = Input.mousePosition.x;
            }
            else if (Input.GetMouseButton(0))
            {
                float delta = Input.mousePosition.x - mouseStart;
                mouseStart = Input.mousePosition.x;
                if (Mathf.Abs(delta) <= deadZone)
                {
                    delta = 0;
                }
                else
                {
                    delta = Mathf.Sign(delta);
                }
                movementDelta += Vector3.right * horizontalSpeed * delta;
                
            }
            GameField();
        }
        private void FixedUpdate()
        {
            if (!GameManager.isGameStarted) return; // if game not started it will return

            Move();

        }

        private void Move()
        {
            rb.MovePosition(rb.position + movementDelta * Time.fixedDeltaTime);
        }
        private void GameField()
        {
            if (transform.position.x > 4.8f)
            {
                transform.position = new Vector3(4.8f, transform.position.y, transform.position.z);

            }
            else if (transform.position.x < -4.8f)
            {
                transform.position = new Vector3(-4.8f, transform.position.y, transform.position.z);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {

            if (collision.collider.tag == "Obstacle")
            {
                if (mentosCount > 0)
                {
                    collision.gameObject.SetActive(false);
                    //çarpma animasyonu
                    transform.GetChild(mentosCount).gameObject.SetActive(false);
                }
                else
                {
                    GameManager.isGameStarted = false;
                    // yanma animasyonu vs.
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Collectible")
            {
                mentosCount++;
                other.gameObject.SetActive(false);
                //toplama animasyonu
                transform.GetChild(mentosCount).gameObject.SetActive(true);
            }
        }
    }
}
