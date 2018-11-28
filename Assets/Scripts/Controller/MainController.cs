using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

    public ColorPicker picker;
    public GameObject artNetController;
    public GameObject udpController;

    public static ColorPicker pickerShare;
    GameObject cloneArtNet;


    // Use this for initialization
    void Start () {
        cloneArtNet = Instantiate(artNetController);
        pickerShare = picker;
        cloneArtNet.GetComponent<ArtNetController>().Initiate();
        
    }

    private void OnDestroy()
    {
        Destroy(cloneArtNet);
    }
}
