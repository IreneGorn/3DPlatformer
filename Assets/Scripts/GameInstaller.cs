using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IGameManager>().To<GameManager>().AsSingle();
        Container.Bind<ILevelGenerator>().To<LevelGenerator>().AsSingle();
        Container.Bind<IPlayerController>().To<PlayerController>().AsSingle();
        Container.Bind<ITrapManager>().To<TrapManager>().AsSingle();
        Container.Bind<IUIManager>().To<UIManager>().AsSingle();
    }
}
