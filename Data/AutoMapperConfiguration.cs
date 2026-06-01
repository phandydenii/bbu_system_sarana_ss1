using AutoMapper;
using BBU_SYSTEM.DTOs;
using BBU_SYSTEM.Models;
using BBU_SYSTEM.ViewModel; 

namespace BBU_SYSTEM.Data;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<Degree, DegreeDto>().ReverseMap();
        CreateMap<Branch, BranchDto>().ReverseMap();
        CreateMap<Faculty, FacultyDto>().ReverseMap();
        CreateMap<Field, FieldDto>().ReverseMap();
        CreateMap<Faculty, FacultyDto>().ReverseMap();
        CreateMap<Group, GroupDto>().ReverseMap();
        CreateMap<GroupRoom, GroupRoomDto>().ReverseMap();
        CreateMap<Promotion, PromotionDto>().ReverseMap();
        CreateMap<School, SchoolDto>().ReverseMap();
        CreateMap<Stage, StageDto>().ReverseMap();
        CreateMap<Term, TermDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Privilege, PrivilegeDto>().ReverseMap();
        CreateMap<UserPriviledge, UserPriviledgeDto>().ReverseMap();
        CreateMap<ContactPerson, ContactPersonDto>().ReverseMap();
        CreateMap<Extend, ExtendDto>().ReverseMap();
        CreateMap<StudentScholarship, StudentScholarshipDto>().ReverseMap();
        CreateMap<StudentCertificate, StudentCertificateDto>().ReverseMap();
        CreateMap<StudentJob, StudentJobDto>().ReverseMap();
        CreateMap<Disability, DisabilityDto>().ReverseMap();
        CreateMap<HighSchool, HighSchoolDto>().ReverseMap();
        CreateMap<Student, StudentDto>().ReverseMap();
        CreateMap<Student, StudentUpdateViewModel>().ReverseMap();
        CreateMap<StudentUpdateViewModel, StudentDto>().ReverseMap();
        CreateMap<Registry, RegistryDto>().ReverseMap();
        CreateMap<LetterCategory, LetterCategoryDto>().ReverseMap();
        CreateMap<DailyReport, DailyReportDto>().ReverseMap();
        CreateMap<DailyReportImages, DailyReportImagesDto>().ReverseMap();
        CreateMap<PrivilegeGroup, PrivilegeGroupDto>().ReverseMap();
        CreateMap<LetterCertification, LetterCertificationDto>().ReverseMap();
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Score, ScoreDto>().ReverseMap();
        CreateMap<FieldCertificate,FieldCertificateDto>().ReverseMap();
        CreateMap<SummaryReport,SummaryReportDto>().ReverseMap(); 
        CreateMap<University, UniversityDto>().ReverseMap();
    }
}