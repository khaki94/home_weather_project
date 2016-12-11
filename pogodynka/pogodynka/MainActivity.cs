using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Timers;
using Android.Media;

namespace pogodynka
{
    [Activity(Label = "pogodynka", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        MediaPlayer _player;

        bool allowChangeTime = true;
        TextView myTimer;
        TextView date;
        TextView minute;
        TextView hour;

        Timer timer;
       
        View view;

        int _minute = 0, _hour = 0;

        int count = 1;

        bool isMusic = false;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            myTimer = (TextView)FindViewById(Resource.Id.tvTimer);
            date = (TextView)FindViewById(Resource.Id.tvDate);
            date.Text = (DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString());
            myTimer.Text = (DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());

            minute = (TextView)FindViewById(Resource.Id.tvMinute);
            hour = (TextView)FindViewById(Resource.Id.tvHour);

            hour.Text = DateTime.Now.Hour.ToString();
            minute.Text = DateTime.Now.Minute.ToString();

            _hour = Convert.ToInt32(hour.Text.ToString());
            _minute = Convert.ToInt32(minute.Text.ToString());


            Button button1 = (Button)FindViewById(Resource.Id.bMinusHour);
            Button button2 = (Button)FindViewById(Resource.Id.bPlusHour);
            Button button3 = (Button)FindViewById(Resource.Id.bMinusMinute);
            Button button4 = (Button)FindViewById(Resource.Id.bPlusMinute);

            button1.Click += delegate
            {
                if(allowChangeTime)
                    MinusHour(view);
            };
            button2.Click += delegate
            {
                if (allowChangeTime)
                    PlusHour(view);
            };
            button3.Click += delegate
            {
                if (allowChangeTime)
                    MinusMinute(view);
            };
            button4.Click += delegate
            {
                if (allowChangeTime)
                    PlusMinute(view);
            };

            Button button5 = (Button)FindViewById(Resource.Id.bSetAlarm);
            button5.Click += delegate
            {
                    SetAlarm(view);
            };

            _player = MediaPlayer.Create(this, Resource.Drawable.hangouts_incoming_call);

        }

        protected override void OnResume()
        {
            base.OnResume();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += NextSecond;
            timer.Start();

        }

        private void NextSecond(object sender, ElapsedEventArgs e)
        {
 
            
            string h, m;
            RunOnUiThread(() =>
            {
                h = DateTime.Now.Hour.ToString();
                m = DateTime.Now.Minute.ToString();
                myTimer.Text = (h + ":" + m + ":" + DateTime.Now.Second.ToString());
            });

            int _h = Convert.ToInt32(DateTime.Now.Hour.ToString());
            int _m = Convert.ToInt32(DateTime.Now.Minute.ToString());
            Button button5 = (Button)FindViewById(Resource.Id.bSetAlarm);
            if (button5.Text == "Reset Alarm")
                if (_hour == _h)
                    if (_minute == _m)
                        RunOnUiThread(() =>
                        {
                            button5.Text = "BUDZE CIE!!!!!!";
                            _player.Start();
                            isMusic = true;
                        });
            if (isMusic)
            {
                    _player.Start();
            }
        }
 
        public void PlusMinute(View view)
        {
            _minute += 1;
            if (_minute >= 60)
            {
                _minute = 0;
                _hour++;
             }
            hour.Text = _hour.ToString();
            minute.Text = _minute.ToString();
        }

        public void MinusMinute(View view)
        {
            _minute -= 1;
            if(_minute < 0)
            {
                _minute = 59;
                _hour--;
                if(_hour < 0)
                    _hour = 23;
            }
            hour.Text = _hour.ToString();
            minute.Text = _minute.ToString();
        }

        public void PlusHour(View view)
        {
            _hour++;
            if (_hour >= 24)
                _hour = 0;
            hour.Text = _hour.ToString();
        }

        public void MinusHour(View view)
        {
            _hour--;
            if (_hour < 0)
                _hour = 23;
            hour.Text = _hour.ToString();
        }
        public void SetAlarm(View view)
        {
            Button button5 = (Button)FindViewById(Resource.Id.bSetAlarm);
            if (button5.Text == "Set Alarm")
            {
                button5.Text = "Reset Alarm";
                allowChangeTime = false;
            }
            else if(button5.Text == "BUDZE CIE!!!!!!")
            {
                button5.Text = "Set Alarm";
                allowChangeTime = true;
                _player.Stop();
                isMusic = false;
            }
            else if(button5.Text == "Reset Alarm")
            {
                button5.Text = "Set Alarm";
                allowChangeTime = true;
            }
        }
    }
}

