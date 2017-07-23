using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

public class HandlerRoleCastSkill : BaseHandler
{
    public HandlerRoleCastSkill(HandlerType type, ServerMain agent) : base(type, agent)
    {
    }
    //0 协议号 1 uid 2 skillid
    public override void onProcess(string msg, Socket clientPee)
    {
        //解析
        string[] msgLst = msg.Split(',');
        int uid = int.Parse(msgLst[1]);
        int skillId = int.Parse(msgLst[2]);

        //协议号 1 uid 2skillid
        string str = string.Format("{0}{1}{2}{3}", ProtocolConst.onReqsRoleCastSkill, uid.ToString(), ',', (int)skillId);
        byte[] buff = Encoding.UTF8.GetBytes(str);
        this.agent.broadCastMsg(buff, uid);
    }

}

