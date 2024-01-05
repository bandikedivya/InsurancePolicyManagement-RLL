using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Data;
using DAL.Repository;
using Moq;
using NUnit.Framework;

[TestFixture]
public class AdminRepositoryTests
{
    private Mock<InsuranceDbContext> mockContext;
    private IAdminRepository adminRepository;

    [SetUp]
    public void Setup()
    {
        mockContext = new Mock<InsuranceDbContext>();
        adminRepository = new AdminRepository(mockContext.Object);
    }

    [Test]
    public void CreateAdmin_ShouldAddAdminToContext()
    {
        // Arrange
        var admin = new Admin { /* set admin properties */ };

        // Act
        adminRepository.CreateAdmin(admin);

        // Assert
        mockContext.Verify(c => c.Admins.Add(It.IsAny<Admin>()), Times.Once);
        mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }


    [Test]
    public void GetAdminById_ShouldReturnAdminFromContext()
    {
        // Arrange
        var adminId = 1;
        var expectedAdmin = new Admin { Id = adminId, /* set other admin properties */ };
        mockContext.Setup(c => c.Admins.Find(adminId)).Returns(expectedAdmin);

        // Act
        var result = adminRepository.GetAdminById(adminId);

        // Assert
        Assert.AreEqual(expectedAdmin, result);
    }

    // Similar test cases for other AdminRepository methods...
}

[TestFixture]
public class CustomerRepositoryTests
{
    private Mock<InsuranceDbContext> mockContext;
    private ICustomerRepository customerRepository;

    [SetUp]
    public void Setup()
    {
        mockContext = new Mock<InsuranceDbContext>();
        customerRepository = new CustomerRepository(mockContext.Object);
    }

    [Test]
    public void CreateCustomer_ShouldAddCustomerToContext()
    {
        // Arrange
        var customer = new Customer { /* set customer properties */ };

        // Act
        customerRepository.CreateCustomer(customer);

        // Assert
        mockContext.Verify(c => c.Customers.Add(It.IsAny<Customer>()), Times.Once);
        mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Test]
    public void GetCustomerById_ShouldReturnCustomerFromContext()
    {
        // Arrange
        var customerId = 1;
        var expectedCustomer = new Customer { Id = customerId, /* set other customer properties */ };
        mockContext.Setup(c => c.Customers.Find(customerId)).Returns(expectedCustomer);

        // Act
        var result = customerRepository.GetCustomerById(customerId);

        // Assert
        Assert.AreEqual(expectedCustomer, result);
    }

    // Similar test cases for other CustomerRepository methods...
}
