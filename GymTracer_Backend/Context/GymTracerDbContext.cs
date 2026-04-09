using System.Text.Json;
using GymTracer.Converters;
using GymTracer.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace GymTracer.Context
{
    public partial class GymTracerDbContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<TrainingUser> TrainingUsers { get; set; }
        public DbSet<UsageLog> UsageLogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserTicket> UserTickets { get; set; }
        public GymTracerDbContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#if DEBUG
                optionsBuilder.UseMySQL("server=localhost;database=gamesapi;uid=root;pwd=;");
#endif
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(user => user.Email).IsUnique();

            modelBuilder.Entity<Payment>().HasIndex(payment => payment.ReceiptNumber).IsUnique();

            modelBuilder.Entity<Token>().HasIndex(token => token.TokenString).IsUnique();

            modelBuilder.Entity<Card>().HasIndex(card => card.Code).IsUnique();

            modelBuilder.Entity<TrainingUser>().HasIndex(t => new { t.TrainingId, t.UserId }).IsUnique();

            modelBuilder.Entity<Ticket>().Property(t => t.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<Ticket>().HasIndex(t => t.IsActive);
            modelBuilder.Entity<Ticket>().ToTable(t => t.HasCheckConstraint("Tax_key_positive", "\"Tax_key\" >= 0"));

            modelBuilder.Entity<UserTicket>().HasIndex(ut => ut.PaymentId).IsUnique();

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new IntToBoolConverter());

            var cards = JsonSerializer.Deserialize<List<Card>>(File.ReadAllText("ExampleData/Cards.json"), options) ?? [];
            modelBuilder.Entity<Card>().HasData(cards);

            var payments = JsonSerializer.Deserialize<List<Payment>>(File.ReadAllText("ExampleData/Payments.json"), options) ?? [];
            modelBuilder.Entity<Payment>().HasData(payments);

            var tickets = JsonSerializer.Deserialize<List<Ticket>>(File.ReadAllText("ExampleData/Tickets.json"), options) ?? [];
            modelBuilder.Entity<Ticket>().HasData(tickets);

            var tokens = JsonSerializer.Deserialize<List<Token>>(File.ReadAllText("ExampleData/Tokens.json"), options) ?? [];
            modelBuilder.Entity<Token>().HasData(tokens);

            var trainings = JsonSerializer.Deserialize<List<Training>>(File.ReadAllText("ExampleData/Trainings.json"), options) ?? [];
            modelBuilder.Entity<Training>().HasData(trainings);

            var trainingUsers = JsonSerializer.Deserialize<List<TrainingUser>>(File.ReadAllText("ExampleData/TrainingUsers.json"), options) ?? [];
            modelBuilder.Entity<TrainingUser>().HasData(trainingUsers);

            var usageLogs = JsonSerializer.Deserialize<List<UsageLog>>(File.ReadAllText("ExampleData/UsageLogs.json"), options) ?? [];
            modelBuilder.Entity<UsageLog>().HasData(usageLogs);

            var users = JsonSerializer.Deserialize<List<User>>(File.ReadAllText("ExampleData/Users.json"), options) ?? [];
            modelBuilder.Entity<User>().HasData(users);

            var userTickets = JsonSerializer.Deserialize<List<UserTicket>>(File.ReadAllText("ExampleData/UserTickets.json"), options) ?? [];
            modelBuilder.Entity<UserTicket>().HasData(userTickets);


        }
    }
}
