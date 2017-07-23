using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

//创建角色
public class HandlerRoleCreate : BaseHandler
{

    private RoleDataPool roles;

    public HandlerRoleCreate(HandlerType type, ServerMain agent) : base(type, agent)
    {
    }

    public override void onProcess(string msg, Socket clientPeer)
    {
        base.onProcess(msg, clientPeer);
        RoleData data = roles.players.Dequeue();
        string dt = data.uid.ToString() + ',' + data.tempId.ToString();
        byte[] buff = Encoding.UTF8.GetBytes(ProtocolConst.createRole + dt);
        clientPeer.Send(buff);
        //通知之前的玩家 创建 当前玩家
        this.agent.broadCastMsg(Encoding.UTF8.GetBytes(ProtocolConst.createNetRole + dt));
        //通知当前玩家 创建 之前的所有玩家
        List<PlayerInfo> players = this.agent.getAllPlayer();
        if (players != null && players.Count > 0)
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < players.Count; i++)
            {
                str.Append(ProtocolConst.createNetRole + players[i].roleData.uid.ToString() + ',' + players[i].roleData.tempId.ToString() + ';');
            }
            byte[] netPlayer = Encoding.UTF8.GetBytes(str.ToString());
            clientPeer.Send(netPlayer);
        }
        //添加到玩家列表中
        this.agent.addPlayer(new PlayerInfo(data, clientPeer));
    }

    public override void init()
    {
        roles = new RoleDataPool();
    }

}

