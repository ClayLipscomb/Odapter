using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace OdapterWnFrm.Controls {
    public partial class OdatperCheckBox : CheckBox {
        #region Constructor Region
        public OdatperCheckBox() {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);
            Appearance = Appearance.Normal;
            FlatStyle = FlatStyle.Flat;
            CheckAlign = ContentAlignment.MiddleCenter;
            FlatAppearance.BorderSize = 0;
            AutoSize = false;
        }
        #endregion

        #region Paint Region
        protected override void OnPaint(PaintEventArgs e) {
            var g = e.Graphics;
            var rect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);
            var size = 11;

            // defaults
            var backgroundColor = ColorWnFrm.BackgroundColor;
            var backgroundColorInput = ColorWnFrm.BackgroundColorInput;
            var foreColorInput = ColorWnFrm.ForeColorInput;
            var foreColorInputDisabled = ColorWnFrm.ForeColorInputDisabled;
            var borderColorInput = ColorWnFrm.BorderColorInput;

            if (Enabled) {
                if (Focused) {
                    borderColorInput = Color.YellowGreen; // BorderColorInput;
                } else {
                    borderColorInput = ColorWnFrm.BorderColorInput;
                }
            } else {
                borderColorInput = ColorWnFrm.BorderColorInputDisabled; 
            }

            // entire control background
            using (var b = new SolidBrush(backgroundColor /*Colors.GreyBackground*/)) {
                g.FillRectangle(b, rect);
            }

            // border of check box square
            using (var p = new Pen(borderColorInput)) {
                var boxRect = new Rectangle(0, (rect.Height / 2) - (size / 2) - 2, size + 1, size + 1);
                g.DrawRectangle(p, boxRect);
            }

            // background of check box square
            using (var b = new SolidBrush(backgroundColorInput)) {
                Rectangle boxRect = new Rectangle(2, (rect.Height / 2) - ((size - 4) / 2), size - 3, size - 3);
                g.FillRectangle(b, boxRect);
            }

            // check
            if (Checked) {
                var color = Enabled ? foreColorInput : foreColorInputDisabled;
                using (var b = new SolidBrush(color)) {
                    using (Font font = new Font("Wingdings", 9f, FontStyle.Bold))
                        e.Graphics.DrawString("ü", font, b, 0, 2);
                }
            }
        }
        #endregion
    }
}