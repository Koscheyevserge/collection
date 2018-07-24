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
                .ForMember(ctqm => ctqm.Id, config => config.MapFrom(ct => (int)ct))
                .ForMember(cctqm => cctqm.Name, config => config.MapFrom(c =>
                    ((RussianNameAttribute)typeof(ClientType).GetMember(c.ToString())[0]
                        .GetCustomAttributes(typeof(RussianNameAttribute), false)[0]).Name));
            CreateMap<PaymentSource, PaymentSourceQM>()
                .ForMember(ctqm => ctqm.Id, config => config.MapFrom(ct => (int)ct));
            CreateMap<ContactCommunicationType, ContactCommunicationTypeQM>()
                .ForMember(ctqm => ctqm.Id, config => config.MapFrom(ct => (int)ct))
                .ForMember(cctqm => cctqm.Name, config => config.MapFrom(c =>
                    ((RussianNameAttribute)typeof(ContactCommunicationType).GetMember(c.ToString())[0]
                        .GetCustomAttributes(typeof(RussianNameAttribute), false)[0]).Name));
            CreateMap<VehicleType, VehicleTypeQM>()
                .ForMember(vtqm => vtqm.Id, config => config.MapFrom(vt => (int)vt))
                .ForMember(vtqm => vtqm.Name, config => config.MapFrom(vt =>
                    ((RussianNameAttribute)typeof(VehicleType).GetMember(vt.ToString())[0]
                        .GetCustomAttributes(typeof(RussianNameAttribute), false)[0]).Name));
            CreateMap<Sex, SexQM>()
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
            CreateMap<TaskResult, TaskResultQM>();
            CreateMap<TaskType, TaskTypeQM>();
            CreateMap<ContactCommunication, ContactCommunicationQM>();
            CreateMap<Contact, ContactQM>();
            CreateMap<Data.Entities.Task, TaskQM>();
            CreateMap<Client, ClientQM>();
            CreateMap<Payment, PaymentQM>();
            CreateMap<Agreement, AgreementQM>()
                .ForMember(aqm => aqm.AgreementDate, config => config.MapFrom(a => a.Date));
            CreateMap<Accrual, AccrualQM>();
            CreateMap<VehicleManufacturer, VehicleManufacturerQM>();
            CreateMap<VehicleModel, VehicleModelQM>();
            CreateMap<Vehicle, VehicleQM>();
            #endregion

            #region Upr entities to Entities
            CreateMap<Upr.Entities.Client, Client>()
                .ForMember(c => c.UprId, config => config.MapFrom(uc => Guid.Parse(uc.Id)))
                .ForMember(c => c.Id, config => config.Ignore())
                .ForMember(c => c.TaxNumber, config => config.MapFrom(uc => string.IsNullOrEmpty(uc.TaxNumber.Trim()) ? uc.Code : uc.TaxNumber))
                .ForMember(c => c.AddressJuridical, config => config.MapFrom(uc => uc.AddressJur))
                .ForMember(c => c.AddressPhysical, config => config.MapFrom(uc => uc.AddressPhys))
                .ForMember(c => c.Type, config => config.MapFrom(uc =>
                    uc.Code.Length == 8
                    ? ClientType.Juridical
                    : uc.Name.Contains("ФОП") || uc.Name.Contains("СПД") || uc.NameFull.Contains("Фізична особа-підприємець")
                        ? ClientType.Entrepreneur
                        : ClientType.Physical));
            CreateMap<Upr.Entities.Agreement, Agreement>()
                .ForMember(c => c.UprId, config => config.MapFrom(uc => Guid.Parse(uc.Id)))
                .ForMember(c => c.Id, config => config.Ignore())
                .ForMember(c => c.ClientId, config => config.Ignore())
                .ForMember(c => c.VehicleId, config => config.Ignore())
                .ForMember(c => c.Currency, config => config.MapFrom(uc =>
                    uc.Currency.Trim().ToLower() == "грн"
                        ? Currency.UAH
                        : uc.Currency.Trim().ToLower() == "usdмежбанк" || uc.Currency.Trim().ToLower() == "usd"
                            ? Currency.USD_Межбанк
                            : uc.Currency.Trim().ToLower() == "долармбфін"
                                ? Currency.USD_MB_Fin
                                : Currency.Undefined));
            #endregion
        }
    }
}
