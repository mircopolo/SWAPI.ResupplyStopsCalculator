using System;
using System.Collections.Generic;
using System.Text;

namespace Swapi.Core
{
    /// <summary>
    /// Class tha can unsubscribe from IObservable supscription.
    /// </summary>
    public sealed class DisposableSubscription<T> : IDisposable
    {
        private IList<IObserver<T>> _observers;
        private readonly IObserver<T> _observer;

        public DisposableSubscription
            (IList<IObserver<T>> observers, IObserver<T> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DoDispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void DoDispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
        #endregion


    }
}
