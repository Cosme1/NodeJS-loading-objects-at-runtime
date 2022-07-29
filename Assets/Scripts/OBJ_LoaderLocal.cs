using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dummiesman;
using UnityEngine.Networking;
using WebSocketSharp;
using System.IO;
using System.Text;


public class OBJ_LoaderLocal : MonoBehaviour
{
    WebSocket ws; // websocket variable
    string objPath = "C:/Users/cosme/Documents/NodeJS/Tree.obj"; // path to the 3d model
    GameObject loadedObject; // gameobject used to load 3d model
    void Start()
    {
        ws = new WebSocket("ws://localhost:8080"); // calls server on this address
        ws.Connect();
        ws.OnMessage += (sender, e) => {
            Debug.Log("Message recieved from " + ((WebSocket)sender).Url + ", Data: " + e.Data);
        };
        
        
        
    }

    // the Download() method is called during a button press
    public void Download(){
        try
        {
            ws.Send("test");
        }
        catch (System.Exception)
        {
            
            Debug.Log("No connection");
        }
        if (loadedObject != null)  //deletes the previous gameObject so they don't multiply when the button is pressed more than once
            Destroy(loadedObject);
         

        loadedObject = new OBJLoader().Load(objPath); // loads 3d model from path into gameobject through Dummiesman plugin
    }
}
