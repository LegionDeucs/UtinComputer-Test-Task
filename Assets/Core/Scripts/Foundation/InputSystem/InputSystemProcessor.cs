using System;
using UnityEngine;
using Zenject;

public class InputSystemProcessor : StateMachine<InputType, InputSystemProcessorContext>
{
    public InputSystem InputSystem { get; }

    public event Action OnAttackLoadingStart;
    public event Action OnAttackLoadingFinished;

    public InputSystemProcessor(IInstantiator instantiator) : base(instantiator)
    {
        InputSystem = new InputSystem();
        InputSystem.Enable();
        RegisterState<BaseInputType>();
    }



    public override TState EnterState<TState>()
    {
        if(currentState != null)
            UnsubToInput();

        var state  = base.EnterState<TState>();
        SubToInput();

        return state;
    }

    private void SubToInput()
    {
        currentState.OnAttackLoadingStart += () => OnAttackLoadingStart?.Invoke();
        currentState.OnAttackLoadingFinished += () => OnAttackLoadingFinished?.Invoke();
    }

    private void UnsubToInput()
    {
        currentState.OnAttackLoadingStart -= () => OnAttackLoadingStart?.Invoke();
        currentState.OnAttackLoadingFinished -= () => OnAttackLoadingFinished?.Invoke();
    }
}
