using Happilly.Application.Interfaces;
using AutoMapper;
using Happilly.Application.Abstractions;
using Happilly.Domain.Entities;
using Happilly.Application.Dtos;

namespace Happilly.Application.Services
{
    public class MedicineService : BaseService<Medicine, MedicineDto>
    {
        public MedicineService(IRepository<Medicine> medicineRepository, IMapper mapper) : base(medicineRepository, mapper)
        {

        }
    }
}