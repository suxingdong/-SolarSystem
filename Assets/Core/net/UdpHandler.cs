using System;
using System.Collections;
using System.Collections.Generic;
using ProtoMsg;
using engine;

public class UdpHandler : DefaultSocketListener {
    public UdpHandler(SocketDataConsumer consumer) : base(consumer) { }
    public void handle(ByteBuffer buffer) {
        CMessageID code = (CMessageID)buffer.readShort();
        buffer.code = code;
//         switch (code) {
//             case Code.S_OtherMove:App.sceneManager.handle(buffer); break;
//         }
    }
   
}
