﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a CodeSmith Template.
//
//     DO NOT MODIFY contents of this file. Changes to this
//     file will be lost if the code is regenerated.
//     Author:Yubao Li
// </autogenerated>
//------------------------------------------------------------------------------

using System;

namespace OpenAuth.Domain
{
    /// <summary>
	/// 角色表
	/// </summary>
    public partial class Role :Entity
    {
        public Role()
        {
            this.Name= string.Empty;
          this.Status= 0;
          this.Type= 0;
          this.CreateTime= DateTime.Now;
          this.CreateId= string.Empty;
        }

        /// <summary>
	    /// 角色名称
	    /// </summary>
        public string Name { get; set; }
        /// <summary>
	    /// 当前状态
	    /// </summary>
        public int Status { get; set; }
        /// <summary>
	    /// 角色类型
	    /// </summary>
        public int Type { get; set; }
        /// <summary>
	    /// 创建时间
	    /// </summary>
        public System.DateTime CreateTime { get; set; }
        /// <summary>
	    /// 创建人ID
	    /// </summary>
        public string CreateId { get; set; }

    }
}