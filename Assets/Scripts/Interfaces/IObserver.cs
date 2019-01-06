using System;
namespace Game.Core
{
    public interface IObserver<T> where T : EventArgs
    {
        void OnNotified(object sender, T eventArgs);
    }
}
