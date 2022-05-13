using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] Ball _ballPrefab;
    public override void InstallBindings()
    {
        Container.Bind<SaveManager>().AsSingle();
        Container.Bind<GameManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
        Container.Bind<AdService>().AsSingle();
        Container.Bind<Ball>().FromInstance(_ballPrefab);
        Container.BindInterfacesAndSelfTo<SpawnManager>().AsSingle();
        Container.Bind<PauseManager>().AsSingle();
    }
}