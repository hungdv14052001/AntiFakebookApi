using AutoMapper;
using AntiFakebookApi.Common;
using AntiFakebookApi.Database;
using AntiFakebookApi.Repositories;
using AntiFakebookApi.Request;
using AntiFakebookApi.Models;
using AntiFakebookApi.Dto;

namespace AntiFakebookApi.Services
{
    public class KeySearchService
    {
        private readonly KeySearchRepository _keySearchRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public KeySearchService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _keySearchRepository = new KeySearchRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        public object GetSavedSearch(int accountId)
        {
            try
            {
                return _keySearchRepository.FindByCondition(row => row.AccountId == accountId).OrderBy(row => row.UpdatedDate).ToList().Select(row => row.GetString()).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object DelSavedSearch(int accountId, int search_id, int all)
        {
            try
            {
                if (all == 1)
                {
                    var keySearchList = _keySearchRepository.FindByCondition(row => row.AccountId == accountId).ToList();
                    foreach (var keySearch in keySearchList)
                    {
                        _keySearchRepository.DeleteByEntity(keySearch);
                    }
                    _keySearchRepository.SaveChange();
                }
                else if (all == 0)
                {
                    var keySearch = _keySearchRepository.FindOrFail(search_id);
                    if (keySearch == null)
                    {
                        throw new Exception("save search dont exist");
                    }
                    _keySearchRepository.DeleteByEntity(keySearch);
                    _keySearchRepository.SaveChange();
                }
                return "true";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
