using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;
using System.Text;

public class PlayerInfo
{
    public RoleData roleData;
    public Socket clientPeer;

    public PlayerInfo(RoleData data, Socket socket)
    {
        roleData = data;
        clientPeer = socket;
    }

}

public class ServerMain
{
    private Dictionary<int, PlayerInfo> dictPlayer = new Dictionary<int, PlayerInfo>();
    private Dictionary<HandlerType, BaseHandler> handlerMap = new Dictionary<HandlerType, BaseHandler>();
    private Socket serverSocket = null;

    private string ip = "192.168.1.100";
    private int port = 10000;
    public void startServer()
    {
        addHandler();
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPAddress address = IPAddress.Parse(ip);
        IPEndPoint point = new IPEndPoint(address, port);
        serverSocket.Bind(point);
        serverSocket.Listen(10);
        Thread th = new Thread(ServerAccept);
        th.IsBackground = true;
        th.Start(serverSocket);
    }


    //监听客户端链接
    private void ServerAccept(object socket)
    {
        Socket serverSocket = socket as Socket;
        while (true)
        {
            Socket clientPeer = serverSocket.Accept();
            Thread th = new Thread(ReceiveClient);
            th.IsBackground = true;
            th.Start(clientPeer);
        }
    }

    //接受客户端消息
    private void ReceiveClient(object socket)
    {
        AppMain.debug("有客户端连接");
        Socket clientPeer = socket as Socket;
        byte[] buff = new byte[1024 * 1024];
        while (true)
        {
            int n = clientPeer.Receive(buff);
            if (n == 0)
            {
                break;
            }
            string str = Encoding.UTF8.GetString(buff, 0, n);
            checkClientMsg(str, clientPeer);

        }
    }

    //解析客户端消息并给handler处理
    private void checkClientMsg(string msg, Socket clientPeer)
    {
        string[] lst = msg.Split(',');
        //0协议号        //1 playerid 没有是-1   2 3参数
        int proId = int.Parse(lst[0]);
        HandlerType type = (HandlerType)proId;
        if (handlerMap.ContainsKey(type))
        {
            handlerMap[type].onProcess(msg, clientPeer);
        }
    }

    //add handler
    private void addHandler()
    {
        handlerMap.Add(HandlerType.roleCreate, new HandlerRoleCreate(HandlerType.roleCreate, this));

        handlerMap.Add(HandlerType.syncPos, new HandlerSyncPos(HandlerType.syncPos, this));
        handlerMap.Add(HandlerType.roleCastSkill, new HandlerRoleCastSkill(HandlerType.roleCastSkill, this));

    }

    //common func 
    public List<PlayerInfo> getAllPlayer()
    {
        return new List<PlayerInfo>(dictPlayer.Values);
    }

    public void sendMsg(byte[] buff, int id)
    {
        if (dictPlayer.ContainsKey(id))
        {
            dictPlayer[id].clientPeer.Send(buff);
        }
    }

    public void broadCastMsg(byte[] buff)
    {
        AppMain.debug("广播消息: " + Encoding.UTF8.GetString(buff));
        foreach (var item in dictPlayer)
        {
            item.Value.clientPeer.Send(buff);
        }
    }

    public void broadCastMsg(byte[] buff, int selfId)
    {
        foreach (var item in dictPlayer)
        {
            if (item.Value.roleData.uid != selfId)
                item.Value.clientPeer.Send(buff);
        }
    }

    public void addPlayer(PlayerInfo info)
    {
        if (!dictPlayer.ContainsKey(info.roleData.uid))
            this.dictPlayer.Add(info.roleData.uid, info);
    }

    public void onAppQuit()
    {
        this.serverSocket.Close();
    }

}
