/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   IRole.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-04-06
 *   职    责   ：   规范流程环节中用户角色的行为
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
using Workflow.Platform.Common.Entities;

namespace Workflow.Platform.Common.Interface
{
    public interface IRole
    {
        void InitialParameter(string WorkflowServerUrl, string WorkflowId);
        List<RoleInfo> GetAllRole();
    }
}
