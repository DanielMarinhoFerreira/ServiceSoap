using ServiceSoap.Dto;
using ServiceSoap.Models;
using System.ServiceModel;

namespace ServiceSoap.Interface
{
    [ServiceContract(Namespace = "http://www.tempuri.org/")]
    public interface IUsuario : IICrud<UsuarioDto, Usuario>
    {
        [OperationContract]
        Task<int> GetIdUsuario(string nome, string login, string Email);

        [OperationContract]
        Task<UsuarioDto> Update(UsuarioDto entity);
    }
}
