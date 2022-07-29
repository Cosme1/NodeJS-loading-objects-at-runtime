using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dummiesman; // This is the imported plugin for loading obj files
using UnityEngine.Networking;
using WebSocketSharp;
using System.IO;
using System.Text;


public class OBJ_Loader : MonoBehaviour
{
    WebSocket ws; // websocket variable
    string objPath = "C:/Users/cosme/Documents/NodeJS/Tree.obj"; // path to the 3d model
    GameObject loadedObject; // gameobject used to load 3d model
    void Start()
    {
        try
        {
            ws = new WebSocket("ws://localhost:8080"); // calls server on this address
            ws.Connect();
            ws.OnMessage += (sender, e) => {
                Debug.Log("Message recieved from " + ((WebSocket)sender).Url + ", Data: " + e.Data);
            };
        }
        catch (System.Exception)
        {
            
            throw;
        }
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

        var www  = new WWW("https://people.sc.fsu.edu/~jburkardt/data/obj/lamp.obj"); // saves adress

        while (!www.isDone) // This is necessary to make sure the download of the 3d model is finished before loading it into the scene
            System.Threading.Thread.Sleep(1); //small timeout loop

        var textStream = new MemoryStream(Encoding.UTF8.GetBytes(www.text)); // loads the obj data as a string into the textStream variable

        if (loadedObject != null)  //deletes the previous gameObject so they don't multiply when the button is pressed more than once
            Destroy(loadedObject);

        loadedObject = new OBJLoader().Load(textStream); // the plugin loads the obj from the string into a gameObject (it's a bit more complicated for fbx)
    }
}
