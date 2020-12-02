using Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFNav.Service;

namespace WPFNav
{
    /// <summary>
    /// Interaction logic for ManageOrder.xaml
    /// </summary>
    public partial class ManageOrder : Page
    {
        Order order;
        public ManageOrder(int orderId)
        {
            InitializeComponent();
            loadOrder(orderId);
        }

        private async Task loadOrder(int orderId)
        {
            LocalService service = new LocalService();
            order = await service.GetOrder(orderId);
            OrderInfo.Text = order.OrderId.ToString() + ": " + order.OrderStatus;
            foreach (var item in order.OrderLineList)
            {
                OrderInfo.Text += "\n " + item.Product.ProductName + ". Quantity: " + item.Quantity;
            }
            OrderInfo.Text += "\n Ordered: " + order.OrderedTime.ToString();
            OrderInfo.Text += "\n Pick-up time: " + order.PickUpTime.ToString();
            foreach (var item in Enum.GetValues(typeof(Order.StatusEnum)).Cast<Order.StatusEnum>())
            {
                OrderStatusList.Items.Add(item);
            }
        }

        private async void UpdateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            this.order.OrderStatus = OrderStatusList.SelectedValue.ToString();
            LocalService service = new LocalService();
            if (await service.UpdateOrder(this.order))
            {
                OrderInfo.Text = "updated successfully";
            }
            else
            {
                OrderInfo.Text = "failed to update. please try again";
            }
        }
    }
}
