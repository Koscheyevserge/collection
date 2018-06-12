using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models.Query;
using Data.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Query
{
    [Produces("application/json")]
    [Route("api/query/clients")]
    public class ClientsController : Controller
    {
        [HttpGet("{id}")]
        public ClientQM Get(int id)
        {
            return new ClientQM
            {
                Id = 1,
                Code = "333551322",
                Name = "ULF-finance",
                Agreements = new List<AgreementQM>
                {
                    new AgreementQM
                    {
                        Id = 417,
                        Code = "ДФЛ от 01.01.2018 №417",
                        ClientId = 1,
                        AgreementDate = new DateTime(2018, 1, 1),
                        ShippmentDate = new DateTime(2018, 1, 1),
                        ParticipationAmountUAH = 27023,
                        ParticipationAmount = 10000,
                        CurrencyId = (int)Currency.USD_Межбанк,
                        CurrencyName = "USD",
                        CurrencyRate = 27.023,
                        Accruals = new List<AccrualQM>
                        {
                            new AccrualQM
                            {
                                Id = 1,
                                Date = new DateTime(2018, 6, 8),
                                Number = "23134",
                                AgreementId = 417,
                                CurrencyId = (int)Currency.UAH,
                                CurrencyName = "UAH",
                                CurrencyRate = 1,
                                Amount = 9000,
                                AmountUAH = 9000
                            }
                        },
                        Payments = new List<PaymentQM>
                        {
                            new PaymentQM
                            {
                                Id = 1,
                                AgreementId = 417,
                                Date = new DateTime(2018, 6, 8),
                                Code = "1223",
                                Source = (int)PaymentSource.ULF,
                                Currency = (int)Currency.UAH
                            }
                        },
                        PaymentSchedule = new List<PaymentScheduleItemQM>
                        {
                        }
                    }
                }
            };
        }
    }
}