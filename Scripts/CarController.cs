using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Driving Stats")]
    public float forwardForce = 250f;
    public float turnSpeed = 150f;
    public float dragAmount = 10f;

    [Header("Audio Settings")]
    public AudioSource startEngineSource;
    public AudioSource engineSource;
    public AudioSource collisionSource;

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = dragAmount;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        engineSource.loop = true;
        engineSource.Stop();
    }

    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        HandleEngineSound();
    }

    void HandleEngineSound()
    {
        if (Mathf.Abs(moveInput) > 0.1f || Mathf.Abs(turnInput) > 0.1f)
        {
            if (!engineSource.isPlaying) engineSource.Play();
        }
        else
        {
            if (engineSource.isPlaying) engineSource.Stop();
        }
    }

    void FixedUpdate()
    {
        Vector3 driveDirection = -transform.up;
        rb.AddForce(driveDirection * moveInput * forwardForce, ForceMode.Acceleration);

        if (rb.linearVelocity.magnitude > 0.1f)
        {
            float direction = moveInput >= 0 ? 1f : -1f;
            float rotation = turnInput * turnSpeed * direction * Time.fixedDeltaTime;
            transform.Rotate(Vector3.forward, rotation);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collisionSource.isPlaying && collision.relativeVelocity.magnitude > 2f)
        {
            collisionSource.Play();
        }
    }
}