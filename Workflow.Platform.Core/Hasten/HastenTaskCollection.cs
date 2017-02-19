/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   HastenTaskCollection.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-05-19
 *   职    责   ：   在内存中存取催单任务
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-05-19        1.0.0.0        余树杰        初版　 
 *
 *
 *   
 *   
 *******************************************************************************/

using System;
using System.Collections.Generic;
using Workflow.Domain.DomainObjects;

namespace Workflow.Platform.Core.Hasten
{
    /// <summary>
    /// 催单任务集合
    /// </summary>
    public class HastenTaskCollection
    {
        private static List<HastenTask> _hastenTasks;

        private static object _lockObject = new object();

        public static List<HastenTask> HastenTasks
        {
            get
            {
                lock (_lockObject)
                {
                    if (null == _hastenTasks)
                    {
                        _hastenTasks = new List<HastenTask>();
                    }
                    return _hastenTasks;
                }
            }
        }
    }
}
