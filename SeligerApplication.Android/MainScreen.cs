using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace SeligerApplication.Android
{
    [Activity(Label = "SeligerApplication.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainScreen : ListActivity
    {
        MainAdapter adapter;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            adapter = new MainAdapter(this);
            ListAdapter = adapter;
        }

        //protected override void OnListItemClick(ListView l, View v, int position, long id)
        //{
            //var s = adapter[position];
            //var sample = new Intent(this);
            //this.StartActivity(sample);
        //}
    }
}

