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
        public bool IsTextBoAavailable
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
            IsTextBoAavailable = false;
            pressSaveFile = false;
            pressButtomAddRecord = false;
        }

        /// <summary>
        /// Чтение из xml-файла
        /// </summary>
        /// <param name="stream"></param>
        private void FromXML(FileStream stream)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(stream);
                XmlElement element = xml.DocumentElement;
                _books.Items.Clear();

                foreach (XmlNode xnode in element)
                {
                    Book book = new Book();
                    int checkCount = 0;

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
                                    if (checkCount == 0)
                                        book.Author = childnode.InnerText;
                                    else
                                        book.Author += childnode.InnerText + ";";
                                    checkCount++;
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
                txtBxAuthor.Text = _books.Items[0].Author;
                txtBxCategory.Text = _books.Items[0].Category;
                txtBxPrice.Text = _books.Items[0].Price.ToString();
            }
            catch
            {
                MessageBox.Show(this, "Ошибка! Не удаётся прочитать\n данные из файла.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                dtGrdVwBooks.Rows[i].Cells[1].Value = _books.Items[i].Author;
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
                XmlElement authorElem = xmlDocument.CreateElement("author");
                XmlElement yearElem = xmlDocument.CreateElement("year");
                XmlElement priceElem = xmlDocument.CreateElement("price");

                XmlText categoryText = xmlDocument.CreateTextNode(_books.Items[i].Category);
                XmlText langText = xmlDocument.CreateTextNode("en");

                XmlText titleText = xmlDocument.CreateTextNode(_books.Items[i].Title);
                XmlText authorText = xmlDocument.CreateTextNode(_books.Items[i].Author);
                XmlText yearText = xmlDocument.CreateTextNode(_books.Items[i].Year.ToString());
                XmlText priceText = xmlDocument.CreateTextNode(_books.Items[i].Price.ToString());

                categoryAttr.AppendChild(categoryText);
                langAttr.AppendChild(langText);

                titleElem.AppendChild(titleText);
                authorElem.AppendChild(authorText);
                yearElem.AppendChild(yearText);
                priceElem.AppendChild(priceText);

                bookElem.Attributes.Append(categoryAttr);
                titleElem.Attributes.Append(langAttr);

                bookElem.AppendChild(titleElem);
                bookElem.AppendChild(authorElem);
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
                Book book = new Book
                {
                    Title = dtGrdVwBooks.Rows[i].Cells[0].Value.ToString(),
                    Author = dtGrdVwBooks.Rows[i].Cells[1].Value.ToString(),
                    Category = dtGrdVwBooks.Rows[i].Cells[2].Value.ToString(),
                    Price = float.Parse(dtGrdVwBooks.Rows[i].Cells[3].Value.ToString().Replace('.', ','))
                };

                var index = booksList.Items.FindIndex(x => (x.Title == (book.Title) && (x.Author == book.Author) && (x.Category == book.Category)));
                book.Year = booksList.Items[index].Year;

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
            IsTextBoAavailable = true;
            btnDeleteRecord.Enabled = true;

            if (dtGrdVwBooks.CurrentCell != null)
            {
                int selectedRowNum = dtGrdVwBooks.CurrentCell.RowIndex;

                txtBxTitle.Text = dtGrdVwBooks.Rows[selectedRowNum].Cells[0].Value.ToString();
                txtBxAuthor.Text = dtGrdVwBooks.Rows[selectedRowNum].Cells[1].Value.ToString();
                txtBxCategory.Text = dtGrdVwBooks.Rows[selectedRowNum].Cells[2].Value.ToString();
                txtBxPrice.Text = dtGrdVwBooks.Rows[selectedRowNum].Cells[3].Value.ToString();

                _selectedBook.Title = txtBxTitle.Text;
                _selectedBook.Author = txtBxAuthor.Text;
                _selectedBook.Category = txtBxCategory.Text;

                var index = _books.Items.FindIndex(x => (x.Title == (_selectedBook.Title) && (x.Author == _selectedBook.Author)
                     && (x.Category == _selectedBook.Category)));
                if (index == -1) index = 0;
                _selectedBook.Year = _books.Items[index].Year;
                txtBxYear.Text = _selectedBook.Year.ToString();

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
                Book book = new Book
                {
                    Title = txtBxTitle.Text,
                    Author = txtBxAuthor.Text,
                    Category = txtBxCategory.Text,
                    Year = Int16.Parse(txtBxYear.Text),
                    Price = float.Parse(txtBxPrice.Text)
                };

                if (dtGrdVwBooks.RowCount == 0)
                    throw new Exception("Удаление невозможно, так как в таблице нет элементов");

                if (_books.Items.Remove(book))
                {
                    int selectedRowNum = dtGrdVwBooks.CurrentCell.RowIndex;
                    table.Rows.RemoveAt(selectedRowNum);
                }
                else
                {
                    throw new Exception("Не удаётся удалить выбранный элемент");
                }
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
                IsTextBoAavailable = true;
                IsRedacted = false;
                FileName = dialogOpenFile.FileName;
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
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load("TestDt.xslt");
            xslt.Transform(FileName, "Books.html");
            string path = Application.StartupPath + @"\Books.html";
            var uri = new Uri(path);
            BrowserForm bf = new BrowserForm(uri);
            bf.Show();
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

                    Book book = new Book
                    {
                        Title = txtBxTitle.Text,
                        Author = txtBxAuthor.Text,
                        Category = txtBxCategory.Text,
                        Year = Int16.Parse(txtBxYear.Text),
                        Price = float.Parse(txtBxPrice.Text.Replace('.', ','))
                    };
                    _books.Items.Add(book);

                    dtGrdVwBooks.Rows[_books.Items.Count - 1].Cells[0].Value = book.Title;
                    dtGrdVwBooks.Rows[_books.Items.Count - 1].Cells[1].Value = book.Author;
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
        /// Кнопка "Сохранить изменения"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Book book = new Book
                {
                    Title = txtBxTitle.Text,
                    Author = txtBxAuthor.Text,
                    Category = txtBxCategory.Text,
                    Year = Int16.Parse(txtBxYear.Text),
                    Price = float.Parse(txtBxPrice.Text.Replace('.', ','))
                };

                var index = _books.Items.FindIndex(x => (x.Title == (_selectedBook.Title) && (x.Author == _selectedBook.Author)
                   && (x.Category == _selectedBook.Category)));

                _books.Items[index].Title = book.Title;
                _books.Items[index].Author = book.Author;
                _books.Items[index].Category = book.Category;
                _books.Items[index].Year = book.Year;
                _books.Items[index].Price = book.Price;

                int selectedRowNum = dtGrdVwBooks.CurrentCell.RowIndex;
                dtGrdVwBooks.Rows[selectedRowNum].Cells[0].Value = txtBxTitle.Text;
                dtGrdVwBooks.Rows[selectedRowNum].Cells[1].Value = txtBxAuthor.Text;
                dtGrdVwBooks.Rows[selectedRowNum].Cells[2].Value = txtBxCategory.Text;
                dtGrdVwBooks.Rows[selectedRowNum].Cells[3].Value = txtBxPrice.Text;  
                
                MessageBox.Show("Данные успешно изменены", "Выполнено", MessageBoxButtons.OK, MessageBoxIcon.Information);

                IsTextBoAavailable = false;
                ClearTextBoxs();
            }

            catch
            {
                MessageBox.Show("Не удалось сохранить изменения!", "Не Выполнено", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
