using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
public class UDPSend {

    private UdpClient udpClient;

    private string senderIp;
    private int senderPort;

    public UDPSend(string sendIp, int sendPort)
    {
        Debug.Log(String.Format("Send to ip {0} and port {1}", sendIp, senderPort));
        this.senderIp = sendIp;
        this.senderPort = sendPort;
        try { udpClient = new UdpClient(); }
        catch (Exception e)
        {
            Debug.Log("Failed to start UDP client connection: " + e.Message);
            return;
        }
    }

    public void Send(string message)
    {
        Debug.Log(String.Format("Send msg to ip:{0} port:{1} msg:{2}", senderIp, senderPort, message));
        IPEndPoint serverEndpoint = new IPEndPoint(IPAddress.Parse(senderIp), senderPort);
        Byte[] sendBytes = Encoding.ASCII.GetBytes(message);
        udpClient.Send(sendBytes, sendBytes.Length, serverEndpoint);
    }

    public void Stop()
    {
        udpClient.Close();
    }

}
