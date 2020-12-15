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
        List<Order> orders;
        Order order;
        public ManageOrder(int orderId)
        {
            InitializeComponent();
            orders = new List<Order>();
            loadOrders();
            //loadOrder(orderId);
        }

        private async void loadOrders()
        {
            LocalService service = new LocalService();
            orders = await service.GetAllOrders();
            orders = orders.OrderByDescending(x => x.OrderId).ToList();
            OrderList.Items.Clear();
            foreach (var item in orders)
            {
                ListBoxItem listBoxItem = new ListBoxItem();
                listBoxItem.Content = item.OrderId + " " + item.OrderStatus;
                listBoxItem.Uid = item.OrderId.ToString();
                OrderList.Items.Add(listBoxItem);
            }
        }

        private async void loadOrder(int orderId)
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
            OrderStatusList.Items.Clear();
            foreach (var item in Enum.GetValues(typeof(Order.StatusEnum)).Cast<Order.StatusEnum>())
            {
                OrderStatusList.Items.Add(item);
            }
        }

        private async void UpdateOrderButton_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                if (null == OrderStatusList.SelectedValue)
                {
                    MessageBox.Show("Status not selected");
                    throw new Exception("Not selected status");
                }
                this.order.OrderStatus = OrderStatusList.SelectedValue.ToString();
                LocalService service = new LocalService();
                if (await service.UpdateOrder(this.order))
                {
                    MessageBox.Show("Updated successfully");
                    OrderInfo.Text = "";
                    //OrderInfo.Text = "Updated successfully";
                }
                else
                {
                    MessageBox.Show("Failed to update. Please try again");

                    // OrderInfo.Text = "failed to update. please try again";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update. Please try again");
            }
           
            OrderStatusList.Visibility = Visibility.Hidden;
            UpdateOrderButton.Visibility = Visibility.Hidden;
            loadOrders();
        }

        private void OrderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                ListBox s = (ListBox)sender;
                if (s.HasItems)
                {
                ListBoxItem selectedItem = (ListBoxItem)e.AddedItems[0];
                loadOrder(int.Parse(selectedItem.Uid));
                OrderStatusList.Visibility = Visibility.Visible;
                UpdateOrderButton.Visibility = Visibility.Visible;
            }
          
            
        }
    }
}
