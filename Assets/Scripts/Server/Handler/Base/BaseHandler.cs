using System;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public enum HandlerType
{


    syncPos = 9000,
    roleCastSkill,

    roleCreate = 10000,

}

public class BaseHandler
{
    protected HandlerType HType;
    protected ServerMain agent;

    public BaseHandler(HandlerType type, ServerMain agent)
    {
        this.HType = type;
        this.agent = agent;
        init();
    }

    public virtual void init()
    {

    }

    public virtual void onProcess(string msg, Socket clientPee)
    {
    }

}

