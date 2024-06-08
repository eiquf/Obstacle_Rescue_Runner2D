using UnityEngine;
using Zenject;

namespace ObstacleRescue.Installers
{
    public sealed class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private PlayerAnimation _playerAnimation;
        [SerializeField] private PlayerShadow _playerShadow;
        [SerializeField] private MovementSettings _movementSettings;
        public override void InstallBindings() => Bind();
        private void Bind()
        {
            Container.Bind<Player>().FromInstance(_player).AsSingle();
            Container.Bind<PlayerAnimation>().FromInstance(_playerAnimation).AsSingle();
            Container.Bind<PlayerShadow>().FromInstance(_playerShadow).AsSingle();
            Container.Bind<MovementSettings>().FromInstance(_movementSettings).AsSingle();
        }
    }
}