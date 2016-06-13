using UnityEngine;
using System.Collections;
using engine;
using System.Collections.Generic;
using ProtoMsg;
using NET;
public abstract class BaseHandle
{
    public abstract void RegistMsg();
    //public abstract void regist(Dictionary<CMessageID, TcpHandler.DelegateMethod> handles);
    //public abstract void onResult(CMessageID code, ByteBuffer buffer);
}