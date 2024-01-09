using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ͨ��InputSystem��ȡ���������Ϣ
/// </summary>
public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlterateAction;
    public event EventHandler OnPauseAction;
    private PlayerInputActions playerInputActions;//inputSystem���
    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += InteractPerformed;
        playerInputActions.Player.InteractAlterate.performed += InteractAlteratePerformed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    } 

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= InteractPerformed;
        playerInputActions.Player.InteractAlterate.performed -= InteractAlteratePerformed;
        playerInputActions.Player.Pause.performed -= Pause_performed;

        playerInputActions.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlteratePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlterateAction?.Invoke(this,EventArgs.Empty);
    }

    private void InteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }
}
