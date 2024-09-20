using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public GameObject playerPrefab;
    public GameObject gameManagerPrefab;
    public GameObject levelGeneratorPrefab;
    public GameObject uiManagerPrefab;
    public GameObject trapManagerPrefab;
    public CameraSetup cameraSetupPrefab;

    public override void InstallBindings()
    {
        Container.Bind<IGameManager>().To<GameManager>()
            .FromComponentInNewPrefab(gameManagerPrefab)
            .AsSingle()
            .NonLazy();
        
        Container.Bind<ILevelGenerator>().To<LevelGenerator>()
            .FromComponentInNewPrefab(levelGeneratorPrefab)
            .AsSingle()
            .NonLazy();
        
        Container.Bind<IPlayerController>().To<PlayerController>()
            .FromComponentInNewPrefab(playerPrefab)
            .AsSingle()
            .NonLazy();

        Container.Bind<IUIManager>().To<UIManager>()
            .FromComponentInNewPrefab(uiManagerPrefab)
            .AsSingle()
            .NonLazy();

        Container.Bind<ITrapManager>().To<TrapManager>()
            .FromComponentInNewPrefab(trapManagerPrefab)
            .AsSingle()
            .NonLazy();
        
        Container.Bind<CameraSetup>().FromComponentInNewPrefab(cameraSetupPrefab).AsSingle().NonLazy();
    }
}
