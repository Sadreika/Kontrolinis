using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using System;
using System.Timers;

namespace Kontrolinis
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public DateTime selectedDataTime { get; set; }
        public DateTime today = DateTime.Today;
        public Random random = new Random();
        public System.TimeSpan diff = new System.TimeSpan();
        Timer timer = new Timer();
        Timer timer1 = new Timer();
        public int dateInt { get; set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            var calendar = FindViewById<CalendarView>(Resource.Id.calendarView1);
            calendar.Visibility = ViewStates.Invisible;
            Button button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += delegate
            {
                if(calendar.Visibility.Equals(ViewStates.Visible))
                {
                    calendar.Visibility = ViewStates.Invisible;
                }
                else
                {
                    calendar.Visibility = ViewStates.Visible;
                }


                
                
            };
            calendar.DateChange += Calendar_DateChange;
            Button button2 = FindViewById<Button>(Resource.Id.button2);
            button2.Click += Button2_Click;
            Button button3 = FindViewById<Button>(Resource.Id.button3);
            button3.Click += Button3_Click;
            Button button4 = FindViewById<Button>(Resource.Id.button4);
            button4.Click += Button4_Click;
        }

        private void Calendar_DateChange(object sender, CalendarView.DateChangeEventArgs e)
        {
            var newdatetime = new DateTime(e.Year, e.Month, e.DayOfMonth);
            this.selectedDataTime = newdatetime;
            TextView textView = FindViewById<TextView>(Resource.Id.textView1);
            textView.Text = this.today.ToString() + " " + this.selectedDataTime.ToString();

            timer1.Interval = 5000;
            timer1.Elapsed += Timer_Elapsed1; ;
            timer1.Start();

            diff = newdatetime.Subtract(today);
            string date = diff.ToString();
            string[] splitedDate = date.Split('.');
            dateInt = Int16.Parse(splitedDate[0]);
            if(dateInt < 0)
            {
                dateInt = dateInt * -1;
            }
            timerrunning();
        }

        private void Timer_Elapsed1(object sender, ElapsedEventArgs e)
        {
            timer1.Stop();
        }

        public void timerrunning()
        {
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.textView1);
            textView.Text = random.Next(0, dateInt).ToString();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            var activity = (Activity)this;
            activity.FinishAffinity();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            timerrunning();
        }

        private void Button2_Click(object sender, System.EventArgs e)
        {
            timer.Stop();
            //timer = null;
        }

    }
}