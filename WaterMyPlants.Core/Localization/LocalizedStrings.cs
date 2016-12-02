using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterMyPlants.Core.Localization
{
    public class LocalizedStrings
    {
        private static readonly AppStrings LocalizedStringsResources = new AppStrings();

        public AppStrings AppStrings => LocalizedStringsResources;
    }
}
