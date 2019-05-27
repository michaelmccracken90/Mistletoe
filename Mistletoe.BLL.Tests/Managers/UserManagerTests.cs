namespace Mistletoe.BLL.Managers.Tests
{
    using AutoFixture;
    using KellermanSoftware.CompareNetObjects;
    using Mistletoe.Models;
    using Mistletoe.DAL;
    using Mistletoe.DAL.Interfaces;
    using Mistletoe.Entities;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Xunit;
    using Mistletoe.Common;

    public class UserManagerTests : IDisposable
    {
        private List<UserModel> allUserModels;
        private List<UserModel> activeUserModels;
        private static UserModel userModel1;
        private static UserModel userModel2;

        private User user1;
        private User user2;

        private Mock<IRepository<User>> mockUserRepository;

        public UserManagerTests() 
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<string, int>().ConvertUsing(stringValue => Convert.ToInt32(stringValue));
                config.CreateMap<int, string>().ConvertUsing(intValue => Convert.ToString(intValue));
                config.CreateMap<Campaign, CampaignModel>().ReverseMap();
                config.CreateMap<Template, TemplateModel>().ReverseMap();
                config.CreateMap<User, UserModel>().ReverseMap();
                config.CreateMap<Email_Address, EmailAddressModel>().ReverseMap();
                config.CreateMap<Template_Email_Addresses, TemplateEmailAddressesModel>().ReverseMap();
            });

            userModel1 = new UserModel
            {
                UserName = "Superman",
                Email = "superman@heroes.com",
                IsActive = true,
                IsAdmin = true,
                UserID = "User1"
            };

            userModel2 = new UserModel
            {
                UserName = "Wonderwoman",
                Email = "wonderwoman@heroes.com",
                IsActive = false,
                IsAdmin = true,
                UserID = "User2"
            };

            user1 = new User
            {
                UserName = "Superman",
                Email = "superman@heroes.com",
                IsActive = true,
                IsAdmin = true,
                UserID = "User1"
            };

            user2 = new User
            {
                UserName = "Wonderwoman",
                Email = "wonderwoman@heroes.com",
                IsActive = false,
                IsAdmin = true,
                UserID = "User2"
            };

            allUserModels = new List<UserModel> { userModel1, userModel2 };

            activeUserModels = new List<UserModel> { userModel1 };

            var mockCampaignRepository = new Mock<IRepository<Campaign>>();
            var mockTemplateRepository = new Mock<IRepository<Template>>();

            mockUserRepository = new Mock<IRepository<User>>();
            var mockEmailAddressRepository = new Mock<IRepository<Email_Address>>();
            var mockTemplateEmailAddressesRepository = new Mock<IRepository<Template_Email_Addresses>>();

            var mockContext = new Mock<MistletoeDataEntities>();

            Helper.UnitOfWorkInstance = new UnitOfWork(mockContext.Object,
                                                       mockCampaignRepository.Object,
                                                       mockTemplateRepository.Object,
                                                       mockUserRepository.Object,
                                                       mockEmailAddressRepository.Object,
                                                       mockTemplateEmailAddressesRepository.Object);
        }

        private Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

            return dbSetMock;
        }

        public void Dispose()
        {
            allUserModels = null;
            activeUserModels = null;
            AutoMapper.Mapper.Reset();
        }        

        [Fact()]
        public void GetActiveUserModelsTest()
        {
            // Arrange
            var actualUserManager = new UserManager(allUserModels);

            // Act
            var actualUserModels = actualUserManager.GetActiveUserModels();

            // Assert            
            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(actualUserModels, activeUserModels);
            Assert.True(result.AreEqual);
        }

        [Fact()]
        public void GetActiveUserModelsInvalidTest()
        {
            // Arrange
            var actualUserManager = new UserManager(null);
            
            // Act
            var actualUserModels = actualUserManager.GetActiveUserModels();

            // Assert            
            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(actualUserModels, activeUserModels);
            Assert.False(result.AreEqual);
        }

        [Fact()]
        public void GetAllUserModelsTest()
        {
            // Arrange
            var actualUserManager = new UserManager(allUserModels);

            // Act
            var actualAllUserModels = actualUserManager.GetAllUserModels();

            // Assert            
            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(actualAllUserModels, allUserModels);
            Assert.True(result.AreEqual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("0")]
        public void IsUserAdminInvalidTest(string userId)
        {
            // Arrange
            var userManager = new UserManager(allUserModels);

            // Act
            var isUserAdmin = userManager.IsUserAdmin(userId);

            // Assert
            Assert.False(isUserAdmin);
        }

        [Theory]
        [InlineData("User1")]
        [InlineData("User2")]
        public void IsUserAdminValidTest(string userId)
        {
            // Arrange
            var userManager = new UserManager(allUserModels);

            // Act
            var isUserAdmin = userManager.IsUserAdmin(userId);

            // Assert
            Assert.True(isUserAdmin);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("User0")]
        public void UserExistsInvalidTest(string userId)
        {
            // Arrange
            var userManager = new UserManager(allUserModels);

            // Act
            var userExists = userManager.UserExists(userId);

            // Assert
            Assert.False(userExists);
        }

        [Theory]
        [InlineData("User1")]
        [InlineData("User2")]
        public void UserExistsValidTest(string userId)
        {
            // Arrange
            var userManager = new UserManager(allUserModels);

            // Act
            var userExists = userManager.UserExists(userId);

            // Assert
            Assert.True(userExists);
        }

        public static IEnumerable<object[]> UserModelTestData = new List<object[]>
        {
            new object[] 
            {
                new UserModel
                    {
                        UserName = "SupportMaL",
                        Email = "support@mossandlichens.com",
                        IsActive = true,
                        IsAdmin = true,
                        UserID = "User3"
                    }
            }
        };

        [Theory]
        [MemberData(nameof(UserModelTestData))]
        public void AddUserModelValidTest(UserModel NewUser)
        {
            // Arrange
            var actualUserManager = new UserManager(new List<UserModel> { userModel1 });
            mockUserRepository.Setup(x => x.GetByID("User1")).Returns(user1);

            // Act            
            var userModelAdded = actualUserManager.AddUserModel(NewUser);

            // Assert
            Assert.True(userModelAdded);
        }

        [Theory]
        [InlineData(null)]
        public void AddUserModelInvalidNullTest(UserModel NewUser)
        {
            // Arrange
            var actualUserManager = new UserManager(new List<UserModel> { NewUser });
            
            // Act            
            var userModelAdded = actualUserManager.AddUserModel(NewUser);

            // Assert
            Assert.False(userModelAdded);
        }

        [Theory]
        [MemberData(nameof(UserModelTestData))]
        public void AddUserModelInvalidTest(UserModel NewUser)
        {
            // Arrange
            var actualUserManager = new UserManager(new List<UserModel> { NewUser });
            mockUserRepository.Setup(x => x.GetByID("User3")).Throws(new Exception("Duplicate key"));

            // Act            
            var userModelAdded = actualUserManager.AddUserModel(NewUser);

            // Assert
            Assert.False(userModelAdded);
        }

        [Theory]
        [MemberData(nameof(UserModelTestData))]
        public void UpdateUserModelTest(UserModel UserToUpdate)
        {
            // Arrange
            var actualUserManager = new UserManager(new List<UserModel> { UserToUpdate });

            // Act            
            var userModelUpdated = actualUserManager.UpdateUserModel(UserToUpdate);

            // Assert
            Assert.True(userModelUpdated);
        }

        [Theory]
        [InlineData("User1", false)]
        public void ChangeUserStatusTest(string UserID, bool newStatus)
        {
            // Arrange
            var actualUserManager = new UserManager(new List<UserModel> { userModel1 });

            // Act            
            var changeUserStatus = actualUserManager.ChangeUserStatus(UserID, newStatus);

            // Assert
            Assert.True(changeUserStatus);
        }

        [Theory]
        [InlineData("User1")]
        public void DeleteUserModelTest(string UserID)
        {
            // Arrange
            var actualUserManager = new UserManager(new List<UserModel> { userModel1 });

            // Act            
            var deleteUserModel = actualUserManager.DeleteUserModel(UserID);

            // Assert
            Assert.True(deleteUserModel);
        }
    }
}
