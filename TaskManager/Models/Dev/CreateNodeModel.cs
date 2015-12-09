using System.ComponentModel.DataAnnotations;
using TaskManager.Helper.CustomValidation;

namespace TaskManager.Models.Dev
{
    public class CreateNodeModel
    {
        [Required]
        [Display(Name = "节点名")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "节点IP")]
        [IpValidation]
        public string IP { get; set; }
        [Required]
        [Display(Name = "节点端口")]
        [Range(1, 65535)]
        public int Port { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}