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
                context.Add(new Customer { Name = "John", Email = "john@gmail.com", Adress = "Ijsstraat 23 A", PhoneNumber = "04254154554" });
                context.Add(new Customer { Name = "Richy", Email = "richy@gmail.com", Adress = "Palmstraat 3", PhoneNumber = "04254154554" });
                context.Add(new Customer { Name = "Alice", Email = "alice@yahoo.com", Adress = "Maple Lane 7", PhoneNumber = "04254154554" });
                context.Add(new Customer { Name = "Bob", Email = "bob@hotmail.com", Adress = "Oak Street 15", PhoneNumber = "04254154554" });
                context.Add(new Customer { Name = "Eva", Email = "eva@gmail.com", Adress = "Cedar Avenue 10", PhoneNumber = "04254154554" });
                context.Add(new Customer { Name = "David", Email = "david@gmail.com", Adress = "Willow Road 22", PhoneNumber = "04254154554" });
                context.Add(new Customer { Name = "Sophie", Email = "sophie@gmail.com", Adress = "Birch Court 8", PhoneNumber = "04254154554" });
                context.Add(new Customer { Name = "Alex", Email = "alex@gmail.com", Adress = "Pine Drive 14", PhoneNumber = "04254154554" });
                context.Add(new Customer { Name = "Grace", Email = "grace@gmail.com",Adress = "Elm Place 31", PhoneNumber = "04254154554" });
                context.Add(new Customer { Name = "Michael", Email = "michael@gmail.com", Adress = "Cypress Street 5", PhoneNumber = "04254154554" });

                context.SaveChanges();
            }


            if (!context.Car.Any())
            {
                context.Add(new Car { Make = "BMW", Model = "M3", LicensePlate = "1BGF154", ChassisNumber = "A4YBX457WE82SSD9W", CustomerId = 1 });
                context.Add(new Car { Make = "Audi", Model = "A3", LicensePlate = "1DTU366", ChassisNumber = "B2RTK987UH34WIE1E", CustomerId = 1 });
                context.Add(new Car { Make = "Mercedes", Model = "C180", LicensePlate = "1RHO339", ChassisNumber = "C9KLP456TG23XMN8Z", CustomerId = 2 });
                context.Add(new Car { Make = "Volkswagen", Model = "Golf", LicensePlate = "1JUL082", ChassisNumber = "D7QWE123VB56NMK0I", CustomerId = 2 });
                context.Add(new Car { Make = "Ford", Model = "Fiesta", LicensePlate = "2ABC456", ChassisNumber = "E1YXZ789CD23KLR45", CustomerId = 3 });
                context.Add(new Car { Make = "Chevrolet", Model = "Malibu", LicensePlate = "2DEF789", ChassisNumber = "F6GHI012JL34NPQR7", CustomerId = 3 });
                context.Add(new Car { Make = "Toyota", Model = "Camry", LicensePlate = "2GHI012", ChassisNumber = "K8MNOPQ9R01234567", CustomerId = 4 });
                context.Add(new Car { Make = "Honda", Model = "Accord", LicensePlate = "2JKL345", ChassisNumber = "S2ABCDEF4G567890H", CustomerId = 4 });
                context.Add(new Car { Make = "Nissan", Model = "Altima", LicensePlate = "2PQR678", ChassisNumber = "IJ56KLM78NO90PQR1", CustomerId = 5 });
                context.Add(new Car { Make = "Mazda", Model = "CX-5", LicensePlate = "2STU901", ChassisNumber = "X1YZA567BC2DEFG34", CustomerId = 5 });
                context.Add(new Car { Make = "Subaru", Model = "Outback", LicensePlate = "2VWX234", ChassisNumber = "HJ56KLMN7OP890QR1", CustomerId = 6 });
                context.Add(new Car { Make = "Kia", Model = "Sportage", LicensePlate = "2YZA567", ChassisNumber = "IJKL890123LMN4567", CustomerId = 6 });
                context.Add(new Car { Make = "Hyundai", Model = "Tucson", LicensePlate = "2BCD890", ChassisNumber = "OP9QRSTUVWXYZ12345A", CustomerId = 7 });
                context.Add(new Car { Make = "Volvo", Model = "XC60", LicensePlate = "2EFG123", ChassisNumber = "BCD6789012DEFG34", CustomerId = 7 });

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
                context.Add(new Order { CarID = 5, OrderDetails = "Inspect engine performance" });
                context.Add(new Order { CarID = 5, OrderDetails = "Replace spark plugs" });
                context.Add(new Order { CarID = 6, OrderDetails = "Check and adjust brakes" });
                context.Add(new Order { CarID = 6, OrderDetails = "Replace air filter" });
                context.Add(new Order { CarID = 7, OrderDetails = "Diagnose electrical issues" });
                context.Add(new Order { CarID = 7, OrderDetails = "Refill windshield washer fluid" });
                context.Add(new Order { CarID = 8, OrderDetails = "Alignment and balancing" });
                context.Add(new Order { CarID = 8, OrderDetails = "Check and adjust suspension" });
                context.Add(new Order { CarID = 9, OrderDetails = "Replace worn-out belts" });
                context.Add(new Order { CarID = 9, OrderDetails = "Inspect and replace shocks" });
                context.Add(new Order { CarID = 10, OrderDetails = "Complete engine tune-up" });
                context.Add(new Order { CarID = 10, OrderDetails = "Rotate and balance tires" });

                context.SaveChanges();
            }

            if (!context.Appointment.Any())
            {
                context.Add(new Appointment { CarID = 1, AppointmentDate = DateTime.Now.AddDays(1), Status = "Confirmed", RequiredService = "Regular checkup" });
                context.Add(new Appointment { CarID = 2, AppointmentDate = DateTime.Now.AddDays(2), Status = "Pending", RequiredService = "Oil change" });
                context.Add(new Appointment { CarID = 3, AppointmentDate = DateTime.Now.AddDays(3), Status = "Confirmed", RequiredService = "Brake inspection" });
                context.Add(new Appointment { CarID = 4, AppointmentDate = DateTime.Now.AddDays(4), Status = "Pending", RequiredService = "Transmission fluid change" });
                context.Add(new Appointment { CarID = 5, AppointmentDate = DateTime.Now.AddDays(5), Status = "Confirmed", RequiredService = "Wheel alignment" });
                context.Add(new Appointment { CarID = 6, AppointmentDate = DateTime.Now.AddDays(6), Status = "Pending", RequiredService = "Air conditioning check" });
                context.Add(new Appointment { CarID = 7, AppointmentDate = DateTime.Now.AddDays(7), Status = "Confirmed", RequiredService = "Diagnostic testing" });
                context.Add(new Appointment { CarID = 8, AppointmentDate = DateTime.Now.AddDays(8), Status = "Pending", RequiredService = "Battery replacement" });
                context.Add(new Appointment { CarID = 9, AppointmentDate = DateTime.Now.AddDays(9), Status = "Confirmed", RequiredService = "Exhaust system inspection" });
                context.Add(new Appointment { CarID = 10, AppointmentDate = DateTime.Now.AddDays(10), Status = "Pending", RequiredService = "Coolant flush" });
                context.Add(new Appointment { CarID = 1, AppointmentDate = DateTime.Now.AddDays(11), Status = "Confirmed", RequiredService = "Tire rotation" });
                context.Add(new Appointment { CarID = 2, AppointmentDate = DateTime.Now.AddDays(12), Status = "Pending", RequiredService = "Engine diagnostics" });

                context.SaveChanges();
            }

            if (!context.Invoice.Any())
            {
                context.Add(new Invoice { IssueDate = DateTime.Now, TotalAmount = 100.50m, Details = "Service invoice", CarID = 2 });
                context.Add(new Invoice { IssueDate = DateTime.Now, TotalAmount = 75.25m, Details = "Parts replacement", CarID = 3 });
                context.Add(new Invoice { IssueDate = DateTime.Now, TotalAmount = 120.75m, Details = "Major service", CarID = 4 });
                context.Add(new Invoice { IssueDate = DateTime.Now, TotalAmount = 50.00m, Details = "Minor repairs", CarID = 5 });
                context.Add(new Invoice { IssueDate = DateTime.Now, TotalAmount = 90.20m, Details = "Diagnostic fee", CarID = 6 });
                context.Add(new Invoice { IssueDate = DateTime.Now, TotalAmount = 200.00m, Details = "Complete overhaul", CarID = 7 });
                context.Add(new Invoice { IssueDate = DateTime.Now, TotalAmount = 80.50m, Details = "Brake system service", CarID = 8 });
                context.Add(new Invoice { IssueDate = DateTime.Now, TotalAmount = 60.75m, Details = "Electrical repairs", CarID = 9 });
                context.Add(new Invoice { IssueDate = DateTime.Now, TotalAmount = 150.30m, Details = "Suspension maintenance", CarID = 10 });
                context.Add(new Invoice { IssueDate = DateTime.Now.AddDays(15), TotalAmount = 95.00m, Details = "Exhaust system repairs", CarID = 1 });
                context.Add(new Invoice { IssueDate = DateTime.Now, TotalAmount = 110.00m, Details = "Tire replacement", CarID = 2 });
                context.Add(new Invoice { IssueDate = DateTime.Now.AddDays(2), TotalAmount = 70.50m, Details = "Oil change service", CarID = 3 });

                context.SaveChanges();
            }

            if (!context.Expense.Any())
            {
                context.Add(new Expense { Description = "Office supplies", Amount = 50.25m, Date = DateTime.Now.AddDays(-5) });
                context.Add(new Expense { Description = "Travel expenses", Amount = 120.50m, Date = DateTime.Now.AddDays(-10) });
                context.Add(new Expense { Description = "Equipment maintenance", Amount = 80.75m, Date = DateTime.Now.AddDays(-15) });
                context.Add(new Expense { Description = "Training materials", Amount = 35.00m, Date = DateTime.Now.AddDays(-20) });
                context.Add(new Expense { Description = "Software licenses", Amount = 150.30m, Date = DateTime.Now.AddDays(-25) });
                context.Add(new Expense { Description = "Advertisement costs", Amount = 200.00m, Date = DateTime.Now.AddDays(-30) });
                context.Add(new Expense { Description = "Utilities bill", Amount = 90.20m, Date = DateTime.Now.AddDays(-35) });
                context.Add(new Expense { Description = "Website hosting", Amount = 60.75m, Date = DateTime.Now.AddDays(-40) });
                context.Add(new Expense { Description = "Professional services", Amount = 110.00m, Date = DateTime.Now.AddDays(-45) });
                context.Add(new Expense { Description = "Miscellaneous expenses", Amount = 75.50m, Date = DateTime.Now.AddDays(-50) });

                context.SaveChanges();
            }


        }
    }
    
}
