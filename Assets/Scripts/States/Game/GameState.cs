using System;
using Object = UnityEngine.Object;

namespace Game.Core
{
    public class GameState : IState, IMonoNotification, IObservable<PauseButtonPressedArgs>,    IObserver<PauseButtonPressedArgs>,
                                                        IObservable<HomeButtonPressedArgs>,     IObserver<HomeButtonPressedArgs>,
                                                        IObservable<RestartButtonPressedArgs>,  IObserver<RestartButtonPressedArgs>,
                                                        IObservable<ContinueButtonPressedArgs>, IObserver<ContinueButtonPressedArgs>,
                                                                                                IObserver<ScoreUpdatedArgs>
    {
        #region Fields

        private GameModel _model;
        private GameView _view;
        private bool _isPaused;

        #endregion //Fields

        #region Events

        private event EventHandler<PauseButtonPressedArgs> _pauseButtonPressed;
        private event EventHandler<HomeButtonPressedArgs> _homeButtonPressed;
        private event EventHandler<RestartButtonPressedArgs> _restartButtonPressed;
        private event EventHandler<ContinueButtonPressedArgs> _continueButtonPressed;

        #endregion //Events

        #region Constructor

        public GameState(GameModel model, GameView view)
        {
            _model = model;
            _view = view;
        }

        #endregion //Constructor

        #region IMonoNotification Interface

        void IMonoNotification.FixedUpdate()
        {
            if (!_isPaused)
                (_model as IMonoNotification).FixedUpdate();
        }

        void IMonoNotification.Update()
        {
            if (!_isPaused)
                (_model as IMonoNotification).Update();
        }

        void IMonoNotification.LateUpdate()
        {
            if (!_isPaused)
                (_model as IMonoNotification).LateUpdate();
        }

        #endregion //IMonoNotification Interface

        #region IState Interface

        void IState.Begin()
        {
            SubscribeEvents();
        }

        void IState.End()
        {
            UnsubscribeEvents();
            Object.DestroyImmediate(_view.gameObject);
            Object.DestroyImmediate(_model);
        }
        
        #endregion //IState Interface

        #region Private Methods

        private void SubscribeEvents()
        {
            (_view as IObservable<PauseButtonPressedArgs>).Attach(this);
            (_view as IObservable<HomeButtonPressedArgs>).Attach(this);
            (_view as IObservable<RestartButtonPressedArgs>).Attach(this);
            (_view as IObservable<ContinueButtonPressedArgs>).Attach(this);
            (_model as IObservable<ScoreUpdatedArgs>).Attach(this);
        }

        private void UnsubscribeEvents()
        {
            (_view as IObservable<PauseButtonPressedArgs>).Detach(this);
            (_view as IObservable<HomeButtonPressedArgs>).Detach(this);
            (_view as IObservable<RestartButtonPressedArgs>).Detach(this);
            (_view as IObservable<ContinueButtonPressedArgs>).Detach(this);
            (_model as IObservable<ScoreUpdatedArgs>).Detach(this);
        }

        #endregion //Private Methods

        #region IObserver Interface

        void IObserver<PauseButtonPressedArgs>.OnNotified(object sender, PauseButtonPressedArgs eventArgs)
        {
            _isPaused = true;

            (this as IObservable<PauseButtonPressedArgs>).Notify(eventArgs);
        }

        void IObserver<HomeButtonPressedArgs>.OnNotified(object sender, HomeButtonPressedArgs eventArgs)
        {
            (this as IObservable<HomeButtonPressedArgs>).Notify(eventArgs);
        }

        void IObserver<RestartButtonPressedArgs>.OnNotified(object sender, RestartButtonPressedArgs eventArgs)
        {
            (this as IObservable<RestartButtonPressedArgs>).Notify(eventArgs);
        }

        void IObserver<ContinueButtonPressedArgs>.OnNotified(object sender, ContinueButtonPressedArgs eventArgs)
        {
            _isPaused = false;

            (this as IObservable<ContinueButtonPressedArgs>).Notify(eventArgs);
        }

        void IObserver<ScoreUpdatedArgs>.OnNotified(object sender, ScoreUpdatedArgs eventArgs)
        {
            _view.SetScore(eventArgs.NewScore);
        }

        #endregion //IObserver Interface

        #region IObservable Interface

        void IObservable<PauseButtonPressedArgs>.Attach(IObserver<PauseButtonPressedArgs> observer)
        {
            _pauseButtonPressed += observer.OnNotified;
        }

        void IObservable<PauseButtonPressedArgs>.Detach(IObserver<PauseButtonPressedArgs> observer)
        {
            _pauseButtonPressed -= observer.OnNotified;
        }

        void IObservable<PauseButtonPressedArgs>.Notify(PauseButtonPressedArgs eventArgs)
        {
            if (_pauseButtonPressed != null)
                _pauseButtonPressed.Invoke(this, eventArgs);
        }

        void IObservable<HomeButtonPressedArgs>.Attach(IObserver<HomeButtonPressedArgs> observer)
        {
            _homeButtonPressed += observer.OnNotified;
        }

        void IObservable<HomeButtonPressedArgs>.Detach(IObserver<HomeButtonPressedArgs> observer)
        {
            _homeButtonPressed -= observer.OnNotified;
        }

        void IObservable<HomeButtonPressedArgs>.Notify(HomeButtonPressedArgs eventArgs)
        {
            if (_homeButtonPressed != null)
                _homeButtonPressed.Invoke(this, eventArgs);
        }

        void IObservable<RestartButtonPressedArgs>.Attach(IObserver<RestartButtonPressedArgs> observer)
        {
            _restartButtonPressed += observer.OnNotified;
        }

        void IObservable<RestartButtonPressedArgs>.Detach(IObserver<RestartButtonPressedArgs> observer)
        {
            _restartButtonPressed -= observer.OnNotified;
        }

        void IObservable<RestartButtonPressedArgs>.Notify(RestartButtonPressedArgs eventArgs)
        {
            if (_restartButtonPressed != null)
                _restartButtonPressed.Invoke(this, eventArgs);
        }

        void IObservable<ContinueButtonPressedArgs>.Attach(IObserver<ContinueButtonPressedArgs> observer)
        {
            _continueButtonPressed += observer.OnNotified;
        }

        void IObservable<ContinueButtonPressedArgs>.Detach(IObserver<ContinueButtonPressedArgs> observer)
        {
            _continueButtonPressed -= observer.OnNotified;
        }

        void IObservable<ContinueButtonPressedArgs>.Notify(ContinueButtonPressedArgs eventArgs)
        {
            if (_continueButtonPressed != null)
                _continueButtonPressed.Invoke(this, eventArgs);
        }

        #endregion //IObservable Interface
    }
}