using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArtNet;
using System.Diagnostics;
using System;

public class ArtNetController : MonoBehaviour {

    // send RGB data over network using ARTNET protocol
    private static Engine ArtNetEngine;
    // keep track of how long our ARTNET calls take
    private Stopwatch ArtNetStopWatch;
    private Color color;

    public void Initiate()
    {
        ArtNetEngine = null;
        ArtNetStopWatch = new Stopwatch();

        MainController.pickerShare.onValueChanged.AddListener(color =>
        {
            this.color = color;
            SetRgbSlider(color);
        });
        MainController.pickerShare.CurrentColor = color;
    }

    void OnDestroy()
    {
        ArtNetEngine = null;    
    }

    // records the position of the RGB slider
    public void SetRgbSlider(Color newColor)
    {
        RGB rgb = new RGB();
        rgb.Red = Mathf.FloorToInt(color.r * 255);
        rgb.Green = Mathf.FloorToInt(color.g * 255);
        rgb.Blue = Mathf.FloorToInt(color.b * 255);

        SendDMX(rgb);
    }


    private void SendDMX(RGB rgb)
    {
        print("Color to send is " + rgb.ToString());

        // initialize the engine if needed
        if (ArtNetEngine == null)
        {
            ArtNetEngine = new Engine("ArtNet Engine", "192.168.42.2");
            ArtNetEngine.Start();
        }

        // build DMX data
        byte[] DMXData = new byte[512];
        for (int i = 0; i < DMXData.Length; i += 3)
        {
            DMXData[i] = Convert.ToByte(rgb.Red);
            if (i + 1 < DMXData.Length)
                DMXData[i + 1] = Convert.ToByte(rgb.Green);
            if (i + 2 < DMXData.Length)
                DMXData[i + 2] = Convert.ToByte(rgb.Blue);
        }
        // send DMX data
        ArtNetStopWatch.Restart();
        ArtNetEngine.SendDMX(0, DMXData, DMXData.Length);
        ArtNetStopWatch.Stop();
        print(String.Format("Sending data took: {0}ms",ArtNetStopWatch.ElapsedMilliseconds));
    }
}

public class RGB
{
    private static byte MAX = (byte)255;
    private static byte MIN = (byte)0;

    public RGB()
    {
        Init(MIN, MIN, MIN);
    }

    public RGB(int red, int green, int blue)
    {
        Init(red, green, blue);
    }


    public int Red
    {
        get; set;
    }

    public int Green
    {
        get; set;
    }

    public int Blue
    {
        get; set;
    }

    private void Init(int red, int green, int blue)
    {
        this.Red = red;
        this.Green = green;
        this.Blue = blue;
    }

    override
    public string ToString()
    {
        return String.Format("RGB Color is ({0},{1},{2})",Red,Green,Blue);
    }
}