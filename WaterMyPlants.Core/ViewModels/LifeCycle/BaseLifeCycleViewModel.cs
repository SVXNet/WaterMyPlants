using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;

namespace WaterMyPlants.Core.ViewModels.LifeCycle
{
    /// <summary>
    /// This base view model should be standard with no app specific code (so it can be easily shared with other projects)
    /// </summary>
    public abstract class BaseLifeCycleViewModel : MvxViewModel, ILifeCycle
    {
        private const string LogTag = nameof(BaseLifeCycleViewModel);

        #region Progress

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private string _busyMessage;

        public string BusyMessage
        {
            get { return _busyMessage; }
            set { SetProperty(ref _busyMessage, value); }
        }

        #endregion

        #region Life Cycle

        public bool ViewIsVisible { get; set; }
        public bool ReadyToShow { get; set; }

        public override async void Start()
        {
            base.Start();
            Mvx.TaggedTrace(MvxTraceLevel.Diagnostic, LogTag, $"{nameof(Start)} - Start");

            await InitializeViewAsync();

            //Once the view is initialized it can be shown, set ReadyToShow to true, the ShowView loop will then move on to showing the view
            ReadyToShow = true;
        }

        protected abstract Task InitializeViewAsync();

        /// <summary>
        /// Called from OnViewShown
        /// </summary>
        public virtual async Task ShowView(bool backNavigation)
        {
            Mvx.TaggedTrace(MvxTraceLevel.Diagnostic, LogTag, $"{nameof(ShowView)} - Start");
            //Wait until ReadyToShow = true
            var delay = TimeSpan.FromMilliseconds(50);
            while (!ReadyToShow)
            {
                Mvx.TaggedTrace(MvxTraceLevel.Diagnostic, LogTag,
                    $"{nameof(ShowView)} - Waiting for the view to be ready to show");
                await Task.Delay(delay);
            }
            Mvx.TaggedTrace(MvxTraceLevel.Diagnostic, LogTag, $"{nameof(ShowView)} - Now ready to show");
            SubscribeToEvents();
            ViewIsVisible = true;
        }

        public virtual void HideView()
        {
            Mvx.TaggedTrace(MvxTraceLevel.Diagnostic, LogTag, $"{nameof(HideView)} - Start");
            UnsubscribeFromEvents();
            ViewIsVisible = false;
        }

        public virtual void CloseView()
        {
            Mvx.TaggedTrace(MvxTraceLevel.Diagnostic, LogTag, $"{nameof(CloseView)} - Start");
            Close(this);
        }

        /// <summary>
        /// Override this method to subscribe to any events.
        /// Be sure to unsubscribe from events as well
        /// </summary>
        protected virtual void SubscribeToEvents()
        {
        }

        /// <summary>
        /// override this method to unsubscribe from any events
        /// </summary>
        protected virtual void UnsubscribeFromEvents()
        {
        }

        #region ILifeCycle Implementation

        public async void OnViewShown(CoreNavigationMode navigationMode)
        {
            Mvx.TaggedTrace(MvxTraceLevel.Diagnostic, LogTag,
                $"{nameof(OnViewShown)} - Start ({nameof(navigationMode)} = {navigationMode})");
            //View is now visible, if it wasn't visible before call ShowView
            if (!ViewIsVisible)
            {
                await ShowView(navigationMode == CoreNavigationMode.Back);
            }
        }

        public void OnViewHidden()
        {
            Mvx.TaggedTrace(MvxTraceLevel.Diagnostic, LogTag, $"{nameof(OnViewHidden)} - Start");
            //View is now hidden, if it was visible before, call HideView
            if (ViewIsVisible)
            {
                HideView();
            }
        }

        public void OnViewClosed()
        {
            Mvx.TaggedTrace(MvxTraceLevel.Diagnostic, LogTag, $"{nameof(OnViewClosed)} - Start");
            CloseView();
        }

        #endregion

        #endregion

    }
}
