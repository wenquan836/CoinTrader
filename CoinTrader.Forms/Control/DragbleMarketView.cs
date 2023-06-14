using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;

namespace CoinTrader.Forms.Control
{
    public class DragbleMarketView:System.Windows.Forms.UserControl
    {

        private bool dragging = false;
        private Point dragPoint;
        private System.Windows.Forms.Control oldParent;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Left)
            {
                if (!dragging)
                {
                    dragPoint = e.Location;
                    dragging = true;
                    oldParent = this.Parent;
                    this.Capture = true;

                    System.Windows.Forms.Control rootParent = this.Parent.Parent;
                    var position = default(Point);
                    System.Windows.Forms.Control p = this;

                    while (p != rootParent)
                    {
                        position.X += p.Location.X;
                        position.Y += p.Location.Y;
                        p = p.Parent;
                    }
                    this.Parent = rootParent;
                    this.Location = position;
                    this.BringToFront();
                }
                else
                {
                    Point delta = default(Point);
                    delta.X = e.Location.X - dragPoint.X;
                    delta.Y = e.Location.Y - dragPoint.Y;
                    var point = this.Location;
                    point.X += delta.X;
                    point.Y += delta.Y;
                    this.Location = point;
                   
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (dragging)
            {
                var myBound = this.Bounds;

                var scrLocation = LocalToScreenPoint(this, e.Location);
                scrLocation = ScreenPointToLocalPoint(oldParent, scrLocation);
                scrLocation.X = scrLocation.X + myBound.Width / 2;
                scrLocation.Y = scrLocation.Y + myBound.Height / 2;

                int insertIndex = oldParent.Controls.Count;
                int index = 0;
                foreach (System.Windows.Forms.Control control in oldParent.Controls)
                {
                    var rect = control.Bounds;
                    if (rect.Contains(scrLocation))
                    {
                        insertIndex = index;
                        break;
                    }

                    index++;
                }

                this.Parent = oldParent;

                oldParent.Controls.SetChildIndex(this, insertIndex);
                dragging = false;
                this.Capture = false;
            }
        }
        private Point ScreenPointToLocalPoint(System.Windows.Forms.Control control, Point scrPoint)
        {
            var srcLocation = LocalToScreenPoint(control, control.Location);
            var result = default(Point);
            result.X = scrPoint.X - srcLocation.X;
            result.Y = scrPoint.Y - srcLocation.Y;
            return result;
        }

        private Point LocalToScreenPoint(System.Windows.Forms.Control control, Point source)
        {
            var point = default(Point);

            var node = control;

            while (node != null)
            {
                point.X += node.Location.X;
                point.Y += node.Location.Y;
                node = node.Parent;
            }

            return point;
        }
 
    }
}
