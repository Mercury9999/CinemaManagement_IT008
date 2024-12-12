using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.CustomControls
{
    public class MyMessageBox
    {
        public static void Show(string Text)
        {
            W_MessageBox messageBox = new W_MessageBox(Text);
            messageBox.ShowDialog();
        }

        public static bool ShowYesNo(string Text)
        {
            Y_MessageBox messageBox = new Y_MessageBox(Text);
            messageBox.ShowDialog();
            return messageBox.Result;
        }

    }
}
