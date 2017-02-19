/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowLibraryResolver.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-12
 *   职    责   ：   流程类库解析器
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
using System.Activities;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Workflow.Platform.Core.Common
{
    /// <summary>
    /// 流程类库解析器
    /// </summary>
    public class WorkflowLibraryResolver
    {
        public WorkflowLibraryResolver(string workflowLibraryPath)
        {
            this.WorkflowLibraryPath = workflowLibraryPath;
        }

        /// <summary>
        /// 流程类库的存放目录
        /// </summary>
        private string WorkflowLibraryPath { get; set; }

        /// <summary>
        /// 当前请求已解析到的流程类型
        /// </summary>
        private Type WorkflowType { get; set; }

        /// <summary>
        /// 获取所有流程类型
        /// </summary>
        public IList<Type> GetWorkflowTypes()
        {
            List<Type> workflowTypes = new List<Type>();
            foreach (var assembly in GetWorkflowLibraries())
            {
                try
                {
                    foreach (var type in assembly.GetTypes().Where(t => typeof(Activity).IsAssignableFrom(t)))
                    {
                        workflowTypes.Add(type);
                    }
                }
                catch (Exception ex)
                {//这里捕获异常仅仅是为了解决Sprint1版本中的测试问题，后续迭代版本提供工作流打包规范后可不在此作异常处理
                    Workflow.Platform.Common.Helper.WorkflowLogger.InfoFormat("WorkflowLibraryResolver.GetWorkflowTypes:{0}", ex.Message);
                    continue;
                }
            }
            return workflowTypes;
        }

        /// <summary>
        /// 获取指定流程类型
        /// </summary>
        /// <param name="workflowFullName">流程类型的完全限定名称</param>
        public Type GetWorkflowType(string workflowFullName)
        {
            if (null == this.WorkflowType)
            {
                this.WorkflowType = GetWorkflowTypes().FirstOrDefault(
                                    t => string.Compare(t.FullName, workflowFullName, true) == 0);
            }
            return this.WorkflowType;
        }

        /// <summary>
        /// 获取所有的流程类库
        /// </summary>
        public IList<Assembly> GetWorkflowLibraries()
        {
            string[] files = Directory.GetFiles(this.WorkflowLibraryPath);
            List<Assembly> libraries = new List<Assembly>();
            foreach (string file in files)
            {
                try
                {
                    libraries.Add(Assembly.LoadFrom(file));
                }
                catch(Exception ex)
                {//这里捕获异常仅仅是为了解决Sprint1版本中的测试问题，后续迭代版本提供工作流打包规范后可不在此作异常处理
                    Workflow.Platform.Common.Helper.WorkflowLogger.InfoFormat("WorkflowLibraryResolver.GetWorkflowLibraries:{0}", ex.Message);
                    continue;
                }
            }
            return libraries;
        }

        /// <summary>
        /// 获取指定流程类型的Xaml定义
        /// </summary>
        /// <param name="workflowFullName">流程类型的完全限定名称</param>
        public XDocument GetWorkflowXaml(string workflowFullName)
        {
            XDocument workflowXamlDoc = null;
            string resourceName = FindXamlResourceName(workflowFullName);
            using (Stream workflowStream = GetWorkflowType(workflowFullName).Assembly.GetManifestResourceStream(resourceName))
            {
                workflowXamlDoc = XDocument.Load(workflowStream);
            }
            return workflowXamlDoc;
        }

        /// <summary>
        /// 查找指定流程类型的Xaml定义文件资源名
        /// </summary>
        /// <param name="workflowFullName">流程类型的完全限定名称</param>
        public string FindXamlResourceName(string workflowFullName)
        {
            Type workflowType = GetWorkflowType(workflowFullName);
            string[] resourceNames = workflowType.Assembly.GetManifestResourceNames();
            string compareText = string.Format("{0}.g.xaml", 
                                 workflowFullName.Substring(workflowFullName.LastIndexOf('.') + 1));
            for (int i = 0; i < resourceNames.Length; i++)
            {
                string resourceName = resourceNames[i];
                if (resourceName.Contains(compareText) || resourceName.Equals(compareText))
                {
                    return resourceName;
                }
            }
            throw new InvalidOperationException("Not found the XAML file for workflow.");
        }
    }
}
