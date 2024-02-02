﻿using AutoMapper;
using Application.Commons;
using Domain.Entities;
using Application.ViewModel.AccountDTO;
using Application.ViewModel.RegisterAccountDTO;
using Application.ViewModel.UpdateAccountDTO;


namespace Infrastructures.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            //Account
            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<AccountDTO, AccountDTO>().ReverseMap();
            //CreateAccount ROLE: GUEST
            CreateMap<CreatedAccountDTO, Account>().ReverseMap();
            CreateMap<CreatedAccountDTO, AccountDTO>();
            //RegisterAccount ROLE: HOST
            CreateMap<RegisterAccountDTO, Account>().ReverseMap();
            CreateMap<RegisterAccountDTO, AccountDTO>();
            //UpdateAccount
            CreateMap<UpdateAccountDTO, Account>().ReverseMap();
            CreateMap<UpdateAccountDTO, AccountDTO>();




            CreateMap(typeof(Pagination<>), typeof(Pagination<>));
        }
    }
}
