using UnityEngine;

namespace Game.Core
{
    public class MainMenuFactory
    {
        #region Singleton

        public static MainMenuFactory Instance { get; private set; }

        static MainMenuFactory()
        {
            Instance = new MainMenuFactory();
        }

        private MainMenuFactory()
        {
        }
        
        #endregion //Singleton

        #region Factory Methods

        public MainMenuModel CreateModel()
        {
            var model = Resources.Load<MainMenuModel>("MainMenuModel");
            return Object.Instantiate(model);
        }

        public MainMenuView CreateView()
        {
            var prefab = Resources.Load<GameObject>("Prefabs/MainMenuView");
            return Object.Instantiate(prefab).GetComponent<MainMenuView>();
        }

        public MainMenuState CreateController(MainMenuModel model, MainMenuView view)
        {
            return new MainMenuState(model, view);
        }

        #endregion //Factory Methods
    }
}