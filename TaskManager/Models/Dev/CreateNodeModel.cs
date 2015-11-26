using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models.Dev
{
    public class CreateNodeModel
    {
        [Required]
        [Display(Name = "节点名")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "节点IP")]
        [RegularExpression(@"\b(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\.(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\.(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\.(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\b")]
        public string IP { get; set; }
        [Required]
        [Display(Name = "节点端口")]
        public int Port { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}