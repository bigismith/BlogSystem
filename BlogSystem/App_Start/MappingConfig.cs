using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogSystem.Models;
using BlogSystem.ViewModels;

namespace BlogSystem.App_Start
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Post, PostViewModel>()
                    .ForMember(p => p.DateTime, map => map.MapFrom(p => p.DateCreated))
                    .ForMember(p => p.Username, map => map.MapFrom(p => p.Author.UserName));

                config.CreateMap<Post, PostShortViewModel>()
                    .ForMember(p => p.DateTime, map => map.MapFrom(p => p.DateCreated))
                    .ForMember(p => p.Username, map => map.MapFrom(p => p.Author.UserName));

                config.CreateMap<PostShortViewModel, Post>()
                    //.ForMember(p => p.DateCreated, map => map.MapFrom(p => p.DateTime))
                    //.ForMember(p => p.Author, map => map.MapFrom(p => p.Username))
                    ;

                config.CreateMap<Comment, CommentViewModel>();
            });
        }
    }
}