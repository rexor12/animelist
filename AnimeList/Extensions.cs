using System;
using System.Windows.Forms;

namespace AnimeList
{
    public static class Extensions
    {
        public static void Each(this Control.ControlCollection control, Action<object> callback)
        {
            foreach (var child in control)
            {
                callback(child);
            }
        }
    }
}
