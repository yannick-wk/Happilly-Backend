using AutoMapper;
using Happilly.Application.Dtos;
using Happilly.Domain.Entities;

namespace Happilly.Presentation.Mapping.Profiles
{
    public class ReminderProfile : Profile
    {
        public ReminderProfile()
        {
            CreateMap<Reminder, ReminderDto>();
            CreateMap<ReminderDto, Reminder>();
        }
    }
}
