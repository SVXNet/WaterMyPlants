using MvvmCross.Localization;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using WaterMyPlants.Core.Localization;

namespace WaterMyPlants.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.RegisterSingleton<IMvxTextProvider>(new MvxResxTextProvider(AppStrings.ResourceManager));

            RegisterAppStart<ViewModels.FirstViewModel>();
        }
    }
}
