namespace WinformTemplate
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ���������ÿ���̨�ౣ��һ������Ӧ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            //var dis = Math.Abs(this.Size.Height - Lab_Console.Size.Height);
            //SContainer_Main.SplitterDistance = dis;

            // ��鴰���Ƿ���С��
            if (this.WindowState == FormWindowState.Minimized)
            {
                // �����С������ִ�� SplitterDistance ������
                return;
            }

            var dis = Math.Abs(this.Size.Height - Lab_Console.Size.Height);

            // �ڴ������������ʱ���� SplitterDistance
            var newSplitterDistance = Math.Max(
                SContainer_Main.Panel1MinSize, Math.Min(SContainer_Main.Width - SContainer_Main.Panel2MinSize, dis)
            );

            SContainer_Main.SplitterDistance = newSplitterDistance;
        }
    }
}
