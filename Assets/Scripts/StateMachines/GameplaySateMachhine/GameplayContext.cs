using MyCore.StateMachine;
using System;
using UnityEngine;
using Zenject;

public class GameplayContext : MonoBehaviour, IStateMachineContext
{
    [Inject] private GameplayStateMachine gameplayStateMachine;
    [Inject] private CameraController cameraController;

    internal void Win()
    {
        gameplayStateMachine.EnterState<WinGameState>();
    }

    private void Start()
    {
        gameplayStateMachine.EnterState<GameState>();
    }
}
