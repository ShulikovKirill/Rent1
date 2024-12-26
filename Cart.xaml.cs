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
    /// Логика взаимодействия для Cart.xaml
    /// </summary>
    public partial class Cart : Window
    {
        private CartService _cartService = new CartService();
        private decimal _totalPrice = 0;

        public Cart()
        {
            InitializeComponent();
            LoadCart();
        }

        private void LoadCart()
        {
            CartItemsPanel.Children.Clear();
            _totalPrice = 0;

            using ProductApplicationContext db = new ProductApplicationContext();
            {
                
                var cartItems = _cartService.GetCartItems();
                foreach (var cartItem in cartItems)
                {
                    var product = db.Products.FirstOrDefault(p => p.Id == cartItem.ProductId);
                    if (product != null)
                    {
                        var cartCard = CreateCartCard(product, cartItem.Quantity);
                        CartItemsPanel.Children.Add(cartCard);
                        _totalPrice += product.Price * cartItem.Quantity;
                    }
                }

                TotalPriceText.Text = $"{_totalPrice:C}";
            }
        }

        private UIElement CreateCartCard(Product product, int quantity)
        {
            var border = new Border
            {
                Width = 200,
                Height = 300,
                Background = Brushes.White,
                CornerRadius = new CornerRadius(10),
                Margin = new Thickness(10)
            };

            var stackPanel = new StackPanel();

            var image = new Image
            {
                Source = new BitmapImage(new Uri(product.ImageSource, UriKind.RelativeOrAbsolute)),
                Width = 180,
                Height = 180,
                Margin = new Thickness(10, 15, 10, 0),
                HorizontalAlignment = HorizontalAlignment.Center
            };

            var nameBlock = new TextBlock
            {
                Text = product.Name,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(10, 0, 10, 0),
                TextAlignment = TextAlignment.Center
            };

            var quantityBlock = new TextBlock
            {
                Text = $"Количество: {quantity}",
                FontSize = 14,
                Margin = new Thickness(10, 0, 10, 0),
                TextAlignment = TextAlignment.Center
            };

            var priceBlock = new TextBlock
            {
                Text = $"Цена: {product.Price * quantity:C}",
                FontSize = 14,
                Margin = new Thickness(10, 0, 10, 5),
                TextAlignment = TextAlignment.Center
            };

            var removeButton = new Button
            {
                Content = "Удалить",
                Background = new SolidColorBrush(Color.FromRgb(105, 103, 181)),
                Foreground = Brushes.White,
                FontFamily = new FontFamily("Cascadia Mono"),
                Height = 30,
                Margin = new Thickness(10, 0, 10, 5),
                BorderThickness = new Thickness(0),
                Tag = product.Id 
            };
            removeButton.Click += RemoveFromCart_Click;

            stackPanel.Children.Add(image);
            stackPanel.Children.Add(nameBlock);
            stackPanel.Children.Add(quantityBlock);
            stackPanel.Children.Add(priceBlock);
            stackPanel.Children.Add(removeButton);
            border.Child = stackPanel;

            return border;
        }

        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int productId)
            {
                _cartService.RemoveFromCart(productId);
                LoadCart();
            }
        }

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Оформление заказа на сумму {_totalPrice:C}", "Корзина", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}