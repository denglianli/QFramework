/****************************************************************************
 * Copyright (c) 2017 liangxie
****************************************************************************/

namespace QFramework
{
    using System;
    using System.Threading;
    using QFramework;
    
    public abstract class OperatorObserverBase<TSource, TResult> : IDisposable, IObserver<TSource>
    {
        protected internal volatile IObserver<TResult> observer;
        IDisposable cancel;

        public OperatorObserverBase(IObserver<TResult> observer, IDisposable cancel)
        {
            this.observer = observer;
            this.cancel = cancel;
        }

        public abstract void OnNext(TSource value);

        public abstract void OnError(Exception error);

        public abstract void OnCompleted();

        public void Dispose()
        {
            observer = QFramework.EmptyObserver<TResult>.Instance;
            var target = System.Threading.Interlocked.Exchange(ref cancel, null);
            if (target != null)
            {
                target.Dispose();
            }
        }
    }
}