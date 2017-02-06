using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NLog;
using System.Web.UI;
using System.Web.Mvc;

namespace PlatinumTravel.Models
{
    public class Slider
    {
        Logger testlog = LogManager.GetCurrentClassLogger();

        public string imgName { get; set; }
        public Image img { get; set; }
        public string mainText { get; set; }
        public string secondaryText { get; set; }
        public bool isActive { get; set; }

        private string pathToImg = HttpContext.Current.Server.MapPath("~/Slider/");

        public Slider()
        {
        }

        public Slider(string fileName)
        {
            this.pathToImg += fileName;
            this.imgName = fileName;
            this.img = Image.FromFile(pathToImg);
        }

        public void UploadSliderImg(UploadSlideModel newSlide)
        {
            using(PlatinumDBContext db = PlatinumDBContext.GetConnection())
            {
                try
                { 
                    SlideModel slide = new SlideModel();
                    slide.imgName = newSlide.imgName+=Path.GetExtension(newSlide.slideFile.FileName);
                    slide.mainText = newSlide.mainText;
                    slide.secondaryText = newSlide.secondaryText;
                    slide.isActive = newSlide.isActive;

                    db.Slides.Add(slide);
                    db.Entry(slide).State = EntityState.Added;

                    newSlide.slideFile.SaveAs(pathToImg += newSlide.imgName);
                    db.SaveChanges();
                    testlog.Info("Слайд загружен.");
                }
                catch(Exception e)
                {
                    testlog.Fatal("При загрузке изобр. слайдера возникла ошибка. " + e.Message);
                }
           }
        }
    }


    /// <summary>
    /// Представление для работы с бд 
    /// </summary>
    [Table("Slides")]
    public class SlideModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string imgName { get; set; }
        [Required]
        public string mainText { get; set; }
        [Required]
        public string secondaryText { get; set; }
        [Required]
        public bool isActive { get; set; }


        public string getSrc()
        {
            using(Control ctrl = new Control())
            {
                return ctrl.ResolveClientUrl("~/Slider/" + imgName);
            }
        }

    }



    /// <summary>
    /// Вид флормы для загрузки изображения слайдера
    /// </summary>
    public class UploadSlideModel
    {
        [Required]
        [MaxLength(20)]
        public string imgName { get; set; }

        [Required]
        [MaxLength(20)]
        public string mainText { get; set; }

        [Required]
        [MaxLength(20)]
        public string secondaryText { get; set; }

        [Required]
        public bool isActive { get; set; }

        [Required]
        public HttpPostedFileBase slideFile { get; set; }
    }
}