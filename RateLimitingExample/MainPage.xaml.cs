using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using RateLimiting;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RateLimitingExample
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

        private void UIElement_OnPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            e.Pointer.Debounce(100, DebounceAction);
            e.Pointer.Throttle(100, ThrottleAction);

            var border = new Border()
            {
                Background = new SolidColorBrush(Color.FromArgb(255, 0, 191, 255)),
                Margin = new Thickness(1),
                Width = 1,
                Height = StackPanelRegular.Height
            };

            StackPanelRegular.Children.Add(border);
            ThrottleAction(null);
            DebounceAction(null);

            if (StackPanelRegular.ActualWidth > this.ActualWidth - 400)
            {
                StackPanelRegular.Children.Clear();
                StackPanelDebounce.Children.Clear();
                StackPanelThrottle.Children.Clear();
            }
        }

        private async void ThrottleAction(object o)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                if (o != null && StackPanelThrottle.Children.Count > 0)
                {
                    StackPanelThrottle.Children.RemoveAt(StackPanelThrottle.Children.Count - 1);
                }
                var border = new Border()
                {
                    Background = o == null? new SolidColorBrush(Colors.Transparent) : new SolidColorBrush(Color.FromArgb(255, 255, 219, 147)),
                    Margin = new Thickness(1),
                    Width = 1,
                    Height = StackPanelThrottle.Height
                };

                StackPanelThrottle.Children.Add(border);
            });

        }

        private async void DebounceAction(object o)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                if (o != null && StackPanelDebounce.Children.Count > 0)
                {
                    StackPanelDebounce.Children.RemoveAt(StackPanelDebounce.Children.Count - 1);
                }
                var border = new Border()
                {
                    Background = o == null ? new SolidColorBrush(Colors.Transparent) :  new SolidColorBrush(Color.FromArgb(255, 32, 178, 170)),
                Margin = new Thickness(1),
                    Width = 1,
                    Height = StackPanelDebounce.Height
                };

                StackPanelDebounce.Children.Add(border);
            });
        }
    }
}
