﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ThreadingApplication.GUI
{
    class ConverterView : StateView
    {
        public ConverterView()
        {

        }

        public override Grid getView(ViewManager viewer, ObjectPool objPool)
        {
            Grid grid;
            Grid converterGrid = new Grid();
            converterGrid.Name = "ConverterGrid";
            converterGrid.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 150, 180, 250));
            createColumns(converterGrid, 2);
            createRows(converterGrid, 25);
            createMenu(converterGrid, viewer, objPool);
            Grid.SetColumn(converterGrid, 0);
            if (objPool.getState("Converter") == null)
            {
                grid = new Grid();
                grid.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 184, 197, 219));
                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(1, GridUnitType.Star);
                grid.ColumnDefinitions.Add(col);

                ColumnDefinition col1 = new ColumnDefinition();
                col1.Width = new GridLength(20, GridUnitType.Star);
                grid.ColumnDefinitions.Add(col1);

                Grid converterView = new Grid();
                createColumns(converterView, 20);
                createRows(converterView, 20);

                TextBlock content = new TextBlock();
                content.Text = "Convertor";
                content.FontSize = 25;
                Grid.SetColumn(content, 3);
                Grid.SetRow(content, 3);
                Grid.SetRowSpan(content, 2);
                Grid.SetColumnSpan(content, 7);
                converterView.Children.Add(content);
                TextBox numberz = new TextBox();
                Grid.SetColumn(numberz, 5);
                Grid.SetRow(numberz, 6);
                numberz.VerticalAlignment = VerticalAlignment.Top;
                Grid.SetColumnSpan(numberz, 5);
                converterView.Children.Add(numberz);

                ComboBox currency1 = new ComboBox();
                currency1.Items.Add("EUR");
                currency1.Items.Add("USD");
                currency1.Items.Add("RON");
                currency1.Items.Add("GBP");
                currency1.Items.Add("SGD");
                currency1.Items.Add("ZAR");
                currency1.Items.Add("CNY");
                currency1.SelectedIndex = 0;

                Grid.SetColumn(currency1, 12);
                Grid.SetRow(currency1, 6);
                Grid.SetRowSpan(currency1, 5);
                Grid.SetColumnSpan(currency1, 5);
                converterView.Children.Add(currency1);

                TextBlock convertedNumbers = new TextBlock();
                convertedNumbers.Text = "0";
                convertedNumbers.VerticalAlignment = VerticalAlignment.Top;
                Grid.SetColumn(convertedNumbers, 5);
                Grid.SetRow(convertedNumbers, 8);
                Grid.SetRowSpan(convertedNumbers, 5);
                Grid.SetColumnSpan(convertedNumbers, 5);
                converterView.Children.Add(convertedNumbers);

                ComboBox currency2 = new ComboBox();
                currency2.Items.Add("EUR");
                currency2.Items.Add("USD");
                currency2.Items.Add("RON");
                currency2.Items.Add("GBP");
                currency2.Items.Add("SGD");
                currency2.Items.Add("ZAR");
                currency2.Items.Add("CNY");
                currency2.SelectedIndex = 0;
                Grid.SetColumn(currency2, 12);
                Grid.SetRow(currency2, 8);
                Grid.SetRowSpan(currency2, 5);
                Grid.SetColumnSpan(currency2, 5);
                converterView.Children.Add(currency2);

                Button converterButton = new Button();
                Grid.SetColumn(converterButton, 8);
                Grid.SetRow(converterButton, 10);
                Grid.SetRowSpan(converterButton, 5);
                Grid.SetColumnSpan(converterButton, 5);
                converterView.Children.Add(converterButton);
                converterButton.Content = "Convert";
                converterButton.VerticalAlignment = VerticalAlignment.Center;
                converterButton.Click += delegate (object sender, RoutedEventArgs e)
                {
                    if (!(!string.IsNullOrWhiteSpace(numberz.Text) && Regex.IsMatch(numberz.Text, @"^[0-9]+.?[0-9]*$")))
                    {
                        createErrorMessage("You can only convert numbers");
                        numberz.Text = "";
                    }
                };
                grid.Children.Add(converterGrid);
                Grid.SetColumn(converterView, 1);
                grid.Children.Add(converterView);
                objPool.setObjectState("Converter", grid);
            }else
            {
                grid = objPool.getState("Converter");
                grid.Children.Remove(converterGrid);
                grid.Children.Add(converterGrid);
            }
            current = grid;
            return current;
        }

        private void converter_Click(object sender, RoutedEventArgs e)
        {
            //if (!(!string.IsNullOrWhiteSpace(numberz.Text) && Regex.IsMatch(numberz.Text, @"^[0-9]+.?[0-9]*$")))
            //{
            //    createErrorMessage("You can only convert numbers");
            //    //numberz.Text = "";
            //}
        }

        private async void createErrorMessage(String message)
        {
            var dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }
    }
}
