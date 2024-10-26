using Assets.game.Scripts.Game.Gameplay.Services;
using Assets.game.Scripts.Game.GameRoot.Services;
using BaCon;

namespace Assets.game.Scripts.Game.Gameplay.Root
{
    public class GameplayRegistrations
    {
        public static void Registar(DIContainer container, GameplayEnterParams gameplayEnterParams)
        {
            container.RegisterFactory(c => new SomeGameplayService(c.Resolve<SomeCommonService>())).AsSingle();
        }
    }
}