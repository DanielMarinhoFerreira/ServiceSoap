using System.ServiceModel;

namespace ServiceSoap.Interface
{
    [ServiceContract(Namespace = "http://www.tempuri.org/")]
    public interface IICrud<T1 , T2>
    {
        [OperationContract]
        Task<T1> Create(T2 entity);

        [OperationContract]
        Task<T1> Read(int id);

        [OperationContract]
        Task<IEnumerable<T1>> ReadAll();

        [OperationContract]
        Task<bool> Delete(int id);
    }
}
