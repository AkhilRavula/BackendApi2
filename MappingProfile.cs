using AutoMapper;
using BackendApi2.Entities.DTOModels;
using BackendApi2.Entities.Models;


namespace BackendApi2;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Employee, EmployeeDTO>();

        CreateMap<Skill, SkillDTO>();   

        CreateMap<EmployeeForCreateDto,Employee>();

        CreateMap<SkillCreationDto,Skill>();


    }


}
