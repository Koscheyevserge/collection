using Data.Contexts;
using Data.Entities;
using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Api.Initializers
{
    public class DevInitializer : IDbInitializer
    {
        private CollectionContext _context;

        public DevInitializer(CollectionContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            _context.Database.EnsureCreated();

            if (_context.Tasks.Any() || _context.Clients.Any())
                return;
            var taskResult1 = new TaskResult
            {
                Name = "Согласился платить",
                Code = "Agreed"
            };
            var taskType1 = new TaskType
            {
                Name = "Позвонить клиенту просрочка 10+ дней",
                Code = "CallToClient10DaysDebt",
                Description = "Позвонить клиенту",
                Results = new List<TaskResult>
                {
                    taskResult1,
                    new TaskResult
                    {
                        Name = "Отказался платить",
                        Code = "NotAgreed"
                    },
                    new TaskResult
                    {
                        Name = "Просит реструктуризацию",
                        Code = "NeedRestruckt"
                    },
                    new TaskResult
                    {
                        Name = "Не берет трубку",
                        Code = "Ignore"
                    },
                    new TaskResult
                    {
                        Name = "Вне зоны",
                        Code = "Unavailable"
                    },
                    new TaskResult
                    {
                        Name = "Задача отменена",
                        Code = "Cancel"
                    }
                }
            };
            var taskType2 = new TaskType
            {
                Name = "Реструктуризировать ДЗ 10+ дней",
                Code = "Restruckt10DaysDebt",
                Description = "Отправить запрос на реструктуризацию",
                Results = new List<TaskResult>
                {
                    new TaskResult
                    {
                        Name = "Запрос отправлен",
                        Code = "Sent"
                    },
                    new TaskResult
                    {
                        Name = "Реструктуризация не одобрена",
                        Code = "NotAgreed"
                    },
                    new TaskResult
                    {
                        Name = "Задача отменена",
                        Code = "Cancel"
                    }
                }
            };
            var taskType3 = new TaskType
            {
                Name = "Отправить требования 30+ дней",
                Code = "SendRequest30DaysDebt",
                Description = "Отправить требования клиенту",
                Results = new List<TaskResult>
                {
                    new TaskResult
                    {
                        Name = "Требования отправлены",
                        Code = "Sent"
                    },
                    new TaskResult
                    {
                        Name = "Клиент недоступен",
                        Code = "Unavailable"
                    },
                    new TaskResult
                    {
                        Name = "Задача отменена",
                        Code = "Cancel"
                    }
                }
            };
            var taskType4 = new TaskType
            {
                Name = "Реструктуризировать ДЗ 30+ дней",
                Code = "Restruckt30DaysDebt",
                Description = "Отправить запрос на реструктуризацию",
                Results = new List<TaskResult>
                {
                    new TaskResult
                    {
                        Name = "Запрос отправлен",
                        Code = "Sent"
                    },
                    new TaskResult
                    {
                        Name = "Реструктуризация не одобрена",
                        Code = "NotAgreed"
                    },
                    new TaskResult
                    {
                        Name = "Задача отменена",
                        Code = "Cancel"
                    }
                }
            };
            var taskType5 = new TaskType
            {
                Name = "Реструктуризировать ДЗ 60+ дней",
                Code = "Restruckt60DaysDebt",
                Description = "Отправить запрос на реструктуризацию",
                Results = new List<TaskResult>
                {
                    new TaskResult
                    {
                        Name = "Запрос отправлен",
                        Code = "Sent"
                    },
                    new TaskResult
                    {
                        Name = "Реструктуризация не одобрена",
                        Code = "NotAgreed"
                    },
                    new TaskResult
                    {
                        Name = "Задача отменена",
                        Code = "Cancel"
                    }
                }
            };
            var taskType6 = new TaskType
            {
                Name = "Скип-трейсинг 30+ дней",
                Code = "Trace30DaysDebt",
                Description = "Идентифицировать местонахождение клиента и предмета лизинга",
                Results = new List<TaskResult>
                {
                    new TaskResult
                    {
                        Name = "Клиент и ПЛ найдены",
                        Code = "Found"
                    },
                    new TaskResult
                    {
                        Name = "Клиент найден, ПЛ не найден",
                        Code = "NotFoundVehicle"
                    },
                    new TaskResult
                    {
                        Name = "ПЛ найден, клиент не найден",
                        Code = "NotFoundClient"
                    },
                    new TaskResult
                    {
                        Name = "Клиент не найден",
                        Code = "NotFound"
                    },
                    new TaskResult
                    {
                        Name = "Задача отменена",
                        Code = "Cancel"
                    }
                }
            };
            var taskType7 = new TaskType
            {
                Name = "Посетить клиента 30+ дней",
                Code = "Visit30DaysDebt",
                Description = "Гарантийное письмо, акт осмотра ПЛ, акт сверки задолженности",
                Results = new List<TaskResult>
                {
                    new TaskResult
                    {
                        Name = "Встреча совершилась, акты подписаны",
                        Code = "Success"
                    },
                    new TaskResult
                    {
                        Name = "Встреча совершилась, акты не подписаны",
                        Code = "VisitNotSigned"
                    },
                    new TaskResult
                    {
                        Name = "Встреча не совершилась",
                        Code = "Fail"
                    },
                    new TaskResult
                    {
                        Name = "Задача отменена",
                        Code = "Cancel"
                    }
                }
            };
            var taskType8 = new TaskType
            {
                Name = "Ограничить право пользования ПЛ 30+ дней",
                Code = "Restrict30DaysDebt",
                Description = "Ограничить право пользования ПЛ"
            };
            var taskType9 = new TaskType
            {
                Name = "Расторгнуть ДЛ 60+ дней",
                Code = "Disolve60DaysDebt",
                Description = "Расторгнуть договор лизинга",
                Results = new List<TaskResult>
                {
                    new TaskResult
                    {
                        Name = "Задача выполнена",
                        Code = "Success"
                    },
                    new TaskResult
                    {
                        Name = "Задача не выполнена",
                        Code = "Fail"
                    },
                    new TaskResult
                    {
                        Name = "Задача отменена",
                        Code = "Cancel"
                    }
                }
            };
            var taskType10 = new TaskType
            {
                Name = "Уведомить клиента о рассторжении ДЛ 60+ дней",
                Code = "DisolveNotificate60DaysDebt",
                Description = "Уведомить клиента о рассторжении договора лизинга",
                Results = new List<TaskResult>
                {
                    new TaskResult
                    {
                        Name = "Клиент уведомлен",
                        Code = "Success"
                    },
                    new TaskResult
                    {
                        Name = "Клиент игнорирует",
                        Code = "Fail"
                    },
                    new TaskResult
                    {
                        Name = "Задача отменена",
                        Code = "Cancel"
                    }
                }
            };
            var taskType11 = new TaskType
            {
                Name = "Скип-трейсинг 60+ дней",
                Code = "Trace60DaysDebt",
                Description = "Идентифицировать местонахождение клиента и предмета лизинга",
                Results = new List<TaskResult>
                {
                    new TaskResult
                    {
                        Name = "Клиент и ПЛ найдены",
                        Code = "Found"
                    },
                    new TaskResult
                    {
                        Name = "Клиент найден, ПЛ не найден",
                        Code = "NotFoundVehicle"
                    },
                    new TaskResult
                    {
                        Name = "ПЛ найден, клиент не найден",
                        Code = "NotFoundClient"
                    },
                    new TaskResult
                    {
                        Name = "Клиент не найден",
                        Code = "NotFound"
                    },
                    new TaskResult
                    {
                        Name = "Задача отменена",
                        Code = "Cancel"
                    }
                }
            };
            var taskType12 = new TaskType
            {
                Name = "Изъять ПЛ 60+ дней",
                Code = "Take60DaysDebt",
                Description = "Изъять предмет лизинга и отвезти его на арендованную парковку",
                Results = new List<TaskResult>
                {
                    new TaskResult
                    {
                        Name = "ПЛ изъят",
                        Code = "Success"
                    },
                    new TaskResult
                    {
                        Name = "ПЛ не изъят",
                        Code = "Fail"
                    },
                    new TaskResult
                    {
                        Name = "Задача отменена",
                        Code = "Cancel"
                    }
                }
            };
            var accruals1 = new List<Accrual>
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
            };
            var accruals2 = new List<Accrual>
            {
                new Accrual
                {
                    Date = new DateTime(2018, 5, 1),
                    Amount = 3000,
                    Currency = Currency.UAH
                }
            };
            var payments1 = new List<Payment>
            {
                new Payment
                {
                    Date = new DateTime(2018, 6, 8),
                    Amount = 2000,
                    Currency = Currency.UAH,
                    Source = PaymentSource.TKB
                }
            };
            var payments2 = new List<Payment>
            {
                new Payment
                {
                    Date = new DateTime(2018, 3, 3),
                    Amount = 2000,
                    Currency = Currency.UAH,
                    Source = PaymentSource.TKB
                },
                new Payment
                {
                    Date = new DateTime(2018, 5, 1),
                    Amount = 1000,
                    Currency = Currency.UAH,
                    Source = PaymentSource.TKB
                }
            };
            var client1 = new Client
            {
                Name = "ООО Юнионком",
                Code = "335524891",
                Contacts = new List<Contact>
                {
                    new Contact
                    {
                        DateOfBirth = new DateTime(1996, 8, 7),
                        IsMain = true,
                        Name = "Сергей",
                        Surname = "Кощеев",
                        Patronym = "Александрович",
                        Sex = Sex.Male,
                        Communications = new List<ContactCommunication>
                        {
                            new ContactCommunication
                            {
                                IsMain = true,
                                Number = "+380975556501",
                                Type = ContactCommunicationType.Phone
                            },
                            new ContactCommunication
                            {
                                IsMain = true,
                                Number = "koscheyevserge4@gmail.com",
                                Type = ContactCommunicationType.Email
                            },
                            new ContactCommunication
                            {
                                IsMain = false,
                                Number = "555-55-55",
                                Type = ContactCommunicationType.Undefined
                            }
                        }
                    },
                    new Contact
                    {
                        DateOfBirth = null,
                        IsMain = false,
                        Name = "Елена",
                        Surname = null,
                        Patronym = "Викторовна",
                        Sex = Sex.Undefined
                    }
                },
                Agreements = new List<Agreement>
                {
                    new Agreement
                    {
                        Date = new DateTime(2017, 12, 3),
                        Currency = Currency.UAH,
                        Code = "ДФЛ 03.12.2017 №417",
                        ParticipationAmount = 100000,
                        ShippmentDate = new DateTime(2017, 12, 3),
                        Accruals = accruals1,
                        Payments = payments1
                    }
                },
                Type = ClientType.Juridical,
                Tasks = new List<Data.Entities.Task>
                {
                    new Data.Entities.Task { Date = new DateTime(2018, 6, 9), Type = taskType1, Result = taskResult1 },
                    new Data.Entities.Task { Date = new DateTime(2018, 6, 10), Type = taskType3 }
                }
            };
            var client2 = new Client
            {
                Name = "ООО ТАВРИЯ В",
                Code = "335521323",
                Contacts = new List<Contact>
                {
                    new Contact
                    {
                        DateOfBirth = new DateTime(1996, 8, 7),
                        IsMain = true,
                        Name = "Сергей",
                        Surname = "Кощеев",
                        Patronym = "Александрович",
                        Sex = Sex.Male,
                        Communications = new List<ContactCommunication>
                        {
                            new ContactCommunication
                            {
                                IsMain = true,
                                Number = "+380975556501",
                                Type = ContactCommunicationType.Phone
                            },
                            new ContactCommunication
                            {
                                IsMain = true,
                                Number = "koscheyevserge4@gmail.com",
                                Type = ContactCommunicationType.Email
                            },
                            new ContactCommunication
                            {
                                IsMain = false,
                                Number = "555-55-55",
                                Type = ContactCommunicationType.Undefined
                            }
                        }
                    },
                    new Contact
                    {
                        DateOfBirth = null,
                        IsMain = false,
                        Name = "Елена",
                        Surname = null,
                        Patronym = "Викторовна",
                        Sex = Sex.Undefined
                    }
                },
                Agreements = new List<Agreement>
                {
                    new Agreement
                    {
                        Date = new DateTime(2018, 3, 3),
                        Currency = Currency.UAH,
                        Code = "ДФЛ 03.03.2018 №417",
                        ParticipationAmount = 120000,
                        ShippmentDate = new DateTime(2018, 3, 3),
                        Accruals = accruals2,
                        Payments = payments2
                    }
                },
                Type = ClientType.Juridical,
                Tasks = new List<Data.Entities.Task>
                {
                    new Data.Entities.Task { Date = new DateTime(2018, 6, 9), Type = taskType2 },
                    new Data.Entities.Task { Date = new DateTime(2018, 6, 10), Type = taskType1 }
                }
            };

            _context.Clients.Add(client1);
            _context.Clients.Add(client2);
            _context.TaskTypes.Add(taskType4);
            _context.TaskTypes.Add(taskType5);
            _context.TaskTypes.Add(taskType6);
            _context.TaskTypes.Add(taskType7);
            _context.TaskTypes.Add(taskType8);
            _context.TaskTypes.Add(taskType9);
            _context.TaskTypes.Add(taskType10);
            _context.TaskTypes.Add(taskType11);
            _context.TaskTypes.Add(taskType12);
            _context.SaveChanges();
        }
    }
}
