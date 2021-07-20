using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace BookStore
{
    public partial class BookStoreForm : Form
    {
        /// <summary>
        /// Состояние открытого файла
        /// </summary>
        private bool _isOpened;

        /// <summary>
        /// Состояние редактирования данных
        /// </summary>
        private bool _isTextBoAavailable;

        /// <summary>
        /// Состояния кнопок "Сохранить" и "Сохранить изменения"
        /// </summary>
        private bool _isRedacted;

        /// <summary>
        /// Отслеживание нажатия кнопки "Добавить запись"
        /// </summary>
        private bool pressButtomAddRecord;

        /// <summary>
        /// Отслеживание нажатия пункта меню "Сохранить" 
        /// </summary>
        private bool pressSaveFile;

        /// <summary>
        /// Адрес и имя файла
        /// </summary>
        private string _filename;

        /// <summary>
        /// Список книг
        /// </summary>
        private Books _books;

        /// <summary>
        /// Текущая книга
        /// </summary>
        private Book _selectedBook;

        /// <summary>
        /// Таблица, которая отображается в DataGridView
        /// </summary>
        private DataTable table;

        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName
        {
            get
            {
                return _filename;
            }
            set
            {
                _filename = value;
            }
        }

        /// <summary>
        /// Состояние открытия файла 
        /// </summary>
        public bool IsOpened
        {
            get
            {
                return this._isOpened;
            }
            set
            {
                dtGrdVwBooks.Enabled = value;
                SaveFile.Enabled = value;
                SaveAsFile.Enabled = value;
                CloseFile.Enabled = value;
                btnAddRecord.Enabled = value;
                btnDeleteRecord.Enabled = value;
                itemHTMLReport.Enabled = value;
                btnClear.Enabled = value;
                btnSave.Enabled = value;
                btnSaveEdit.Enabled = value;

                txtBxAuthor.Enabled = value;
                txtBxCategory.Enabled = value;
                txtBxPrice.Enabled = value;
                txtBxTitle.Enabled = value;
                txtBxYear.Enabled = value;

                btnSaveEdit.Visible = value;

                itemAddress.Text = (value) ? this.FileName : "<Адрес файла>";
                _isOpened = value;
            }
        }

        /// <summary>
        /// Состояние редактирования данных
        /// </summary>
        public bool IsTextBoxAvailable
        {
            get
            {
                return this._isTextBoAavailable;
            }
            set
            {
                txtBxAuthor.Enabled = value;
                txtBxCategory.Enabled = value;
                txtBxPrice.Enabled = value;
                txtBxTitle.Enabled = value;
                txtBxYear.Enabled = value;
                _isTextBoAavailable = value;
            }
        }

        /// <summary>
        /// Состояния кнопок "Сохранить" и "Сохранить изменения"
        /// </summary>
        public bool IsRedacted
        {
            get
            {
                return this._isRedacted;
            }
            set
            {
                btnSaveEdit.Visible = value;
                btnSave.Visible = !value;
                btnSaveEdit.Enabled = value;
                btnClear.Enabled = value;
                _isRedacted = value;
            }
        }

        public BookStoreForm()
        {
            InitializeComponent();
            _books = new Books();
            _selectedBook = new Book();
            IsOpened = false;
            IsRedacted = false;
            IsTextBoxAvailable = false;
            pressSaveFile = false;
            pressButtomAddRecord = false;
        }

        /// <summary>
        /// Чтение из xml-файла
        /// </summary>
        /// <param name="stream"></param>
        private void FromXML(FileStream stream)
        {
            // getBook
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(stream);
                XmlElement element = xml.DocumentElement;
                _books.Items.Clear();

                foreach (XmlNode xnode in element)
                {
                    Book book = new Book();

                    if (xnode.Attributes.Count > 0)
                    {
                        XmlNode attr = xnode.Attributes.GetNamedItem("category");
                        if (attr != null)
                            book.Category = attr.Value;
                    }

                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        switch (childnode.Name)
                        {
                            case ("title"):
                                {
                                    book.Title = childnode.InnerText;
                                    break;
                                }
                            case ("author"):
                                {
                                    book.Authors.Add(childnode.InnerText);
                                    break;
                                }
                            case ("year"):
                                {
                                    book.Year = Int16.Parse(childnode.InnerText);
                                    break;
                                }
                            case ("price"):
                                {
                                    book.Price = float.Parse(childnode.InnerText.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture);
                                    break;
                                }
                            default:
                                {
                                    throw new IOException("Ошибка! Информация не может быть записана в файл.");
                                }
                        }

                    }

                    _books.Items.Add(book);
                }

                CreateTable(_books.Items.Count);
                FillListBooksToDataGridView();

                stream.Close();

                txtBxTitle.Text = _books.Items[0].Title;
                int i = 0;
                while (i < (_books.Items[0].Authors.Count - 1))
                {
                    txtBxAuthor.Text += _books.Items[0].Authors[i] + "; ";
                    i++;
                }
                txtBxAuthor.Text += _books.Items[0].Authors[i];

                txtBxCategory.Text = _books.Items[0].Category;
                txtBxYear.Text = _books.Items[0].Year.ToString();
                txtBxPrice.Text = _books.Items[0].Price.ToString();
            }
            catch
            {
                MessageBox.Show(this, "Ошибка! Не удаётся прочитать\n данные из файла.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsOpened = false;
        }
    }

        /// <summary>
        /// Создание таблицы
        /// </summary>
        private void CreateTable(int count)
        {
            table = new DataTable("Table");
            DataColumn[] columns = new DataColumn[4];
            for (int i = 0; i < 4; i++)
            {
                columns[i] = new DataColumn(i.ToString());
                table.Columns.Add(columns[i]);
            }
            for (int i = 0; i < count; i++)
            {
                DataRow new_row = table.NewRow();
                table.Rows.Add(new_row);
            }
            dtGrdVwBooks.DataSource = table;

            // устанавливаем динамическое расширение строки
            dtGrdVwBooks.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dtGrdVwBooks.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            string[] array = { "Книга", "Автор", "Категория", "Цена" };
            int k = 140;
            for (int i = 0; i < array.Length; i++)
            {
                dtGrdVwBooks.Columns[i].HeaderText = array[i];
                dtGrdVwBooks.Columns[i].Width = k - i*10;
            }
        }

        /// <summary>
        /// Заполнение строк в DataGridView
        /// </summary>
        private void FillListBooksToDataGridView()
        {
            for (byte i = 0; i < _books.Items.Count; i++)
            {
                dtGrdVwBooks.Rows[i].Cells[0].Value = _books.Items[i].Title;
                int j = 0;
                while (j < (_books.Items[i].Authors.Count - 1))
                {
                    dtGrdVwBooks.Rows[i].Cells[1].Value += _books.Items[i].Authors[j] + "; ";
                    j++;
                }
                dtGrdVwBooks.Rows[i].Cells[1].Value += _books.Items[i].Authors[j]; 

                dtGrdVwBooks.Rows[i].Cells[2].Value = _books.Items[i].Category;
                dtGrdVwBooks.Rows[i].Cells[3].Value = _books.Items[i].Price;
            }
        }

        /// <summary>
        /// Запись в xml-файл
        /// </summary>
        private void ToXML()
        {
            XmlDocument xmlDocument = new XmlDocument();

            //создание декларации документа
            XmlDeclaration XmlDec = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDocument.AppendChild(XmlDec);

            // создание корневого элемента
            XmlElement bookStoreElem = xmlDocument.CreateElement("bookstore");
            xmlDocument.AppendChild(bookStoreElem);

            XmlElement xRoot = xmlDocument.DocumentElement;

            for (int i = 0; i < _books.Items.Count; i++)
            {
                XmlElement bookElem = xmlDocument.CreateElement("book");

                XmlAttribute categoryAttr = xmlDocument.CreateAttribute("category");
                XmlAttribute langAttr = xmlDocument.CreateAttribute("lang");

                XmlElement titleElem = xmlDocument.CreateElement("title");
                //XmlElement authorElem = xmlDocument.CreateElement("author");
                XmlElement yearElem = xmlDocument.CreateElement("year");
                XmlElement priceElem = xmlDocument.CreateElement("price");

                XmlText categoryText = xmlDocument.CreateTextNode(_books.Items[i].Category);
                XmlText langText = xmlDocument.CreateTextNode("en");

                XmlText titleText = xmlDocument.CreateTextNode(_books.Items[i].Title);

                int j = 0;
                XmlText authorText;
                //MessageBox.Show("_books.Items[i].Authors.Count = " + _books.Items[i].Authors.Count);
                while (j < _books.Items[i].Authors.Count)
                {
                    //MessageBox.Show("j = " + j);
                    //MessageBox.Show("_books.Items[i].Authors[j] = " + _books.Items[i].Authors[j]);
                    XmlElement authorElem = xmlDocument.CreateElement("author");
                    authorText = xmlDocument.CreateTextNode(_books.Items[i].Authors[j]);
                    authorElem.AppendChild(authorText);
                    bookElem.AppendChild(authorElem);
                    j++;
                }

                XmlText yearText = xmlDocument.CreateTextNode(_books.Items[i].Year.ToString());
                XmlText priceText = xmlDocument.CreateTextNode(_books.Items[i].Price.ToString());

                categoryAttr.AppendChild(categoryText);
                langAttr.AppendChild(langText);

                titleElem.AppendChild(titleText);

                yearElem.AppendChild(yearText);
                priceElem.AppendChild(priceText);

                bookElem.Attributes.Append(categoryAttr);
                titleElem.Attributes.Append(langAttr);

                bookElem.AppendChild(titleElem);
                
                bookElem.AppendChild(yearElem);
                bookElem.AppendChild(priceElem);

                bookStoreElem.AppendChild(bookElem);
                xRoot.AppendChild(bookElem);

                if (pressSaveFile)
                    xmlDocument.Save(FileName);
                else
                    xmlDocument.Save(dialogSaveFile.FileName);
            }

            if (pressSaveFile)
                xmlDocument.Load(FileName);
            else
                xmlDocument.Load(dialogSaveFile.FileName);

        }

        /// <summary>
        /// Заполнение списка книг информацией из DataGridView
        /// </summary>
        private void FillListBooksFromDataGridView()
        {
            // заводим вспомогательный список, чтобы сохранилась информация о годе издания каждой книги
            Books booksList = new Books();
            for (byte i = 0; i < _books.Items.Count; i++)
            {
                booksList.Items.Add(_books.Items[i]);
            }

            _books.Items.Clear();

            for (byte i = 0; i < dtGrdVwBooks.RowCount; i++)
            {
                Book book = new Book();
                book.Title = dtGrdVwBooks.Rows[i].Cells[0].Value.ToString();
                book.Authors = new List<string>();
                int j = 0;

                string[] authors = dtGrdVwBooks.Rows[i].Cells[1].Value.ToString().Split(';');

                while (j < booksList.Items[i].Authors.Count)
                {
                    book.Authors.Add(authors[j].Trim());
                    j++;
                }

                book.Category = dtGrdVwBooks.Rows[i].Cells[2].Value.ToString();
                book.Price = float.Parse(dtGrdVwBooks.Rows[i].Cells[3].Value.ToString().Replace('.', ','));
                book.Year = booksList.Items[i].Year;

                _books.Items.Add(book);
            }
        }

        /// <summary>
        /// Кнопка "Добавить запись"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            pressButtomAddRecord = true;
            dtGrdVwBooks.ClearSelection();
            ClearTextBoxs();
            btnDeleteRecord.Enabled = false;
        }

        /// <summary>
        /// Отображение выбранной строки в поле добавления новой строки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtGrdVwBooks_SelectionChanged(object sender, EventArgs e)
        {
            IsTextBoxAvailable = true;
            btnDeleteRecord.Enabled = true;

            if (dtGrdVwBooks.CurrentCell != null)
            {
                int selectedRowNum = dtGrdVwBooks.CurrentCell.RowIndex;

                txtBxTitle.Text = dtGrdVwBooks.Rows[selectedRowNum].Cells[0].Value.ToString();
                txtBxAuthor.Text = dtGrdVwBooks.Rows[selectedRowNum].Cells[1].Value.ToString();
                txtBxCategory.Text = dtGrdVwBooks.Rows[selectedRowNum].Cells[2].Value.ToString();
                txtBxPrice.Text = dtGrdVwBooks.Rows[selectedRowNum].Cells[3].Value.ToString();

                _selectedBook.Title = txtBxTitle.Text;

                if (txtBxTitle.Text != "")
                {
                    string[] authors = txtBxAuthor.Text.Split(';');
                    int j = 0;
                    _selectedBook.Authors = new List<string>();
                    while (j < authors.Length)
                    {
                        _selectedBook.Authors.Add(authors[j]);
                        j++;
                    }
                    _selectedBook.Category = txtBxCategory.Text;

                    int index = dtGrdVwBooks.CurrentCell.RowIndex;
                    _selectedBook.Year = _books.Items[index].Year;
                    txtBxYear.Text = _selectedBook.Year.ToString();
                }              

                btnSaveEdit.Enabled = false;              
            }
        }

        /// <summary>
        /// Кнопка "Удалить запись"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteRecord_Click(object sender, EventArgs e)
        {
            try
            {
                Book book = InitBook();

                if (dtGrdVwBooks.RowCount == 0)
                    throw new Exception("Удаление невозможно, так как в таблице нет элементов");

                _books.Items.Remove(book);
                int selectedRowNum = dtGrdVwBooks.CurrentCell.RowIndex;
                table.Rows.RemoveAt(selectedRowNum);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Не выполнено", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Отключение сортировки столбцов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtGrdVwBooks_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dtGrdVwBooks.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        /// <summary>
        /// Очистка поля ввода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearTextBoxs();
            btnSaveEdit.Enabled = false;
        }

        /// <summary>
        /// Пункт меню "Открыть" в меню "Файл"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFile_Click(object sender, EventArgs e)
        {
            dialogOpenFile.FileName = "";
            if (dialogOpenFile.ShowDialog(this) == DialogResult.OK)
            {
                IsOpened = true;
                IsTextBoxAvailable = true;
                IsRedacted = false;
                FileName = dialogOpenFile.FileName;
                itemAddress.Text = FileName;
                FileStream stream = new FileStream(FileName, FileMode.Open);
                FromXML(stream);
            }
        }

        /// <summary>
        /// Пункт меню "Сохранить" в меню "Файл"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFile_Click(object sender, EventArgs e)
        {
            pressSaveFile = true;
            WriteToFile();
        }

        /// <summary>
        /// Пункт меню "Сохранить как" в меню "Файл"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsFile_Click(object sender, EventArgs e)
        {
            pressSaveFile = false;
            dialogSaveFile.FileName = "";
            if (dialogSaveFile.ShowDialog(this) == DialogResult.OK)
                WriteToFile();
        }

        /// <summary>
        /// Извлечение данных из таблицы и запись их в файл
        /// </summary>
        private void WriteToFile()
        {
            try
            {
                FillListBooksFromDataGridView();
                ToXML();
                MessageBox.Show("Данные успешно записаны в файл", "Выполнено", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show(this, "Ошибка! Не удаётся записать\n данные в файл.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Вкладка "Отчёт HTML"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemHTMLReport_Click(object sender, EventArgs e)
        {
            try
            {
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load("TestDt.xslt");
                xslt.Transform(FileName, "Books.html");
                string path = Application.StartupPath + @"\Books.html";
                var uri = new Uri(path);
                BrowserForm bf = new BrowserForm(uri);
                bf.Show();
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Кнопка "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Int16.Parse(txtBxYear.Text) > DateTime.Now.Year)
                    MessageBox.Show("Год издания не может быть больше текущего года!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    // создаём строку DataGridView
                    DataRow new_row = table.NewRow();
                    table.Rows.Add(new_row);

                    Book book = InitBook();

                    _books.Items.Add(book);

                    dtGrdVwBooks.Rows[_books.Items.Count - 1].Cells[0].Value = book.Title;
                    int j = 0;
                    while (j < book.Authors.Count)
                    {
                        dtGrdVwBooks.Rows[_books.Items.Count - 1].Cells[1].Value = book.Authors[j] + '\n';
                        j++;
                    }

                    dtGrdVwBooks.Rows[_books.Items.Count - 1].Cells[2].Value = book.Category;
                    dtGrdVwBooks.Rows[_books.Items.Count - 1].Cells[3].Value = book.Price;
                }

                ClearTextBoxs();
            }
            catch
            {
                MessageBox.Show(this, "Не удаётся добавить\nновую запись.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Отслеживание изменения текста в поле ввода названия книги
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBxTitle_TextChanged(object sender, EventArgs e)
        {
            btnClear.Enabled = true;
            IsRedacted = !pressButtomAddRecord;
        }

        /// <summary>
        /// Отслеживание изменения текста в поле ввода автора книги
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBxAuthor_TextChanged(object sender, EventArgs e)
        {
            btnClear.Enabled = true;
            IsRedacted = !pressButtomAddRecord;
        }

        /// <summary>
        /// Отслеживание изменения текста в поле ввода категории книги
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBxCategory_TextChanged(object sender, EventArgs e)
        {
            btnClear.Enabled = true;
            IsRedacted = !pressButtomAddRecord;
        }

        /// <summary>
        /// Отслеживание изменения текста в поле ввода года издания книги
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBxYear_TextChanged(object sender, EventArgs e)
        {
            btnClear.Enabled = true;
            IsRedacted = !pressButtomAddRecord;
        }

        /// <summary>
        /// Отслеживание изменения текста в поле ввода цены книги
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBxPrice_TextChanged(object sender, EventArgs e)
        {
            btnClear.Enabled = true;
            IsRedacted = !pressButtomAddRecord;
        }

        /// <summary>
        /// Считывание информации о книге из полей ввода
        /// </summary>
        /// <returns></returns>
        private Book InitBook()
        {
            Book book = new Book();
            book.Title = txtBxTitle.Text;
            book.Category = txtBxCategory.Text;
            book.Year = Int16.Parse(txtBxYear.Text);
            book.Price = float.Parse(txtBxPrice.Text.Replace('.', ','));

            book.Authors = new List<string>();
            string[] authors = txtBxAuthor.Text.Split(';');

            int j = 0;
            while (j < authors.Length)
            {
                book.Authors.Add(authors[j].Trim());
                j++;
            }

            return book;
        }

        /// <summary>
        /// Кнопка "Сохранить изменения"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Book book = InitBook();

                int selectedRowNum = dtGrdVwBooks.CurrentCell.RowIndex;

                _books.Items[selectedRowNum].Title = book.Title;
                _books.Items[selectedRowNum].Authors = new List<string>();
                int j = 0;
                while (j < _books.Items[selectedRowNum].Authors.Count)
                {
                    _books.Items[selectedRowNum].Authors.Add(book.Authors[j]);
                    j++;
                }

                _books.Items[selectedRowNum].Category = book.Category;
                _books.Items[selectedRowNum].Year = book.Year;
                _books.Items[selectedRowNum].Price = book.Price;

                dtGrdVwBooks.Rows[selectedRowNum].Cells[0].Value = txtBxTitle.Text;
                dtGrdVwBooks.Rows[selectedRowNum].Cells[1].Value = txtBxAuthor.Text;
                dtGrdVwBooks.Rows[selectedRowNum].Cells[2].Value = txtBxCategory.Text;
                dtGrdVwBooks.Rows[selectedRowNum].Cells[3].Value = txtBxPrice.Text;  
                
                MessageBox.Show("Данные успешно изменены", "Выполнено", MessageBoxButtons.OK, MessageBoxIcon.Information);

                IsTextBoxAvailable = false;
                ClearTextBoxs();
            }

            catch
            {
                MessageBox.Show("Не удалось сохранить изменения!", "Не выполнено", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Очистка полей ввода информации о книге
        /// </summary>
        public void ClearTextBoxs()
        {
            txtBxTitle.Text = "";
            txtBxAuthor.Text = "";
            txtBxCategory.Text = "";
            txtBxYear.Text = "";
            txtBxPrice.Text = "";
        }

        /// <summary>
        /// Пункт меню "Закрыть" в меню "Файл"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseFile_Click(object sender, EventArgs e)
        {
            IsOpened = false;
            ClearTextBoxs();
            table.Clear();
        }
    }
}
