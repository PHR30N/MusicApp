using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Data;
using System.Configuration;
namespace MusicApp
{
    class Program
    {
        static void Main(string[] args)
        {
            UserProfile user1 = new UserProfile("Youssed","y@gmail.com", "123456789", "0123456789", 20, "Tunisia");
            user1.SaveToDatabase();
            user1.ViewProfile();
        }
    }
}
