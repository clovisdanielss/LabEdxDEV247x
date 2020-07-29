
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FirstLab.Models{
    [Table("product")]
    public class Product{
        [Key,Column("id"),DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id {set; get;}
        [Column("value")]
        public float value {set; get;}
        [Column("name")]
        public string name {set; get;}
    }

}