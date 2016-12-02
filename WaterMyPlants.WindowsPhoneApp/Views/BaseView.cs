using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml.Navigation;
using MvvmCross.WindowsCommon.Views;
using WaterMyPlants.Core.ViewModels.LifeCycle;

namespace WaterMyPlants.WindowsPhoneApp.Views
{
    public abstract class BaseView : MvxWindowsPage
    {
        protected ILifeCycle LifeCycleViewModel => ViewModel as ILifeCycle;
        protected bool Subscribed { get; set; }

        protected virtual void OnBackButtonPressed(object sender, BackPressedEventArgs e)
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
            HardwareButtons.BackPressed += OnBackButtonPressed;
            Subscribed = true;
        }

        protected virtual void UnsubscribeFromMessages()
        {
            HardwareButtons.BackPressed -= OnBackButtonPressed;
            Subscribed = false;
        }
    }
}
