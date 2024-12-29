using UnityEngine;
using Zenject;

namespace ObstacleRescue.Installers
{
    public sealed class PlayerInstaller : MonoInstaller
    {
        [Header("Scripts")]

        [SerializeField] private Player _player;

        [SerializeField] private Health _health;

        [SerializeField] private GameCamera _mainCamera;
        public override void InstallBindings()
        {
            BindInterfaces();
            Bind();
            BindHealthSystem();
        }
        private void Bind()
        {
            Container.Bind<Player>().FromInstance(_player).AsSingle();

            Container.Bind<GameCamera>().FromInstance(_mainCamera).AsSingle();

        }
        private void BindHealthSystem()
        {
            Container.Bind<Health>().FromInstance(_health).AsSingle();
        }
        private void BindInterfaces()
        {
            Container.BindInterfacesAndSelfTo<CharacterAnimation>().AsSingle();
        }
    }
}