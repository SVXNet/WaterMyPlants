using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Plugins.Location;

namespace WaterMyPlants.Core.Services
{
    public interface ILocationService
    {
        /// <summary>
        /// Start the watcher to get location messages sent
        /// </summary>
        /// <param name="options"></param>
        void StartLocationWatcher(MvxLocationOptions options);

        /// <summary>
        /// Stop the watcher
        /// </summary>
        void StopLocationWatcher();

        /// <summary>
        /// Get a location
        /// </summary>
        /// <param name="options"></param>
        /// <param name="refresh">When true, get a fresh location. When false the last known location will be returned if available</param>
        /// <returns></returns>
        Task<MvxCoordinates> GetLocationAsync(MvxLocationOptions options, bool refresh);
    }
}
