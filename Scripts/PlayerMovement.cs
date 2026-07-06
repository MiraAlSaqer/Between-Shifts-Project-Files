using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float groundDamping = 1f;
    public float airControl = 0.4f;

    [Header("Audio")]
    public AudioSource footstepSound;

    [Header("Ground Check")]
    public float playerHeight = 2f;
    public LayerMask whatIsGround;

    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;
    private bool grounded;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        HandleFootsteps();
    }

    private void FixedUpdate()
    {
        GroundCheck();
        ApplyDamping();
        MovePlayer();
    }

    private void GroundCheck()
    {
        grounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            playerHeight * 0.5f + 0.3f,
            whatIsGround
        );
    }

    private void ApplyDamping()
    {
        rb.linearDamping = grounded ? groundDamping : 0f;
    }

    private void MovePlayer()
    {
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (inputDir.magnitude > 1f)
            inputDir.Normalize();

        Vector3 currentVel = rb.linearVelocity;
        Vector3 targetVel = inputDir * moveSpeed;
        float control = grounded ? 1f : airControl;

        rb.linearVelocity = Vector3.Lerp(
            currentVel,
            new Vector3(targetVel.x, currentVel.y, targetVel.z),
            Time.fixedDeltaTime * 12f * control
        );
    }

    private void HandleFootsteps()
    {
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        if (isMoving)
        {
            if (!footstepSound.isPlaying) footstepSound.Play();
        }
        else
        {
            if (footstepSound.isPlaying) footstepSound.Stop();
        }
    }
}