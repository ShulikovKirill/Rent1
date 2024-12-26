using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace test
{
    /// <summary>
    /// Логика взаимодействия для GuestShopView.xaml
    /// </summary>
    public partial class GuestShopView : Window
    {
        private ProductService _productService;
        public GuestShopView()
        {
            InitializeComponent();
            _productService = new ProductService();
            RefreshProducts();
        }

        private void LeaveClick(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();

        }
        private void RefreshProducts()
        {
            ProductsPanel.Children.Clear();

            var products = _productService.GetAllProducts();

            foreach (var product in products)
            {
                var productCard = CreateProductCard(product);
                ProductsPanel.Children.Add(productCard);
            }
        }

        private UIElement CreateProductCard(Product product)
        {

            Border border = new Border
            {
                Width = 200,
                Height = 300,
                Background = Brushes.White,
                CornerRadius = new CornerRadius(10),
                Margin = new Thickness(10)
            };

            StackPanel stackPanel = new StackPanel();

            Image image = new Image
            {
                Source = new BitmapImage(new Uri(product.ImageSource, UriKind.RelativeOrAbsolute)),
                Width = 180,
                Height = 180,
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center
            };

            TextBlock nameBlock = new TextBlock
            {
                Text = product.Name,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(10, 5, 10, 0),
                TextAlignment = TextAlignment.Center
            };

            TextBlock priceBlock = new TextBlock
            {
                Text = $"Цена: {product.Price} руб.",
                FontSize = 14,
                Margin = new Thickness(10, 0, 10, 5),
                TextAlignment = TextAlignment.Center
            };

            Button GoLogButton = new Button
            {
                Content = "Войти в аккаунт",
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6967B5")),
                Foreground = Brushes.White,
                FontFamily = new FontFamily("Cascadia Mono"),
                Height = 30,
                Margin = new Thickness(10),
                BorderThickness = new Thickness(0),
                Tag = product.Id
            };
            GoLogButton.Click += (s, e) =>
            {
                if (MessageBox.Show("Для добавления товара в корзину необходимо войти в аккаунт.\n" +
                    "Войти в аккаунт?", "Вход", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    new MainWindow().Show();
                    this.Close();
                }

            };

            stackPanel.Children.Add(image);
            stackPanel.Children.Add(nameBlock);
            stackPanel.Children.Add(priceBlock);
            stackPanel.Children.Add(GoLogButton);

            border.Child = stackPanel;

            return border;
        }

    }
}
