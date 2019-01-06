using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core
{
    public class GameView : MonoBehaviour, IObservable<PauseButtonPressedArgs>,
                                           IObservable<HomeButtonPressedArgs>,
                                           IObservable<RestartButtonPressedArgs>,
                                           IObservable<ContinueButtonPressedArgs>
    {
        #region Fields

        [SerializeField] private GameObject _topBar;
        [SerializeField] private GameObject _panelPause;
        [SerializeField] private Button _buttonPause;
        [SerializeField] private Button _buttonHome;
        [SerializeField] private Button _buttonRestart;
        [SerializeField] private Button _buttonContinue;
        [SerializeField] private Text _textScore;

        #endregion //Fields

        #region Events

        private event EventHandler<PauseButtonPressedArgs> _pauseButtonPressed;
        private event EventHandler<HomeButtonPressedArgs> _homeButtonPressed;
        private event EventHandler<RestartButtonPressedArgs> _restartButtonPressed;
        private event EventHandler<ContinueButtonPressedArgs> _continueButtonPressed;

        #endregion //Events

        #region Unity Methods

        private void Awake()
        {
            _panelPause.SetActive(false);
            
            _buttonPause.onClick.RemoveAllListeners();
            _buttonPause.onClick.AddListener(OnPauseButtonPressed);
            
            _buttonHome.onClick.RemoveAllListeners();
            _buttonHome.onClick.AddListener(OnHomeButtonPressed);
            
            _buttonRestart.onClick.RemoveAllListeners();
            _buttonRestart.onClick.AddListener(OnRestartButtonPressed);
            
            _buttonContinue.onClick.RemoveAllListeners();
            _buttonContinue.onClick.AddListener(OnContinueButtonPressed);

            _textScore.text = "0";
        }

        #endregion //Unity Methods

        #region Public Methods

        public void SetScore(int score)
        {
            _textScore.text = score.ToString();
        }

        #endregion

        #region Private Methods

        private void OnPauseButtonPressed()
        {
            TogglePausePanel();

            (this as IObservable<PauseButtonPressedArgs>).Notify(null);
        }
        
        private void OnHomeButtonPressed()
        {
            (this as IObservable<HomeButtonPressedArgs>).Notify(null);
        }
        
        private void OnRestartButtonPressed()
        {
            (this as IObservable<RestartButtonPressedArgs>).Notify(null);
        }
        
        private void OnContinueButtonPressed()
        {
            TogglePausePanel();

            (this as IObservable<ContinueButtonPressedArgs>).Notify(null);
        }

        private void TogglePausePanel()
        {
            _panelPause.SetActive(!_panelPause.activeSelf);
            _topBar.SetActive(!_topBar.activeSelf);
        }

        #endregion //Private Methods

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