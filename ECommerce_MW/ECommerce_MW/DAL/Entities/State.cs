using System.ComponentModel.DataAnnotations;

namespace ECommerce_MW.DAL.Entities
{
    public class State : Entity                                                                                 //Aquí las herencias, específicamente de Entity
    {
        #region Properties

        [Display(Name = "Estado")]                                                                              //Como quiero mostrar la inteerfaz gráfica a nivel de usuario
        [MaxLength(50, ErrorMessage = "El campo {0} debe ser de {1} caracteres.")]                              //[(50)...]Tamaño de caracteres... 0, Nombre de la prop, 1 es el Maxlength osea el 50 en este caso
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]                                               //0, Nombre de la prop
        public string Name { get; set; }                               //Relación1
        public Country Country { get; set; }                                                                    //State tiene la forean key
        public ICollection<City> Cities { get; set; }                //Relación2                                //De muchos a uno se genera la Coleccción, yo puedo tener n estados dentro de este país, además se va a traer todos lo datos
        public int CitiesNumber => Cities == null ? 0 : Cities.Count;                                           //Conteo de estados, esta es una propiedad de lectura, para no mapearla,unicamente voy a implementar el set de esta propiedad, es como un select de Bd, para buscar datos     ? entonces, : sino, if ternario

        #endregion

    }
}
