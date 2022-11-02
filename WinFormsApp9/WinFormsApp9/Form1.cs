namespace WinFormsApp9
{
    public partial class Form1 : Form
    {
        Bitmap b;
        Graphics g;
        Rectangle rect;

        Dictionary<double, long> Lap = new Dictionary<double, long>();
        bool por = false;


        Pen penRectangle = new Pen(Color.Black, 0.2f);



        public Form1()
        {
            InitializeComponent();
            Lap.Add(0, 10);
            Lap.Add(1, 57);
            Lap.Add(2, 28);
            Lap.Add(7, 42);
            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);
            rect = new Rectangle(20, 20, b.Width - 40, b.Height - 40);
            generateHistogram(rect, Lap, por);
            pictureBox1.Image = b;
        }

        private void generateHistogram(Rectangle r, Dictionary<double, long> Lap, bool por = false)
        {

            int n = Lap.Count;

            double maxValue = Lap.Values.Max();
            for (int i = 0; i < n; i++)
            {
                Rectangle q;
                int lf, hi, rg, bo;
                if (por)
                {
                    lf = fromXRealToXVirtual(i, 0, n, r.Left, r.Width);
                    hi = fromYRealToYVirtual(Lap.ElementAt(i).Value, 0, maxValue, r.Top, r.Height);
                    rg = fromXRealToXVirtual(i + 1, 0, n, r.Left, r.Width);
                    bo = fromYRealToYVirtual(0, 0, maxValue, r.Top, r.Height);
                }
                else
                {
                    lf = fromXRealToXVirtual(0, 0, maxValue, r.Left, r.Width);
                    hi = fromYRealToYVirtual(i + 1, 0, n, r.Top, r.Height);
                    rg = fromXRealToXVirtual(Lap.ElementAt(i).Value, 0, maxValue, r.Left, r.Width);
                    bo = fromYRealToYVirtual(i, 0, n, r.Top, r.Height);
                }
                q = Rectangle.FromLTRB(lf, hi, rg, bo);


                g.FillRectangle(new SolidBrush(Color.Yellow), q);
                g.DrawRectangle(penRectangle, q);

            }
        }

        private void redraw()
        {

            g.Clear(BackColor);
            generateHistogram(rect, Lap, por);
            g.DrawRectangle(penRectangle, rect);

            pictureBox1.Image = b;
        }


        private int fromXRealToXVirtual(double x, double minX, double maxX, int left, int w)
        {
            return left + (int)(w * (x - minX) / (maxX - minX));
        }

        private int fromYRealToYVirtual(double y, double minY, double maxY, int top, int h)
        {
            return top + (int)(h - h * (y - minY) / (maxY - minY));
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           
                por = change.Checked;
                redraw();

            
        }
    }
}
