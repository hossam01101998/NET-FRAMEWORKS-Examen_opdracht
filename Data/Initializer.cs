using Garage2.Data;
using NET_FRAMEWORKS_EXAMEN_OPDRACHT.Models;

namespace NET_FRAMEWORKS_EXAMEN_OPDRACHT.Data
{
    public class Initializer
    {

        public static void DbSetInitializer(Garage2Context context)
        {
            Console.WriteLine("iniciando");

            context.Database.EnsureCreated();
            // Seeders

            if (!context.Customer.Any())
            {
                context.Add(new Customer { Name = "John", Email = "john@gmail.com", Adress = "Ijsstraat 23 A" });
                context.Add(new Customer { Name = "Richy", Email = "richy@gmail.com", Adress = "Palmstraat 3" });
                context.SaveChanges();
            }

            if (!context.Car.Any())
            {
                context.Add(new Car { Make = "BMW", Model = "M3", LicensePlate = "1-ABC-123", ChassisNumber = "123456789", CustomerId = 1 });
                context.Add(new Car { Make = "Audi", Model = "A3", LicensePlate = "1-DEF-456", ChassisNumber = "987654321", CustomerId = 1 });
                context.Add(new Car { Make = "Mercedes", Model = "C180", LicensePlate = "1-GHI-789", ChassisNumber = "123456789", CustomerId = 2 });
                context.Add(new Car { Make = "Volkswagen", Model = "Golf", LicensePlate = "1-JKL-012", ChassisNumber = "987654321", CustomerId = 2 });
                context.SaveChanges();
            }

            if (!context.Order.Any())
            {
                context.Add(new Order { CarID = 1, OrderDetails = "Change tyres" });
                context.Add(new Order { CarID = 1, OrderDetails = "Change timing belt" });
                context.Add(new Order { CarID = 2, OrderDetails = "Noise in the transmission" });
                context.Add(new Order { CarID = 2, OrderDetails = "Change oil and filters" });
                context.Add(new Order { CarID = 3, OrderDetails = "Change diesel filter" });
                context.Add(new Order { CarID = 3, OrderDetails = "Change brake pads" });
                context.Add(new Order { CarID = 4, OrderDetails = "Change oil and filters" });
                context.Add(new Order { CarID = 4, OrderDetails = "New wheels" });
                context.SaveChanges();
            }

            if (!context.Appointment.Any())
            {
                context.Add(new Appointment { CarID = 1, AppointmentDate = DateTime.Now.AddDays(1), Status = "Confirmed", RequiredService = "Regular checkup" });
                context.Add(new Appointment { CarID = 2, AppointmentDate = DateTime.Now.AddDays(2), Status = "Pending", RequiredService = "Oil change" });
                context.SaveChanges();
            }

            if (!context.Invoice.Any())
            {
                context.Add(new Invoice { CustomerID = 1, IssueDate = DateTime.Now, TotalAmount = 100.50m, Details = "Service invoice", CarID = 2 });
                context.Add(new Invoice { CustomerID = 2, IssueDate = DateTime.Now, TotalAmount = 75.25m, Details = "Parts replacement", CarID = 3 });
                context.SaveChanges();
            }

     

        }
    }
}
