using System;
using System.Threading;

namespace RateLimiting
{
    /// <summary>
    /// Rate Limiting (debounce, throttle) for C# Portable Class Library
    /// </summary>
    public static class RateLimitingExtentionForObject
    {
        #region Private Properties

        private static Timer _throttleTimerInterval;
        private static Timer _debounceTimerInterval;

        private static Action<object> _debounceAction;
        private static object _lastObjectDebounce;

        private static Action<object> _throttleAction;
        private static object _lastObjectThrottle;

        #endregion

        #region Debounce

        /// <summary>
        /// Debounce reset timer and after last item recieved give you last item. 
        /// <exception cref="http://demo.nimius.net/debounce_throttle/">See this example for understanding what is RateLimiting and Debounce</exception>
        /// </summary>
        /// <param name="obj">Your object</param>
        /// <param name="interval">Milisecond interval</param>
        /// <param name="debounceAction">Called when last item call this method and after interval was finished</param>
        public static void Debounce(this object obj, int interval, Action<object> debounceAction)
        {
            _lastObjectDebounce = obj;
            _debounceAction = debounceAction;

            _debounceTimerInterval?.Dispose();

            _debounceTimerInterval = new Timer(DebounceTimerIntervalOnTick, obj, interval, interval);
        }

        /// <summary>
        /// DispatchTimer tick event for debounce
        /// </summary>
        /// <param name="state"></param>
        private static void DebounceTimerIntervalOnTick(object state)
        {
            _debounceTimerInterval?.Dispose();

            if (_debounceTimerInterval != null)
            {
                _debounceAction?.Invoke(_lastObjectDebounce);
            }

            _debounceTimerInterval = null;
        }

        #endregion

        #region Throttle

        /// <summary>
        /// Throttle give you last objcet when timer was ticked and invoke throttleAction callback.
        /// <exception cref="http://demo.nimius.net/debounce_throttle/">See this example for understanding what is RateLimiting and Throttle</exception>
        /// </summary>
        /// <param name="obj">Your object</param>
        /// <param name="interval">Milisecond interval</param>
        /// <param name="throttleAction">Invoked last object when timer ticked invoked</param>
        public static void Throttle(this object obj, int interval, Action<object> throttleAction)
        {
            _lastObjectThrottle = obj;
            _throttleAction = throttleAction;

            if (_throttleTimerInterval == null)
            {
                _throttleTimerInterval = new Timer(ThrottleTimerIntervalOnTick, obj, interval, interval);
            }
        }

        /// <summary>
        /// DispatchTimer tick event for throttle
        /// </summary>
        /// <param name="state"></param>
        private static void ThrottleTimerIntervalOnTick(object state)
        {
            _throttleTimerInterval?.Dispose();
            _throttleTimerInterval = null;

            if (_lastObjectThrottle != null)
            {
                _throttleAction?.Invoke(_lastObjectThrottle);
            }
        }

        #endregion
    }
}
