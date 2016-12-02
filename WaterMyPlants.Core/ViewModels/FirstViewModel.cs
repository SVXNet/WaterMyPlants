using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using WaterMyPlants.Core.ViewModels.LifeCycle;

namespace WaterMyPlants.Core.ViewModels
{
    public class FirstViewModel : BaseLifeCycleViewModel
    {
        private string _hello = "Hello MvvmCross";
        public string Hello
        { 
            get { return _hello; }
            set { SetProperty (ref _hello, value); }
        }

        protected override async Task InitializeViewAsync()
        {
        }
    }
}
