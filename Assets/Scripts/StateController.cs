using System;
using UnityEngine;

namespace Game.Core
{
    public enum StateType
    {
        MainMenu,
        Game
    }

    public class StateController : IMonoNotification, IObserver<PlayButtonPressedArgs>,
                                                      IObserver<HomeButtonPressedArgs>,
                                                      IObserver<RestartButtonPressedArgs>
    {
        #region Fields

        private IState _activeState;

        #endregion //Fields

        #region Events


        #endregion //Events

        #region IMonoNotification

        void IMonoNotification.FixedUpdate()
        {
            if (_activeState != null && _activeState is IMonoNotification)
                (_activeState as IMonoNotification).FixedUpdate();
        }

        void IMonoNotification.Update()
        {
            if (_activeState != null && _activeState is IMonoNotification)
                (_activeState as IMonoNotification).Update();
        }

        void IMonoNotification.LateUpdate()
        {
            if (_activeState != null && _activeState is IMonoNotification)
                (_activeState as IMonoNotification).LateUpdate();
        }

        #endregion //IMonoNotification

        #region Public Methods

        public void Init()
        {
            SwitchState(StateType.MainMenu);
        }

        #endregion

        #region Private Methods

        private void SwitchState(StateType stateType)
        {
            // End state routine
            if (_activeState != null)
                _activeState.End();

            _activeState = null;

            // Create new state
            switch (stateType)
            {
                case StateType.MainMenu:
                    var mainMenuModel = MainMenuFactory.Instance.CreateModel();
                    var mainMenuView = MainMenuFactory.Instance.CreateView();
                    _activeState = MainMenuFactory.Instance.CreateController(mainMenuModel, mainMenuView);
                    
                    (_activeState as IObservable<PlayButtonPressedArgs>).Attach(this);
                    break;
                case StateType.Game:
                    var gameModel = GameFactory.Instance.CreateModel();
                    var gameView = GameFactory.Instance.CreateView();
                    _activeState = GameFactory.Instance.CreateController(gameModel, gameView);

                    (_activeState as IObservable<HomeButtonPressedArgs>).Attach(this);
                    (_activeState as IObservable<RestartButtonPressedArgs>).Attach(this);
                    break;
            }

            // Begin state routine
            if (_activeState != null)
                _activeState.Begin();
        }

        #endregion //Private methods

        #region IObserver Interface

        void IObserver<PlayButtonPressedArgs>.OnNotified(object sender, PlayButtonPressedArgs eventArgs)
        {
            SwitchState(StateType.Game);
        }

        void IObserver<HomeButtonPressedArgs>.OnNotified(object sender, HomeButtonPressedArgs eventArgs)
        {
            SwitchState(StateType.MainMenu);
        }

        void IObserver<RestartButtonPressedArgs>.OnNotified(object sender, RestartButtonPressedArgs eventArgs)
        {
            SwitchState(StateType.Game);
        }

        #endregion //IObserver Interface

        #region IObservable Interface

        #endregion //IObservable Interface
    }
}