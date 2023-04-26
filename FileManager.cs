using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SkiaSharp;
using ShimSkiaSharp;
using Svg;

namespace CS321_Final_1_
{
    public class FileManager
    {
        public static void Print(PrintDocument printDoc)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDoc;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
            }
        }

        public static void PrintPage(object sender, PrintPageEventArgs e)
        {
            Panel panel = (Panel)sender;
            Bitmap bmp = new Bitmap(panel.Width, panel.Height);
            panel.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
            e.Graphics.DrawImage(bmp, e.PageBounds);
        }

        public static void LoadFile(Graphics g, PictureBox pictureBox, Stack<Bitmap> redoStack, Stack<Bitmap> undoStack)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string currentFilePath = openFileDialog.FileName;
                Bitmap loadedBm = new Bitmap(currentFilePath);

                redoStack.Clear();

                // Push the current state of the bitmap onto the undo stack
                if (undoStack.Count > 0)
                {
                    Bitmap prevBm = undoStack.Peek();
                    if (!prevBm.Equals(loadedBm))
                    {
                        undoStack.Push(new Bitmap(loadedBm));
                    }
                }
                else
                {
                    undoStack.Push(new Bitmap(loadedBm));
                }
                // Copy the loaded image to the current bitmap
                g.DrawImage(loadedBm, 0, 0);
                pictureBox.Refresh();
            }
        }

        public static void NewFile(string filename)
        {
            Form1 newForm = new Form1();
            newForm.Show();
        }
    }
}
