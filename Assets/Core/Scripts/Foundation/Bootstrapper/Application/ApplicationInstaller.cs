using UnityEngine;
using MyCore.StateMachine;
using Zenject;
using SLS;

namespace Installers
{
    public class ApplicationInstaller : MonoInstaller
    {
        [SerializeField] private LevelLoader levelLoader;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ApplicationContext>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ApplicationStateMachine>().AsSingle().NonLazy();

            InstallSaveSystem();

            Container.Bind<LevelLoader>().FromInstance(levelLoader).AsSingle().NonLazy();

            InstallService();
            InstallInputSystem();

        }

        private void InstallSaveSystem()
        {
            Container.Bind<SaveLoadSystem>().AsSingle();
        }

        private void InstallService()
        {
        }


        private void InstallInputSystem()
        {
            Container.Bind<InputSystemProcessorContext>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<InputSystemProcessor>().AsSingle().NonLazy();
        }
    }
}