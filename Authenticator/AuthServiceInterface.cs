using System.ServiceModel;

namespace Authenticator
{
    [ServiceContract]
    public interface AuthServiceInterface
    {
        [OperationContract]
        //[FaultContract(typeof(ArgumentOutOfRangeException))]
        void Register(string name, string password, out string result);

        [OperationContract]
        void Login(string name, string password, out string result);

        [OperationContract]
        void Validate(string token, out string result);
        
        [OperationContract]
        void ClearingToken();
    }
}