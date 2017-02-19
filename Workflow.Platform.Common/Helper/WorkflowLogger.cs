/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowLogger.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-08-13
 *   职    责   ：   工作流平台日志记录器
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-08-13        1.0.0.0        余树杰        初版　 
 *
 *
 *   
 *   
 *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Workflow.Platform.Common.Helper
{
    public static class WorkflowLogger
    {
        private static ILog _workflowLogger = LogManager.GetLogger("Workflow");

        /// <summary>
        /// 记录致命错误
        /// </summary>
        public static void Fatal(object message)
        {
            _workflowLogger.Fatal(message);
        }

        /// <summary>
        /// 记录致命错误
        /// </summary>
        public static void FatalFormat(string format, params object[] args)
        {
            _workflowLogger.FatalFormat(format, args);
        }

        /// <summary>
        /// 记录一般错误
        /// </summary>
        public static void Error(object message)
        {
            _workflowLogger.Error(message);
        }

        /// <summary>
        /// 记录一般错误
        /// </summary>
        public static void ErrorFormat(string format, params object[] args)
        {
            _workflowLogger.ErrorFormat(format, args);
        }

        /// <summary>
        /// 记录警告信息
        /// </summary>
        public static void Warn(object message)
        {
            _workflowLogger.Warn(message);
        }

        /// <summary>
        /// 记录警告信息
        /// </summary>
        public static void WarnFormat(string format, params object[] args)
        {
            _workflowLogger.WarnFormat(format, args);
        }

        /// <summary>
        /// 记录一般信息
        /// </summary>
        public static void Info(object message)
        {
            _workflowLogger.Info(message);
        }

        /// <summary>
        /// 记录一般信息
        /// </summary>
        public static void InfoFormat(string format, params object[] args)
        {
            _workflowLogger.InfoFormat(format, args);
        }

        /// <summary>
        /// 记录调试信息
        /// </summary>
        public static void Debug(object message)
        {
            _workflowLogger.Debug(message);
        }

        /// <summary>
        /// 记录调试信息
        /// </summary>
        public static void DebugFormat(string format, params object[] args)
        {
            _workflowLogger.DebugFormat(format, args);
        }
    }
}
