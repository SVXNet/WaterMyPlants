using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using MvvmCross.iOS.Views;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using WaterMyPlants.Core.ViewModels.LifeCycle;

namespace WaterMyPlants.iOS.Views
{
    public class BaseView : MvxViewController
    {
        public BaseView()
        {

        }

        public BaseView(string nibName, NSBundle bundle) : base(nibName, bundle)
        {

        }

        private const string LogTag = nameof(BaseView);
        protected ILifeCycle LifeCycleViewModel => ViewModel as ILifeCycle;
        protected bool Subscribed { get; set; }

        CoreNavigationMode navigationMode;
        public override void ViewDidLoad()
        {
            Mvx.TaggedTrace(MvxTraceLevel.Diagnostic, LogTag, "ViewDidLoad - Start");
            base.ViewDidLoad();
            navigationMode = CoreNavigationMode.New;
        }

        public override void ViewWillAppear(bool animated)
        {
            Mvx.TaggedTrace(MvxTraceLevel.Diagnostic, LogTag, "ViewWillAppear - Start");
            base.ViewWillAppear(animated);

            if (!Subscribed)
            {
                SubscribeToMessages();
            }
            LifeCycleViewModel?.OnViewShown(navigationMode);
            navigationMode = CoreNavigationMode.Back;
        }

        public override void ViewDidDisappear(bool animated)
        {
            Mvx.TaggedTrace(MvxTraceLevel.Diagnostic, LogTag, "ViewDidDisappear - Start");
            base.ViewDidDisappear(animated);
            LifeCycleViewModel.OnViewHidden();
            UnsubscribeFromMessages();
        }

        public override void ViewDidUnload()
        {
            Mvx.TaggedTrace(MvxTraceLevel.Diagnostic, LogTag, "ViewDidUnload - Start");
            base.ViewDidUnload();
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
}
