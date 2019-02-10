using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisWithNetCoreSampleApp.Models
{
    public class User
    {
        public User()
        {

        }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int DepartmentId { get; set; }
        //public Department DepartmentInfo { get; set; }
        public string Email { get; set; }
        public int SicilNo { get; set; }
    }
}
