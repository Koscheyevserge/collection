using Api.Models.Query;
using AutoMapper;
using Data.Enums;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Attributes;

namespace Api.MappingProfiles
{
    public class DevMappingProfile : Profile
    {
        public DevMappingProfile()
        {
            #region Enums to QMs            
            CreateMap<ClientType, ClientTypeQM>()
                .ForMember(ctqm => ctqm.Id, config => config.MapFrom(ct => (int)ct));
            CreateMap<Currency, CurrencyQM>()
                .ForMember(cqm => cqm.Id, config => config.MapFrom(c => (int)c))
                .ForMember(cqm => cqm.Name, config => config.MapFrom(c => 
                    ((RussianNameAttribute)typeof(Currency).GetMember(c.ToString())[0]
                        .GetCustomAttributes(typeof(RussianNameAttribute), false)[0]).Name))
                .ForMember(cqm => cqm.CurrencyRate, config => config.UseValue(1))
                .ForMember(cqm => cqm.ShortName, config => config.MapFrom(c =>
                    ((RussianNameAttribute)typeof(Currency).GetMember(c.ToString())[0]
                        .GetCustomAttributes(typeof(RussianNameAttribute), false)[0]).ShortName))
                .ForMember(cqm => cqm.Symbol, config => config.MapFrom(c =>
                    ((RussianNameAttribute)typeof(Currency).GetMember(c.ToString())[0]
                        .GetCustomAttributes(typeof(RussianNameAttribute), false)[0]).Symbol))
                .ForMember(cqm => cqm.Abbreviation, config => config.MapFrom(c =>
                    ((RussianNameAttribute)typeof(Currency).GetMember(c.ToString())[0]
                        .GetCustomAttributes(typeof(RussianNameAttribute), false)[0]).Abbreviation));
            #endregion

            #region Entities to QMs
            CreateMap<TaskResultsItem, TaskResultsItemQM>();
            CreateMap<TaskResultsGroup, TaskResultsGroupQM>();
            CreateMap<TaskType, TaskTypeQM>();
            CreateMap<Data.Entities.Task, TaskQM>();
            CreateMap<Client, ClientQM>();
            CreateMap<Agreement, AgreementQM>()
                .ForMember(aqm => aqm.AgreementDate, config => config.MapFrom(a => a.Date));
            CreateMap<Accrual, AccrualQM>();
            #endregion
        }
    }
}
