using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private BasicMovement playerMovement;
    private Vector3 movementInput;
    private InputSystem_Player.PlayerActions playerActions;

    void Awake()
    {
        playerActions = new InputSystem_Player().Player;
        playerActions.Move.performed += OnMove;
        playerActions.Move.canceled += OnMoveCancel;
        playerActions.Attack.performed += OnAttack;
        playerActions.Enable();
    }

    void OnDestroy()
    {
        playerActions.Move.performed -= OnMove;
        playerActions.Move.canceled -= OnMoveCancel;
        playerActions.Attack.performed -= OnAttack;
        playerActions.Disable();
    }

    void FixedUpdate()
    {
        if(movementInput.magnitude>0.01f)
            playerMovement.MovePlayer(movementInput, false);
    }
    void OnMove(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();
        movementInput = new Vector3(input.x, 0, input.y);
    }

    void OnMoveCancel(InputAction.CallbackContext context)
    {
        movementInput = Vector3.zero;
    }
    void OnAttack(InputAction.CallbackContext context)
    {
    }
}