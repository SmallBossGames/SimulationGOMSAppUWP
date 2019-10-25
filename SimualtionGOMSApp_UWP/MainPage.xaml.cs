using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

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
            this.InitializeComponent();
        }

        private readonly List<(string Tag, Type Page)> pages = new List<(string Tag, Type Page)>();

        private void MainNavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var navItemTag = args.InvokedItemContainer.Tag.ToString();
            NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
        }

        private void NavView_Navigate(string navItemTag, NavigationTransitionInfo transitionInfo)
        {
            var item = pages.FirstOrDefault(p => p.Tag.Equals(navItemTag, StringComparison.Ordinal));
            var page = item.Page;
            var preNavPageType = ContentFrame.CurrentSourcePageType;

            if (!(page is null) && !Equals(preNavPageType, page))
            {
                ContentFrame.Navigate(page, null, transitionInfo);
            }
        }

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            MainNavView.IsBackEnabled = ContentFrame.CanGoBack;

            var item = pages.FirstOrDefault(p => p.Page == e.SourcePageType);

            MainNavView.SelectedItem = MainNavView.MenuItems
                .OfType<NavigationViewItem>()
                .First(n => n.Tag.Equals(item.Tag));

            MainNavView.Header =
                ((NavigationViewItem)MainNavView.SelectedItem)?.Content?.ToString();

        }

        private void MainNavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) 
            => On_BackRequested();

        private bool On_BackRequested()
        {
            if (!ContentFrame.CanGoBack)
                return false;

            if (MainNavView.IsPaneOpen &&
                (MainNavView.DisplayMode == NavigationViewDisplayMode.Compact ||
                 MainNavView.DisplayMode == NavigationViewDisplayMode.Minimal))
                return false;

            ContentFrame.GoBack();
            return true;
        }

        private void MainNavView_Loaded(object sender, RoutedEventArgs e)
        {
            pages.Add(("lab1", typeof(Pages.GOMSSimulationPage)));
            pages.Add(("lab2", typeof(Pages.WorkstationSimulationPage)));


            ContentFrame.Navigated += On_Navigated;
            MainNavView.SelectedItem = MainNavView.MenuItems[0];
            NavView_Navigate("lab1", new EntranceNavigationTransitionInfo());

            var goBack = new KeyboardAccelerator { Key = VirtualKey.GoBack };
            goBack.Invoked += BackInvoked;
            this.KeyboardAccelerators.Add(goBack);

            var altLeft = new KeyboardAccelerator
            {
                Key = VirtualKey.Left,
                Modifiers = VirtualKeyModifiers.Menu
            };
            altLeft.Invoked += BackInvoked;
            this.KeyboardAccelerators.Add(altLeft);
        }

        private void BackInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            On_BackRequested();
            args.Handled = true;
        }
    }
}
