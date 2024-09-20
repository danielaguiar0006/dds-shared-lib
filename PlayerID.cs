using System.Net;

namespace dds_shared_lib
{
    public class PlayerID
    {
        public string m_HashedId { get; private set; }

        public PlayerID(string hashedId)
        {
            m_HashedId = hashedId;
        }

        public PlayerID(IPEndPoint endPoint)
        {
            m_HashedId = GetHashedId(endPoint);
        }

        public void SetHashedId(IPEndPoint endPoint)
        {
            m_HashedId = GetHashedId(endPoint);
        }

        // Input: IPEndPoint
        // Output: Hashed ID as a string
        public static string GetHashedId(IPEndPoint endPoint) // TODO: This is called a lot, so we might want to cache the results...
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(endPoint.ToString()));
                string base64Hash = Convert.ToBase64String(hashBytes);
                return base64Hash.Substring(0, 16);
            }
        }

        // NOTE: Very important to override Equals and GetHashCode
        // when using PlayerIDs as keys in dictionaries
        public override bool Equals(object obj)
        {
            if (obj is PlayerID other)
            {
                return m_HashedId.Equals(other.m_HashedId);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return m_HashedId.GetHashCode();
        }

        public override string ToString()
        {
            return m_HashedId;
        }


    }
}
