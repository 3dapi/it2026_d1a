using System;
using System.Windows.Forms;

namespace _26031027_JeonSeunghyo_GameProject
{
    public class GameForm : Form
    {
        public GameForm()
        {
            this.Text = "My 2D Game - 26031027 전승효";
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // TODO: glc2dcsharp 초기화
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            // TODO: 리소스 정리
        }
    }
}
