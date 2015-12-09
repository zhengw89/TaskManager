using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models.Org
{
    public class CreateUserModel
    {
        [Required]
        [Display(Name = "用户ID")]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "用户名称")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }
}