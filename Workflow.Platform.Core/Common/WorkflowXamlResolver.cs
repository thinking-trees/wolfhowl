/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowXamlResolver.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-06-03
 *   职    责   ：   解析流程类型的XAML定义文档
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-06-03        1.0.0.0        余树杰        初版　 
 *    
 *
 *   
 *   
 *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Workflow.Platform.Common.Helper;

namespace Workflow.Platform.Core.Common
{
    /// <summary>
    /// 流程类型的XAML定义文档解析器
    /// </summary>
    public class WorkflowXamlResolver
    {
        private const string NS = "clr-namespace:Workflow.Activities.CustomActivities;assembly=Workflow";//自定义活动的名称空间
        private const string PREFIX = "wac";//XAML文档中表示自定义名称空间的前缀
        private const string ACTIVITY_TYPE_ATTRIBUTE = "ActivityType";//XAML文档中表示ActivityType的属性名
        private const string ACTIVITY_STATE_ATTRIBUTE = "State";//XAML文档中表示ActivityState的属性名
        private const string ACTIVITY_DISPLAYNAME_ATTRIBUTE = "DisplayName";//XAML文档中表示DisplayName的属性名
        private const string WORKFLOW_CONTAINER = "Flowchart";//XAML文档中所有Activity的顶层容器名，工作流定义文档中的一条原则：必须以Flowchar活动作为工作流的根容器

        /// <summary>
        /// 获取工作流的业务类型名称
        /// </summary>
        public static string GetWorkflowBusinessName(XDocument wfXamlDoc)
        {
            XNamespace defaultNs = wfXamlDoc.Root.GetDefaultNamespace();
            XName flowchartName = defaultNs + WORKFLOW_CONTAINER;
            XElement flowchart = wfXamlDoc.Descendants(flowchartName).First();
            string name = flowchart.Attribute(ACTIVITY_DISPLAYNAME_ATTRIBUTE).Value;
            return name;
        }

        /// <summary>
        /// 从工作流定义文档中解析环节信息
        /// </summary>
        /// <param name="wfXamlDoc">工作流定义文档</param>
        public static IEnumerable<XElement> ParseActivities(XDocument wfXamlDoc)
        {
            XmlReader wfXamlReader = wfXamlDoc.CreateReader();
            XmlNameTable nameTable = wfXamlReader.NameTable;
            XmlNamespaceManager nsManager = new XmlNamespaceManager(nameTable);
            nsManager.AddNamespace(PREFIX, NS);
            string expression = string.Format("//{0}:*", PREFIX);
            List<XElement> activities = wfXamlDoc.XPathSelectElements(expression, nsManager).ToList();
            return SortActivities(activities);
        }

        /// <summary>
        /// 对环节排序，以确保初始化流程环节信息时环节编号与流程图顺序一致
        /// </summary>
        private static List<XElement> SortActivities(List<XElement> activities)
        {
            XElement endActivity = null;
            try
            {
                endActivity = activities.Single(e => "End" == ParseActivityState(e));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("There are more than one or no end activity in the workflow definition document", "WorkflowXamlResolver SortActivities");
                WorkflowLogger.Info("WorkflowXamlResolver SortActivities:There are more than one or no end activity in the workflow definition document");
                WorkflowLogger.Error(ex);
                throw;
            }
            activities.Remove(endActivity);
            activities = activities.OrderBy(e => ParseActivityState(e).TrimStart('0')).ToList();
            activities.Add(endActivity);
            return activities;
        }

        /// <summary>
        /// 解析环节元素的类型信息
        /// </summary>
        /// <param name="activity">流程环节元素信息</param>
        public static string ParseActivityType(XElement activity)
        {
            return activity.Attribute(ACTIVITY_TYPE_ATTRIBUTE).Value;
        }

        /// <summary>
        /// 解析环节元素的状态信息
        /// </summary>
        public static string ParseActivityState(XElement activity)
        {
            return activity.Attribute(ACTIVITY_STATE_ATTRIBUTE).Value;
        }

        /// <summary>
        /// 解析环节元素的名称信息
        /// </summary>
        public static string ParseActivityName(XElement activity)
        {
            return activity.Attribute(ACTIVITY_DISPLAYNAME_ATTRIBUTE).Value;
        }
    }
}
