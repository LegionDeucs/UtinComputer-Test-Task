using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InputSystemProcessorContext : MonoBehaviour, IStateMachineContext
{
    [Inject] private InputSystemProcessor inputSystemProcessor;

    public InputSystemProcessor InputSystemProcessor => inputSystemProcessor;

    private void Start()
    {
        inputSystemProcessor.EnterState<BaseInputType>();
    }
}
