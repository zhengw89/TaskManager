using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Models.Ta
{
    public class CreateTaskModel
    {
        [Required]
        [Display(Name = "任务名称")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "节点ID")]
        public string NodeId { get; set; }
        [Required]
        [Display(Name = "CRON表达式")]
        public string Cron { get; set; }
        [Required]
        [Display(Name = "DLL文件名")]
        public string DllName { get; set; }
        [Required]
        [Display(Name = "任务执行类全名")]
        public string ClassName { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
        [Required]
        [Display(Name = "文件")]
        public HttpPostedFileBase File { get; set; }

        public List<SelectListItem> AllNodes { get; set; }
    }
}