using LinkedOutApi.Entities;

namespace LinkedOutApi.Interfaces.Admin
{
    public interface IAdminRepository
    {
        Task<Entities.Admin> GetAdminById(int id);
        Task<Entities.Admin> UpdateAdmin(Entities.Admin entity);
        Task<Entities.Admin> DeleteAdmin(int id);
        Task<Entities.Admin> CreateAdmin(Entities.Admin entity);

    }
}
