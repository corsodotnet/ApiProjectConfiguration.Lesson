using ApiProject.Lesson.Persistence.Configuration;
using System;
using System.Collections.Generic;

namespace ApiProject.Lesson.Utils
{
    public class SeedData
    {
        public static void SeedDatabase(DatabaseCxt dbCtx)
        {
            //using (dbCtx)
            //{
            //    dbCtx.RemoveRange(); 
            //}

                // -> Delete() ->> 


                Corso corso = new Corso()
                {
                    Name = "Informatica",
                    DatePublished = DateTime.Now,
                    Students = new List<Studente>()
                };
            List<Studente> students = new List<Studente>()
            {
                new Studente() { Name = "Bruno", DatePublished = DateTime.Now },
                new Studente() { Name = "Mario", DatePublished = DateTime.Now },
                new Studente() { Name = "Luca", DatePublished = DateTime.Now },
                new Studente() { Name = "Maria", DatePublished = DateTime.Now }
            };

            corso.Students.AddRange(students);

            using (dbCtx)
            {
                dbCtx.Corso.Add(corso);

                try
                {
                    dbCtx.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
