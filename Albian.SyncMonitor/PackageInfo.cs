namespace FastDFS.Client.Component
{
    /// <summary>
    /// �������Ϣ
    /// </summary>
    public class PackageInfo
    {
        private byte[] _body;

        /// <summary>
        /// �õ��������ô������
        /// </summary>
        /// <value>The body.</value>
        public byte[] Body
        {
            get { return _body; }
            set { _body = value; }
        }

        private byte _errorNo;

        /// <summary>
        /// �õ��������ô����
        /// </summary>
        /// <value>The error no.</value>
        public byte ErrorNo
        {
            get { return _errorNo; }
            set { _errorNo = value; }
        }

        /// <summary>
        /// ��ʼ�� <see cref="PackageInfo"/> ����.
        /// </summary>
        /// <param name="errorNo">�����.</param>
        /// <param name="body">�������.</param>
        public PackageInfo(byte errorNo, byte[] body)
        {
            _errorNo = errorNo;
            _body = body;
        }
    }
}