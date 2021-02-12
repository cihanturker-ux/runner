using UnityEngine;

namespace thirtwo.Scripts.PlayerController
{

    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Components")]
        private Rigidbody rb;
        [Header("Character Info")]
        [SerializeField] private float turnSpeed;
        [SerializeField] private float runSpeed;
        private float horizontal;
        private Vector3 direction;
        public int mentosCount;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void Update()
        {
            if (GameManager.isGameStarted)
            {
                horizontal = Input.GetAxis("Horizontal");
                direction = new Vector3(horizontal, 0, 0);
            }
        }
        private void FixedUpdate()
        {
            if (GameManager.isGameStarted)
            {
                Move();
            }
        }

        private void Move()
        {
            rb.AddForce(0, 0, runSpeed, ForceMode.Force);
            rb.AddForce(direction * Time.fixedDeltaTime * turnSpeed, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            
             if(collision.collider.tag == "Obstacle")
            {
                if(mentosCount > 0)
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
