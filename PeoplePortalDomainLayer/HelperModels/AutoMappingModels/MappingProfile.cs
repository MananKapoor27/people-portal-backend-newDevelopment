using AutoMapper;
using PeoplePortalDomainLayer.Entities.Dto.ConfigurationDto;
using PeoplePortalDomainLayer.Entities.Dto.DepartmentDto;
using PeoplePortalDomainLayer.Entities.Dto.DesignationDto;
using PeoplePortalDomainLayer.Entities.Dto.EmployeeDto;
using PeoplePortalDomainLayer.Entities.Dto.EmployeeSkillDto;
using PeoplePortalDomainLayer.Entities.Dto.EmployeeTypeDto;
using PeoplePortalDomainLayer.Entities.Dto.LoginDto;
using PeoplePortalDomainLayer.Entities.Dto.ProjectDto;
using PeoplePortalDomainLayer.Entities.Dto.ProjectManagementDto;
using PeoplePortalDomainLayer.Entities.Dto.ProjectRequirementDto;
using PeoplePortalDomainLayer.Entities.DTO.ConfigurationDto;
using PeoplePortalDomainLayer.Entities.DTO.DepartmentDesignationDto;
using PeoplePortalDomainLayer.Entities.DTO.EmployeeDto;
using PeoplePortalDomainLayer.Entities.DTO.EmployeeFeatureDto;
using PeoplePortalDomainLayer.Entities.DTO.LoginDto;
using PeoplePortalDomainLayer.Entities.DTO.SkillDto;
using PeoplePortalDomainLayer.Entities.Models;
using PeoplePortalDomainLayer.HelperModels.ElasticSearchModels;
using System.Collections.Generic;

namespace PeoplePortalDomainLayer.HelperMappers.AutoMappingModels
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BasicConfiguration, BasicConfigurationDto>().ReverseMap();
            CreateMap<BasicConfiguration, FeatureLevelDto>().ReverseMap();
            CreateMap<BasicConfiguration, LevelDepartmentDesignationMappingDto>().ReverseMap();
            CreateMap<BasicConfiguration, LevelDesignationDto>().ReverseMap();

            CreateMap<DepartmentDesignation, AddNewDesignationDto>().ReverseMap();
            CreateMap<DepartmentDesignation, DepartmentDesignationDto>().ReverseMap();
            CreateMap<DepartmentDesignation, DepartmentUpdateDto>().ReverseMap();

            CreateMap<Department, DepartmentCreateDto>().ReverseMap();
            CreateMap<Department, DepartmentUpdateDto>().ReverseMap();

            CreateMap<Designation, DesignationListDto>().ReverseMap();

            
            CreateMap<Employee, AddEmployeeOfficialInformationDto>().ReverseMap();

            CreateMap<Employee, BulkAddEmployeesDto>().ReverseMap();
            CreateMap<Employee, CsvResultDto>().ReverseMap();
            CreateMap<Employee, EditEmployeeDto>().ReverseMap();
            CreateMap<Employee, EditEmployeeOfficialInformationDto>().ReverseMap();
            CreateMap<Employee, EditEmployeePersonalInformationDto>().ReverseMap();
            CreateMap<Employee, EditEmployeeEducationalInformationDto>().ReverseMap();

            CreateMap<Employee, EmployeeLinksDto>().ReverseMap();
            CreateMap<Employee, GetAllEmployeeDetailsDto>().ReverseMap();
            CreateMap<Employee, GetEmployeeIdDto>().ReverseMap();
            CreateMap<Employee, GetEmployeeListDto>().ReverseMap();
            CreateMap<Employee, EmployeeBasicDetailsDto>().ReverseMap();

            CreateMap<Feature, EmployeeFeatureUpdationDetailDto>().ReverseMap();
            CreateMap<Employee, FeatureLevelDetailsDto>().ReverseMap();
            CreateMap<Employee, GetEmployeeFeatureUpdateDto>().ReverseMap();

            CreateMap<EmployeeSkill, EmployeeSkillDto>().ReverseMap();

            CreateMap<Login, ForgotPasswordDto>().ReverseMap();
            CreateMap<Login, LoginCredentialsDto>().ReverseMap();
            CreateMap<Login, LoginUserDetailsDto>().ReverseMap();
            CreateMap<Login, ResetPasswordDto>().ReverseMap();

            CreateMap<Project, EmployeeBasicDetailsDto>().ReverseMap();
            CreateMap<Project, GetProjectListDto>().ReverseMap();
            CreateMap<Project, GetProjectsDetailsDto>().ReverseMap();
            CreateMap<Project, ProjectDetailsDto>().ReverseMap();
            CreateMap<Project, UpdateProjectDto>().ReverseMap();

            CreateMap<Skill, AddSkillDto>().ReverseMap();

            CreateMap<ProjectManagement, ProjectManagementViewDto>().ReverseMap();          
            CreateMap<ProjectManagement, EmployeesInProjectDto>().ReverseMap();
            CreateMap<ProjectManagement, GetEmployeeDetailDto>().ReverseMap();
            CreateMap<ProjectManagement, EmployeeBasicDetailsDto>().ReverseMap();

            CreateMap<ProjectRequirements, ProjectRequirementDto>().ReverseMap();
            CreateMap<ProjectRequirements, EditProjectRequirementDto>().ReverseMap();
            CreateMap<ProjectRequirements, ProjectRequirementCsvDto>().ReverseMap();
            CreateMap<ProjectRequirements, ViewProjectRequirementDto>().ReverseMap();

          
            CreateMap<ElasticEmployeeDetailsDto, AddEmployeeOfficialInformationDto>().ReverseMap();

            CreateMap<ElasticEmployeeDetailsDto, BulkAddEmployeesDto>().ReverseMap();

            CreateMap<ReportingManager, ReportingManager>().ReverseMap();

            CreateMap<EmployeeType, EmployeeTypeDto>().ReverseMap();
            CreateMap<List<EmployeeType>, List<EmployeeTypeDto>>().ReverseMap();
        }
    }
}
