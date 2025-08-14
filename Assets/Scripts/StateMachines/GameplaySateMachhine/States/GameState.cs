using JetBrains.Annotations;
using System;

namespace MyCore.StateMachine
{
    public class GameState : GameplayStateMachineBaseState
    {
        private readonly InputSystemProcessor inputSystemProcessor;
        private readonly InputProvider inputProvider;

        public GameState(GameplayContext context, InputSystemProcessor inputSystemProcessor, InputProvider inputProvider) : base(context)
        {
            this.inputSystemProcessor = inputSystemProcessor;
            this.inputProvider = inputProvider;
        }

        public override void Dispose()
        {
            
        }

        public override void OnStateEnter()
        {
            SupToInput();
        }

        private void SupToInput()
        {
            inputSystemProcessor.OnAttackLoadingStart += inputProvider.AttackLoadingStart;
            inputSystemProcessor.OnAttackLoadingFinished += inputProvider.AttackLoadingFinished;
        }

        public override void OnStateExit()
        {
            inputSystemProcessor.OnAttackLoadingStart -= inputProvider.AttackLoadingStart;
            inputSystemProcessor.OnAttackLoadingFinished -= inputProvider.AttackLoadingFinished;
        }
    }
}