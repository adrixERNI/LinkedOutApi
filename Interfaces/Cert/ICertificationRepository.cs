using System;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Interfaces.Cert;

public interface ICertificationRepository
{
     Task<Certification> CreateAsync(Certification certification);
}
