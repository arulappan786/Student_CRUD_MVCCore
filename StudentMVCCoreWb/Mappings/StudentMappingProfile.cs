using AutoMapper;

namespace StudentMVCCoreWb.Mappings
{
    public class StudentMappingProfile : Profile
    {
        public StudentMappingProfile()
        {
            CreateMap<Entities.Student, Models.StudentViewModel>().ReverseMap();
            CreateMap<Entities.Student, Models.StudentCreateViewModel>().ReverseMap();
        }
    }
}
