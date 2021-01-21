using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2020_11_18_Text_Editor
{
    public partial class MainForm : Form
    {
        Stack<string> undoList = new Stack<string>();
        public MainForm()
        {
            InitializeComponent();
            toolStrip1.ImageList = imageList1;

            for (int i = 0, j = 0; i < toolStrip1.Items.Count; i++)
            {
                if (!(toolStrip1.Items[i] is ToolStripSeparator))
                {
                    toolStrip1.Items[i].ImageIndex = j++;
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(richTextBox1.TextLength > 0)
            {
                DialogResult dialogResult = MessageBox.Show(    
                   "Do you want to save changes?",
                   "Notepad",
                    MessageBoxButtons.YesNoCancel
                    );
                if(dialogResult == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, e);

                }
                richTextBox1.Clear();
            }
        }
        private void Menu_Copy(System.Object sender, System.EventArgs e)
        {
            // Ensure that text is selected in the text box.   
            if (richTextBox1.SelectionLength > 0)
                // Copy the selected text to the Clipboard.
                richTextBox1.Copy();
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            .
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Rtf files (*.rtf)|*.rtf|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(fileDialog.FileName, RichTextBoxStreamType.RichText);
            }
            this.Text = fileDialog.FileName;
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start("chrome.exe", e.LinkText);
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Rtf, richTextBox1.SelectedRtf);
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText(TextDataFormat.Rtf))
            {
                richTextBox1.SelectedRtf
                    = Clipboard.GetData(DataFormats.Rtf).ToString();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            undoList.Push(richTextBox1.Text);
        }

        private void undoToolStripButton_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
            {
                richTextBox1.Text = "";
                richTextBox1.Text = undoList.Pop();
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = fontDialog.Font;
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.ShowDialog();
            this.BackColor = colorDialog.Color;
        }

        private void textColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.ShowDialog();
            richTextBox1.SelectionColor = colorDialog.Color;
        }

    }


        
            
    
}
