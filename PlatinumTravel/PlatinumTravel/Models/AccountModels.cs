using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PlatinumTravel.Models
{
    //Модели используемые для 
    //представлений и работы с бд 
    //при ауентификации
    
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Необходимо ввести имя пользователя.")]
        [Display(Name ="Имя пользователя: ")]
        [MaxLength(20, ErrorMessage = "Имя пользователя должно содержать больше 3 и меньше 20 символов"), MinLength(3, ErrorMessage = "Имя пользователя должно содержать больше 3 и меньше 20 символов")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Необходимо ввести пароль.")]
        [Display(Name = "Пароль: ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }

    /// <summary>
    /// Класс для выборки и вставки данных и пользователе.
    /// </summary>
    [Table("Profiles")]
    public class Profile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым")]
        [MaxLength(20, ErrorMessage = "Имя пользователя должно содержать больше 3 и меньше 20 символов"), MinLength(3, ErrorMessage = "Имя пользователя должно содержать больше 3 и меньше 20 символов")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым")]
        [MaxLength(20, ErrorMessage = "Имя пользователя должно содержать больше 3 и меньше 20 символов"), MinLength(3, ErrorMessage = "Имя пользователя должно содержать больше 3 и меньше 20 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым")]
        public string Role { get; set; }
    }

}