using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NET_FRAMEWORKS_EXAMEN_OPDRACHT.Models
{
    public class Invoice
    {

        public int InvoiceId { get; set; }
        [ForeignKey("Car")]
        public int CarID { get; set; }

        [ForeignKey("Customer")]
        public int CustomerID { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime IssueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Details { get; set; }

        public Customer Customer { get; set; }
        public Car Car { get; set; }

        public Invoice()
        {
         
        }

        public Invoice(DateTime issueDate, decimal totalAmount, string details, Car car, Customer customer)
        {

            IssueDate = issueDate;
            TotalAmount = totalAmount;
            Details = details;
            Car = car;
            Customer = customer;
            InvoiceId = GenerateUniqueId();
        }



        private static int nextInvoiceId = 1;

        private int GenerateUniqueId()
        {
            return nextInvoiceId++;
        }
    }
}
