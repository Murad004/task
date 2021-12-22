using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

namespace TCPClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Task.Run(() =>
            {

                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                var ipAdress = IPAddress.Parse("10.2.27.49");
                var port = 27001;
                var ep = new IPEndPoint(ipAdress, port);
                try
                {
                    socket.Connect(ep);
                    if (socket.Connected)
                    {
                        while (true)
                        {
                            var message = this.MsgTxtBox.Text;
                            var bytes = Encoding.UTF8.GetBytes(message);
                            socket.Send(bytes);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Can not connect to the server . . .");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Can not connect to the server . . .");
                    MessageBox.Show(ex.Message);
                }
            });
        }


    }
}
