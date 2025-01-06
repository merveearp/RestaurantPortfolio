using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantPortfolio.Models.Entities
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [Display(Name = "Ad Soyad")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Rezervasyon tarihi zorunludur.")]
        [Display(Name = "Rezervasyon Tarihi")]
        [DataType(DataType.Date)]
        public DateTime ReservationDate { get; set; }

        [Display(Name = "Gönderim Zamanı")]
        public DateTime SendingTime { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Kişi sayısı zorunludur.")]
        [Range(1, 12, ErrorMessage = "Kişi sayısı 1 ile 12 arasında olmalıdır.")]
        [Display(Name = "Kişi Sayısı")]
        public int PeopleCount { get; set; }

        public bool IsRead { get; set; } 

        public bool IsActive { get; set; } 
    }
}
