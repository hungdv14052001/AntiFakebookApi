using AutoMapper;
using AntiFakebookApi.Common;
using AntiFakebookApi.Database;
using AntiFakebookApi.Repositories;
using AntiFakebookApi.Request;
using AntiFakebookApi.Models;
using AntiFakebookApi.Dto;
using System.Linq;
using AntiFakebookApi.Common.Enum;

namespace AntiFakebookApi.Services
{
    public class PostHandleService
    {
        private readonly PostRepository _postRepository;
        private readonly AccountRepository _accountRepository;
        private readonly CommentRepository _commentRepository;
        private readonly ReactionRepository _reactionRepository;
        private readonly NotificationService _notificationService;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public PostHandleService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _notificationService = new NotificationService(apiOption, databaseContext, mapper, webHost);
            _postRepository = new PostRepository(apiOption, databaseContext, mapper);
            _accountRepository = new AccountRepository(apiOption, databaseContext, mapper);
            _commentRepository = new CommentRepository(apiOption, databaseContext, mapper);
            _reactionRepository = new ReactionRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        public object GetMarkComment(int id)
        {
            try
            {
                var post = _postRepository.FindOrFail(id);
                if (post == null)
                {
                    throw new Exception("Post Id invalid");
                }

                var commentListByPost = _commentRepository.FindByCondition(row => row.PostId == post.Id).ToList();
                var posterIdList = commentListByPost.Select(row => row.AccountId).ToList();
                var posterList = _accountRepository.FindByCondition(row => posterIdList.Contains(row.Id)).ToList();
                var commentWithPosterList = commentListByPost.Select(row => new CommentWithPosterDto(row, _mapper.Map<PosterDto>(posterList.Where(p => p.Id == row.AccountId).FirstOrDefault())));

                var author = _mapper.Map<PosterDto>(_accountRepository.FindOrFail(post.AccountId)).GetString();
                return new
                {
                    post = post.GetString(),
                    author = author,
                    comments = commentWithPosterList,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object SetMarkComment(int accountId, SetMarkCommentRequest request)
        {
            try
            {
                var post = _postRepository.FindOrFail(request.Id);
                if (post == null)
                {
                    throw new Exception("Post Id invalid");
                }

                var comment = new Comment()
                {
                    AccountId = accountId,
                    PostId = request.Id,
                    Content = request.Content,
                    Type = request.Type
                };

                _commentRepository.Create(comment);
                _commentRepository.SaveChange();

                var commentListByPost = _commentRepository.FindByCondition(row => row.PostId == post.Id).ToList();
                var posterIdList = commentListByPost.Select(row => row.AccountId).ToList();
                var posterList = _accountRepository.FindByCondition(row => posterIdList.Contains(row.Id)).ToList();
                var commentWithPosterList = commentListByPost.Select(row => new CommentWithPosterDto(row, _mapper.Map<PosterDto>(posterList.Where(p => p.Id == row.AccountId).FirstOrDefault())));

                var posterCurrent = _mapper.Map<PosterDto>(_accountRepository.FindOrFail(accountId)).GetString();

                // create notification
                _notificationService.CreateNotification(NotificationTypeEnum.CommentPost, post.AccountId, accountId, post.Id);

                return new
                {
                    Id = comment.Id.ToString(),
                    Content = comment.Content,
                    Type = comment.Type.ToString(),
                    poster = posterCurrent,
                    comments = commentWithPosterList
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object Feel(int accountId, FeelRequest request)
        {
            try
            {
                var post = _postRepository.FindOrFail(request.Id);
                if (post == null)
                {
                    throw new Exception("Post Id invalid");
                }

                var reaction = _reactionRepository.FindByCondition(row => row.AccountId == accountId && row.PostId == request.Id).FirstOrDefault();
                if (reaction == null)
                {
                    reaction = new Reaction()
                    {
                        AccountId = accountId,
                        PostId = request.Id,
                        Type = request.Type,
                    };
                    _reactionRepository.Create(reaction);
                    _reactionRepository.SaveChange();
                }
                else
                {
                    reaction.Type = request.Type;
                }

                // create notify
                _notificationService.CreateNotification(NotificationTypeEnum.LikePost, post.AccountId, accountId, post.Id);

                return new
                {
                    disappointed = _reactionRepository.FindByCondition(row => row.PostId == request.Id && row.Type == 0).Count().ToString(),
                    kudos = _reactionRepository.FindByCondition(row => row.PostId == request.Id && row.Type == 1).Count().ToString(),
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
