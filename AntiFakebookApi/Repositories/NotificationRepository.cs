using AutoMapper;
using AntiFakebookApi.Common;
using AntiFakebookApi.Database;
using AntiFakebookApi.Models;
using AntiFakebookApi.Respositories;

namespace AntiFakebookApi.Repositories
{
    public class NotificationRepository : BaseRespository<Notification>
    {
        private IMapper _mapper;
        public NotificationRepository(ApiOption apiConfig, DatabaseContext databaseContext, IMapper mapper) : base(apiConfig, databaseContext)
        {
            this._mapper = mapper;
        }
    }
}
