using UnityEngine;

namespace thirtwo.Scripts.PlayerController
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(AudioSource))]
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
        private Animator anim;
        private AudioSource audioSource;
        [Header("Sesler")]
        [SerializeField] private AudioClip pickMentosSound;
        [SerializeField] private AudioClip obstacleTackleSound;
        [SerializeField] private AudioClip confettiSound;
        private LevelProgressUI levelProgress;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            levelProgress = FindObjectOfType<LevelProgressUI>();
        }
        private void Update()
        {
            if (!GameManager.isGameStarted && levelProgress.levelFinish)// if game not started it will return
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
            anim.SetFloat("Run", 1.0f);
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Collectible")
            {
                transform.GetChild(0).GetChild(mentosCount).gameObject.SetActive(true);
                mentosCount++;
                Debug.Log(mentosCount);
                other.gameObject.SetActive(false);
                //toplama animasyonu
                audioSource.PlayOneShot(pickMentosSound);
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Obstacle")
            {
                if (mentosCount > 0)
                {
                    if (!anim.IsInTransition(0) && anim.GetFloat("Run") == 1.0f)
                    {
                        mentosCount--;
                        Debug.Log(mentosCount);
                        //çarpma animasyonu
                        transform.GetChild(0).GetChild(mentosCount).gameObject.SetActive(false);
                        anim.SetTrigger("takilma");
                        audioSource.PlayOneShot(obstacleTackleSound);
                    }
                }
                else
                {
                    GameManager.isGameStarted = false;
                    // yanma animasyonu vs.
                    anim.Play("Lose");
                }
            }
        }

        public void ConfettiSound()
        {
            audioSource.PlayOneShot(confettiSound);
        }
    }
}
