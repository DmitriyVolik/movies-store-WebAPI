using AutoMapper;
using DAL.Entities;
using WebAPI.ViewModels;

namespace WebAPI.AutoMapper  
{  
    public class AutoMapperProfile : Profile  
    {  
        public AutoMapperProfile()  
        {  
            CreateMap<CommentRequestViewModel, Comment>();
            CreateMap<UserRequestViewModel, User>();  
        }  
    }  
}