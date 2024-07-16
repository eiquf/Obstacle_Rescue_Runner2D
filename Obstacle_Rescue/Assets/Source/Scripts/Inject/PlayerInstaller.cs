using UnityEngine;
using Zenject;

namespace ObstacleRescue.Installers
{
    public sealed class PlayerInstaller : MonoInstaller
    {
        [Header("Scripts")]

        [SerializeField] private Player _player;

        [SerializeField] private HealthFactory _healthFactory;

        [SerializeField] private MovementSettings _movementSettings;
        [SerializeField] private MainCameraFactory _mainCamera;

        [Header("Components")]
        [SerializeField] private Animator _animator;
        public override void InstallBindings()
        {
            BindInterfaces();
            BindComponents();
            Bind();
            BindHealth();
        }
        private void Bind()
        {
            Container.Bind<Player>().FromInstance(_player).AsSingle();

            Container.Bind<MovementSettings>().FromInstance(_movementSettings).AsSingle();
            Container.Bind<MainCameraFactory>().FromInstance(_mainCamera).AsSingle();

        }
        private void BindHealth()
        {
            _healthFactory = FindFirstObjectByType<HealthFactory>();

            Container.Bind<HealthFactory>().FromInstance(_healthFactory).AsSingle();
        }
        private void BindInterfaces()
        {
            Container.BindInterfacesAndSelfTo<PlayerAnimation>().AsSingle().NonLazy();
        }
        private void BindComponents()
        {
            Container.Bind<Animator>().FromInstance(_animator).AsSingle();
        }

    }
}