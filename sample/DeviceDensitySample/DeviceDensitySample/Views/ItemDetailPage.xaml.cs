using DeviceDensitySample.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace DeviceDensitySample.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}