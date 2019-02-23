using System;

namespace Extensions
{
    public static class ActionExtensions
    {
        public static void SafeInvoke(this Action action)
        {
            if (action != null)
            {
                action.Invoke();
            }
        }
    }
}