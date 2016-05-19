using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using UserTest.Models;

namespace UserTest.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult Index()
        {
            return View();
        }

        #region CRUD methods

        public static Guid CreateCustomer(Customer customer)
        {
            using (EntityContext db = new EntityContext())
            {

                customer.Id = Guid.NewGuid();
                byte[] salt = CreateSalt();
                customer.Salt = System.Text.Encoding.Default.GetString(salt);
                byte[] password = System.Text.ASCIIEncoding.Default.GetBytes(customer.Password);
                byte[] stringpassword = GenerateSaltedHash(password, salt);
                customer.Password = System.Text.Encoding.Default.GetString(stringpassword);


                db.Persons.Add(customer);
                db.SaveChanges();
                return customer.Id;

            }
        }

        public static Guid CreateEmployee(Employee employee)
        {
            using (EntityContext db = new EntityContext())
            {

                employee.Id = Guid.NewGuid();
                byte[] salt = CreateSalt();
                employee.Salt = System.Text.Encoding.Default.GetString(salt);
                byte[] password = System.Text.ASCIIEncoding.Default.GetBytes(employee.Password);
                byte[] stringpassword = GenerateSaltedHash(password, salt);
                employee.Password = System.Text.Encoding.Default.GetString(stringpassword);


                db.Persons.Add(employee);
                db.SaveChanges();
                return employee.Id;
            }
        }
        public static Customer FindCustomer(Guid id)
        {
            using (EntityContext db = new EntityContext())
            {
                return db.Persons.Find(id) as Customer;
            }
        }

        public static Employee FindEmployee(Guid id)
        {
            using (EntityContext db = new EntityContext())
            {
                return db.Persons.Find(id) as Employee;
            }
        }


        public static bool DeleteEmployee(Employee employee)
        {
            using (EntityContext db = new EntityContext())
            {
                db.Entry(employee).State = EntityState.Deleted;
                return db.SaveChanges() == 1;
            }


        }

        public static bool DeleteCustomer(Customer customer)
        {
            using (EntityContext db = new EntityContext())
            {
                db.Entry(customer).State = EntityState.Deleted;
                return db.SaveChanges() == 1;
            }


        }

        #endregion  

        #region  Salt and hashing
        public static byte[] CreateSalt()
        {
            //Generate a cryptographic random number.
            byte[] random = Guid.NewGuid().ToByteArray();
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(random);
            return random;
        }


        public static byte[] GenerateSaltedHash(byte[] password, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
              new byte[password.Length + salt.Length];

            for (int i = 0; i < password.Length; i++)
            {
                plainTextWithSaltBytes[i] = password[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[password.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }

        //metode til at checke om password der er givet også er identisk til det hashede password som er i databasen
        public static bool ComparePW(string checkpassword, string email)
        {
            using (EntityContext db = new EntityContext())
            {

                Person person = db.Persons.FirstOrDefault(x => x.Email == email);
                string personSalt = person.Salt;
                string personPassword = person.Password;
                byte[] checkpasswordbyte = System.Text.ASCIIEncoding.Default.GetBytes(checkpassword);
                byte[] checkPersonSalt = System.Text.ASCIIEncoding.Default.GetBytes(personSalt);
                byte[] compareHash = GenerateSaltedHash(checkpasswordbyte, checkPersonSalt);
                string compareHashString = System.Text.Encoding.Default.GetString(compareHash);


                if (compareHashString == personPassword)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion
    }
}