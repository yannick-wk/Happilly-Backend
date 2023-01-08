using AutoMapper;
using Happilly.Application.Abstractions;
using Happilly.Application.Dtos;
using Happilly.Application.Interfaces;
using Happilly.Domain.Entities;

namespace Happilly.Application.Services
{
    public class ReminderService : BaseService<Reminder, ReminderDto>
    {
        public ReminderService(IRepository<Reminder> reminderRepository, IMapper mapper) : base(reminderRepository, mapper)
        {

        }
    }
}
