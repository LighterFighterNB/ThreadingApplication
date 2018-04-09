﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using ThreadingApplication.Elements;
using ThreadingApplication.GUI;
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
        private ViewManager mng;
        private StateView view;
        private ObjectPool objPool;
        private ChartObjectPool chartPool;
        public MainPage()
        {
            this.InitializeComponent();
            mng = new ViewManager(this);
            objPool = new ObjectPool();
            chartPool = new ChartObjectPool();
            view = new LoginView();
            this.Content = view.getView(mng, objPool, chartPool);
            // displayResult();
        }
        public async Task displayResult()
        {
            AlphaApiFactory apif = new AlphaApiFactory();
            AlphaManager am = apif.getApiRequest("daily", "BTC", "EUR");
            await am.setStocks();
        }

        public void update()
        {
            Debug.WriteLine(mng.getCurrentView().ToString());
            this.Content = mng.getCurrentView().getView(mng,objPool,chartPool);
        }

    }
}
