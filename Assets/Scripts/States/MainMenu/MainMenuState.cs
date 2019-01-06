using System;
using Object = UnityEngine.Object;

namespace Game.Core
{
    public class MainMenuState : IState, IObservable<PlayButtonPressedArgs>, IObserver<PlayButtonPressedArgs>
    {
        #region Fields

        private MainMenuModel _model;
        private MainMenuView _view;

        #endregion //Fields

        #region Events

        private event EventHandler<PlayButtonPressedArgs> _playButtonPressed; 

        #endregion //Events

        #region Constructor

        public MainMenuState(MainMenuModel model, MainMenuView view)
        {
            _model = model;
            _view = view;
        }

        #endregion //Constructor

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
            (_view as IObservable<PlayButtonPressedArgs>).Attach(this);
        }

        private void UnsubscribeEvents()
        {
            (_view as IObservable<PlayButtonPressedArgs>).Detach(this);
        }

        #endregion //Private Methods
        
        #region IObserver Interface

        void IObserver<PlayButtonPressedArgs>.OnNotified(object sender, PlayButtonPressedArgs eventArgs)
        {
            (this as IObservable<PlayButtonPressedArgs>).Notify(eventArgs);
        }

        #endregion //IObserver Interface
        
        #region IObservable Interface

        void IObservable<PlayButtonPressedArgs>.Attach(IObserver<PlayButtonPressedArgs> observer)
        {
            _playButtonPressed += observer.OnNotified;
        }

        void IObservable<PlayButtonPressedArgs>.Detach(IObserver<PlayButtonPressedArgs> observer)
        {
            _playButtonPressed -= observer.OnNotified;
        }

        void IObservable<PlayButtonPressedArgs>.Notify(PlayButtonPressedArgs eventArgs)
        {
            if (_playButtonPressed != null)
                _playButtonPressed.Invoke(this, eventArgs);
        }

        #endregion //IObservable Interface
    }
}