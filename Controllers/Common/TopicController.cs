using LinkedOutApi.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;

namespace LinkedOutApi.Controllers.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        //[HttpPost("/api/admin/batch/{id}/topic")]
    }
}
