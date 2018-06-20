using Data.Contexts;
using Data.Entities;
using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Initializers
{
    public class DevInitializer : IDbInitializer
    {
        private ULFContext _context;

        public DevInitializer(ULFContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            _context.Database.EnsureCreated();

            if (_context.Tasks.Any() || _context.Clients.Any())
                return;
            var taskResult1 = new TaskResultsItem
            {
                Name = "Согласился платить",
                Code = "CallToClient_Success_Agreed"
            };
            var taskType1 = new TaskType {
                Name = "Позвонить клиенту",
                Code = "CallToClient",
                Description = "Позвонить клиенту",
                ResultsGroups = new List<TaskResultsGroup>
                {
                    new TaskResultsGroup {
                        Name = "Удалось дозвониться",
                        Code = "CallToClient_Success",
                        Items = new List<TaskResultsItem>
                        {
                            taskResult1,
                            new TaskResultsItem
                            {
                                Name = "Отказался платить",
                                Code = "CallToClient_Success_NotAgreed"
                            }
                        }
                    },
                    new TaskResultsGroup
                    {
                        Name = "Не удалось дозвониться",
                        Code = "CallToClient_Fail",
                        Items = new List<TaskResultsItem>
                        {
                            new TaskResultsItem
                            {
                                Name = "Не берет трубку",
                                Code = "CallToClient_Fail_Ignore"
                            },
                            new TaskResultsItem
                            {
                                Name = "Вне зоны",
                                Code = "CallToClient_Fail_Unavailable"
                            }
                        }
                    }
                }
            };

            var client1 = new Client
            {
                Name = "ООО Юнионком",
                Code = "335524891",
                Agreements = new List<Agreement>
                {
                    new Agreement
                    {
                        Date = new DateTime(2017, 12, 3),
                        Currency = Currency.UAH,
                        Code = "ДФЛ 03.12.2017 №417",
                        ParticipationAmount = 100000,
                        ShippmentDate = new DateTime(2017, 12, 3),                        
                        Accruals = new List<Accrual>
                        {
                            new Accrual
                            {
                                Date = new DateTime(2018, 6, 8),
                                Amount = 7000,
                                Currency = Currency.UAH
                            },
                            new Accrual
                            {
                                Date = new DateTime(2018, 6, 1),
                                Amount = 3000,
                                Currency = Currency.UAH
                            }
                        }                        
                    }
                },
                Type = ClientType.Juridical,
                Tasks = new List<Task>
                {
                    new Task { Date = new DateTime(2018, 6, 9), Type = taskType1, Result = taskResult1 },
                    new Task { Date = new DateTime(2018, 6, 10), Type = taskType1 }
                }
            };
            var client2 = new Client
            {
                Name = "ООО ТАВРИЯ В",
                Code = "335521323",
                Agreements = new List<Agreement>
                {
                    new Agreement
                    {
                        Date = new DateTime(2018, 3, 3),
                        Currency = Currency.UAH,
                        Code = "ДФЛ 03.03.2018 №417",
                        ParticipationAmount = 120000,
                        ShippmentDate = new DateTime(2018, 3, 3),
                        Accruals = new List<Accrual>
                        {
                            new Accrual
                            {
                                Date = new DateTime(2018, 5, 1),
                                Amount = 3000,
                                Currency = Currency.UAH
                            }
                        }
                    }
                },
                Type = ClientType.Juridical,
                Tasks = new List<Task>
                {
                    new Task { Date = new DateTime(2018, 6, 9), Type = taskType1 },
                    new Task { Date = new DateTime(2018, 6, 10), Type = taskType1 }
                }
            };

            _context.Clients.Add(client1);
            _context.Clients.Add(client2);
            _context.SaveChanges();
        }
    }
}
