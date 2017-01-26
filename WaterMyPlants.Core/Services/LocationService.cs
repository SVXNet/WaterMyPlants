using System;
using System.Threading.Tasks;
using MvvmCross.Platform;
using MvvmCross.Plugins.Location;
using MvvmCross.Plugins.Messenger;

namespace WaterMyPlants.Core.Services
{
    public class LocationService
        : ILocationService
    {
        private readonly IMvxLocationWatcher _watcher;
        private readonly IMvxMessenger _messenger;

        public LocationService(IMvxLocationWatcher watcher, IMvxMessenger messenger)
        {
            _watcher = watcher;
            _messenger = messenger;
        }

        private void OnLocation(MvxGeoLocation location)
        {
            var message = new LocationMessage(this, location.Coordinates);
            _messenger.Publish(message);
        }

        private void OnError(MvxLocationError error)
        {
            Mvx.Error("Seen location error {0}", error.Code);
        }

        public void StartLocationWatcher(MvxLocationOptions options)
        {
            _watcher.Start(options, OnLocation, OnError);
        }

        public void StopLocationWatcher()
        {
            _watcher.Stop();
        }

        public Task<MvxCoordinates> GetLocationAsync(MvxLocationOptions options, bool refresh)
        {
            var tcs = new TaskCompletionSource<MvxCoordinates>();
            if (!refresh && _watcher.LastSeenLocation != null)
            {
                tcs.SetResult(_watcher.LastSeenLocation.Coordinates);
            }
            _watcher.Start(options, location =>
            {
                tcs.SetResult(location.Coordinates);
            }, error =>
            {
                tcs.SetException(new Exception(error.Code.ToString()));
            });

            return tcs.Task;
        }
    }
}