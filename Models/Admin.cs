using System.ComponentModel.DataAnnotations;

namespace ApiMaPay.Models
{
    public class Admin
    {
        [Required]
        public string useremail { get; set; }

        [Required]
        public string password { get; set; }
    }

    public class AdminDetails
    {
        /*will use for responce object*/
        public int id { get; set; }
        public Result result { get; set; }
    }

    public class Result
    {
        public bool result { get; set; }
        public string message { get; set; }
    }
}
