using AutoMapper;
using AntiFakebookApi.Common;
using AntiFakebookApi.Database;
using AntiFakebookApi.Repositories;
using AntiFakebookApi.Request;
using AntiFakebookApi.Models;
using AntiFakebookApi.Dto;

namespace AntiFakebookApi.Services
{
    public class PostService
    {
        private readonly PostRepository _postRepository;
        private readonly AccountRepository _accountRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public PostService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _postRepository = new PostRepository(apiOption, databaseContext, mapper);
            _accountRepository = new AccountRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        public object AddPost(int accountId, AddPostRequest request)
        {
            try
            {
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
                return post;
            }
            catch(Exception ex)
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
                    post = post,
                    author = account
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

                return post;
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
    }
}
