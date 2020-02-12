using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOTNETQRCode
{
    static class Program
    {
        internal class HideOnStartupApplicationContext : ApplicationContext
        {
            private Form mainFormInternal;

            // 构造函数，主窗体被存储在mainFormInternal
            public HideOnStartupApplicationContext(Form mainForm)
            {
                this.mainFormInternal = mainForm;

                this.mainFormInternal.Closed += new EventHandler(mainFormInternal_Closed);
            }

            // 当主窗体被关闭时，退出应用程序
            void mainFormInternal_Closed(object sender, EventArgs e)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            /*HideOnStartupApplicationContext context = new HideOnStartupApplicationContext(new Form1());
            Application.Run(context);*/
        }
    }
}