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

        public async Task<BatchReadDTO> CreateBatchAsync(BatchCreateDTO batchDTO)
        {
            var mappedBatch = _mapper.Map<Batch>(batchDTO);
            var createdBatch = await _batchRepository.CreateBatchAsync(mappedBatch);

            return _mapper.Map<BatchReadDTO>(createdBatch);
        }

        public async Task<BatchReadDTO> DeleteBatchAsync(int id)
        {
            var batchToDelete = await _batchRepository.DeleteBatchAsync(id);
            var mappedBatch = _mapper.Map<BatchReadDTO>(batchToDelete);
            return mappedBatch;
        }

        public async Task<BatchReadUserTopicDTO> GetBatchByIdAsync(int id)
        {
            var batch = await _batchRepository.GetBatchByIdAsync(id);
            var mappedBatch = _mapper.Map<BatchReadUserTopicDTO>(batch);
            return mappedBatch;

        }

        public async Task<ICollection<BatchReadUserTopicDTO>> GetBatchesAsync()
        {
            var batches = await _batchRepository.GetBatchesAsync();
            if (batches == null)
            {
                return null;
            }
            var mappedBatches = _mapper.Map<ICollection<BatchReadUserTopicDTO>>(batches);
            return mappedBatches;
        }

        public async Task<BatchReadDTO> UpdateBatchAsync(int id, BatchReadDTO batchDTO)
        {
            if (string.IsNullOrWhiteSpace(batchDTO.Name) || string.IsNullOrWhiteSpace(batchDTO.Status))
            {
                throw new ArgumentException("Batch Name and/or Status Can't Be Empty");
            }
            var mappedBatch = _mapper.Map<Batch>(batchDTO);
            var batch = await _batchRepository.UpdateBatchAsync(id, mappedBatch);
            
            return _mapper.Map<BatchReadDTO>(batch);
        }
    }
}
