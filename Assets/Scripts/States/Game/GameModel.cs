using System;
using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(fileName = "NewGameModel", menuName = "Game/Create GameModel")]
    public class GameModel : ScriptableObject, IMonoNotification, IObservable<ScoreUpdatedArgs>
    {
        #region Fields

        private int _score;

        #endregion //Fields

        #region Events

        private event EventHandler<ScoreUpdatedArgs> _scoreUpdated;

        #endregion //Events

        #region Public Methods

        public void Init()
        {
            Debug.Log("GameModel::Initializing");
        }

        public void End()
        {
            Debug.Log("GameModel::End");
        }

        #endregion //Public Methods

        #region IMonoNotification Interface

        void IMonoNotification.FixedUpdate()
        {
        }

        void IMonoNotification.Update()
        {
            _score++;
            OnScoreUpdated();
        }

        void IMonoNotification.LateUpdate()
        {
        }

        #endregion //IMonoNotification Interface

        #region Private Methods

        private void OnScoreUpdated()
        {
            (this as IObservable<ScoreUpdatedArgs>).Notify(new ScoreUpdatedArgs { NewScore = _score });
        }

        #endregion //Private Methods

        #region IObservable Interface

        void IObservable<ScoreUpdatedArgs>.Attach(IObserver<ScoreUpdatedArgs> observer)
        {
            _scoreUpdated += observer.OnNotified;
        }

        void IObservable<ScoreUpdatedArgs>.Detach(IObserver<ScoreUpdatedArgs> observer)
        {
            _scoreUpdated -= observer.OnNotified;
        }

        void IObservable<ScoreUpdatedArgs>.Notify(ScoreUpdatedArgs eventArgs)
        {
            if (_scoreUpdated != null)
                _scoreUpdated.Invoke(this, eventArgs);
        }

        #endregion //IObservable Interface
    }
}