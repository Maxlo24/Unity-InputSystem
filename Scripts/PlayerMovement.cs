using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement Speeds")]
    [SerializeField] private float m_walkSpped = 3.0f;
    [SerializeField] private float m_upDownRange = 80.0f;
    [SerializeField] private float m_sprintMultiplier = 2.0f;
    [SerializeField] private float m_jumpForce = 5.0f;
    [SerializeField] private float m_gravity = 9.81f;


    private Camera mainCamera;
    private Vector3 currentMovement = Vector3.zero;
    private float verticalRotation;
    private CharacterController m_characterController;

    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        InputGlobalManager.Instance.SetCursorLock(true);
        InputGlobalManager.Instance.SetCursorVisibility(false);
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        float speedMultifplier = InputGlobalManager.Instance.GetSprint() ? m_sprintMultiplier : 1f;
        float leftSpeed = InputGlobalManager.Instance.moveAmt.x * m_walkSpped * speedMultifplier;
        float forswardSpeed = InputGlobalManager.Instance.moveAmt.y * m_walkSpped * speedMultifplier;

        Vector3 horizontalMovement = new Vector3(leftSpeed, 0, forswardSpeed);
        horizontalMovement = transform.rotation * horizontalMovement;

        HandleGravityAndJumping();

        currentMovement.x = horizontalMovement.x;
        currentMovement.z = horizontalMovement.z;

        m_characterController.Move(currentMovement * Time.deltaTime);
    }

    void HandleGravityAndJumping()
    {
        if (m_characterController.isGrounded)
        {
            currentMovement.y = -0.5f;
            if (InputGlobalManager.Instance.GetJump())
            {
                currentMovement.y = m_jumpForce;
            }
        }
        else
        {
            currentMovement.y -= m_gravity * Time.deltaTime;
        }
    }

    void HandleRotation()
    {
        float horizontalSpeed = InputGlobalManager.Instance.lookAmt.x;

        transform.Rotate(0, horizontalSpeed, 0);

        verticalRotation -= InputGlobalManager.Instance.lookAmt.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -m_upDownRange, m_upDownRange);

        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
}
