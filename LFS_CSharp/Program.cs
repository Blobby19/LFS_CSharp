using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Net.NetworkInformation;

namespace LFS_CSharp
{
    static class Program
    {

        static UdpClient client;
        static IPEndPoint endpoint;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            endpoint = new IPEndPoint(IPAddress.Any, 29967);
            client = new UdpClient(endpoint);
            client.BeginReceive(new AsyncCallback(DataReceived), endpoint);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static void DataReceived(IAsyncResult ar)
        {
            byte[] bResp = client.EndReceive(ar, ref endpoint);
            string sResponse = Encoding.UTF8.GetString(bResp);
            Console.WriteLine(sResponse);
            client.Close();
        }
    }

    
}