﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

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

  public class ServerManager
  {
    private IPAddress ip { get; set; }
    private int port { get; set; }
    private TcpListener server { get; set; }
    private TcpClient client { get; set; } = default(TcpClient);
    private Hashtable clientsList = new Hashtable();
       
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
        string clientName = message.ToString();
        if (!clientsList.Contains(clientName))
        {
          //TODO Hashovanie users
          //adding new client to list
          clientsList.Add(clientName, client); 

          //send all clients that new client has been connected
          Broadcast("User " + clientName, clientName);
          Console.WriteLine(clientName + "connected");

          HandleClinet clientHandler = new HandleClinet();

          clientHandler.startClient(client, clientName, clientsList, this);
          
        }
        else
        {
          Console.WriteLine("Already Connected !!!");
        }

        
      }

      client.Close();
      server.Stop();

    }
    public void Broadcast(string message, string userName)
    {
      try
      {
        foreach (DictionaryEntry item in clientsList)
        {

          TcpClient broadcastSocket;

          broadcastSocket = (TcpClient)item.Value;

          if (!broadcastSocket.Connected)
            continue;          

          NetworkStream broadcastStream = broadcastSocket.GetStream();

          byte[] broadcastBytes = null;

          broadcastBytes = Encoding.ASCII.GetBytes(userName + ": " + message.ToString());

          broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);

          broadcastStream.Flush();


        }
      }
      catch
      {
        Console.WriteLine("Unable to broadcast");
      }

    }
    public void RemoveClient(string clientName)
    {
      clientsList.Remove(clientName);
      Broadcast("has just disconected",clientName);
      Console.WriteLine(clientName + " disconnected");
    }
  }

  public class HandleClinet : IDisposable
  {
    TcpClient clientSocket;

    string clNo;

    Hashtable clientsList;

    ServerManager serverManager;

    bool connected = false;

    public void Dispose()
    {      
      serverManager.RemoveClient(clNo);
      this.clientSocket = null;
      this.clientsList = null;
      this.serverManager = null;
      connected = false;
      
    }

    public void startClient(TcpClient inClientSocket, string clineNo, Hashtable cList, ServerManager serverManager)
    {
      this.clientSocket = inClientSocket;
      this.clNo = clineNo;
      this.clientsList = cList;
      this.serverManager = serverManager;
      Thread ctThread = new Thread(doChat);
      ctThread.Start();
      connected = true;

    }
    private void doChat()
    {
      byte[] bytesFrom = new byte[10025];
      string dataFromClient = null;  
      
      while (clientSocket.Connected)
      {
        try
        {
          NetworkStream networkStream = clientSocket.GetStream();

          networkStream.Read(bytesFrom, 0, 255);

          dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);

          dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf(" "));

          Console.WriteLine("From client - " + clNo + " : " + dataFromClient);

          serverManager.Broadcast(dataFromClient, clNo);

        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.ToString());
        }
        finally
        {
          this.Dispose();          
        }

      }//end while

    }//end doChat

  } //end class handleClinet

}
