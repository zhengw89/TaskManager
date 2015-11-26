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
        [Display(Name = "任务执行类全名")]
        public string ClassName { get; set; }
        [Required]
        [Display(Name = "任务执行方法名")]
        public string MethodName { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
        [Required]
        [Display(Name = "文件")]
        public HttpPostedFileBase File { get; set; }

        public List<SelectListItem> Nodes { get; set; }
    }
}