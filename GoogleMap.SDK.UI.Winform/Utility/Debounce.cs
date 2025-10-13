using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace GoogleMap.SDK.UI.Winform.Utility
{
    public static class Debounce
    {
        public static Timer timer;
        public static Control control;

        public static void DelayCallback(this Control control, Action<object> action, object data, int debounceTime = 0)
        {
            Debounce.control = control;
            control.Tag = action;
            if (debounceTime != 0 && timer != null)
            {
                timer.Change(debounceTime, Timeout.Infinite);
            }
            else
            {
                timer = new Timer(Callback2, data, debounceTime, Timeout.Infinite);
            }
        }
        public static void DelayCallback(this Control control, Action action, int debounceTime = 0)
        {
            Debounce.control = control;
            control.Tag = action;
            if (debounceTime != 0 && timer != null)
            {
                timer.Change(debounceTime, Timeout.Infinite);
            }
            else
            {
                timer = new Timer(Callback, null, debounceTime, Timeout.Infinite);
            }
        }

        //public static void DelayCallback<T>(this Form form, Action<T> action, T data, int debounceTime = 0)
        //{
        //    if (debounceTime != 0 && timer != null)
        //    {
        //        timer.Change(debounceTime, Timeout.Infinite);
        //    }
        //    else
        //    {
        //        //action = action;
        //        timer = new Timer(Callback, data, debounceTime, Timeout.Infinite);
        //    }
        //}

        public static void Callback(object data)
        {
            control.Invoke(new Action(() =>
            {
                Action action = (Action)control.Tag;
                action.Invoke();
            }));
        }

        public static void Callback2(object data)
        {
            control.Invoke(new Action(() =>
            {
                Action<object> action = (Action<object>)control.Tag;
                action.Invoke(data);
            }));
        }

    }

}
