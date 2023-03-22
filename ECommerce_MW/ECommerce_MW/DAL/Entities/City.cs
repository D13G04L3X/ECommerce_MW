using System.ComponentModel.DataAnnotations;

namespace ECommerce_MW.DAL.Entities
{
    public class City : Entity                                                                                  //Aquí las herencias, específicamente de Entity
    {
        #region Properties

        [Display(Name = "Ciudad")]                                                                              //Como quiero mostrar la inteerfaz gráfica a nivel de usuario
        [MaxLength(50, ErrorMessage = "El campo {0} debe ser de {1} caracteres.")]                              //[(50)...]Tamaño de caracteres... 0, Nombre de la prop, 1 es el Maxlength osea el 50 en este caso
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]                                               //0, Nombre de la prop
        public string Name { get; set; }
        public State State { get; set; }                                                                        //State tiene la forean key
        
        #endregion

    }
}
