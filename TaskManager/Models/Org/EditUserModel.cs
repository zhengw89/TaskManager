using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models.Org
{
    public class EditUserModel
    {
        [Required]
        [Display(Name = "用户ID")]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "用户名称")]
        public string UserName { get; set; }
    }
}