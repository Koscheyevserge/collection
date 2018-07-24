using Api.Models.Command;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Interfaces
{
    public interface ICommand<T> where T : BaseСM
    {
        Task<IActionResult> Do(T model);
        Task<IActionResult> Undo(Guid key);
    }
}