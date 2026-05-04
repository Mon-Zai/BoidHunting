using UnityEngine;
using System;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/InputReader")]
public class InputReader : ScriptableObject, PlayerInputActions.IPlayerActions
{
    public event Action SpawnStartedEvent;
    public event Action SpawnCanceledEvent;
    public Vector2 MoveInput { get; private set; }
    public event Action MoveStartedEvent;
    public event Action MoveCanceledEvent;
    private PlayerInputActions _actions;

    private void OnEnable()
    {
        _actions ??= new PlayerInputActions();
        _actions.Player.SetCallbacks(this);
        _actions.Player.Enable();
    }

    private void OnDisable() => _actions.Player.Disable();

    public void OnSpawn(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            SpawnStartedEvent?.Invoke();
        }
        if (ctx.canceled)
        {
            SpawnCanceledEvent?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();

        if (ctx.started)
        {
            MoveStartedEvent?.Invoke();
        }
        if (ctx.canceled)
        {
            MoveCanceledEvent?.Invoke();
        }
    }
}