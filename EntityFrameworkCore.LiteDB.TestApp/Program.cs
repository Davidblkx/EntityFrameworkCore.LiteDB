using System;

namespace EntityFrameworkCore.LiteDB.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new LiteDBContext())
            {
                context.Users.Add(new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "david pires"
                });

                context.SaveChanges();
            }

            Console.ReadKey();
        }
    }
}