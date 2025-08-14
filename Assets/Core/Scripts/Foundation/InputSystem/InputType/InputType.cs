using System;

public abstract class InputType : BaseState<InputSystemProcessorContext>, IInputType
{
    protected InputType(InputSystemProcessorContext context) : base(context)
    {

    }

    public abstract event Action OnAttackLoadingStart;
    public abstract event Action OnAttackLoadingFinished;
}
