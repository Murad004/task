using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using task.Command;

namespace task.MVVM.ViewModel
{
    public class MainViewModel:BaseViewModel
    {


        public RelayCommand Run { get; set; }
        public MainViewModel(MainWindow mainWindow)
        {
            Run = new RelayCommand((sender) =>
            {
                Task.Run(() =>
                    {

                        var ipAdress = IPAddress.Parse("10.2.27.49");
                        int port = 27001;
                        IPEndPoint ep = new IPEndPoint(ipAdress, port);
                        using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                        {
                            socket.Bind(ep);
                            socket.Listen(10);
                            while (true)
                            {
                                var client = socket.Accept();
                                MessageBox.Show("Connect true");
                                Task.Run(() =>
                                {
                                    var length = 0;
                                    var bytes = new byte[1024];
                                    do
                                    {
                                        length = client.Receive(bytes);
                                        var message = Encoding.UTF8.GetString(bytes, 0, length);
                                        mainWindow.ImagesListBox.Items.Add(message);
                                    } while (true);
                                });
                            }
                        }
                    });
            });
        }

        
    }
}
