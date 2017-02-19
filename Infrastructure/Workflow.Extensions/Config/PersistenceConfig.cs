/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   PersistenceConfig.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-12
 *   职    责   ：   存储工作流实例持久化操作的配置
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
using System.Xml.Linq;

namespace Workflow.Extensions
{
    public class PersistenceConfig
    {
        #region 属性
        /// <summary>
        /// 持久性参与者扩展字段的默认名称空间
        /// </summary>
        public static XNamespace NS
        {
            get { return "http://www.wolfhowl.com"; }
        }

        /// <summary>
        /// 持久性参与者扩展字段名称集合
        /// </summary>
        public static List<XName> PromoteProperties
        {
            get
            {
                var promoteProperties = new List<XName>
                {
                    NS.GetName("InstanceName"),
                    NS.GetName("CreateUserID"),
                    NS.GetName("CreateUserName"),
                    NS.GetName("CreationTime"),
                    NS.GetName("CurrentActivityID"),
                    NS.GetName("CurrentActivityName"),
                    NS.GetName("CurrentUserID"),
                    NS.GetName("CurrentUserName"),
                    NS.GetName("ArrivalTime"),
                    NS.GetName("Comment"),
                    NS.GetName("InstanceID"),
                    NS.GetName("WorkflowID"),
                    NS.GetName("BasicBusinessInfo"),
                    NS.GetName("CurrentActivityHastenAmount")
                };
                return promoteProperties;
            }
        }
        #endregion 属性
    }
}
