namespace Workflow.Platform.Common.Helper
{
    /// <summary>
    /// �ļ�ѹ�����ѹ������
    /// </summary>
    public class GZipResult
    {
        /// <summary>
        /// ѹ�����а����������ļ�,������Ŀ¼�µ��ļ�
        /// </summary>
        public GZipFileInfo[] Files = null;
        /// <summary>
        /// Ҫѹ�����ļ���
        /// </summary>
        public int FileCount = 0;
        public long TempFileSize = 0;
        public long ZipFileSize = 0;
        /// <summary>
        /// ѹ���ٷֱ�
        /// </summary>
        public int CompressionPercent = 0;
        /// <summary>
        /// ��ʱ�ļ�
        /// </summary>
        public string TempFile = null;
        /// <summary>
        /// ѹ���ļ�
        /// </summary>
        public string ZipFile = null;
        /// <summary>
        /// �Ƿ�ɾ����ʱ�ļ�
        /// </summary>
        public bool TempFileDeleted = false;
        public bool Errors = false;
    }
}