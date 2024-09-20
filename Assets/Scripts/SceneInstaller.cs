using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public GameObject _playerPrefab;
    public GameObject _gameManagerPrefab;
    public GameObject _levelGeneratorPrefab;
    public GameObject _uiManagerPrefab;
    public GameObject _trapManagerPrefab;
    public CameraSetup _cameraSetupPrefab;

    public override void InstallBindings()
    {
        Container.Bind<IGameManager>().To<GameManager>()
            .FromComponentInNewPrefab(_gameManagerPrefab)
            .AsSingle()
            .NonLazy();
        
        Container.Bind<ILevelGenerator>().To<LevelGenerator>()
            .FromComponentInNewPrefab(_levelGeneratorPrefab)
            .AsSingle()
            .NonLazy();
        
        Container.Bind<IPlayerController>().To<PlayerController>()
            .FromComponentInNewPrefab(_playerPrefab)
            .AsSingle()
            .NonLazy();

        Container.Bind<IUIManager>().To<UIManager>()
            .FromComponentInNewPrefab(_uiManagerPrefab)
            .AsSingle()
            .NonLazy();

        Container.Bind<ITrapManager>().To<TrapManager>()
            .FromComponentInNewPrefab(_trapManagerPrefab)
            .AsSingle()
            .NonLazy();
        
        Container.Bind<CameraSetup>().FromComponentInNewPrefab(_cameraSetupPrefab).AsSingle().NonLazy();
    }
}
