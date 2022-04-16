using System;
using System.Threading.Tasks;

namespace Zetbox.API.Client.ZetboxService {
    
    
    public interface IZetboxService {

        Task<byte[]> SetObjects(System.Guid version, byte[] msg, Zetbox.API.ObjectNotificationRequest[] notificationRequests);

        Task<byte[]> GetObjects(System.Guid version, Zetbox.API.SerializableExpression query);
        
        Task<byte[]> GetListOf(System.Guid version, Zetbox.API.SerializableType type, int ID, string property);
        
        Task<byte[]> FetchRelation(System.Guid version, System.Guid relId, int role, int ID);
        
        Task<System.IO.Stream> GetBlobStream(System.Guid version, int ID);
        
        Task<BlobResponse> SetBlobStream(Zetbox.API.Client.ZetboxService.BlobMessage request);
        Task<Tuple<byte[], byte[]>> InvokeServerMethod(System.Guid version, Zetbox.API.SerializableType type, int ID, string method, Zetbox.API.SerializableType[] parameterTypes, byte[] parameter, byte[] changedObjects, Zetbox.API.ObjectNotificationRequest[] notificationRequests);
    }
    
    public partial class BlobMessage {
        
        public string FileName;
        
        public string MimeType;
        
        public System.Guid Version;
        
        public System.IO.Stream Stream;
        
        public BlobMessage() {
        }
        
        public BlobMessage(string FileName, string MimeType, System.Guid Version, System.IO.Stream Stream) {
            this.FileName = FileName;
            this.MimeType = MimeType;
            this.Version = Version;
            this.Stream = Stream;
        }
    }
    
    public partial class BlobResponse {
        
        public int ID;
        
        public System.IO.Stream BlobInstance;
        
        public BlobResponse() {
        }
        
        public BlobResponse(int ID, System.IO.Stream BlobInstance) {
            this.ID = ID;
            this.BlobInstance = BlobInstance;
        }
    }
}
