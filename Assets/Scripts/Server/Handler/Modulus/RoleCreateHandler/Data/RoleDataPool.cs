using System;
using System.Collections.Generic;

public struct RoleData
{
    public int tempId;
    public int uid;
    public string pos;

    public RoleData(int uid, int tempId)
    {
        this.uid = uid;
        this.tempId = tempId;
        this.pos = "0,0,0";
    }

}

public class RoleDataPool
{
    public Queue<RoleData> players = new Queue<RoleData>();

    public RoleDataPool()
    {
        RoleData dt1 = new RoleData(200001, 1008611);
        RoleData dt2 = new RoleData(200002, 1008611);
        RoleData dt3 = new RoleData(200003, 1008611);
        RoleData dt4 = new RoleData(200004, 1008611);
        RoleData dt5 = new RoleData(200005, 1008611);
        RoleData dt6 = new RoleData(200006, 1008611);
        RoleData dt7 = new RoleData(200007, 1008611);
        RoleData dt8 = new RoleData(200008, 1008611);
        players.Enqueue(dt1); players.Enqueue(dt2); players.Enqueue(dt3);
        players.Enqueue(dt4); players.Enqueue(dt5); players.Enqueue(dt6);
        players.Enqueue(dt7); players.Enqueue(dt8);
    }



}

