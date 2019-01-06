using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core
{
    #region EventArgs

    #endregion //EventArgs
    
    public class MainMenuView : MonoBehaviour, IObservable<PlayButtonPressedArgs>
    {
        #region Fields

        [SerializeField] private Button _buttonPlay;

        #endregion //Fields

        #region Events

        private event EventHandler<PlayButtonPressedArgs> _playButtonPressed; 

        #endregion //Events

        #region Unity Methods

        private void Awake()
        {
            _buttonPlay.onClick.RemoveAllListeners();
            _buttonPlay.onClick.AddListener(OnPlayButtonPressed);
        }

        #endregion //Unity Methods

        #region Private Methods
        
        private void OnPlayButtonPressed()
        {
            (this as IObservable<PlayButtonPressedArgs>).Notify(null);
        }

        #endregion //Private Methods
        
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