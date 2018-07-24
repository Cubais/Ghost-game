﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace GhostGameServer
{
  class Program
  {
    static void Main(string[] args)
    {
      var serverManager = new ServerManager(Dns.GetHostEntry("localhost").AddressList[0],8080);
      serverManager.RunServer();
    }
  }

  class ServerManager
  {
    private IPAddress ip { get; set; }
    private int port { get; set; }
    private TcpListener server { get; set; }
    private TcpClient client { get; set; } = default(TcpClient);

    private List<TcpClient> users = new List<TcpClient>();
   
    public ServerManager(IPAddress ip, int port)
    {
      this.ip = ip;
      this.port = port;
      server = new TcpListener(ip, port);
    }

    public void RunServer()
    {
      try
      {
        server.Start();
        Console.WriteLine("Server started...");
      }
      catch(Exception ex)
      {
        Console.WriteLine(ex.ToString());
        
      }

      while (true)
      {
        client = server.AcceptTcpClient();

        byte[] recievedBuffer = new byte[100];
        NetworkStream stream = client.GetStream();

        stream.Read(recievedBuffer, 0, recievedBuffer.Length);

        StringBuilder message = new StringBuilder();

        foreach (byte b in recievedBuffer)
        {
          if (b.Equals(00))
          {
            break;
          }
          else
          {
            message.Append(Convert.ToChar(b));
          }
        }

        if (!users.Contains(client))
        {
          users.Add(client);
          Console.WriteLine(message.ToString() + " has just connected...");
        }
        else
        {
          Console.WriteLine("ALready Connected !!!");
        }

        
      }
    }
  }

}
