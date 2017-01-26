using MvvmCross.Plugins.Location;
using MvvmCross.Plugins.Messenger;

namespace WaterMyPlants.Core.Services
{
    public class LocationMessage : MvxMessage
    {
        public LocationMessage(object sender, MvxCoordinates coordinates) : base(sender)
        {
            Coordinates = coordinates;
        }
        public MvxCoordinates Coordinates { get; }
    }
}
