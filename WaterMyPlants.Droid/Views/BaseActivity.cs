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
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using WaterMyPlants.Core.ViewModels.LifeCycle;

namespace WaterMyPlants.Droid.Views
{
    public abstract class BaseActivity : MvxActivity
    {
        private const string LogTag = nameof(BaseActivity);


        protected ILifeCycle LifeCycleViewModel => ViewModel as ILifeCycle;
        protected abstract int ResourceId { get; }
        protected bool Subscribed { get; set; }
        CoreNavigationMode _navigationMode;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(ResourceId);
            _navigationMode = CoreNavigationMode.New;
        }

        protected override void OnResume()
        {
            Mvx.TaggedTrace(MvxTraceLevel.Diagnostic, LogTag, "OnResume - Start");
            base.OnResume();
            if (!Subscribed)
            {
                SubscribeToMessages();
            }
            LifeCycleViewModel?.OnViewShown(_navigationMode);
            _navigationMode = CoreNavigationMode.Back;
        }

        protected override void OnPause()
        {
            Mvx.TaggedTrace(MvxTraceLevel.Diagnostic, LogTag, "OnPause - Start");
            base.OnPause();

            LifeCycleViewModel.OnViewHidden();
            UnsubscribeFromMessages();
        }

        protected virtual void SubscribeToMessages()
        {
            Mvx.TaggedTrace(MvxTraceLevel.Diagnostic, LogTag, "SubscribeToMessages - Start");
            Subscribed = true;
        }

        protected virtual void UnsubscribeFromMessages()
        {
            Mvx.TaggedTrace(MvxTraceLevel.Diagnostic, LogTag, "UnsubscribeFromMessages - Start");
            Subscribed = false;
        }

    }

    public abstract class BaseActivity<TViewModel> : BaseActivity where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}