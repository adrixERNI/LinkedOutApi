using LinkedOutApi.Entities;

namespace LinkedOutApi.Interfaces.Cert
{
    public interface ICertificationRepository
    {
        Task<Certification> CreateAsync(Certification certification);

        Task<Certification> DeleteCertificationAsync(int id);

        //Task<Certification> UpdateCertificationAsync(int id, CertificationUpdateDTO cert);
        Task<Certification> UpdateCertificationAsync(int id, Certification cert);

        Task<Certification> GetByIdCertificationAsync(int id);
    }
}


