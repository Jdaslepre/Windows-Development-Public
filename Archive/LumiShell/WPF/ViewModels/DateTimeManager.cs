using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Markup;
using System.Windows.Threading;

namespace WPF.ShellManager
{
    public class DateTimeManager : INotifyPropertyChanged
    {
        /*




         These are case sensitive.




         Hour - h (1-2 digits / 12 Hour Clock)

         The “h” custom format specifier represents the hour as a number from 1 through 12; that is, the hour
         represented by a 12-hour clock that counts the whole hours since midnight or noon. A particular hour
         after midnight is indistinguishable from the same hour after noon. The hour is not rounded, and a 
         single-digit hour is formatted without a leading zero. For example, given a time of 5:43 in the
         morning or afternoon, this custom format specifier displays “5”.

         If the “h” format specifier is used without other custom format specifiers, it’s interpreted as
         a standard date and time format specifier and throws a FormatException.



         Minute - m (1-2 digits)


         The "m" custom format specifier represents the minute as a number from 0 through 59. The minute
         represents whole minutes that have passed since the last hour. A single-digit minute is formatted
         without a leading zero.

         If the “m” format specifier is used without the other custom format specifiers, it’s interpreted
         as the “m” standard date and time format specifier.



         Minute - mm (2 digits)

         The “mm” custom format specifier (plus any number of additional “m” specifiers) represents the
         minute as a number from 00 through 59. The minute represents whole minutes that have passed
         since the last hour. A single-digit minute is formatted with a leading zero.

         MM - month (two digits)
         MMM - month (abbreviated)
         MMMM - month (full)

         */

        public string _time;
        public string _date;

        public event PropertyChangedEventHandler PropertyChanged;

        public DateTimeManager()
        {
            // Initialize properties
            Time = DateTime.Now.ToString("h:mm tt");
            Date = DateTime.Now.ToString("dddd, MMMM d");

            // Update time every second
            var timer = new Windows.UI.Xaml.DispatcherTimer();
            timer.Interval = TimeSpan.FromTicks(15);
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        // Property for time
        public string Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        // Property for date
        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        // Method to update time
        public void Timer_Tick(object sender, object e)
        {
            Time = DateTime.Now.ToString("h:mm tt");
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}