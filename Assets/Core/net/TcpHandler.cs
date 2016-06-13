using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using engine;
using ProtoMsg;
using NET;
public class TcpHandler :DefaultSocketListener {

    public delegate void DelegateMethod(ByteBuffer buff);  //声明了一个Delegate Type
    public DelegateMethod delegateMethod;   //声明了一个Delegate对象

    private static Dictionary<CMessageID, DelegateMethod> handles = new Dictionary<CMessageID, DelegateMethod>();
    public TcpHandler(SocketDataConsumer consumer) : base(consumer) { }

    public static void RegistMsg(CMessageID CMsId, DelegateMethod func)
    {
        handles.Add(CMsId, func);
    }
    public override void onConnected() {
        //App.login.onConnected();
        //App.connect();
    }
    public void init()
    {

    }

    public void handle(ByteBuffer buffer) {
        CMessageID code = (CMessageID)buffer.readShort();
        buffer.code = code;
        UtilLog.Log("收到消息"+code);
        switch (code) {
            case CMessageID.MS_Error:                
                break;
            default:
                handles[code](buffer);
//                 BaseHandle baseHandle = handles[code];
//                 if (baseHandle != null)
//                 {
//                     baseHandle.onResult(code, buffer);
// 			    }
			break;
        }
    }

    private void onError(ByteBuffer buffer) {
        
    }
    
}