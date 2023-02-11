﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using CuaHangSach.Droid;

[assembly: ExportRenderer(typeof(Shell), typeof(CustomTabShell))]
namespace CuaHangSach.Droid
{
    public class CustomTabShell : ShellRenderer
    {
        public CustomTabShell(Context context) : base(context)
        {
        }

        protected override IShellItemRenderer CreateShellItemRenderer(ShellItem shellItem)
        {
            return new CustomTabShellItem(this);
        }

    }

}