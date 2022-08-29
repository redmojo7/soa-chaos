
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PublishingConsole
{
    [ServiceContract]
    public interface PublishServiceInterface
    {
        [OperationContract]
        void Registration(in string UserName, in string Password);

        [OperationContract]
        void Login(in string UserName, in string Password);

        [OperationContract]
        void PublishService(in string Name, in string Description, in Uri ApiEndpoint, in int NumberOfOperands, in string OperandType);

        [OperationContract]
        void UnPublishService(in Uri uri);
    }
}
