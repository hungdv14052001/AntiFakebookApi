using AutoMapper;
using AntiFakebookApi.Common;
using AntiFakebookApi.Database;
using AntiFakebookApi.Dto;
using AntiFakebookApi.Request;
using AntiFakebookApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AntiFakebookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeySearchController : BaseApiController<KeySearchController>
    {
        private readonly KeySearchService _keySearchService;
        public KeySearchController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost)
        {
            _keySearchService = new KeySearchService(apiConfig, databaseContext, mapper, webHost);
        }

        [HttpGet]
        [Route("get_saved_search")]
        public MessageData GetSavedSearch(int? index, int? count)
        {
            try
            {
                var res = _keySearchService.GetSavedSearch(AccountId);
                return new MessageData(res);
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpDelete]
        [Route("del_saved_search")]
        public MessageData DelSavedSearch(int search_id, int all)
        {
            try
            {
                var res = _keySearchService.DelSavedSearch(AccountId, search_id, all);
                return new MessageData(res);
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
