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

using System.Collections.ObjectModel;
using SimualtionGOMSApp_UWP.ViewModel;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace SimualtionGOMSApp_UWP
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            ViewModel = new MainPageViewModel();
            this.InitializeComponent();

            MinErrorNumBox.Minimum = MaxErrorNumBox.Minimum = 0;
            MinErrorNumBox.Maximum = MaxErrorNumBox.Maximum = 1;
            StepErrorNumBox.Minimum = 0;
            StepErrorNumBox.Maximum = 100000;

            HandsTimeNumBox.Minimum = 
                KeyboardTimeNumBox.Minimum = 
                PositionTimeNumBox.Minimum = 
                MenthalTimeNumBox.Minimum = 0;

            HandsTimeNumBox.Maximum =
                KeyboardTimeNumBox.Maximum =
                PositionTimeNumBox.Maximum =
                MenthalTimeNumBox.Maximum = 1000000;
        }

        MainPageViewModel ViewModel { get; }

        private void AddNode_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.OuterNodes.Add(new Models.OuterNodeModel());
        }

        private void RemoveNode_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedOtherNodeIndex < 0)
                return;

            ViewModel.OuterNodes.RemoveAt(ViewModel.SelectedOtherNodeIndex);
        }

        private void AddMapping_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.NodeMappings.Add(new Models.NodeMappingModel());
        }

        private void RemoveMapping_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedNodeMapIndex < 0)
                return;

            ViewModel.NodeMappings.RemoveAt(ViewModel.SelectedNodeMapIndex);
        }

        private void Simulate_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SimulationRange();
        }

        private void NumberFieldChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            var selectionPos = sender.SelectionStart;
            if (double.TryParse(sender.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var result))
            {
                sender.Text = result.ToString();
            }
            else
            {
                sender.Text = "0";
            }
            sender.SelectionStart = selectionPos;
        }


    }
}
