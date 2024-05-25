using UnityEngine;
using Zenject;

public class InjectComponentIntaller : MonoInstaller
{
    [SerializeField] private GameObject _container;
    public override void InstallBindings()
    {
        Container.Bind<SoundController>().FromInstance(_container.GetComponentInChildren<SoundController>());
        Container.Bind<VibrationController>().FromInstance(_container.GetComponentInChildren<VibrationController>());
        Container.Bind<LoadingScreenFactory>().FromInstance(_container.GetComponentInChildren<LoadingScreenFactory>());
    }
}
