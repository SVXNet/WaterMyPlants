namespace WaterMyPlants.Core.ViewModels.LifeCycle
{
    public interface ILifeCycle
    {
        void OnViewShown(CoreNavigationMode navigationMode);

        void OnViewHidden();

        void OnViewClosed();
    }
}