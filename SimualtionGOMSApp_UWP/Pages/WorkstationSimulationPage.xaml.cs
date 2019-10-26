using SimualtionGOMSApp_UWP.ViewModel;
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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace SimualtionGOMSApp_UWP.Pages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class WorkstationSimulationPage : Page
    {
        public WorkstationSimulationPage()
        {
            ViewModel = new WorkstationViewModel();
            this.InitializeComponent();

            MinErrorNumBox.Minimum = MaxErrorNumBox.Minimum = 0;
            MinErrorNumBox.Maximum = MaxErrorNumBox.Maximum = 1;
            StepErrorNumBox.Minimum = 0;
            StepErrorNumBox.Maximum = 100000;

            HandsTimeNumBox.Minimum =
                KeyboardTimeNumBox.Minimum =
                PositionTimeNumBox.Minimum =
                MenthalTimeNumBox.Minimum =
                FixExpectedValueNumBox.Minimum =
                FixLossRatioNumBox.Minimum =
                ManExpectedValueNumBox.Minimum =
                ManLossRatioNumBox.Minimum = 0;

            HandsTimeNumBox.Maximum =
                KeyboardTimeNumBox.Maximum =
                PositionTimeNumBox.Maximum =
                MenthalTimeNumBox.Maximum = 
                FixExpectedValueNumBox.Maximum=
                FixLossRatioNumBox.Maximum =
                ManExpectedValueNumBox.Maximum =
                ManLossRatioNumBox.Maximum = 1000000;

            TimeIntervalNumBox.Minimum = 0;
            TimeIntervalNumBox.Maximum = 1000000;
        }

        WorkstationViewModel ViewModel { get; }

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
            ViewModel.Simulation();
        }
    }
}
