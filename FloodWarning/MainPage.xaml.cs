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
            /// This code is for the use of Geolocation in the app. The r relates to the radius from the current location.

              var position = await LocationManager.GetPosition();

              var lat = position.Coordinate.Latitude;
              var lon = position.Coordinate.Longitude;
              var r = 1000.0;

              RootObject myFlooding = 
                  await OpenFloodWarningProxy.GetFloodWarnings(lat,lon,r);             

            /*This is the old call to the API - anything in the brackets must be the same type as specified in the GetFloodWarnings code. For example a county of River.

            RootObject myFlooding = await OpenFloodWarningProxy.GetFloodWarnings(51.1,0.12,1000.0); */

          ///  This is what is returned. The bracketted item is the specific TA in the returned list.

            ResultMessage.Text = myFlooding.items[0].message;

            ResultTimeUpdated.Text = myFlooding.items[0].timeMessageChanged;

            ResultTACode.Text = myFlooding.items[0].floodAreaID;

        }

    }
}
