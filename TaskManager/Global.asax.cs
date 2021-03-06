﻿using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TaskManager
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        #region RegisterRoutes

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            RegsiterDevRoutes(routes);
            RegisterOrgRoutes(routes);
            RegisterTaskRoutes(routes);

            routes.MapRoute(
                "Default",
                "{controller}/{action}",
                new { controller = "LogIn", action = "Index" }
            );
        }

        private static void RegsiterDevRoutes(RouteCollection routes)
        {
            //节点列表
            routes.MapRoute(
                "Nodes",
                "Nodes/{pageIndex}",
                new { controller = "Node", action = "Index", pageIndex = UrlParameter.Optional }
            );
            //节点详情
            routes.MapRoute(
                "Node",
                "Node/{nodeId}",
                new { controller = "Node", action = "Detail", nodeId = UrlParameter.Optional }
            );
            //创建节点
            routes.MapRoute(
                "NodeCreate",
                "NodeCreate",
                new { controller = "Node", action = "Create" }
            );
            //删除节点
            routes.MapRoute(
                "NodeDelete",
                "NodeDelete",
                new { controller = "Node", action = "Delete" }
            );
        }

        private static void RegisterOrgRoutes(RouteCollection routes)
        {
            //用户列表
            routes.MapRoute(
                "Users",
                "Users/{pageIndex}",
                new { controller = "User", action = "Index", pageIndex = UrlParameter.Optional }
            );

            routes.MapRoute(
                "CreateUser",
                "CreateUser",
                new { controller = "User", action = "Create" }
            );

            routes.MapRoute(
                "EditUser",
                "EditUser/{userId}",
                new { controller = "User", action = "Edit", userId = UrlParameter.Optional }
            );
        }

        private static void RegisterTaskRoutes(RouteCollection routes)
        {
            //任务列表
            routes.MapRoute(
                "Tasks",
                "Tasks/{pageIndex}",
                new { controller = "Task", action = "Index", pageIndex = UrlParameter.Optional }
            );
            //创建任务
            routes.MapRoute(
                "TaskCreate",
                "TaskCreate",
                new { controller = "Task", action = "Create" }
            );

            routes.MapRoute(
                "TaskJobs",
                "TaskJobs/{pageIndex}",
                new { controller = "TaskJob", action = "Index", pageIndex = UrlParameter.Optional }
            );
        }

        #endregion

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            CreateBaseFolder();
        }

        #region Init Folders

        /// <summary>
        /// 创建程序运行需要的文件夹
        /// </summary>
        private void CreateBaseFolder()
        {
            string basePath = Server.MapPath("~");
            //创建根文件夹Upload
            var sysOneFolder = new List<string> { "Upload" };
            CreateSysFolderPackaing(basePath, sysOneFolder);
            //创建第二级文件夹 
            string uploadFolder = Path.Combine(basePath, "Upload");
            var sysTwoFolder = new List<string>
            {
                "TaskFile"
            };
            CreateSysFolderPackaing(uploadFolder, sysTwoFolder);
        }

        /// <summary>
        /// 封装创建文件夹的方法
        /// </summary>
        /// <param name="path">创建路径</param>
        /// <param name="folderNames">创建文件夹的集合</param>
        private void CreateSysFolderPackaing(string path, IEnumerable<string> folderNames)
        {
            foreach (var folderName in folderNames)
            {
                string uploadFolder = Path.Combine(path, folderName);
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }
            }
        }

        #endregion
    }
}