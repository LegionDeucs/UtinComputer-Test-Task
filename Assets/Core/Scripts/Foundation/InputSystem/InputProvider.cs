using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InputProvider : MonoBehaviour
{
    public event Action OnAttackLoadingFinished;

    public event System.Action OnAttackLoadingStart;

    public void AttackLoadingFinished()
    {
        OnAttackLoadingFinished?.Invoke();
    }

    public void AttackLoadingStart()
    {
        OnAttackLoadingStart?.Invoke();
    }
}
