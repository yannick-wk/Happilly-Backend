using AutoMapper;
using Happilly.Application.Dtos;
using Happilly.Domain.Entities;

namespace Happilly.Presentation.Mapping.Profiles
{
    public class MedicineProfile : Profile
    {
        public MedicineProfile()
        {
            CreateMap<Medicine, MedicineDto>();
            CreateMap<MedicineDto, Medicine>();
        }
    }
}
