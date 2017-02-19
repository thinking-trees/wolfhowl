using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Reflection;
using log4net;
using System.Text.RegularExpressions;

namespace Workflow.Language
{
    /// <summary>
    /// 读取XML文件
    /// </summary>
    internal class ResourceManager
    {
        internal static ILog _logger = LogManager.GetLogger("Language");

        /// <summary>
        /// 将XML数据加载到hashtable中
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public void LoadLanguage(string path, Hashtable table)
        {

            if (table == null)
            {
                table = Hashtable.Synchronized(new Hashtable());
            }
            Assembly asm = Assembly.GetExecutingAssembly();
            using (var stream = asm.GetManifestResourceStream(path))
            {

                var doc = XDocument.Load(stream);
                try
                {
                    //查询语句
                    if (doc.Root != null)
                    {
                        var targetNodes = from target in doc.Root.Descendants("item")
                                          select target;
                        //遍历所获得的目标节点（集合）
                        foreach (var tempXml in targetNodes)
                        {

                            var keyAttribute = tempXml.Attribute("Key").Value;
                            if (!string.IsNullOrEmpty(keyAttribute.ToString()))
                            {
                                var value = tempXml.Value; //需要保证key的唯一性

                                if (!table.ContainsKey(keyAttribute))
                                {
                                    value = value.Replace(@"\r\n", Environment.NewLine);
                                    value = value.Replace(@"\n", Environment.NewLine);
                                    table.Add(keyAttribute, value);
                                }
                            }

                        }
                        targetNodes = null;
                    }
                }
                catch (Exception ex)
                {
                    _logger.InfoFormat("ResourceManager.LoadLanguage {0},{1}", ex.Message, ex.StackTrace);
                }
                finally
                {

                    doc = null;
                }
            }
        }
    }

}