using System;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(menuName = "Input Reader")]
public class InputReader : ScriptableObject, CharacterInput.IPlayerActionsActions
{
    private CharacterInput _input;

    private void OnEnable()
    {
        if (_input == null)
        {
            _input = new CharacterInput();

            _input.PlayerActions.SetCallbacks(this);

            SetActions();
        }
    }

    public void SetActions()
    {
        _input.PlayerActions.Enable();
    }

    public event Action<Vector2> MoveEvent;

    public event Action JumpEvent;
    public event Action JumpCanclelledEvent;

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            JumpEvent?.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            JumpCanclelledEvent?.Invoke();
        }

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }
}
