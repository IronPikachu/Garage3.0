#nullable disable
using Garage_2._0;
using Garage_2._0.Models;
using Microsoft.EntityFrameworkCore;

public class GarageVehicleContext : DbContext
{
    private Random gen = new Random();
    public GarageVehicleContext(DbContextOptions<GarageVehicleContext> options)
        : base(options)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
        Global.Garagecapacity = config.GetValue<int>("GarageCapacity:Capacity");
    }

    public DbSet<Vehicle> Vehicle { get; set; }
    public DbSet<Member> Member { get; set; }
    public DbSet<VehicleType> VehicleType { get; set; }
    public DbSet<Membership> Membership { get; set; }
    public DbSet<MemberHasMembership> MemberHasMembership { get; set; }
    public DbSet<ParkingSpot> ParkinSpot { get; set; }

    //adds seed data to the database
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Member>()
                    .OwnsOne(m => m.Name)
                    .Property(n => n.FirstName)
                    .HasColumnName("FirstName");
        modelBuilder.Entity<Member>()
                    .OwnsOne(m => m.Name)
                    .Property(n => n.LastName)
                    .HasColumnName("LastName");

        modelBuilder.Entity<Member>()
                    .HasMany(m => m.Vehicles);
        
        modelBuilder.Entity<ParkingSpot>()
                    .HasData(
                    GetParkingSpots());
        modelBuilder.Entity<Membership>()
                    .HasData(
                     GetMemberShips());
        modelBuilder.Entity<VehicleType>()
                    .HasData(
                     GetVehicleTypes());
    }
    private static IEnumerable<ParkingSpot> GetParkingSpots()
    {
        var parkingSpots = new List<ParkingSpot>();
        for (int i = 0; i < Global.Garagecapacity; i++)
        {
            var spot = new ParkingSpot
            {
                Id = i + 1,
                Available = true
            };
            parkingSpots.Add(spot);
        }
        return parkingSpots;
    }
    private static IEnumerable<Membership> GetMemberShips()
    {
        var memberships = new List<Membership>();
        Membership standard = new Membership
        {
            Type = "Standard",
            BenefitBase = 1d,
            BenefitHourly = 1d

        };
        memberships.Add(standard);

        Membership pro = new Membership
        {
            Type = "Pro",
            BenefitBase = 0.9d,
            BenefitHourly = 0.9d
        };
        memberships.Add(pro);

        return memberships;
    }
    private static IEnumerable<VehicleType> GetVehicleTypes()
    {
        var vehicleTypes = new List<VehicleType>();
        var car = new VehicleType
        {
            Name = "Car",
            Description = "The regular everyday vehicle most commonly used by people to travel both short and long distances",
            Size = 1
        };
        vehicleTypes.Add(car);
        var bus = new VehicleType
        {
            Name = "Bus",
            Description = "Bigger type of transportation that takes over 6 people",
            Size = 1
        };
        vehicleTypes.Add(bus);
        var motorcycle = new VehicleType
        {
            Name = "Motorcycle",
            Description = "A two wheeled vehicle that makes the owner respected in certain communities",
            Size = 1
        };
        vehicleTypes.Add(motorcycle);
        var zeppelin = new VehicleType
        {
            Name = "Zeppelin",
            Description = "An airship in very limited edition",
            Size = 1
        };
        vehicleTypes.Add(zeppelin);
        var bananamobile = new VehicleType
        {
            Name = "Bananamobile",
            Description = "Dimitris main way of transport, unmatched by any other vehicle. Aquatic, airborne and an atv all at once!",
            Size = 1
        };
        vehicleTypes.Add(bananamobile);

        return vehicleTypes;
    }
    
}
