using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Fingerprint.Dialog;

namespace CRateWallet.Droid
{
    public class MyCustomDialogFragment : FingerprintDialogFragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            view.Background = new ColorDrawable(Color.White); // make it fancyyyy :D
            var image = view.FindViewById<ImageView>(Resource.Id.fingerprint_imgFingerprint);
            image.SetColorFilter(Color.Orange);
            image.SetMinimumWidth(50);
            image.SetMinimumHeight(50);

            return view;
        }
    }
}