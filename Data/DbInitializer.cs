using AchimDaiana_Theater.Models;
using Microsoft.EntityFrameworkCore;

namespace AchimDaiana_Theater.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryContext(serviceProvider.GetRequiredService<DbContextOptions<LibraryContext>>()))
            {
                if (context.Plays.Any())
                {
                    return; 
                }
               /* context.Plays.AddRange(
                new Play
                {
                    Title = "Hamlet",
                    Writer = "William Shakespeare",
                    Genre = "Tragedy",
                    Price=Decimal.Parse("85")},
               
                new Play
                {
                    Title = "Death of Salesman",
                    Writer = "Arthur Miller",
                    Genre = "Drama",
                    Price =Decimal.Parse("56")},
               
                new Play
                {
                    Title = "The Effect",
                    Writer = "Lucy Prebble",
                    Genre = "Romance",
                    Price =Decimal.Parse("48")}
               
                );*/


               /* context.Customers.AddRange(
                new Customer
                {
                    FirstName = "Miruna",
                    LastName = "Cozma",
                    Email = "miruna@yahoo.com",
                },
                new Customer
                {
                    FirstName = "Ana",
                    LastName = "Cadar",
                    Email = "ana@yahoo.com"
                }

                );

                context.SaveChanges();*/
            }
        }
    }
}
