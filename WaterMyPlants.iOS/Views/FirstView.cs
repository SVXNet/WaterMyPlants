using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.Localization;

namespace WaterMyPlants.iOS.Views
{
    public partial class FirstView : BaseView
    {
        public FirstView() : base("FirstView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<FirstView, Core.ViewModels.FirstViewModel>();
            set.Bind(Label).To(vm => vm.Hello);
            set.Bind(TextField).To(vm => vm.Hello);
            set.Bind(TextField)
                .For(t => t.Placeholder)
                .To(vm => vm.TextSource)
                .WithConversion(new MvxLanguageConverter(), "FirstView_EnterTextHeader");
            set.Apply();
        }
    }
}
