/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowServiceReturnResult.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-21
 *   职    责   ：   定义工作流WebService的返回结果
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-03-21        1.0.0.0        余树杰        初版　 
 *    
 *
 *   
 *   
 *******************************************************************************/

using System;

namespace Workflow.Platform.Common.Entities
{
    public class WorkflowServiceReturnResult
    {
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public object Data { get; set; }
    }
}
