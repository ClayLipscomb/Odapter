using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace OdapterWnFrm.Controls {
    public partial class OdapterComboBox : ComboBox {
        private Color _ButtonColor = SystemColors.Control;
        public Color ButtonColor {
            get { return _ButtonColor; }
            set {
                _ButtonColor = value;
                this.Invalidate();
            }
        }

        protected override void WndProc(ref Message m) {
            base.WndProc(ref m);

            switch (m.Msg) {
                case 0xf:
                    Graphics g = this.CreateGraphics();

                    // Draw the background of the dropdown button
                    Rectangle rect = new Rectangle(this.Width - 17, 0, 17, this.Height);
                    g.FillRectangle(new SolidBrush(ColorWnFrm.BackgroundColor), rect);


                    // Create the path for the arrow
                    System.Drawing.Drawing2D.GraphicsPath pth = new System.Drawing.Drawing2D.GraphicsPath();
                    PointF TopLeft = new PointF(this.Width - 13, (this.Height - 5) / 2);
                    PointF TopRight = new PointF(this.Width - 6, (this.Height - 5) / 2);
                    PointF Bottom = new PointF(this.Width - 9, (this.Height + 2) / 2);
                    pth.AddLine(TopLeft, TopRight);
                    pth.AddLine(TopRight, Bottom);

                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    // Draw the arrow
                    g.FillPath(new SolidBrush(ColorWnFrm.LabelColor), pth);

                    // Draw border
                    Rectangle dropDownBounds = new Rectangle(0, 0, Width, Height);
                    ControlPaint.DrawBorder(g, dropDownBounds, ColorWnFrm.BorderColorInput, ButtonBorderStyle.Solid);

                    break;
                default:
                    break;
            }
        }

        private Color _borderColor = Color.White;
        public Color BorderColor {
            get { return _borderColor; }
            set {
                _borderColor = value;
                Invalidate(); // causes control to be redrawn
            }
        }

        private ButtonBorderStyle _borderStyle = ButtonBorderStyle.Solid;
        public ButtonBorderStyle BorderStyle {
            get { return _borderStyle; }
            set {
                _borderStyle = value;
                Invalidate();
            }
        }

        //protected override void OnLostFocus(System.EventArgs e) {
        //    base.OnLostFocus(e);
        //    this.Invalidate();
        //}

        //protected override void OnGotFocus(System.EventArgs e) {
        //    base.OnGotFocus(e);
        //    this.Invalidate();
        //}
        //protected override void OnResize(EventArgs e) {
        //    base.OnResize(e);
        //    this.Invalidate();
        //}
    }
}
