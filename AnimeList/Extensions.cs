using System;
using System.Windows.Forms;

namespace AnimeList
{
    /// <summary>
    /// Defines extension methods for various things.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// For-each convenience method for <see cref="Control.ControlCollection"/>.
        /// </summary>
        /// <param name="controls">the control collection to iterate through</param>
        /// <param name="callback">the method to call for each child</param>
        public static void Each(this Control.ControlCollection controls, Action<object> callback)
        {
            foreach (var child in controls)
            {
                callback(child);
            }
        }
    }
}
