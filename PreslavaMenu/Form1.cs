using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PreslavaMenu
{
    public partial class Form1 : Form
    {
        string nameFile = "unknown";
        public Form1()
        {
            InitializeComponent();
        }
        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            saveDLG.Filter = "Text files|*.txt|Rich Text Files|*.rtf|Word files|*.docx";
            saveDLG.FilterIndex = 2;
            saveDLG.FileName = "";
            saveDLG.Title = "Open In Text Pad";
            if (saveDLG.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            nameFile = saveDLG.FileName;
            this.Text = "Text Pad - " + saveDLG.FileName;
            if (saveDLG.FilterIndex == 1)
            {
                rtb.SaveFile(saveDLG.FileName, RichTextBoxStreamType.PlainText);
            }
            else if (saveDLG.FilterIndex == 2)
            {
                rtb.SaveFile(saveDLG.FileName, RichTextBoxStreamType.RichText);
            }
            else
            {
                MessageBox.Show("CAN  USE ONLY TXT OR RTF!", "ERROR!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            rtb.Modified = false;
        }
        private void menuNew_Click(object sender, EventArgs e)
        {
            if (rtb.Modified)
            {
                DialogResult answer =
                    MessageBox.Show("Save changes?",
                    "Confirmation",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);

                if (answer == DialogResult.Yes)
                {
                    menuSave_Click(sender, e);
                }
                if (answer == DialogResult.Cancel)
                {
                    return;
                }
            }
            rtb.Clear();
            nameFile = "";
            this.Text = "Text Pad - New File";
            rtb.Modified = false;
        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            if (nameFile == "unknown" || nameFile == "")
            {
                menuSaveAs_Click(sender, e);
            }
            else
            {
                rtb.SaveFile(nameFile, RichTextBoxStreamType.RichText);
            }
            rtb.Modified = false;
        }


        private void menuOpen_Click(object sender, EventArgs e)
        {
            if (rtb.Modified)
            {
                DialogResult answer =
                    MessageBox.Show("Save changes?",
                    "Confirmation",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);

                if (answer == DialogResult.Yes)
                {
                    menuSave_Click(sender, e);
                }
                if (answer == DialogResult.Cancel)
                {
                    return;
                }
            }
            openDLG.Filter = "Text files|*.txt|Rich Text Files|*.rtf|Word files|*.doc;*.docx|All files|*.*";
            openDLG.FilterIndex = 4;
            openDLG.FileName = "";
            openDLG.Title = "Open In Text Pad";
            if (openDLG.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            nameFile = openDLG.FileName;
            this.Text = "Text Pad - " + openDLG.SafeFileName;
            if (openDLG.FilterIndex == 1)
            {
                rtb.LoadFile(openDLG.FileName, RichTextBoxStreamType.PlainText);
            }
            else if (openDLG.FilterIndex == 2)
            {
                rtb.LoadFile(openDLG.FileName, RichTextBoxStreamType.RichText);
            }
            else
            {
                MessageBox.Show("Save changes?",
                    "Confirmation",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);
                return;
            }
            rtb.Modified = false;
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void menuCopy_Click(object sender, EventArgs e)
        {
            rtb.Copy();

        }

        private void menuUndo_Click(object sender, EventArgs e)
        {
            rtb.Undo();

        }

        private void menuPaste_Click(object sender, EventArgs e)
        {
            rtb.Paste();

        }

        private void menuCut_Click(object sender, EventArgs e)
        {
            rtb.Cut();

        }

        private void menuRedo_Click(object sender, EventArgs e)
        {
            rtb.Redo();

        }

        private void menuSelectAll_Click(object sender, EventArgs e)
        {
            rtb.SelectAll();

        }

        private void menuFont_Click(object sender, EventArgs e)
        {
            fontDLG.ShowColor = true;
            fontDLG.ShowEffects = true;
            fontDLG.ShowApply = true;
            fontDLG.ShowDialog();
            rtb.SelectionFont = fontDLG.Font;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (rtb.Modified)
            {
                DialogResult answer =
                    MessageBox.Show("Save changes?",
                    "Confirmation",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);

                if (answer == DialogResult.Yes)
                {
                    menuSave_Click(sender, e);
                }
                if (answer == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void menuPrint_Click(object sender, EventArgs e)
        {
            printDLG.AllowSelection = true;
            printDLG.AllowPrintToFile = true;
            printDLG.AllowCurrentPage = true;
            printDLG.AllowSomePages = true;
            printDLG.UseEXDialog = true;
            if (printDLG.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Printed!",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void menuPageSetup_Click(object sender, EventArgs e)
        {
            using (PrintDocument printDocument = new PrintDocument())
            {
                using (PageSetupDialog pageSetupDLG = new PageSetupDialog())
                {
                    pageSetupDLG.Document = printDocument;

                    if (pageSetupDLG.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show("Ready!",
                        "Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void menuWordWrap_Click(object sender, EventArgs e)
        {
            rtb.WordWrap = menuWordWrap.Checked;
        }
    }
}
