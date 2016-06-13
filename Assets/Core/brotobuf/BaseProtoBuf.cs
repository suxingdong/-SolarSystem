using pb = global::Google.ProtocolBuffers;
using pbc = global::Google.ProtocolBuffers.Collections;
using pbd = global::Google.ProtocolBuffers.Descriptors;
using ProtoMsg;
namespace engine
{

    public class iprotocol
    {
        public virtual CMessageID GetMsgID() { return 0; }    // 消息编号,必须唯一且与服务器对应
        public virtual byte[] GetBuff() { return null; }    // 消息编号,必须唯一且与服务器对应
        public virtual void Exec() { }
        public virtual void send() { }
        public virtual void Read() { }
    }

    class BaseProtoBuf<T> : iprotocol where T : new()
    {
        public T data;
        public  BaseProtoBuf()
        {
            data = new T();
        }
        public override void  send()
        {
            ByteBuffer bb = ByteBuffer.create(GetMsgID());
            bb.writeBytes(GetBuff());

            NetManager.I().send(bb);
        }

        public void read()
        {

        }
    }
}
