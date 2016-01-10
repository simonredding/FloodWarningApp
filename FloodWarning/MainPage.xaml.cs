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
          /* This code is for the use of Geolocation in the app.  Not currently in use
            
            var position = await LocationManager.GetPosition();
            
            var lat = position.Coordinate.Latitude;
            var lon = position.Coordinate.Longitude;

            RootObject myFlooding = 
                await OpenFloodWarningProxy.GetFloodWarnings(
                        lat,
                        lon);

            */            
            
            //This is the call to the API - anything in the brackets must be the same type as specified in the GetFloodWarnings code. For example a county of River.

            RootObject myFlooding = await OpenFloodWarningProxy.GetFloodWarnings("London");
                     
            //This is what is returned. The bracketted item is the specific TA in the returned list. 

            ResultFloodWarningsMessage.Text = myFlooding.items[5].message;

            ResultFloodWarningsTimeUpdated.Text = myFlooding.items[5].timeMessageChanged;

            ResultFloodWarningsTACode.Text = myFlooding.items[5].floodAreaID;
            
        }

    }
}
