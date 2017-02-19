using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Collections;
using System.Xml.Linq;
using log4net;

namespace Workflow.Language
{
    /// <summary>
    /// 多语言管理
    /// </summary>
    public class MultiLanguageManager
    {
        private string _culture = "zh-CHS";
        private Hashtable _hashtable = Hashtable.Synchronized(new Hashtable());
        private static MultiLanguageManager _current = null;
        private ResourceManager _resourceManager = null;
        private static object _syc = new object();
        private const string ZH_CHS = "zh-chs";
        private static ILog _logger = LogManager.GetLogger("Language");

        public static MultiLanguageManager Current
        {
            get
            {
                if (_current == null)
                {
                    lock (_syc)
                    {
                        if (_current == null)
                        {
                            _current = new MultiLanguageManager();
                        }
                    }
                }
                return _current;
            }
        }

        /// <summary>
        /// 语言设置，默认是中文。
        /// zh-CHS:中文，en-US:英文
        /// </summary>
        private String Culture
        {
            get
            {
                _culture = System.Configuration.ConfigurationManager.AppSettings["Culture"];
                if (String.IsNullOrEmpty(_culture))
                {
                    _culture = "zh-CHS";
                }
                return _culture;
            }
        }

        /// <summary>
        /// 返回语言包中对应的资源
        /// </summary>
        public string this[string key]
        {
            get
            {
                return this[key, this.Culture];
            }
        }
        /// <summary>
        /// 返回语言包中对应的资源
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="culture">语言类型</param>
        /// <returns></returns>
        private string this[string key, string culture]
        {
            get
            {
                try
                {
                    return GetString(key, culture);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                    return key;
                }
            }
        }

        private MultiLanguageManager()
        {
            _resourceManager = new ResourceManager();
            LoadLanguage(this.Culture);

        }
        /// <summary>
        /// 加载配置文件
        /// </summary>
        private void LoadLanguage(string culture)
        {
            string path = "";

            if (GetCurrentLanguage() == 0) //中文
            {
                path = @"Workflow.Language.MultiLanguage.zh-CN.xml";
            }
            else
            {
                path = @"Workflow.Language.MultiLanguage.en-US.xml";
            }
            _resourceManager.LoadLanguage(path, this._hashtable);
        }

        /// <summary>
        /// 获取当前语言 0:中文; 1:英文
        /// </summary>
        /// <param name="culture"></param>
        /// <returns>0:中文; 1:英文</returns>
        public int GetCurrentLanguage()
        {
            int language = 0;
            if (ZH_CHS.Equals(this.Culture.ToLower())) //中文
            {
                language = 0;
            }
            else
            {
                language = 1;
            }
            return language;
        }

        /// <summary>
        /// 取得字段，如果没取到，返回Key
        /// </summary>
        private string GetString(string key, string culture)
        {
            if (string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }
            if (string.IsNullOrEmpty(culture))
            {
                culture = this.Culture;
            }
            if (_hashtable.ContainsKey(key))
            {
                var resource = _hashtable[key];
                return resource.ToString();
            }
            else
            {
                return key;
            }
        }
    }
}
