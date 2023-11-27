using AutoMapper;
using AntiFakebookApi.Dto;
using AntiFakebookApi.Models;
using AntiFakebookApi.Request;

namespace AntiFakebookApi.Mapper
{
    public class MappingContext : Profile
    {
        public MappingContext()
        {
            CreateMap<Account, PosterDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<Account, FriendDto>();
            //CreateMap<CreateTableRequest, Table>();
            //CreateMap<CreateEmployeeRequest, User>();
        }
    }
}
