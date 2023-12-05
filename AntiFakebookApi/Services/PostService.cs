using AutoMapper;
using AntiFakebookApi.Common;
using AntiFakebookApi.Database;
using AntiFakebookApi.Repositories;
using AntiFakebookApi.Request;
using AntiFakebookApi.Models;
using AntiFakebookApi.Dto;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace AntiFakebookApi.Services
{
    public class PostService
    {
        private readonly PostRepository _postRepository;
        private readonly AccountRepository _accountRepository;
        private readonly KeySearchRepository _keySearchRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public PostService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _postRepository = new PostRepository(apiOption, databaseContext, mapper);
            _accountRepository = new AccountRepository(apiOption, databaseContext, mapper);
            _keySearchRepository = new KeySearchRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        public object AddPost(int accountId, AddPostRequest request)
        {
            try
            {
                var account = _accountRepository.FindOrFail(accountId);
                if (account.Coins < 1) 
                {
                    throw new Exception("Not enough coin");
                }
                var post = new Post()
                {
                    AccountId = accountId,
                    Described = request.Described,
                    Image = "",
                    Video = ""
                };
                if (request.Image != null)
                {
                    var date = DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm");
                    using (FileStream fileStream = File.Create(_webHost.WebRootPath + "\\posts\\images\\" + date + request.Image.FileName))
                    {
                        request.Image.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    post.Image = "posts/images/" + date + request.Image.FileName;
                }
                if (request.Video != null)
                {
                    var date = DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm");
                    using (FileStream fileStream = File.Create(_webHost.WebRootPath + "\\posts\\videos\\" + date + request.Video.FileName))
                    {
                        request.Video.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    post.Video = "posts/videos/" + date + request.Video.FileName;
                }
                _postRepository.Create(post);
                _postRepository.SaveChange();

                // reduce coin

                account.Coins -= 1;
                account.UpdatedDate = DateTime.Now;
                _accountRepository.UpdateByEntity(account);
                _accountRepository.SaveChange();
                return new
                {
                    id = post.Id.ToString(),
                    url = "posts/"+post.Id,
                    coins = account.Coins.ToString(),
                };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public object EditPost(int accountId, EditPostRequest request)
        {
            try
            {
                var account = _accountRepository.FindOrFail(accountId);
                if (account.Coins < 1)
                {
                    throw new Exception("Not enough coin");
                }
                var post = _postRepository.FindOrFail(request.Id);
                if(post == null)
                {
                    throw new Exception("Not enough coin");
                }
                if (request.Image != null)
                {
                    var date = DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm");
                    using (FileStream fileStream = File.Create(_webHost.WebRootPath + "\\posts\\images\\" + date + request.Image.FileName))
                    {
                        request.Image.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    post.Image = "posts/images/" + date + request.Image.FileName;
                }
                if (request.Video != null)
                {
                    var date = DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm");
                    using (FileStream fileStream = File.Create(_webHost.WebRootPath + "\\posts\\videos\\" + date + request.Video.FileName))
                    {
                        request.Video.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    post.Video = "posts/videos/" + date + request.Video.FileName;
                }
                _postRepository.UpdateByEntity(post);
                _postRepository.SaveChange();

                // reduce coin
                account.Coins -= 1;
                account.UpdatedDate = DateTime.Now;
                _accountRepository.UpdateByEntity(account);
                _accountRepository.SaveChange();
                return new
                {
                    coins = account.Coins.ToString(),
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetPost(int id)
        {
            try
            {
                var post = _postRepository.FindOrFail(id);
                if(post == null)
                {
                    throw new Exception("id invalid!");
                }
                var account = _accountRepository.FindOrFail(post.AccountId);
                return new
                {
                    post = post.GetString(),
                    author = account.GetString()
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object DeletePost(int accountId, int id)
        {
            try
            {
                var post = _postRepository.FindOrFail(id);
                if (post == null || post.AccountId != accountId)
                {
                    throw new Exception("Canot delete");
                }
                _postRepository.DeleteByEntity(post);
                _postRepository.SaveChange();

                return "true";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetListPost(int? userId, int? inCamPaint)
        {
            try
            {
                var query = _postRepository.FindAll();
                if(userId != null)
                {
                    query = query.Where(row => row.AccountId == userId);
                }
                var postList = query.ToList();
                var authorIdList = postList.Select(row => row.AccountId).ToList();
                var authorList = _accountRepository.FindByCondition(row => authorIdList.Contains(row.Id)).ToList();

                var postWithAuthorDtoList = postList.Select(row => new PostWithAuthorDto(row, _mapper.Map<PosterDto>(authorList.Where(a => a.Id == row.AccountId).FirstOrDefault()))).ToList();
                return postWithAuthorDtoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object Search(int userId ,SearchRequest request)
        {
            try
            {
                var query = _postRepository.FindAll();
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    query = query.Where(row => row.Described.ToLower().Contains(request.Keyword.ToLower()) || request.Keyword.ToLower().Contains(row.Described.ToLower()));
                }
                var postList = query.ToList();
                var authorIdList = postList.Select(row => row.AccountId).ToList();
                var authorList = _accountRepository.FindByCondition(row => authorIdList.Contains(row.Id)).ToList();

                var postWithAuthorDtoList = postList.Select(row => new PostWithAuthorDto(row, _mapper.Map<PosterDto>(authorList.Where(a => a.Id == row.AccountId).FirstOrDefault()))).ToList();

                var checkKeyWord = _keySearchRepository.FindByCondition(row => request.Keyword == row.KeyWord && userId == row.AccountId).FirstOrDefault();
                if (checkKeyWord != null)
                {
                    checkKeyWord.UpdatedDate = DateTime.Now;
                    _keySearchRepository.UpdateByEntity(checkKeyWord);
                    _keySearchRepository.SaveChange();
                }
                else
                {
                    checkKeyWord = new KeySearch();
                    checkKeyWord.AccountId = userId;
                    checkKeyWord.KeyWord = request.Keyword;
                    checkKeyWord.UpdatedDate = DateTime.Now;
                    checkKeyWord.CreatedDate = DateTime.Now;
                    _keySearchRepository.Create(checkKeyWord);
                    _keySearchRepository.SaveChange();
                }
                return postWithAuthorDtoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetListVideos(int userId)
        {
            try
            {
                var query = _postRepository.FindByCondition(row => row.AccountId == userId && !string.IsNullOrEmpty(row.Video));
                var postList = query.ToList();
                var authorIdList = postList.Select(row => row.AccountId).ToList();
                var authorList = _accountRepository.FindByCondition(row => authorIdList.Contains(row.Id)).ToList();

                var postWithAuthorDtoList = postList.Select(row => new PostWithAuthorDto(row, _mapper.Map<PosterDto>(authorList.Where(a => a.Id == row.AccountId).FirstOrDefault()))).ToList();
                return postWithAuthorDtoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
