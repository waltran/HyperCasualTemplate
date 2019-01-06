using UnityEngine;

namespace Game.Core
{
    public class GameController : MonoBehaviour
    {
        #region Fields

        private StateController _stateController;

        #endregion //Fields

        #region Unity Methods

        private void Awake()
        {
            InitializeStateController();
        }
        
        private void FixedUpdate()
        {
            if (_stateController != null)
                (_stateController as IMonoNotification).FixedUpdate();
        }
        
        private void Update()
        {
            if (_stateController != null)
                (_stateController as IMonoNotification).Update();
        }
        
        private void LateUpdate()
        {
            if (_stateController != null)
                (_stateController as IMonoNotification).LateUpdate();
        }

        #endregion //Unity Methods

        #region Private Methods

        private void InitializeStateController()
        {
            _stateController = new StateController();
            _stateController.Init();
        }

        #endregion //Private Methods
    }
}