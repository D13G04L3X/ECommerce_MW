using System.ComponentModel.DataAnnotations;

namespace ECommerce_MW.DAL.Entities
{
    public class Entity
    {
        #region Properties

        [Key]                                           //Va encima del dato que va a ser la primary key
        [Required]                                      //[Required], Son campos no nuleables, osea que debe haber datos en ellos, null
        public Guid Id { get; set; }                    //Genera un guid, para más seguridad (P_key)
        public DateTime? CreatedDate { get; set; }      //Entidades obligatorias y necesarias
        public DateTime? ModifiedDate { get; set; }     //?, Son campos nuleables, osea no obligatorios, not null

        #endregion

    }
}
