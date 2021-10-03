using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.DeviceDensity.Plugin
{
    [ContentProperty(nameof(Default))]
    public class OnScreenDensityStringExtension : IMarkupExtension<string>
    {
        public string Default { get; set; }
        public string OnePointZero { get; set; }
        public string OnePointFive { get; set; }
        public string TwoPointZero { get; set; }
        public string TwoPointFive { get; set; }
        public string ThreePointZero { get; set; }
        public string ThreePointFive { get; set; }
        public string FourPointZero { get; set; }

        public string ProvideValue(IServiceProvider serviceProvider)
        {
            if (Default == null)
                throw new Exception("OnScreenDensityExtension requires a non-null value to be specified for Default property.");

            return GetValue();
        }

        private string GetValue()
        {
            switch (GetDeviceScreenDensity())
            {
                case 1.0:
                case 1.49:
                    ///
                    return OnePointZero;
                case 1.5:
                case 1.99:
                    ///
                    return OnePointFive;
                case 2.0:
                case 2.49:
                    ///
                    return TwoPointZero;
                case 2.5:
                case 2.99:
                    ///
                    return TwoPointFive;
                case 3.0:
                case 3.49:
                    ///
                    return ThreePointZero;
                case 3.5:
                case 3.99:
                    ///
                    return ThreePointFive;
                case 4.0:
                case 4.9:
                    return FourPointZero;
                default:
                    return Default;
            }
        }

        private double GetDeviceScreenDensity() => DeviceDisplay.MainDisplayInfo.Density;

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<string>).ProvideValue(serviceProvider);
        }
    }
}
