using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SeligerApplication.Android.Resources.Screens
{
    [Activity(Label = "SimpleListItem1")]
    class SimpleListItem1: ListActivity
    {
        
        string[] items;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            items = new string []{"Row 1", "Row 2", "Row 3"};

            this.ListAdapter = new ArrayAdapter<String> (this, Android.Resource.Layout.SimpleListItem1, items);
        }
        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var t = items[position];
            Toast.MakeText(this, t, ToastLength.Short).Show();
            Console.WriteLine("Clicked on " + t);
        }
    }
}