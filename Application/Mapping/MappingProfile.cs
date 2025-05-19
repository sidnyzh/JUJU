using Application.DTO.Customer.Request;
using Application.DTO.Customer.Response;
using Application.DTO.Post.Request;
using Application.DTO.Post.Response;
using AutoMapper;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<CreateCustomerRequest, Customer>();
            CreateMap<CreatePostRequest, Post>();
            CreateMap<UpdateCustomerRequest, Customer>();
            CreateMap<UpdatePostRequest, Post>();
            CreateMap<Post, GetPostResponse>();
            CreateMap<Customer, GetCustomerResponse>();

        }
    }
}
