namespace dds_shared_lib
{
    public class GamePacket : Packet
    {
        public enum OpCode
        {
            GameStart,
            GameEnd,
            PlayerJoin,
            PlayerLeave
        }
        public OpCode m_OpCode;

        public GamePacket()
        {
            m_PacketType = PacketType.GamePacket;
        }
        public GamePacket(OpCode opCode)
        {
            m_PacketType = PacketType.GamePacket;
            m_OpCode = opCode;
        }

        // Serialize the packet data into a byte array
        // NOTE: Make sure to prefix the protocol ID before sending the packet
        public override void Write(BinaryWriter writer)
        {
            writer.Write((byte)m_PacketType);
            writer.Write((byte)m_OpCode);

            if (m_Data != null)
            {
                writer.Write(m_Data.Length);
                writer.Write(m_Data);
            }
            else
            {
                writer.Write(0);
            }
        }

        // Deserialize the packet data from a byte array
        public override void Read(BinaryReader reader)
        {
            // Reading the packet ProtocolID and PacketType are done in the PacketManager.cs
            m_OpCode = (OpCode)reader.ReadByte();
            int dataLength = reader.ReadInt32();
            if (dataLength > 0)
            {
                m_Data = reader.ReadBytes(dataLength);
            }
            else
            {
                m_Data = null;
            }
        }
    }
}
