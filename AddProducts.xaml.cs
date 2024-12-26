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
    /// Логика взаимодействия для AddProducts.xaml
    /// </summary>
    public partial class AddProducts : Window
    {
        public Product NewProduct { get; private set; }

        public AddProducts()
        {
            InitializeComponent();
        }
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProductNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(ProductPriceTextBox.Text) ||
                string.IsNullOrWhiteSpace(ProductImagePathTextBox.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены.");
                return;
            }

            if (!decimal.TryParse(ProductPriceTextBox.Text, out var price))
            {
                MessageBox.Show("Некорректная цена.");
                return;
            }

            NewProduct = new Product
            {
                Name = ProductNameTextBox.Text,
                Price = price,
                ImageSource = ProductImagePathTextBox.Text
            };

            DialogResult = true;
            Close();
        }
    }
}
