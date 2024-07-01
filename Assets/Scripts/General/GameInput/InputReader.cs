using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject, GameInput.IPlayerActions, GameInput.IPlayerUIActions
{
    private GameInput gameInput;

    private void OnEnable()
    {
        if(gameInput == null)
        {
            gameInput = new GameInput();

            gameInput.Player.SetCallbacks(this);
            gameInput.PlayerUI.SetCallbacks(this);

            SetGameplay();
        }
    }

    public void SetGameplay()
    {
        gameInput.Player.Enable();
    }


    public event Action<Vector2> MoveEvent;
    public event Action<Vector2> AimEvent;

    public event Action FireEvent;
    public event Action FireCancelEvent;
    public event Action SwitchWeaponEvent;
    public event Action SwitchWeaponCancelEvent;

    public event Action InventoryOpenEvent;
    public event Action InventoryCloseEvent;


    public void SetGamePlayer()
    {
        gameInput.Player.Enable();
        gameInput.PlayerUI.Disable();
    }

    public void SetUI()
    {
        gameInput.Player.Disable();
        gameInput.PlayerUI.Enable();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            FireEvent?.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            FireCancelEvent?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        AimEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnSwitchWeapon(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            SwitchWeaponEvent?.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            SwitchWeaponCancelEvent?.Invoke();
        }
    }

    public void OnInventoryOpen(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            InventoryOpenEvent?.Invoke();
            SetUI();
        }
    }

    public void OnInventoryClose(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            InventoryCloseEvent?.Invoke();
            SetGameplay();
        }
    }
}
