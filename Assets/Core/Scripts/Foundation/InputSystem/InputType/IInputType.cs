using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputType 
{

    public abstract event Action OnAttackLoadingStart;
    public abstract event Action OnAttackLoadingFinished;
}
