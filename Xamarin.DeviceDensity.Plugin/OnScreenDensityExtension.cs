using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.DeviceDensity.Plugin
{
    [ContentProperty(nameof(Default))]
    public class OnScreenDensityExtension : IMarkupExtension
    {
        public object Default { get; set; }
        public object OnePointZero { get; set; }
        public object OnePointFive { get; set; }
        public object TwoPointZero { get; set; }
        public object TwoPointFive { get; set; }
        public object ThreePointZero { get; set; }
        public object ThreePointFive { get; set; }
        public object FourPointZero { get; set; }
        public IValueConverter Converter { get; set; }

        public object ConverterParameter { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Default == null
                && OnePointZero == null
                && OnePointFive == null
                && TwoPointZero == null
                && TwoPointFive == null
                && ThreePointZero == null
                && ThreePointFive == null
                && FourPointZero == null
                )
                throw new XamlParseException("OnScreenDensityExtension requires a non-null value to be specified for at least one device density or Default.");

            var valueProvider = serviceProvider?.GetService<IProvideValueTarget>() ?? throw new ArgumentException();

            BindableProperty bp;
            PropertyInfo pi = null;
            Type propertyType = null;

            if (valueProvider.TargetObject is Setter setter)
            {
                bp = setter.Property;
            }
            else
            {
                bp = valueProvider.TargetProperty as BindableProperty;
                pi = valueProvider.TargetProperty as PropertyInfo;
            }
            propertyType = bp?.ReturnType
                              ?? pi?.PropertyType
                              ?? throw new InvalidOperationException("Cannot determine property to provide the value for.");
            var value = GetValue();
            var info = propertyType.GetTypeInfo();

            if (value == null && info.IsValueType)
                return Activator.CreateInstance(propertyType);

            if (Converter != null)
                return Converter.Convert(value, propertyType, ConverterParameter, CultureInfo.CurrentUICulture);

            return default;

        }

        private object GetValue()
        {
            switch (GetDeviceScreenDensity())
            {
                case 1.0:
                    ///
                    return OnePointZero;
                case 1.5:
                    ///
                    return OnePointFive;
                case 2.0:
                    ///
                    return TwoPointZero;
                case 2.5:
                    ///
                    return TwoPointFive;
                case 3.0:
                    ///
                    return ThreePointZero;
                case 3.5:
                    ///
                    return ThreePointFive;
                case 4.0:
                    return FourPointZero;
                default:
                    return Default;
            }
        }

        private double GetDeviceScreenDensity() => DeviceDisplay.MainDisplayInfo.Density;
    }
}
