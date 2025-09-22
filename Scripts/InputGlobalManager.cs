using UnityEngine;
//#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
//#endif

public class InputGlobalManager : MonoBehaviour
{
    public static InputGlobalManager Instance { get; private set; }

    public InputActionAsset InputActions;

    private InputAction m_moveAction;
    private InputAction m_lookAction;
    private InputAction m_attackAction;
    private InputAction m_interactAction;
    private InputAction m_crouchAction;
    private InputAction m_jumpAction;
    private InputAction m_pauseActionPlayer;
    private InputAction m_pauseActionUI;

    public Vector2 m_moveAmt;
    public Vector2 m_lookAmt;

    public float mouseSensibility = 5;

    public bool m_attackInputWasHeld;

    public void EnablePlayerIput()
    {
        InputActions.FindActionMap("Player").Enable();
        InputActions.FindActionMap("UI").Disable();
    }

    public void EnableUIIput()
    {
        InputActions.FindActionMap("Player").Disable();
        InputActions.FindActionMap("UI").Enable();
    }

    private void Awake()
    {
        Instance = this;

        m_moveAction = InputSystem.actions.FindAction("Move");
        m_lookAction = InputSystem.actions.FindAction("Look");
        m_attackAction = InputSystem.actions.FindAction("Attack");
        m_interactAction = InputSystem.actions.FindAction("Interact");
        m_crouchAction = InputSystem.actions.FindAction("Crouch");
        m_jumpAction = InputSystem.actions.FindAction("Jump");

        m_pauseActionPlayer = InputSystem.actions.FindAction("Player/Pause");
        m_pauseActionUI = InputSystem.actions.FindAction("UI/Pause");

    }

    private void Update()
    {
        m_moveAmt = m_moveAction.ReadValue<Vector2>();
        m_lookAmt = m_lookAction.ReadValue<Vector2>() * mouseSensibility;
    }

    void LateUpdate()
    {
        m_attackInputWasHeld = GetAttackInputHeld();
    }

    public bool GetAttackInputDown()
    {
        return GetAttackInputHeld() && !m_attackInputWasHeld;
    }

    public bool GetAttackInputReleased()
    {
        return !GetAttackInputHeld() && m_attackInputWasHeld;
    }

    public bool GetAttackInputHeld()
    {
        return m_attackAction.IsPressed();
        //if (CanProcessInput())
        //{
        //    return m_attackAction.IsPressed();
        //}

        //return false;
    }

    public void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void SetCursorVisibility(bool visible)
    {
        Cursor.visible = visible;
    }
}
