using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 通过InputSystem获取玩家输入信息
/// </summary>
public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlterateAction;
    public event EventHandler OnPauseAction;

    public enum Binding
    {
        Move_Up,
        Move_Down, 
        Move_Left, 
        Move_Right,
        Interact,
        InteractAlt,
        Pause,
    }




    private PlayerInputActions playerInputActions;//inputSystem组件
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

    public String GetBindingText(Binding binding)
    {
        switch(binding)
        {
            default:
            case Binding.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlt:
                return playerInputActions.Player.InteractAlterate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
            case Binding.Move_Up:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
        }
    }

    public void RebingBinding(Binding binding)
    {
        playerInputActions.Player.Disable();

        playerInputActions.Player.Move.PerformInteractiveRebinding()
            .OnComplete(callback =>
            {
                Debug.Log(callback.action.bindings[1].path);
                Debug.Log(callback.action.bindings[1].overridePath);
                callback.Dispose();
                playerInputActions.Player.Enable();
            }).Start();
    }
}
