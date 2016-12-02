using System.Globalization;
using System.Resources;
using MvvmCross.Localization;

namespace WaterMyPlants.Core.Localization
{
    public class MvxResxTextProvider : IMvxTextProvider
    {
        private readonly ResourceManager _resourceManager;
        public MvxResxTextProvider(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
            CurrentLanguage = CultureInfo.CurrentUICulture;

        }
        public CultureInfo CurrentLanguage { get; set; }
        public string GetText(string namespaceKey, string typeKey, string name)
        {
            var result = _resourceManager.GetString(name);
            if (!string.IsNullOrWhiteSpace(result))
            {
                return result;
            }
            var resolvedKey = name;
            if (!string.IsNullOrEmpty(typeKey))
            {
                resolvedKey = $"{typeKey}.{resolvedKey}";
            }
            if (!string.IsNullOrEmpty(namespaceKey))
            {
                resolvedKey = $"{namespaceKey}.{resolvedKey}";
            }
            return _resourceManager.GetString(resolvedKey, CurrentLanguage);
        }
        public string GetText(string namespaceKey, string typeKey, string name, params object[] formatArgs)
        {
            var baseText = GetText(namespaceKey, typeKey, name);
            return string.IsNullOrEmpty(baseText) ? baseText : string.Format(baseText, formatArgs);
        }
    }
}