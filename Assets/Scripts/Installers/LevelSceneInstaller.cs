using MyCore.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelSceneInstaller : MonoInstaller
{
    [SerializeField] private GameplayContext gameplayContext;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameplayContext>().FromInstance(gameplayContext).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameplayStateMachine>().AsSingle().NonLazy();
        Container.Bind<ColliderProvider>().FromNewComponentOnNewGameObject().WithGameObjectName(nameof(ColliderProvider)).AsSingle().NonLazy();

    }

}
