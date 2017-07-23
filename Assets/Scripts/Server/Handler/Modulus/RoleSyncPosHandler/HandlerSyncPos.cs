using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

public class HandlerSyncPos : BaseHandler
{
    public HandlerSyncPos(HandlerType type, ServerMain agent) : base(type, agent)
    {
    }
    //0协议号  1 uid   x y  z
    public override void onProcess(string msg, Socket clientPeer)
    {
        //解析
        string[] msgLst = msg.Split(',');
        int uid = int.Parse(msgLst[1]);
        int x = int.Parse(msgLst[2]);
        int y = int.Parse(msgLst[3]);
        int z = int.Parse(msgLst[4]);

        string str = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", ProtocolConst.reqsSyncPos, uid.ToString(), ',', (int)x, ',', (int)y, ',', (int)z);
        byte[] buff = Encoding.UTF8.GetBytes(str);
        this.agent.broadCastMsg(buff, uid);
    }
}

