using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BookStore
{
    public class Books
    {
        /// <summary>
        /// Список книг
        /// </summary>
        private List<Book> items;

        public List<Book> Items
        {
            get
            {
                return this.items;
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Books()
        {
            this.items = new List<Book>();
        }

        /// <summary>
        /// Добавление книги в конец списка
        /// </summary>
        /// <param name="book">добавляемая книга</param>
        public void Add(Book book)
        {
            this.items.Add(book);
        }

        /// <summary>
        /// Удаление элемента
        /// </summary>
        /// <param name="book">удаляемая книга</param>
        public void Remove(Book book)
        {
            this.items.Remove(book);
        }

        /// <summary>
        /// Очистка списка
        /// </summary>
        public void Clear()
        {
            this.items.Clear();
        }

        /// <summary>
        /// Проверка на равенство
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(List<Book> other)
        {
            if (this.Items.Count != other.Count)
                return false;

            for (int i = 0; i < this.items.Count; i++)
            {
                if (this.items[i] != other[i])
                {
                    return false;
                }
            }
            return true;
        }

    }
}
