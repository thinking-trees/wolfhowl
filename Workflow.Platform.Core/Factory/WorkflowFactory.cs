/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowFactory.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-12
 *   职    责   ：   流程类型构造工厂，供WorkflowInstanceManager类使用
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-03-12        1.0.0.0        余树杰        初版　 
 *    
 *
 *   
 *   
 *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Activities;
using System.Linq;
using Workflow.Platform.Core.Common;

namespace Workflow.Platform.Core.Factory
{
    /// <summary>
    /// 流程类型构造工厂，供WorkflowInstanceManager类使用
    /// </summary>
    public class WorkflowFactory
    {
        /// <summary>
        /// 创建指定的流程类型的一个实例
        /// </summary>
        /// <param name="workflowLibraryPath">流程类库的存放目录</param>
        /// <param name="workflowFullName">流程类型的完全限定名称</param>
        public static Activity Create(string workflowLibraryPath, string workflowFullName)
        {
            WorkflowLibraryResolver reslover = new WorkflowLibraryResolver(workflowLibraryPath);
            Type workflowType = reslover.GetWorkflowType(workflowFullName);
            if (null != workflowType)
            {
                return (Activity)Activator.CreateInstance(workflowType);
            }
            return null;
        }
    }
}
