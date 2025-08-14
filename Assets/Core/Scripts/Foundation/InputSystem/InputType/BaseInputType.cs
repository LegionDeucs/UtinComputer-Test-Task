using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInputType : InputType
{
    public BaseInputType(InputSystemProcessorContext context) : base(context)
    {

    }

    public override event Action OnAttackLoadingStart;
    public override event Action OnAttackLoadingFinished;

    public override void Dispose()
    {
        
    }

    public override void OnStateEnter()
    {
        Context.InputSystemProcessor.InputSystem.BaseActionMap.Enable();
        Context.InputSystemProcessor.InputSystem.BaseActionMap.LoadingAttack.started += LoadingAttack_started;
        Context.InputSystemProcessor.InputSystem.BaseActionMap.LoadingAttack.canceled += LoadingAttack_canceled;
    }

    private void LoadingAttack_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnAttackLoadingFinished?.Invoke();
    }

    private void LoadingAttack_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnAttackLoadingStart?.Invoke();
    }

    public override void OnStateExit()
    {
        Context.InputSystemProcessor.InputSystem.BaseActionMap.Enable();
        Context.InputSystemProcessor.InputSystem.BaseActionMap.LoadingAttack.started -= LoadingAttack_started;
        Context.InputSystemProcessor.InputSystem.BaseActionMap.LoadingAttack.canceled -= LoadingAttack_canceled;
    }
}
