using AutoFixture;
using AutoMapper;
using BackendApi2;
using BackendApi2.Contracts;
using BackendApi2.Controllers;
using BackendApi2.Entities.DTOModels;
using BackendApi2.Entities.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BackendApi2Test;

public class EmployeeControllerTest
{


    private readonly IFixture _fixture;
    private readonly Mock<IRepositoryWrapper> _repomock;

    private readonly Mock<IMapper> _mappermock;
    private readonly EmployeesController _employeesController;

   // private readonly Mock<IWebHostEnvironment> webHostEnvironment;
    public EmployeeControllerTest()
    {
        _fixture = new Fixture();
        
      //  webHostEnvironment = _fixture.Freeze<Mock<IWebHostEnvironment>>();
        _repomock = _fixture.Freeze<Mock<IRepositoryWrapper>>();
       // _mappermock = _fixture.Freeze<Mock<IMapper>>();
           var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
        _employeesController = new EmployeesController(_repomock.Object,mapper);

        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior(4));
    }


    [Fact]
    public async Task Test1()
    {
      //Arrange
       var empmockdata = _fixture.Create<IEnumerable<Employee>>();
      
       //Console.WriteLine(empmockdata);
       _repomock.Setup(c=>c.employee.GetEmployees()).ReturnsAsync(empmockdata);
      
      //Act
      var result =await _employeesController.GetALLEmployees();

      //Assert
     // result.As<OkObjectResult>().StatusCode.Value.Should().Be(200);
      result.Should().NotBeNull();
      result.Should().BeAssignableTo<OkObjectResult>();
     result.As<OkObjectResult>().Value.Should().NotBeNull().
     And.BeOfType(typeof(List<EmployeeDTO>));

      _repomock.Verify(x=>x.employee.GetEmployees(),Times.Once());
    }


    [Fact]
    public void Test2()
    {
        //Arrange
       var empmockdata = _fixture.Create<EmployeeForCreateDto>();
      
       //Console.WriteLine(empmockdata);
       _repomock.Setup(c=>c.employee.CreateEmployee(It.IsAny<Employee>()));
      

      //Act
       var result =  _employeesController.CreateEmployee(empmockdata);

     //Assert
      result.As<CreatedAtRouteResult>().StatusCode.Value.Should().Be(201);
      result.Should().NotBeNull();
      result.Should().BeAssignableTo<CreatedAtRouteResult>();
      result.As<CreatedAtRouteResult>().Value.Should().NotBeNull().
       And.BeOfType(typeof(EmployeeForCreateDto));

      _repomock.Verify(x=>x.employee.CreateEmployee(It.IsAny<Employee>()),Times.Once());

    }
}