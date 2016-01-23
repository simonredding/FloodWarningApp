using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FloodWarning
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                /// This code is for the use of Geolocation in the app. The dist relates to the distance (radius) from the current location.
                /// minseverity is related to what level of flood warning to show. 3 = Alert, 2 = Warning, 1= SFW

                var position = await LocationManager.GetPosition();

                var lat = position.Coordinate.Latitude;
                var lon = position.Coordinate.Longitude;

                var dist = 10;
                var minseverity = 3;

                RootObject myFlooding = await OpenFloodWarningProxy.GetFloodWarnings(
                    lat, 
                    lon, 
                    dist,
                    minseverity);

                /*// Schedule update
                var uri = String.Format("http://uwpweatherservice.azurewebsites.net/?lat={0}&lon={1}", lat, lon);

                var tileContent = new Uri(uri);
                var requestedInterval = PeriodicUpdateRecurrence.HalfHour;

                var updater = TileUpdateManager.CreateTileUpdaterForApplication();
                updater.StartPeriodicUpdate(tileContent, requestedInterval);

                string icon = String.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.weather[0].icon);
                ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));

                */

                /*This is the old call to the API - anything in the brackets must be the same type as specified in the GetFloodWarnings code. For example a county of River.

                RootObject myFlooding = await OpenFloodWarningProxy.GetFloodWarnings(51.1,0.12,1000.0); */

                ///  This is what is returned. The bracketted item is the specific numbered TA in the returned list.

                ResultTAName.Text = myFlooding.items[0].severity + " has been issued for " + myFlooding.items[0].description;

                ResultMessage.Text = myFlooding.items[0].message;

                ResultTimeUpdated.Text = "Information last updated at " + myFlooding.items[0].timeMessageChanged;

                ResultTACode.Text = myFlooding.items[0].floodAreaID;
            }
            catch
            {
                ResultMessage.Text = "No flooding information available at this location at this time.";

            }
        }

    }
}
