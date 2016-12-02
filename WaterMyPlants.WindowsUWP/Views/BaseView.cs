using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Navigation;
using MvvmCross.WindowsUWP.Views;
using WaterMyPlants.Core.ViewModels.LifeCycle;

namespace WaterMyPlants.WindowsUWP.Views
{
    public abstract class BaseView : MvxWindowsPage
    {
        protected ILifeCycle LifeCycleViewModel => ViewModel as ILifeCycle;
        protected bool Subscribed { get; set; }

        protected virtual void OnBackButtonPressed(object sender, BackRequestedEventArgs e)
        {
            if (!e.Handled && Frame.CanGoBack)
            {
                LifeCycleViewModel.OnViewClosed();
                e.Handled = true;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            DataContext = null;
            DataContext = ViewModel;

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = Frame.CanGoBack
                ? AppViewBackButtonVisibility.Visible
                : AppViewBackButtonVisibility.Collapsed;

            if (!Subscribed)
            {
                SubscribeToMessages();
            }

            LifeCycleViewModel.OnViewShown((CoreNavigationMode)e.NavigationMode);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            LifeCycleViewModel.OnViewHidden();
            UnsubscribeFromMessages();
        }

        protected virtual void SubscribeToMessages()
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackButtonPressed;
            Subscribed = true;
        }

        protected virtual void UnsubscribeFromMessages()
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= OnBackButtonPressed;
            Subscribed = false;
        }

    }
}
