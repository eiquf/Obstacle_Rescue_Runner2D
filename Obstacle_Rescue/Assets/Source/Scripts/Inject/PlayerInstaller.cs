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
            Bind();
        }
        private void Bind()
        {
            Container.Bind<Player>().FromInstance(_player).AsSingle();
            Container.Bind<GameCamera>().FromInstance(_mainCamera).AsSingle();
            Container.Bind<Health>().FromInstance(_health).AsSingle();
        }
    }
}