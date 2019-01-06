using UnityEngine;
using UnityEngine.UI;

namespace Game.Core
{
    public class GameView : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _panelPause;
        [SerializeField] private Button _buttonPause;
        [SerializeField] private Button _buttonHome;
        [SerializeField] private Button _buttonRestart;
        [SerializeField] private Button _buttonContinue;

        #endregion

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
        }

        #endregion //Unity Methods

        #region Private Methods

        private void OnPauseButtonPressed()
        {
            
        }
        
        private void OnHomeButtonPressed()
        {
            
        }
        
        private void OnRestartButtonPressed()
        {
            
        }
        
        private void OnContinueButtonPressed()
        {
            
        }

        #endregion //Private Methods
    }
}