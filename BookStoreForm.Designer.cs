
namespace BookStore
{
    partial class BookStoreForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtGrdVwBooks = new System.Windows.Forms.DataGridView();
            this.dialogOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.dialogSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.btnDeleteRecord = new System.Windows.Forms.Button();
            this.btnAddRecord = new System.Windows.Forms.Button();
            this.txtBxTitle = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.txtBxAuthor = new System.Windows.Forms.TextBox();
            this.txtBxCategory = new System.Windows.Forms.TextBox();
            this.txtBxYear = new System.Windows.Forms.TextBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtBxPrice = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.itemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.CloseFile = new System.Windows.Forms.ToolStripMenuItem();
            this.itemHTMLReport = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSaveEdit = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.itemAddress = new System.Windows.Forms.ToolStripStatusLabel();
            this.dialogCreateFile = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdVwBooks)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtGrdVwBooks
            // 
            this.dtGrdVwBooks.AllowUserToAddRows = false;
            this.dtGrdVwBooks.AllowUserToResizeColumns = false;
            this.dtGrdVwBooks.AllowUserToResizeRows = false;
            this.dtGrdVwBooks.ColumnHeadersHeight = 34;
            this.dtGrdVwBooks.Location = new System.Drawing.Point(22, 46);
            this.dtGrdVwBooks.Name = "dtGrdVwBooks";
            this.dtGrdVwBooks.ReadOnly = true;
            this.dtGrdVwBooks.RowHeadersVisible = false;
            this.dtGrdVwBooks.RowHeadersWidth = 62;
            this.dtGrdVwBooks.RowTemplate.Height = 28;
            this.dtGrdVwBooks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dtGrdVwBooks.Size = new System.Drawing.Size(737, 342);
            this.dtGrdVwBooks.TabIndex = 0;
            this.dtGrdVwBooks.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dtGrdVwBooks_ColumnAdded);
            this.dtGrdVwBooks.SelectionChanged += new System.EventHandler(this.dtGrdVwBooks_SelectionChanged);
            // 
            // dialogOpenFile
            // 
            this.dialogOpenFile.FileName = "dialogOpenFile";
            // 
            // dialogSaveFile
            // 
            this.dialogSaveFile.Filter = "XML-файл (.xml)|.xml";
            // 
            // btnDeleteRecord
            // 
            this.btnDeleteRecord.BackColor = System.Drawing.Color.Silver;
            this.btnDeleteRecord.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnDeleteRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnDeleteRecord.ForeColor = System.Drawing.Color.Black;
            this.btnDeleteRecord.Location = new System.Drawing.Point(22, 410);
            this.btnDeleteRecord.Name = "btnDeleteRecord";
            this.btnDeleteRecord.Size = new System.Drawing.Size(353, 48);
            this.btnDeleteRecord.TabIndex = 4;
            this.btnDeleteRecord.Text = "Удалить запись";
            this.btnDeleteRecord.UseVisualStyleBackColor = true;
            this.btnDeleteRecord.Click += new System.EventHandler(this.btnDeleteRecord_Click);
            // 
            // btnAddRecord
            // 
            this.btnAddRecord.BackColor = System.Drawing.Color.Silver;
            this.btnAddRecord.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnAddRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnAddRecord.ForeColor = System.Drawing.Color.Black;
            this.btnAddRecord.Location = new System.Drawing.Point(406, 410);
            this.btnAddRecord.Name = "btnAddRecord";
            this.btnAddRecord.Size = new System.Drawing.Size(353, 48);
            this.btnAddRecord.TabIndex = 5;
            this.btnAddRecord.Text = "Добавить запись";
            this.btnAddRecord.UseVisualStyleBackColor = true;
            this.btnAddRecord.Click += new System.EventHandler(this.btnAddRecord_Click);
            // 
            // txtBxTitle
            // 
            this.txtBxTitle.Location = new System.Drawing.Point(950, 65);
            this.txtBxTitle.Name = "txtBxTitle";
            this.txtBxTitle.Size = new System.Drawing.Size(216, 26);
            this.txtBxTitle.TabIndex = 6;
            this.txtBxTitle.TextChanged += new System.EventHandler(this.txtBxTitle_TextChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(811, 68);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(133, 20);
            this.lblTitle.TabIndex = 7;
            this.lblTitle.Text = "Название книги:";
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Location = new System.Drawing.Point(884, 116);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(60, 20);
            this.lblAuthor.TabIndex = 8;
            this.lblAuthor.Text = "Автор:";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(851, 167);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(93, 20);
            this.lblCategory.TabIndex = 9;
            this.lblCategory.Text = "Категория:";
            // 
            // txtBxAuthor
            // 
            this.txtBxAuthor.Location = new System.Drawing.Point(950, 116);
            this.txtBxAuthor.Name = "txtBxAuthor";
            this.txtBxAuthor.Size = new System.Drawing.Size(216, 26);
            this.txtBxAuthor.TabIndex = 10;
            this.txtBxAuthor.TextChanged += new System.EventHandler(this.txtBxAuthor_TextChanged);
            // 
            // txtBxCategory
            // 
            this.txtBxCategory.Location = new System.Drawing.Point(950, 167);
            this.txtBxCategory.Name = "txtBxCategory";
            this.txtBxCategory.Size = new System.Drawing.Size(216, 26);
            this.txtBxCategory.TabIndex = 11;
            this.txtBxCategory.TextChanged += new System.EventHandler(this.txtBxCategory_TextChanged);
            // 
            // txtBxYear
            // 
            this.txtBxYear.Location = new System.Drawing.Point(950, 220);
            this.txtBxYear.Name = "txtBxYear";
            this.txtBxYear.Size = new System.Drawing.Size(216, 26);
            this.txtBxYear.TabIndex = 12;
            this.txtBxYear.TextChanged += new System.EventHandler(this.txtBxYear_TextChanged);
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(834, 223);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(110, 20);
            this.lblYear.TabIndex = 13;
            this.lblYear.Text = "Год издания:";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(892, 273);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(52, 20);
            this.lblPrice.TabIndex = 14;
            this.lblPrice.Text = "Цена:";
            // 
            // txtBxPrice
            // 
            this.txtBxPrice.Location = new System.Drawing.Point(950, 273);
            this.txtBxPrice.Name = "txtBxPrice";
            this.txtBxPrice.Size = new System.Drawing.Size(216, 26);
            this.txtBxPrice.TabIndex = 15;
            this.txtBxPrice.TextChanged += new System.EventHandler(this.txtBxPrice_TextChanged);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Location = new System.Drawing.Point(815, 333);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(172, 55);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemFile,
            this.itemHTMLReport});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1227, 33);
            this.menuStrip1.TabIndex = 21;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // itemFile
            // 
            this.itemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFile,
            this.toolStripSeparator1,
            this.SaveFile,
            this.SaveAsFile,
            this.toolStripSeparator2,
            this.CloseFile});
            this.itemFile.Name = "itemFile";
            this.itemFile.Size = new System.Drawing.Size(69, 29);
            this.itemFile.Text = "Файл";
            // 
            // OpenFile
            // 
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(232, 34);
            this.OpenFile.Text = "Открыть";
            this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(229, 6);
            // 
            // SaveFile
            // 
            this.SaveFile.Name = "SaveFile";
            this.SaveFile.Size = new System.Drawing.Size(232, 34);
            this.SaveFile.Text = "Сохранить";
            this.SaveFile.Click += new System.EventHandler(this.SaveFile_Click);
            // 
            // SaveAsFile
            // 
            this.SaveAsFile.Name = "SaveAsFile";
            this.SaveAsFile.Size = new System.Drawing.Size(232, 34);
            this.SaveAsFile.Text = "Сохранить как";
            this.SaveAsFile.Click += new System.EventHandler(this.SaveAsFile_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(229, 6);
            // 
            // CloseFile
            // 
            this.CloseFile.Name = "CloseFile";
            this.CloseFile.Size = new System.Drawing.Size(232, 34);
            this.CloseFile.Text = "Закрыть";
            this.CloseFile.Click += new System.EventHandler(this.CloseFile_Click);
            // 
            // itemHTMLReport
            // 
            this.itemHTMLReport.Name = "itemHTMLReport";
            this.itemHTMLReport.Size = new System.Drawing.Size(141, 29);
            this.itemHTMLReport.Text = "Отчёт в HTML";
            this.itemHTMLReport.Click += new System.EventHandler(this.itemHTMLReport_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(993, 333);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(173, 55);
            this.btnSave.TabIndex = 22;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveEdit
            // 
            this.btnSaveEdit.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnSaveEdit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSaveEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnSaveEdit.ForeColor = System.Drawing.Color.Black;
            this.btnSaveEdit.Location = new System.Drawing.Point(993, 333);
            this.btnSaveEdit.Name = "btnSaveEdit";
            this.btnSaveEdit.Size = new System.Drawing.Size(173, 55);
            this.btnSaveEdit.TabIndex = 23;
            this.btnSaveEdit.Text = "Сохранить изменения";
            this.btnSaveEdit.UseVisualStyleBackColor = true;
            this.btnSaveEdit.Click += new System.EventHandler(this.btnSaveEdit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemAddress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 472);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1227, 22);
            this.statusStrip1.TabIndex = 24;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // itemAddress
            // 
            this.itemAddress.Name = "itemAddress";
            this.itemAddress.Size = new System.Drawing.Size(0, 15);
            // 
            // BookStoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1227, 494);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnSaveEdit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtBxPrice);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.txtBxYear);
            this.Controls.Add(this.txtBxCategory);
            this.Controls.Add(this.txtBxAuthor);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtBxTitle);
            this.Controls.Add(this.btnAddRecord);
            this.Controls.Add(this.btnDeleteRecord);
            this.Controls.Add(this.dtGrdVwBooks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "BookStoreForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BookStore";
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdVwBooks)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtGrdVwBooks;
        private System.Windows.Forms.OpenFileDialog dialogOpenFile;
        private System.Windows.Forms.SaveFileDialog dialogSaveFile;
        private System.Windows.Forms.Button btnDeleteRecord;
        private System.Windows.Forms.Button btnAddRecord;
        private System.Windows.Forms.TextBox txtBxTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.TextBox txtBxAuthor;
        private System.Windows.Forms.TextBox txtBxCategory;
        private System.Windows.Forms.TextBox txtBxYear;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtBxPrice;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem itemFile;
        private System.Windows.Forms.ToolStripMenuItem OpenFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem SaveFile;
        private System.Windows.Forms.ToolStripMenuItem SaveAsFile;
        private System.Windows.Forms.ToolStripMenuItem itemHTMLReport;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSaveEdit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel itemAddress;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem CloseFile;
        private System.Windows.Forms.SaveFileDialog dialogCreateFile;
    }
}

