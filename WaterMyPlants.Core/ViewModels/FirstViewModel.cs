using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Location;
using MvvmCross.Plugins.PictureChooser;
using WaterMyPlants.Core.Services;
using WaterMyPlants.Core.ViewModels.LifeCycle;

namespace WaterMyPlants.Core.ViewModels
{
    public class FirstViewModel : BaseLifeCycleViewModel
    {
        private IMvxPictureChooserTask _pictureChooser;
        private ILocationService _locationService;

        public FirstViewModel(IMvxPictureChooserTask pictureChooser, ILocationService locationService)
        {
            _pictureChooser = pictureChooser;
            _locationService = locationService;
        }

        private string _hello = "Hello MvvmCross";
        public string Hello
        { 
            get { return _hello; }
            set { SetProperty (ref _hello, value); }
        }

        protected override async Task InitializeViewAsync()
        {
            var location = await _locationService.GetLocationAsync(new MvxLocationOptions { }, true);
        }
    }
}
