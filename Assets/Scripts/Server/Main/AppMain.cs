using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppMain : MonoBehaviour
{
    public static ServerMain Server;

    private Button serverStartBtn;
    private void Awake()
    {
        Server = new ServerMain();
        GameObject canvas = GameObject.Find("Canvas");
        serverStartBtn = canvas.transform.Find("ServerStart").GetComponent<Button>();
        serverStartBtn.onClick.AddListener(onClick);
    }

    private void onClick()
    {
        Server.startServer();
        debug("服务器启动完毕");
        serverStartBtn.gameObject.SetActive(false);
    }

    public static void debug(string msg)
    {
        Debug.Log(msg);
    }

    private void OnApplicationQuit()
    {
        Server.onAppQuit();
    }

}

