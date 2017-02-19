/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   RoleInfo.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-04-06
 *   职    责   ：   流程环节的用户角色实体
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-04-06        1.0.0.0        余树杰        初版　 
 *    
 *
 *   
 *   
 *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workflow.Platform.Common.Entities
{
    public class RoleInfo
    {
        public string RoleID { get; set; }
        public string RoleName { get; set; }
    }
}
