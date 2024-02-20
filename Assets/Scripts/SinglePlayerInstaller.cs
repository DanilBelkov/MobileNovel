using Assets.Scripts.Models;
using Zenject;

public class SinglePlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Player>().FromNew().AsSingle().NonLazy();
    }
}