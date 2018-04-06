using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using ThreadingApplication.Elements;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ThreadingApplication
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            displayResult();
        }
        public async Task displayResult()
        {
            AlphaApiFactory apif = new AlphaApiFactory();
            AlphaManager am = apif.getApiRequest("daily", "BTC", "EUR");
            await am.setStocks();
            doChart();

        }

        public void doChart()
        {
            List<Chart> Source = new List<Chart>();
            Source.Add(new Chart() { Name = "N1", Amount = 50 });
            Source.Add(new Chart() { Name = "N2", Amount = 30 });
            Source.Add(new Chart() { Name = "N3", Amount = 60 });
            Source.Add(new Chart() { Name = "N4", Amount = 90 });
            WinRTXamlToolkit.Controls.DataVisualization.Charting.Chart c = new WinRTXamlToolkit.Controls.DataVisualization.Charting.Chart {  };
            (ColumnChart.Series[0] as LineSeries).ItemsSource = Source;
        }
    }
}
