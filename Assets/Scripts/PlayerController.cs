using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string footstepSound;
    public Transform cam;
    public float speedPC = 300f;
    public float turnSmooth = 0.1f;
    public GameObject UIControlsMob;

    private bool isMovingFoward;
    private Animator animator;
    private float turnSmoothVelocity;
    private Rigidbody rb;
    private Vector3 moveDir;


    void PlayFootsteps()
    {
        if (isMovingFoward)
            FMODUnity.RuntimeManager.PlayOneShot(footstepSound, transform.position);
    }


#if UNITY_EDITOR || UNITY_STANDALONE

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        isMovingFoward = false;
        moveDir = Vector3.zero;
        InvokeRepeating("PlayFootsteps", 0, 0.3f);
    }

    private void Update()
    {
        isMovingFoward = false;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            isMovingFoward = true;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmooth);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.AddForce(moveDir * speedPC * Time.deltaTime, ForceMode.VelocityChange);
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", vertical);
        }
    }





#else

    private float speedMob = 7f;
    private float rotationAngle = 90f;
    private bool swipeLeft, SwipeRight;
    private bool isDragging;
    private bool holdMove;
    private bool isTurning;
    private Vector2 startTouch, swipeDelta;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        isMovingFoward = false;
        InvokeRepeating("PlayFootsteps", 0, 0.3f);
        UIControlsMob.SetActive(true);
    }

    IEnumerator RotateSmooth(float angle, float duration = 0.7f)
    {
        {
            Quaternion from = transform.rotation;
            Quaternion to = transform.rotation;
            to *= Quaternion.Euler(0, angle, 0);
            float elapsed = 0.0f;
            while (elapsed < duration)
            {
                transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.rotation = to;
        }

    }

    void Update()
    {
        if (Input.touches.Length <= 0)
            return;

        DetectTouch();

        if (holdMove)
        {
            isMovingFoward = true;
            transform.Translate(Vector3.forward * speedMob * Time.deltaTime);
            if (animator != null)
            {
                animator.SetFloat("Speed", speedMob);
            }
        }
        else
        {
            isMovingFoward = false;
            transform.Translate(Vector3.zero);
            if (animator != null)
            {
                animator.SetFloat("Speed", 0);
            }
        }

        DetectSwipe();

        if (swipeLeft)
        {
            StartCoroutine(RotateSmooth(-rotationAngle, 1.0f));
        }
        if (SwipeRight)
        {
            StartCoroutine(RotateSmooth(rotationAngle, 1.0f));
        }

    }

    private void DetectTouch()
    {
        if (Input.touches[0].position.x < Screen.width / 2)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                holdMove = true;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {
                holdMove = false;
            }

        }
    }

    private void DetectSwipe()
    {
        swipeLeft = SwipeRight = false;

        if (Input.touches[0].position.x > Screen.width / 2)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDragging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                ResetSwipe();
            }
        }

        swipeDelta = Vector2.zero;
        if (isDragging)
        {
            if (Input.touchCount > 0)
                swipeDelta = Input.touches[0].position - startTouch;
        }

        if (swipeDelta.magnitude > 125)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                    swipeLeft = true;
                else
                    SwipeRight = true;
            }
            ResetSwipe();
        }
    }

    private void ResetSwipe()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }

#endif
}

