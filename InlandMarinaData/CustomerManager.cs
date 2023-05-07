using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaData
{
    /// <summary>
    /// Class is responsible for authenticating and managing users.
    /// </summary>
    public class CustomerManager
    {

        /// <summary>
        /// User is authenticated based on credentials and a user returned if exists or null if not.
        /// </summary>
        /// <param name="username">Username as string</param>
        /// <param name="password">Password as string</param>
        /// <returns>A user object or null.</returns>
        /// <remarks>
        /// Add additional for the docs for this application--for developers.
        /// </remarks>
        public static Customer Authenticate(string username, string password)
        {
            InlandMarinaContext db = new InlandMarinaContext();
            var user = db.Customers.SingleOrDefault(usr => usr.Username == username
                                                    && usr.Password == password);
            return user; //this will either be null or an object
        }
    }
}
