using AutoMapper;
using LinkedOutApi.DTOs.User.UserFolder.BatchFolder;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces.Admin;

namespace LinkedOutApi.Services.Admin
{
    public class BatchService : IBatchService
    {
        private readonly IBatchRepository _batchRepository;
        private readonly IMapper _mapper;

        public BatchService(IBatchRepository batchRepository, IMapper mapper)
        {
            _batchRepository = batchRepository;
            _mapper = mapper;
        }

        public async Task<BatchCreateDTO> CreateBatchAsync(BatchCreateDTO batchDTO)
        {
            var mappedBatch = _mapper.Map<Batch>(batchDTO);
            var createdBatch = await _batchRepository.CreateBatchAsync(mappedBatch);

            return _mapper.Map<BatchCreateDTO>(createdBatch);
        }

        public Task<BatchReadDTO> DeleteBatchAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BatchReadDTO> GetBatchByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BatchReadDTO> GetBatchesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BatchUpdateDTO> UpdateBatchAsync(int id, BatchUpdateDTO batchDTO)
        {
            throw new NotImplementedException();
        }
    }
}
