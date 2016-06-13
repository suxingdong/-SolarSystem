using UnityEngine;
using System.Collections;
using System;
using NET;
namespace engine {

    public class NetManager:BaseMgr<NetManager> {

        public const int BUFFER_MAX = 2048;  // 每个包最大长度
        public const int HEAD_SIZE = 8; //头长度
        const int SEND_PROTOCOL_COUNT = BUFFER_MAX - HEAD_SIZE;

        private SocketClient socket;
        private TcpHandler tcpHandler;
        private SocketDataConsumer tcpConsumer, udpConsumer;
        //private DefaultSocketListener listener;
        private IMessageHandler handler;

        private ByteBuffer buffer;

        public NetManager()
        {
            buffer = new ByteBuffer();
            tcpConsumer = new SocketDataConsumer();
            socket = new SocketClient();
            tcpHandler = new TcpHandler(tcpConsumer);
            socket.init(Configuration.tcpHost, Configuration.tcpPort, tcpHandler);
            //listener = new DefaultSocketListener(tcpConsumer);
            
          
        }
//         public NetManager(IMessageHandler handler) {
//             this.handler = handler;
//             tcpConsumer = new SocketDataConsumer();
//             listener = new DefaultSocketListener(tcpConsumer);
//             buffer = new ByteBuffer();
//         }

        public void connect(string host, int port) {
             if (socket == null) socket = new SocketClient();
                socket.init(host, port, tcpHandler);
             Debug.Log("开始连接");

        }
        //disconnect manually
        public void disconnect() {
            socket.disconnect();
        }

        public void send(ByteBuffer data)
        {
            UtilLog.Log(">>>>消息发送:" + data.ToString());
            socket.send(data);
            //Singleton<NetIoClientSingle>.Instance.Send(data);
        }

        public void updata()
        {
            tcpHandler.consume();
            object[] data = tcpConsumer.data;
            for (int i = 0; i < tcpConsumer.size; i++)
            {
                buffer.reset((byte[])data[i]);
                try
                {
                    tcpHandler.handle(buffer);
                }
                catch (Exception ex)
                {
                    UtilLog.Log("消息未注册");
                    Debug.LogError(ex);
                }

            }
            tcpConsumer.size = 0;

        }
    
    }
}
