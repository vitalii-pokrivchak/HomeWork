using System;
using System.ComponentModel.DataAnnotations;

namespace HomeWork.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Display(Name = "Прізвище Ім'я По-Батькові")]
        [Required(ErrorMessage="Поле `Прізвище Ім'я По-Батькові` є обов'язковим")]
        public string Fio { get; set; }
        [Display(Name = "Дата народження")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage="Поле `Дата Народження` є обов'язковим")]
        public DateTime Birthday { get; set; }
        [Required(ErrorMessage="Поле `Стать` є обов'язковим")]
        [Display(Name = "Стать")]
        public Gender Gender { get; set; }
        [Display(Name = "Місто")]
        [Required(ErrorMessage="Поле `Місто` є обов'язковим")]
        public string City { get; set; }
        [Display(Name = "Номер телефону")]
        [Required(ErrorMessage="Поле `Номер Телефону` є обов'язковим")]
        [Phone(ErrorMessage="Номер телефону введений некоректно . Спробуйте ще раз")]
        public string Phone { get; set; }
        [Display(Name = "Електронна адреса")]
        [Required(ErrorMessage="Поле `Електронна адреса` є обов'язковим")]
        [EmailAddress(ErrorMessage="Електронна адреса введена некоректно . Спробуйте ще раз")]
        public string Email { get; set; }

        public override string ToString()
        {
            return $"{Fio} - {Email}";
        }
    }
}