using UnityEngine;

namespace Game.Core
{
    public class GameFactory
    {
        #region Singleton

        public static GameFactory Instance { get; private set; }

        static GameFactory()
        {
            Instance = new GameFactory();
        }

        private GameFactory()
        {
        }
        
        #endregion //Singleton

        #region Factory Methods

        public GameModel CreateModel()
        {
            var model = Resources.Load<GameModel>("GameModel");
            return Object.Instantiate(model);
        }

        public GameView CreateView()
        {
            var prefab = Resources.Load<GameObject>("Prefabs/StateViews/GameView");
            return Object.Instantiate(prefab).GetComponent<GameView>();
        }

        public GameState CreateController(GameModel model, GameView view)
        {
            return new GameState(model, view);
        }

        #endregion //Factory Methods
    }
}