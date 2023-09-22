namespace FoodieLionApi.Utilities;

using AutoMapper;
using FoodieLionApi.Models.DTO;
using FoodieLionApi.Models.Entities;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserDto, User>();
        CreateMap<WindowDto, Window>();
        CreateMap<DishDto, Dish>();
        CreateMap<NotificationDto, Notification>();
        CreateMap<PostDto, Post>();
        CreateMap<User, UserOutDto>();
    }
}
