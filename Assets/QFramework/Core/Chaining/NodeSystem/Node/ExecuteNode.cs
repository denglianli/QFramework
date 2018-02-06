/****************************************************************************
 * Copyright (c) 2017 liangxie
 * 
 * http://liangxiegame.com
 * https://github.com/liangxiegame/QFramework
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 ****************************************************************************/

namespace QFramework
{
    using System;

    public abstract class ExecuteNode : IExecuteNode
    {
        public Action OnBeganCallback = null;
        public Action OnEndedCallback = null;
        public Action OnDisposedCallback = null;
        
        protected bool mOnBeginCalled = false;

        #region IExecuteNode Support
        public bool Finished { get; protected set; }
        protected bool mDisposed = false;

        public void Break()
        {
            Finished = true;
        }
        #endregion

        #region ResetableSupport

        public void Reset()
        {
            Finished = false;
            mOnBeginCalled = false;
            mDisposed = false;
            OnReset();
        }
        #endregion


        #region IExecutable Support

        public bool Execute(float dt)
        {
            if (!mOnBeginCalled)
            {
                mOnBeginCalled = true;
                OnBegin();
                OnBeganCallback.InvokeGracefully();
            }

            if (!Finished)
            {
                OnExecute(dt);
            }

            if (Finished)
            {                
                OnEnd();
                OnEndedCallback.InvokeGracefully();
            }

            return Finished || mDisposed;
        }

        #endregion

        protected virtual void OnReset()
        {
        }

        protected virtual void OnBegin()
        {
        }

        /// <summary>
        /// finished
        /// </summary>
        protected virtual void OnExecute(float dt)
        {
        }

        protected virtual void OnEnd()
        {
        }

        protected virtual void OnDispose()
        {
        }

        #region IDisposable Support

        public void Dispose()
        {
            if (mDisposed) return;
            mDisposed = true;
            
            OnBeganCallback = null;
            OnEndedCallback = null;
            OnDisposedCallback.InvokeGracefully();
            OnDisposedCallback = null;
            OnDispose();
        }

        #endregion
    }
}