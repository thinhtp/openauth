﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Infrastructure;
using LeaRun.Util.WebControl;
using OpenAuth.App;
using OpenAuth.App.SSO;
using OpenAuth.Domain;
using OpenAuth.Domain.Service;
using OpenAuth.Mvc.Controllers;

namespace OpenAuth.Mvc.Areas.FlowManage.Controllers
{

    public class FormDesignController : BaseController
    {
        private readonly WFFormService _wfFrmMainBll;

        public FormDesignController()
        {
            _wfFrmMainBll = AutofacExt.GetFromFac<WFFormService>();
        }

        #region 视图功能
        /// <summary>
        /// 管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 设计器
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FormLayout()
        {
            return View();
        }
        /// <summary>
        /// 预览表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FormPreview()
        {
            return View();
        }
        /// <summary>
        /// 创建表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FrmBuider()
        {
            return View();
        }
        #endregion

        #region 获取数据

        public string Load(int pageCurrent = 1, int pageSize = 30)
        {
            return JsonHelper.Instance.Serialize(_wfFrmMainBll.Load(pageCurrent, pageSize));
        }

        /// <summary>
        /// 表单树 
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson()
        {
            var data = _wfFrmMainBll.GetAllList();
            var treeList = new List<TreeEntity>();
            foreach (var item in data)
            {
                TreeEntity tree = new TreeEntity();

                tree.id = item.Id.ToString();
                tree.text = item.FrmName;
                tree.value = item.Id.ToString();
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = false;
                tree.parentId = "0";
                tree.Attribute = "Sort";
                tree.AttributeValue = "Frm";
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 设置表单
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFormJson(Guid keyValue)
        {
            var data = _wfFrmMainBll.GetForm(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取表单数据all
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAllListJson()
        {
            var data = _wfFrmMainBll.GetAllList();
            return Content(data.ToJson());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除表单模板
        /// </summary>
        /// <param name="ids">主键值</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveForm(Guid[] ids)
        {
            _wfFrmMainBll.RemoveForm(ids);
            return Content("删除成功。");
        }
        ///// <summary>
        ///// 保存用户表单（新增、修改）
        ///// </summary>
        ///// <param name="keyValue">主键值</param>
        ///// <param name="userEntity">用户实体</param>
        ///// <returns></returns>
        [HttpPost]
        public string SaveForm(string keyValue, WFFrmMain userEntity)
        {
            try
            {
                var user = AuthUtil.GetCurrentUser();
                userEntity.ModifyUserId = user.User.Account;
                userEntity.ModifyUserName = user.User.Name;
                _wfFrmMainBll.SaveForm(keyValue, userEntity);
            }
            catch (Exception e)
            {
                Result.Status = false;
                Result.Message = e.Message;
            }
            return Result.ToJson();
        }
        ///// <summary>
        ///// （启用、禁用）
        ///// </summary>
        ///// <param name="keyValue">主键值</param>
        ///// <param name="State">状态：1-启动；0-禁用</param>
        ///// <returns></returns>
        //[HttpPost]
        //[AjaxOnly]
        //public ActionResult SubmitUpdateState(string keyValue, int State)
        //{
        //    wfFrmMainBLL.UpdateState(keyValue, State);
        //    return Success("操作成功。");
        //}
        ///// <summary>
        ///// 上传文件
        ///// </summary>
        ///// <param name="folderId">文件夹Id</param>
        ///// <param name="Filedata">文件对象</param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult UploadifyFile(string folderId, HttpPostedFileBase Filedata)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(folderId))
        //        {
        //            return Success("虚拟上传文件成功。");
        //        }

        //        Thread.Sleep(500);////延迟500毫秒
        //        //没有文件上传，直接返回
        //        if (Filedata == null || string.IsNullOrEmpty(Filedata.FileName) || Filedata.ContentLength == 0)
        //        {
        //            return HttpNotFound();
        //        }
        //        //获取文件完整文件名(包含绝对路径)
        //        //文件存放路径格式：/Resource/ResourceFile/{userId}{data}/{guid}.{后缀名}
        //        string userId = OperatorProvider.Provider.Current().UserId;
        //        string fileGuid = Guid.NewGuid().ToString();
        //        long filesize = Filedata.ContentLength;
        //        string FileEextension = Path.GetExtension(Filedata.FileName);
        //        string uploadDate = DateTime.Now.ToString("yyyyMMdd");
        //        string virtualPath = string.Format("~/Resource/DocumentFile/{0}/{1}/{2}{3}", userId, uploadDate, fileGuid, FileEextension);
        //        string fullFileName = this.Server.MapPath(virtualPath);
        //        //创建文件夹
        //        string path = Path.GetDirectoryName(fullFileName);
        //        Directory.CreateDirectory(path);
        //        if (!System.IO.File.Exists(fullFileName))
        //        {
        //            //保存文件
        //            Filedata.SaveAs(fullFileName);
        //        }
        //        return Success("上传成功。");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content(ex.Message);
        //    }
        //}
        #endregion
    }
}
