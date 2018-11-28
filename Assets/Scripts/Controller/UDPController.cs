using UnityEngine;

public class UDPController : MonoBehaviour {
    private UDPReceive UDPListener;
    private UDPSend UDPSender;
    

    void Start()
    {
        string sendIp = "192.168.42.2";
        int sendPort = 6454;
        int receivePort = 11000; 

        UDPListener = new UDPReceive(receivePort);
        UDPSender = new UDPSend(sendIp, sendPort);
        UDPSender.Send("Hi!");        
    }

    void Update()
    {
        foreach (var message in UDPListener.getMessages()) Debug.Log("Message received: " + message);
        

    }

    void OnDestroy()
    {
        UDPListener.Stop();
    }
}
