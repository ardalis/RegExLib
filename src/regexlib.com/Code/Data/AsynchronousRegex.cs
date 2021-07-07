using System;
using System.Threading;
using System.Text.RegularExpressions;

namespace RegExLib {

    public sealed class AsynchronousRegex {
        public class AsynchronousRegexResult : IAsyncResult {
            internal object asyncState = null;
            internal Regex expression = null;
            internal MatchCollection matches = null;
            internal int count = -1;
            internal string inputString = null;
            internal bool complete = false;
            internal bool completedSynchronously = false;
            internal bool cancelled = false;
            internal ManualResetEvent waitHandle = null;
            internal AsyncCallback callback = null;
            internal Thread currentThread = null;

            internal AsynchronousRegexResult(Regex innerExpression, string inputString, AsyncCallback callback, object state) {
                this.asyncState = state;
                this.expression = innerExpression;
                this.inputString = inputString;
                this.callback = callback;

                if (this.expression == null || this.inputString == null) {
                    this.complete = true;
                    this.completedSynchronously = true;
                }
                this.waitHandle = new ManualResetEvent(this.completedSynchronously);
            }

            public object AsyncState { get { return this.asyncState; } }
            public bool CompletedSynchronously { get { return this.completedSynchronously; } }
            public bool IsCompleted { get { return this.complete; } }
            public WaitHandle AsyncWaitHandle { get { return this.waitHandle; } }

            public Regex Expression { get { return this.expression; } }
            public string Input { get { return this.inputString; } }
            public MatchCollection Result { get { return this.matches; } }
            public int Count { get { return this.count; } }

            public bool Cancel() {
                if (this.cancelled || this.complete) {
                    return false;
                }

                this.cancelled = true;
                if (this.currentThread != null) {
                    this.currentThread.Abort();
                }
                return true;
            }

            internal void Complete() {
                this.complete = true;
                this.waitHandle.Set();

                if (callback != null) {
                    callback(this);
                }
            }
        }

        public static IAsyncResult BeginInvoke(Regex innerExpression, string inputString, AsyncCallback callback, object state) {

            AsynchronousRegexResult result = new AsynchronousRegexResult(innerExpression, inputString, callback, state);

            if (!result.complete) {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadPool_WaitCallback), result);
            }

            return result;
        }

        private static void ThreadPool_WaitCallback(object state) {
            AsynchronousRegexResult result = state as AsynchronousRegexResult;

            if (result != null && !result.cancelled) {

                result.currentThread = Thread.CurrentThread;

                try {
                    result.matches = result.expression.Matches(result.inputString);
                    result.count = result.matches.Count;
                    result.cancelled = true; // Can't cancel at this point
                    result.Complete();
                } catch (ThreadAbortException) {
                    Thread.ResetAbort();
                } finally {
                    result.currentThread = null;
                }
            }
        }

        public static MatchCollection EndInvoke(IAsyncResult ar) {
            AsynchronousRegexResult result = ar as AsynchronousRegexResult;

            if (result != null) {
                result.AsyncWaitHandle.WaitOne();
                return result.matches;
            } else {
                throw new Exception("EndInvoke was called with the wrong async result");
            }
        }
    }
}
