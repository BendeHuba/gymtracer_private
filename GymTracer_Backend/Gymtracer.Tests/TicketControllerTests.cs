using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using GymTracer.Auth;
using GymTracer.Context;
using GymTracer.Controllers;
using GymTracer.Exceptions;
using GymTracer.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Gymtracer.Tests
{
    public class TicketControllerTests
    {
        protected ControllerContext GetControllerContext(string id)
        {
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, id) };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var principal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext() { User = principal };
            return new ControllerContext() { HttpContext = httpContext };
        }

        private TicketController CreateController(GymTracerDbContext db, TokenHandler tokenHandler, Claim[]? claims = null)
        {
            var controller = new TicketController(db, tokenHandler);
            if (claims != null && claims.Length > 0)
            {
                var identity = new ClaimsIdentity(claims, "TestAuth");
                var principal = new ClaimsPrincipal(identity);
                controller.ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = principal }
                };
            }
            else
            {
                var identity = new ClaimsIdentity();
                var principal = new ClaimsPrincipal(identity);
                controller.ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = principal }
                };
            }
            return controller;
        }

        private GymTracerDbContext GetDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<GymTracerDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var db = new GymTracerDbContext(options);
            db.Database.EnsureCreated();
            
            db.Set<UserTicket>().RemoveRange(db.Set<UserTicket>());
            db.Set<Ticket>().RemoveRange(db.Set<Ticket>());
            db.Set<Training>().RemoveRange(db.Set<Training>());
            db.Set<Payment>().RemoveRange(db.Set<Payment>());
            db.Set<User>().RemoveRange(db.Set<User>());
            db.SaveChanges();
            
            return db;
        }

        private TokenHandler GetTokenHandlerMock(DateTime now)
        {
            var optionsMonitor = new Mock<Microsoft.Extensions.Options.IOptions<AuthOptions>>();
            optionsMonitor.Setup(o => o.Value).Returns(new AuthOptions { ExpirationInMinutes = 60, TokenLength = 32 });
            return new TokenHandler(optionsMonitor.Object);
        }

        [Fact]
        public void GetAllTickets_ShouldReturn200_WithValidTickets()
        {
            var dbName = Guid.NewGuid().ToString();
            using var db = GetDbContext(dbName);
            db.Set<Ticket>().Add(new Ticket { Id = 1001, Type = Ticket_Type.daily, Description = "D", Price = 10, MaxUsage = 1 });
            db.SaveChanges();
            var controller = CreateController(db, GetTokenHandlerMock(DateTime.UtcNow));

            var result = controller.GetAllTickets() as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetAllTickets_FiltersOutInactiveTrainings()
        {
            var dbName = Guid.NewGuid().ToString();
            using var db = GetDbContext(dbName);
            var training = new Training { Id = 1001, Name = "T", Description = "D", Image = "I", Active = false, EndTime = DateTime.UtcNow.AddDays(1) };
            db.Set<Training>().Add(training);
            db.Set<Ticket>().Add(new Ticket { Id = 1002, Description = "D", TrainingId = 1001, Training = training });
            db.SaveChanges();
            var controller = CreateController(db, GetTokenHandlerMock(DateTime.UtcNow));

            var result = controller.GetAllTickets() as ObjectResult;
            var values = result.Value as IEnumerable<object>;

            Assert.Empty(values);
        }

        [Fact]
        public void GetAllTickets_IncludesTicketsWithoutTraining()
        {
            var dbName = Guid.NewGuid().ToString();
            using var db = GetDbContext(dbName);
            db.Set<Ticket>().Add(new Ticket { Id = 1001, Description = "D" });
            db.SaveChanges();
            var controller = CreateController(db, GetTokenHandlerMock(DateTime.UtcNow));

            var result = controller.GetAllTickets() as ObjectResult;
            var values = result.Value as IEnumerable<object>;

            Assert.Single(values);
        }

        [Fact]
        public void GetAllTickets_FiltersOutPastTrainings()
        {
            var dbName = Guid.NewGuid().ToString();
            using var db = GetDbContext(dbName);
            var training = new Training { Id = 1001, Name = "T", Description = "D", Image = "I", Active = true, EndTime = DateTime.UtcNow.AddDays(-1) };
            db.Set<Training>().Add(training);
            db.Set<Ticket>().Add(new Ticket { Id = 1002, Description = "D", TrainingId = 1001, Training = training });
            db.SaveChanges();
            var controller = CreateController(db, GetTokenHandlerMock(DateTime.UtcNow));

            var result = controller.GetAllTickets() as ObjectResult;
            var values = result.Value as IEnumerable<object>;

            Assert.Empty(values);
        }

        [Fact]
        public void GetTicketsOfAUser_Throws401_WhenUnauthorized()
        {
            var dbName = Guid.NewGuid().ToString();
            using var db = GetDbContext(dbName);
            db.Set<User>().Add(new User { Id = 1001, Role = User_Role.customer, Password = "P", Email = "E1001", Name = "N" });
            db.Set<User>().Add(new User { Id = 1002, Role = User_Role.customer, Password = "P", Email = "E1002", Name = "N2" });
            db.SaveChanges();
            
            var mockTokenHandler = GetTokenHandlerMock(DateTime.UtcNow);
            
            var controller = CreateController(db, mockTokenHandler);
            controller.ControllerContext = GetControllerContext("1001");

            var result = controller.GetTicketsOfAUser(1002) as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
        }

        [Fact]
        public void GetTicketsOfAUser_ReturnsValidTickets()
        {
            var dbName = Guid.NewGuid().ToString();
            using var db = GetDbContext(dbName);
            var user = new User { Id = 1001, Role = User_Role.customer, Password = "P", Email = "E1001", Name = "N" };
            db.Set<User>().Add(user);
            var payment = new Payment { Id = 1001, TotalPrice = 10, ReceiptNumber = "REC-1001", IssuerId = 1001 };
            var ticket = new Ticket { Id = 1001, Type = Ticket_Type.daily, Description = "D", MaxUsage = 1 };
            db.Set<UserTicket>().Add(new UserTicket { UserId = 1001, PaymentId = 1001, TicketId = 1001, ExpirationDate = DateTime.UtcNow.AddDays(1), Payment = payment, Ticket = ticket });
            db.SaveChanges();

            var controller = CreateController(db, GetTokenHandlerMock(DateTime.UtcNow), new[] { new Claim(ClaimTypes.NameIdentifier, "1001") });
            var result = controller.GetTicketsOfAUser(1001) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void GetTicketsOfAUser_FiltersExpriredMonthlyTickets()
        {
            var dbName = Guid.NewGuid().ToString();
            using var db = GetDbContext(dbName);
            var user = new User { Id = 1001, Role = User_Role.customer, Password = "P", Email = "E1001", Name = "N" };
            db.Set<User>().Add(user);
            var payment = new Payment { Id = 1001, TotalPrice = 10, ReceiptNumber = "REC-1001", IssuerId = 1001 };
            var ticket = new Ticket { Id = 1001, Type = Ticket_Type.monthly, Description = "D", MaxUsage = 100 };
            db.Set<UserTicket>().Add(new UserTicket { UserId = 1001, PaymentId = 1001, TicketId = 1001, ExpirationDate = DateTime.UtcNow.AddDays(-1), Payment = payment, Ticket = ticket });
            db.SaveChanges();

            var controller = CreateController(db, GetTokenHandlerMock(DateTime.UtcNow), new[] { new Claim(ClaimTypes.NameIdentifier, "1001") });
            var result = controller.GetTicketsOfAUser(1001) as ObjectResult;
            Assert.Empty(result.Value as IEnumerable<object>);
        }

        [Fact]
        public void GetTicketsOfAUser_FiltersFullyUsedTickets()
        {
            var dbName = Guid.NewGuid().ToString();
            using var db = GetDbContext(dbName);
            var user = new User { Id = 1001, Role = User_Role.customer, Password = "P", Email = "E1001", Name = "N" };
            db.Set<User>().Add(user);
            var payment = new Payment { Id = 1001, TotalPrice = 10, ReceiptNumber = "REC-1001", IssuerId = 1001 };
            var ticket = new Ticket { Id = 1001, Type = Ticket_Type.x_usage, Description = "D", MaxUsage = 5 };
            db.Set<UserTicket>().Add(new UserTicket { UserId = 1001, PaymentId = 1001, TicketId = 1001, UsageAmount = 5, ExpirationDate = DateTime.UtcNow.AddDays(1), Payment = payment, Ticket = ticket });
            db.SaveChanges();

            var controller = CreateController(db, GetTokenHandlerMock(DateTime.UtcNow), new[] { new Claim(ClaimTypes.NameIdentifier, "1001") });
            var result = controller.GetTicketsOfAUser(1001) as ObjectResult;
            Assert.Empty(result.Value as IEnumerable<object>);
        }

        [Fact]
        public void GetUnpaidTIcketsOfAUser_Throws401_WhenUnauthorized()
        {
            var dbName = Guid.NewGuid().ToString();
            using var db = GetDbContext(dbName);
            db.Set<User>().Add(new User { Id = 1001, Role = User_Role.customer, Password = "P", Email = "E1001", Name = "N" });
            db.Set<User>().Add(new User { Id = 1002, Role = User_Role.customer, Password = "P", Email = "E1002", Name = "N2" });
            db.SaveChanges();
            
            var mockTokenHandler = GetTokenHandlerMock(DateTime.UtcNow);
            var controller = CreateController(db, mockTokenHandler, new[] { new Claim(ClaimTypes.NameIdentifier, "1001") });
            
            var result = controller.GetUnpaidTIcketsOfAUser(1002) as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
        }

        [Fact]
        public void GetUnpaidTIcketsOfAUser_ReturnsUnpaidTickets()
        {
            var dbName = Guid.NewGuid().ToString();
            using var db = GetDbContext(dbName);
            var user = new User { Id = 1001, Role = User_Role.customer, Password = "P", Email = "E1001", Name = "N" };
            db.Set<User>().Add(user);
            var payment = new Payment { Id = 1001, DueDate = DateTime.UtcNow.AddDays(1), ReceiptNumber = "REC-1001", IssuerId = 1001 };
            var ticket = new Ticket { Id = 1001, Description = "D" };
            db.Set<UserTicket>().Add(new UserTicket { UserId = 1001, PaymentId = 1001, TicketId = 1001, Payment = payment, Ticket = ticket });
            db.SaveChanges();

            var controller = CreateController(db, GetTokenHandlerMock(DateTime.UtcNow), new[] { new Claim(ClaimTypes.NameIdentifier, "1001") });
            var result = controller.GetUnpaidTIcketsOfAUser(1001) as ObjectResult;
            var values = result.Value as IEnumerable<object>;

            Assert.Single(values);
        }

        [Fact]
        public void GetUnpaidTIcketsOfAUser_FiltersPaidTickets()
        {
            var dbName = Guid.NewGuid().ToString();
            using var db = GetDbContext(dbName);
            var user = new User { Id = 1001, Role = User_Role.customer, Password = "P", Email = "E1001", Name = "N" };
            db.Set<User>().Add(user);
            var payment = new Payment { Id = 1001, PaymentDate = DateTime.UtcNow, DueDate = DateTime.UtcNow.AddDays(1), ReceiptNumber = "REC-1001", IssuerId = 1001 };
            var ticket = new Ticket { Id = 1001, Description = "D" };
            db.Set<UserTicket>().Add(new UserTicket { UserId = 1001, PaymentId = 1001, TicketId = 1001, Payment = payment, Ticket = ticket });
            db.SaveChanges();

            var controller = CreateController(db, GetTokenHandlerMock(DateTime.UtcNow), new[] { new Claim(ClaimTypes.NameIdentifier, "1001") });
            var result = controller.GetUnpaidTIcketsOfAUser(1001) as ObjectResult;
            Assert.Empty(result.Value as IEnumerable<object>);
        }

        [Fact]
        public void GetUnpaidTIcketsOfAUser_FiltersPastDueDate()
        {
            var dbName = Guid.NewGuid().ToString();
            using var db = GetDbContext(dbName);
            var user = new User { Id = 1001, Role = User_Role.customer, Password = "P", Email = "E1001", Name = "N" };
            db.Set<User>().Add(user);
            var payment = new Payment { Id = 1001, DueDate = DateTime.UtcNow.AddDays(-1), ReceiptNumber = "REC-1001", IssuerId = 1001 };
            var ticket = new Ticket { Id = 1001, Description = "D" };
            db.Set<UserTicket>().Add(new UserTicket { UserId = 1001, PaymentId = 1001, TicketId = 1001, Payment = payment, Ticket = ticket });
            db.SaveChanges();

            var controller = CreateController(db, GetTokenHandlerMock(DateTime.UtcNow), new[] { new Claim(ClaimTypes.NameIdentifier, "1001") });
            var result = controller.GetUnpaidTIcketsOfAUser(1001) as ObjectResult;
            Assert.Empty(result.Value as IEnumerable<object>);
        }

        [Fact]
        public void PostTicketAndPayment_Throws401_WhenUnauthorized()
        {
            var dbName = Guid.NewGuid().ToString();
            using var db = GetDbContext(dbName);
            db.Set<User>().Add(new User { Id = 1001, Role = User_Role.customer, Password = "P", Email = "E1001", Name = "N" });
            db.Set<User>().Add(new User { Id = 1002, Role = User_Role.customer, Password = "P", Email = "E1002", Name = "N2" });
            db.SaveChanges();
            
            var mockTokenHandler = GetTokenHandlerMock(DateTime.UtcNow);
            var controller = CreateController(db, mockTokenHandler, new[] { new Claim(ClaimTypes.NameIdentifier, "1001") });
            
            var result = controller.PostTicketAndPayment(1002, 1001, true) as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
        }

        [Fact]
        public void PostTicketAndPayment_Throws400_WhenNoTicketFound()
        {
            var dbName = Guid.NewGuid().ToString();
            using var db = GetDbContext(dbName);
            var user = new User { Id = 1001, Role = User_Role.customer, Password = "P", Email = "E1001", Name = "N" };
            db.Set<User>().Add(user);
            db.SaveChanges();

            var controller = CreateController(db, GetTokenHandlerMock(DateTime.UtcNow), new[] { new Claim(ClaimTypes.NameIdentifier, "1001") });
            var result = controller.PostTicketAndPayment(1001, 1001, true) as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void PostTicketAndPayment_CreatesPaidPaymentAndTicket()
        {
            var dbName = Guid.NewGuid().ToString();
            using var db = GetDbContext(dbName);
            var user = new User { Id = 1001, Role = User_Role.customer, Password = "P", Email = "E1001", Name = "N" };
            db.Set<User>().Add(user);
            db.Set<Payment>().Add(new Payment { Id = 1001, ReceiptNumber = "REC-1001", IssuerId = 1001 });
            db.Set<Ticket>().Add(new Ticket { Id = 1001, Type = Ticket_Type.monthly, Price = 100, Description = "D", Tax_key = 27 });
            db.SaveChanges();

            var controller = CreateController(db, GetTokenHandlerMock(DateTime.UtcNow), new[] { new Claim(ClaimTypes.NameIdentifier, "1001") });
            var result = controller.PostTicketAndPayment(1001, 1001, true) as ObjectResult;

            Assert.Equal(201, result.StatusCode);
            var ut = db.Set<UserTicket>().FirstOrDefault(ut => ut.UserId == 1001);
            Assert.NotNull(ut);
            var p = db.Set<Payment>().OrderByDescending(x => x.Id).First();
            Assert.NotNull(p.PaymentDate);
        }

        [Fact]
        public void PostTicketAndPayment_CreatesUnpaidPaymentAndTicket()
        {
            var dbName = Guid.NewGuid().ToString();
            using var db = GetDbContext(dbName);
            var user = new User { Id = 1001, Role = User_Role.customer, Password = "P", Email = "E1001", Name = "N" };
            db.Set<User>().Add(user);
            db.Set<Payment>().Add(new Payment { Id = 1001, ReceiptNumber = "REC-1001", IssuerId = 1001 });
            db.Set<Ticket>().Add(new Ticket { Id = 1001, Type = Ticket_Type.monthly, Price = 100, Description = "D", Tax_key = 27 });
            db.SaveChanges();

            var controller = CreateController(db, GetTokenHandlerMock(DateTime.UtcNow), new[] { new Claim(ClaimTypes.NameIdentifier, "1001") });
            var result = controller.PostTicketAndPayment(1001, 1001, false) as ObjectResult;

            Assert.Equal(201, result.StatusCode);
            var p = db.Set<Payment>().OrderByDescending(x => x.Id).First();
            Assert.Null(p.PaymentDate);
        }

        [Fact]
        public void PatchPayment_Throws401_WhenUnauthorized()
        {
            var dbName = Guid.NewGuid().ToString();
            using var db = GetDbContext(dbName);
            db.Set<User>().Add(new User { Id = 1001, Role = User_Role.customer, Password = "P", Email = "E1001", Name = "N" });
            db.Set<User>().Add(new User { Id = 1002, Role = User_Role.customer, Password = "P", Email = "E1002", Name = "N2" });
            db.SaveChanges();
            
            var mockTokenHandler = GetTokenHandlerMock(DateTime.UtcNow);
            var controller = CreateController(db, mockTokenHandler);
            controller.ControllerContext = GetControllerContext("1001");
            
            var result = controller.PatchPayment(1002, 1001) as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
        }

        [Fact]
        public void PatchPayment_Throws_WhenPaymentNotFound()
        {
            var dbName = Guid.NewGuid().ToString();
            using var db = GetDbContext(dbName);
            var user = new User { Id = 1001, Role = User_Role.customer, Password = "P", Email = "E1001", Name = "N" };
            db.Set<User>().Add(user);
            db.SaveChanges();

            var controller = CreateController(db, GetTokenHandlerMock(DateTime.UtcNow), new[] { new Claim(ClaimTypes.NameIdentifier, "1001") });

            var result = controller.PatchPayment(1001, 1001) as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void PatchPayment_Throws400_WhenAlreadyPaid()
        {
            var dbName = Guid.NewGuid().ToString();
            using var db = GetDbContext(dbName);
            var user = new User { Id = 1001, Role = User_Role.customer, Password = "P", Email = "E1001", Name = "N" };
            db.Set<User>().Add(user);
            var payment = new Payment { Id = 1001, PaymentDate = DateTime.UtcNow, ReceiptNumber = "REC-1001", IssuerId = 1001 };
            var ticket = new Ticket { Id = 1001, Description = "D", Tax_key = 27 };
            db.Set<UserTicket>().Add(new UserTicket { UserId = 1001, PaymentId = 1001, Payment = payment, Ticket = ticket });
            db.SaveChanges();

            var controller = CreateController(db, GetTokenHandlerMock(DateTime.UtcNow));
            controller.ControllerContext = GetControllerContext("1001");
            var result = controller.PatchPayment(1001, 1001) as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void PatchPayment_PaysSuccessfully()
        {
            var dbName = Guid.NewGuid().ToString();
            using var db = GetDbContext(dbName);
            var user = new User { Id = 1001, Role = User_Role.customer, Password = "P", Email = "E1001", Name = "N" };
            db.Set<User>().Add(user);
            var payment = new Payment { Id = 1001, PaymentDate = null, ReceiptNumber = "REC-1001", IssuerId = 1001 };
            var ticket = new Ticket { Id = 1001, Description = "D", Tax_key = 27 };
            db.Set<UserTicket>().Add(new UserTicket { UserId = 1001, PaymentId = 1001, Payment = payment, Ticket = ticket });
            db.SaveChanges();

            var controller = CreateController(db, GetTokenHandlerMock(DateTime.UtcNow), new[] { new Claim(ClaimTypes.NameIdentifier, "1001") });
            var result = controller.PatchPayment(1001, 1001) as ObjectResult;

            Assert.Equal(201, result.StatusCode);
            var p = db.Set<Payment>().Find(1001L);
            Assert.NotNull(p.PaymentDate);
        }
    }
}
