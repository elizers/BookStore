using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;

namespace BookStore
{
    public class Presenter
    {
        private Books _books;

        public Presenter()
        {
            _books = new Books();
        }

        /// <summary>
        /// Количество книг
        /// </summary>
        public int BooksCount
        {
            get => _books.Items.Count;
        }

        /// <summary>
        /// Список книг
        /// </summary>
        public List<Book> Items
        {
            get => _books.Items;
        }

        /// <summary>
        /// Чтение XML-файла
        /// </summary>
        /// <param name="stream"></param>
        public void ReadXmlFile(FileStream stream)
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
        }

        /// <summary>
        /// Запись в XML-файл
        /// </summary>
        /// <param name="pressSaveFile">показывает, нажата ли кнопка "Сохранить"</param>
        /// <param name="FileName">имя открытого файла</param>
        /// <param name="dialogSaveFile_FileName">имя файла, введённое пользователем</param>
        public void WriteToXmlFile(bool pressSaveFile, string FileName, string dialogSaveFile_FileName)
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
                XmlElement yearElem = xmlDocument.CreateElement("year");
                XmlElement priceElem = xmlDocument.CreateElement("price");
                XmlText categoryText = xmlDocument.CreateTextNode(_books.Items[i].Category);
                XmlText langText = xmlDocument.CreateTextNode("en");
                XmlText titleText = xmlDocument.CreateTextNode(_books.Items[i].Title);
                int j = 0;
                XmlText authorText;

                while (j < _books.Items[i].Authors.Count)
                {
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
                    xmlDocument.Save(dialogSaveFile_FileName);
            }

            if (pressSaveFile)
                xmlDocument.Load(FileName);
            else
                xmlDocument.Load(dialogSaveFile_FileName);
        }

        /// <summary>
        /// Редактирование существующих данных в списке книг
        /// </summary>
        /// <param name="book"></param>
        /// <param name="selectedRowNum"></param>
        public void EditData(Book book, int selectedRowNum)
        {
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
        } 
        
        /// <summary>
        /// Формирование HTML-отчёта
        /// </summary>
        /// <param name="fileName"></param>
        public void HTMLReport(string fileName)
        {
            try
            {
                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load("TestDt.xslt");
                xslt.Transform(fileName, "Books.html");
                string path = Application.StartupPath + @"\Books.html";
                var uri = new Uri(path);
                BrowserForm bf = new BrowserForm(uri);
                bf.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
