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
    /// Логика взаимодействия для UserShopView.xaml
    /// </summary>
    public partial class UserShopView : Window
    {
        private readonly ProductService _productService;

        public UserShopView()
        {
            InitializeComponent();
            _productService = new ProductService();
            RefreshProducts();
        }

        private void RefreshProducts()
        {
            ProductsPanel.Children.Clear();

           
            List<Product> products = _productService.GetAllProducts();

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
            Button buyButton = new Button
            {
                Content = "Арендовать(1день)",
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6967B5")),
                Foreground = Brushes.White,
                FontFamily = new FontFamily("Cascadia Mono"),
                Height = 30,
                Margin = new Thickness(10),
                BorderThickness = new Thickness(0),
                Tag = product.Id
            };

            buyButton.Click += (s, e) =>
            {
                
                int productId = product.Id;
                int quantity = 1;

                
                using (var db = new ProductApplicationContext())
                {
                    
                    var existingCartItem = db.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
                    if (existingCartItem != null)
                    {
                        
                        existingCartItem.Quantity += quantity;
                    }
                    else
                    {
                       
                        var cartItem = new CartItem
                        {
                            ProductId = productId,
                            Quantity = quantity
                        };
                        db.CartItems.Add(cartItem);
                    }

                    
                    db.SaveChanges();
                }

               
                MessageBox.Show($"Товар '{product.Name}' добавлен в корзину!");
            };


            stackPanel.Children.Add(image);
            stackPanel.Children.Add(nameBlock);
            stackPanel.Children.Add(priceBlock);
            stackPanel.Children.Add(buyButton);

            border.Child = stackPanel;

            return border;
        }

        private void LeaveClick(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void OpenCart_Click(object sender, RoutedEventArgs e)
        {
            var cart = new Cart();
            cart.ShowDialog();
        }
    }
}