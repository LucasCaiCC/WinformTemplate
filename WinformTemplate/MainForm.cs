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
            var dis = Math.Abs(this.Size.Height - Lab_Console.Size.Height);
            SContainer_Main.SplitterDistance = dis;
        }
    }
}
