using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UNP.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("email")]
        public string Email { get; set; }

        [DisplayName("УНП")]
        public string UNP { get; set; }

        [DisplayName("Наличие в локальной БД")]
        public string LocalStatus { get; set; }

        [DisplayName("ГНаличе в БД портала")]
        public string PortalStatus { get; set; }
    }
}