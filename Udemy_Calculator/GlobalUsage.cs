using System.Runtime.CompilerServices;

namespace Udemy_Calculator
{
    public static class GlobalUsage
    {
        /// <summary>
        /// Get the calling method name
        /// </summary>
        public static string GetCurrentMethodName
        {
            get { return CurrentMethodName(); }
        }

        /// <summary>
        /// Getting the parent name calling method
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static string CurrentMethodName()
        {
            return new System.Diagnostics.StackTrace(2, false).GetFrame(0).GetMethod().Name;
        }
    }
}
