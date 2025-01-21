using UnityEngine;
using Zenject;

public class UXInstaller : MonoInstaller
{
    [SerializeField] private GameObject _container;
    [SerializeField] private LoadingScreen _loadingScreen;
    public override void InstallBindings()
    {
        Container.Bind<SoundController>().FromInstance(_container.GetComponentInChildren<SoundController>());
        Container.Bind<VibrationController>().FromInstance(_container.GetComponentInChildren<VibrationController>());
        Container.Bind<LoadingScreen>().FromInstance(_loadingScreen);

        Container.BindInterfacesAndSelfTo<SoundSFXPlay>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<CharacterAnimation>().AsSingle();
    }
}
