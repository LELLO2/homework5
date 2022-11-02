namespace homework5
{ 
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.pictureBox1.MouseWheel += pictureBox1_MouseWheel;
        }

        Bitmap b;
        Graphics g;

        int x_down;
        int y_down;

        int x_mouse;
        int y_mouse;

        int r_width;
        int r_height;

        Rectangle r;

        bool drag = false;
        bool resizing = false;

   



        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (r.Contains(e.X, e.Y))
            {
                x_mouse = e.X;
                y_mouse = e.Y;

                x_down = r.X;
                y_down = r.Y;

                r_width = r.Width;
                r_height = r.Height;

                if (e.Button == MouseButtons.Left)
                {
                    drag = true;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    resizing = true;
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
            resizing = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int delta_x = e.X - x_mouse;
            int delta_y = e.Y - y_mouse;

            if (r != null)
            {
                if (drag)
                {

                    r.X = x_down + delta_x;
                    r.Y = y_down + delta_y;

                    redraw(r, g);
                }
                else if (resizing)
                {
                    r.Width = r_width + delta_x;
                    r.Height = r_height + delta_y;

                    redraw(r, g);
                }
            }
        }

        private void redraw(Rectangle r, Graphics g)
        {
            g.Clear(Color.White);

            g.DrawRectangle(Pens.Green, r);
            pictureBox1.Image = b;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                r = new Rectangle((int)(e.X - 1.10 * (e.X - r.X)), (int)(e.Y - 1.10 * (e.Y - r.Y)), (int)(r.Width * 1.10), (int)(r.Height * 1.10));
                redraw(r, g);
            }
            else if (r.Height >= 30 && r.Width >= 30)
            {
                r = new Rectangle((int)(e.X - 0.90 * (e.X - r.X)), (int)(e.Y - 0.90 * (e.Y - r.Y)), (int)(r.Width * 0.90), (int)(r.Height * 0.90));
                redraw(r, g);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(b);

            r = new Rectangle(20, 20, 500, 300);
            g.Clear(Color.White);
            g.DrawRectangle(Pens.Green, r);
            pictureBox1.Image = b;
        }
    }
}