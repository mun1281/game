using Assets.game.Scripts.Game.GameRoot.Services;
using Assets.game.Scripts.Game.MainMenu.Services;
using BaCon;

namespace Assets.game.Scripts.Game.MainMenu.Root
{
    public static class MainMenuRegistrations
    {
        public static void Registar(DIContainer container, MainMenuEnterParams mainMenuEnterParams)
        {
            container.RegisterFactory(c => new SomeMainMenuService(c.Resolve<SomeCommonService>())).AsSingle();
        }
    }
}