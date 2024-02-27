using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoMapMAUI.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly IMap map;
        private readonly IGeolocation geolocation;
        private readonly IConnectivity connectivity;
        public MainPageViewModel(IMap map, IGeolocation geolocation, IConnectivity connectivity) 
        {
            this.map = map;
            this.geolocation = geolocation;
            this.connectivity = connectivity;
        }
        [RelayCommand]
        public async Task CheckLocation()
        {
            if(Connectivity.Current.NetworkAccess !=NetworkAccess.Internet) {
                await Shell.Current.DisplayAlert("No InterNet", "Need Internet connection", "Ok");
                return;
            }
            //Get current Location
            var location=await geolocation.GetLastKnownLocationAsync();
            if(location != null)
            {
                location = await geolocation.GetLocationAsync(
                    new GeolocationRequest
                    {
                        DesiredAccuracy=GeolocationAccuracy.Best,
                        Timeout=TimeSpan.FromSeconds(30),
                        RequestFullAccuracy=true
                    }
                   
                    );
            }
            if(location == null)
            {

                await Shell.Current.DisplayAlert("Not Find Location", "Sorry, not Found Location", "Ok");
                return;
            }
            await map.OpenAsync(location.Latitude,location.Longitude,new MapLaunchOptions
            {
                Name="My Current Location",
                NavigationMode=NavigationMode.None
            }
                );
        }
    }
}
