using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Svg;
using static System.Windows.Forms.AxHost;


namespace CS321_Final_1_
{
    public partial class Form1 : Form
    {
        private Stack<Bitmap> undoStack = new Stack<Bitmap>();
        private Stack<Bitmap> redoStack = new Stack<Bitmap>();
        private string currentFilePath;

        public Form1()
        {
            InitializeComponent();

            this.Width = 700;
            this.Height = 500;

            bm = new Bitmap(pictureBox.Width, pictureBox.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            pictureBox.Image = bm;

            undoStack.Push(new Bitmap(bm));
  
    }

        Bitmap bm;
        Graphics g;
        bool paint = false;
        Point px, py;
        Pen p = new Pen(Color.Black, 1);
        Pen erase = new Pen(Color.White, 30);
        int index;
        int x, y, sX, sY, cX, cY;

        ColorDialog cd = new ColorDialog();
        Color new_color;

        public Form1(string filePath)
        {
            InitializeComponent();
            currentFilePath = filePath;
        }

        private void circleButton_Click(object sender, EventArgs e)
        {
            index = 3;
        }

        private void rectangleButton_Click(object sender, EventArgs e)
        {
            index = 4;
        }

        private void lineButton_Click(object sender, EventArgs e)
        {
            index = 5;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if(paint)
            {
                if (index == 3)
                {
                    g.DrawEllipse(p, cX, cY, sX, sY);
                }
                if (index == 4)
                {
                    g.DrawRectangle(p, cX, cY, sX, sY);
                }
                if (index == 5)
                {
                    g.DrawLine(p, cX, cY, x, y);
                }
            }
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            cd.ShowDialog();
            new_color = cd.Color;
            pictureBox.BackColor = cd.Color;
            p.Color = cd.Color;
        }

        

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
            py = e.Location;

            cX = e.X;
            cY = e.Y;
        }

        static Point set_point(PictureBox pb, Point pt)
        {
            float pX = 1f * pb.Width / pb.Width;
            float pY = 1f * pb.Height / pb.Height;
            return new Point((int)(pt.X*pX), (int)(pt.Y*pY));
        }
        
        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if(index == 7)
            {
                Point point = set_point(pictureBox, e.Location);
                Fill(bm, point.X, point.Y, new_color);
            }
        }

        private void fillButton_Click(object sender, EventArgs e)
        {
            index = 7;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            if (undoStack.Count > 1)
            {
                redoStack.Push(undoStack.Pop());
                Bitmap prevBm = undoStack.Peek();
                g.DrawImage(prevBm, 0, 0);
                pictureBox.Refresh();
            }
        }

   

        private void redoButton_Click(object sender, EventArgs e)
        {
            if (redoStack.Count > 0)
            {
                // Pop the latest state of the bitmap from the redo stack
                Bitmap prevBm = redoStack.Pop();
                // Copy the previous state of the bitmap to the current bitmap
                g.DrawImage(prevBm, 0, 0);
                // Push the previous state of the bitmap onto the undo stack
                undoStack.Push(prevBm);
                // Refresh the picture box
                pictureBox.Refresh();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            // save the current file
            if (!string.IsNullOrEmpty(currentFilePath))
            {
                // save the image to the file
                bm.Save(currentFilePath);
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveFileDialog.FileName;
                    // save the image to the file
                    bm.Save(currentFilePath);
                }
            }
        }

        private void exportSVGButton_Click(object sender, EventArgs e)
        {
            // Create an instance of SvgDocument
            SvgDocument svg = new SvgDocument();

            svg.Width = pictureBox.Width;
            svg.Height = pictureBox.Height;
            SvgRectangle rect = new SvgRectangle();
            rect.Fill = new SvgColourServer(Color.White);
            svg.Children.Add(rect);

            // Create a bitmap from the picture box
            Bitmap bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.DrawToBitmap(bmp, pictureBox.Bounds);
            string base64 = Convert.ToBase64String(ImageToByteArray(bmp));

            // Create an SVG image element from the Base64 string
            SvgImage svgImg = new SvgImage();
            svgImg.Href = $"data:image/png;base64,{base64}";
            svg.Children.Add(svgImg);

            // Save the SVG to a file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "SVG files (*.svg)|*.svg|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                {
                    using (XmlTextWriter xtw = new XmlTextWriter(sw))
                    {
                        // Set the formatting options for the XmlTextWriter
                        xtw.Formatting = Formatting.Indented;
                        xtw.Indentation = 4;

                        // Write the SvgDocument to the XmlTextWriter
                        svg.Write(xtw);
                    }
                }
            }
        }

        // Helper method bitmap to a byte array
        private byte[] ImageToByteArray(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
    

        private void openButton_Click(object sender, EventArgs e)
        {
            FileManager.LoadFile(g, pictureBox, redoStack, undoStack);
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            FileManager.NewFile(currentFilePath);
        }

        private PrintDocument printDocument1 = new PrintDocument();
        private Bitmap memoryImage;
        private void printButton_Click(object sender, EventArgs e)
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);

            printDocument1.Print();
        }
        
        private void PrintDocument1_PrintPage(System.Object sender,
        System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;

            sX = x - cX;
            sY = y - cY;

            if (index == 3)
            {
                g.DrawEllipse(p, cX, cY, sX, sY);
            }
            if (index == 4)
            {
                g.DrawRectangle(p, cX, cY, sX, sY);
            }
            if (index == 5)
            {
                g.DrawLine(p, cX, cY, x, y);
            }

            undoStack.Push(new Bitmap(bm));
            redoStack.Clear();
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (paint)
            {
                if (index == 1)
                {
                    px = e.Location;
                    g.DrawLine(p, px, py);
                    py = px;
                }
                if (index == 2)
                {
                    px = e.Location;
                    g.DrawLine(erase, px, py);
                    py = px;
                }
            }
            pictureBox.Refresh();

            x = e.X;
            y = e.Y;
            sX = e.X - cX;
            sY = e.Y - cY;
        }

        private void pencilButton_Click(object sender, EventArgs e)
        {
            index = 1;
        }

        private void eraserButton_Click(object sender, EventArgs e)
        {
            index = 2;
        }

        private void validate(Bitmap bm, Stack<Point>sp, int x, int y, Color old_color, Color new_color)
        {
            Color cx = bm.GetPixel(x, y);
            if (cx == old_color)
            {
                sp.Push(new Point(x, y));
                bm.SetPixel(x, y, new_color);
            }
        }

        public void Fill(Bitmap bm, int x, int y, Color new_clr)
        {
            Color old_color = bm.GetPixel(x, y);
            Stack<Point> pixel = new Stack<Point>();
            pixel.Push(new Point(x, y));
            bm.SetPixel(x, y, new_clr);
            if (old_color == new_clr) return;

            while (pixel.Count > 0)
            {
                Point pt = (Point)pixel.Pop();
                if(pt.X > 0 && pt.Y > 0 && pt.X<bm.Width-1 && pt.Y<bm.Height-1)
                {
                    validate(bm, pixel, pt.X - 1, pt.Y, old_color, new_clr);
                    validate(bm, pixel, pt.X, pt.Y-1, old_color, new_clr);
                    validate(bm, pixel, pt.X + 1, pt.Y, old_color, new_clr);
                    validate(bm, pixel, pt.X, pt.Y + 1, old_color, new_clr);
                }
            }
        }

        public void WelcomeOpenButtonClicked()
        {
            FileManager.LoadFile(g, pictureBox, redoStack, undoStack);
        }
    }
}
