using System;

namespace Game.Core
{
    public interface IObservable<T> where T : EventArgs
    {
        void Attach(IObserver<T> observer);
        void Detach(IObserver<T> observer);
        void Notify(T eventArgs);
    }
}