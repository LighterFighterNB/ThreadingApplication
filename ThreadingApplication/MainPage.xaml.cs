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
            // this.InitializeComponent();
            displayResult();
        }
        public async Task displayResult()
        {
            Api api = new Api();
            await api.setCryptoDayly("BTC", "EUR");
            Debug.WriteLine("Here");
            api.getStocks();
            foreach (Stock s in api.getStocks())
            {
                Debug.WriteLine(s.Date + " ");
                foreach (KeyValuePair<string, string> entry in s.Proprieties)
                {
                    Debug.WriteLine(entry.Key + " " + entry.Value);
                }
            }

            await api.setCryptoMontly("BTC", "EUR");
            Debug.WriteLine("Here");
            api.getStocks();
            foreach (Stock s in api.getStocks())
            {
                Debug.WriteLine(s.Date + " ");
                foreach (KeyValuePair<string, string> entry in s.Proprieties)
                {
                    Debug.WriteLine(entry.Key + " " + entry.Value);
                }
            }
        }
    }
}
