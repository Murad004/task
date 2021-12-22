using Client.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.MVVM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public RelayCommand Send { get; set; }
        public RelayCommand Dialog { get; set; }

        public MainViewModel(MainWindow mainWindow)
        {
            Dialog = new RelayCommand((sender) =>
              {

              });
            Send = new RelayCommand((sender) =>
              {
                  var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                  var ipAdress = IPAddress.Parse("10.2.27.49");
                  var port = 27002;
                  var ep = new IPEndPoint(ipAdress, port);
                  try
                  {
                      socket.Connect(ep);
                      if (socket.Connected)
                      {
                          while (true)
                          {
                              var message = mainWindow.MsgTxtBox.Text;
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
