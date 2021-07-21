using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookStore
{
    public class Book : IEquatable<Book>
    {
        /// <summary>
        /// Текущий год
        /// </summary>
        public int Max_Year = DateTime.Now.Year;

        /// <summary>
        /// Заголовок книги
        /// </summary>
        private string title;

        /// <summary>
        /// Автор книги
        /// </summary>
        private List<string> authors;

        /// <summary>
        /// Категория
        /// </summary>
        private string category;

        /// <summary>
        /// Год издания
        /// </summary>
        private int year;

        /// <summary>
        /// Цена
        /// </summary>
        private float price;

        /// <summary>
        /// Название книги
        /// </summary>
        public string Title
        {
            get => this.title;
            set => this.title = value;
        }

        /// <summary>
        /// Автор
        /// </summary>
        public List<string> Authors
        {
            get => this.authors;
            set => this.authors = value;
        }
        
        /// <summary>
        /// Категория
        /// </summary>
        public string Category
        {
            get => this.category;
            set => this.category = value;
        }

        /// <summary>
        /// Год издания
        /// </summary>
        public int Year
        {
            get
            {
                return this.year;
            }
            set
            {
                if (value > Max_Year)
                {
                    throw new ArgumentOutOfRangeException("Ошибка! Некорректное значение.");
                }
                
                this.year = value;
            }
        }

        /// <summary>
        /// Цена
        /// </summary>
        public float Price
        {
            get
            {
                return this.price;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Ошибка! Некорректное значение {nameof(value)}.");
                }

                this.price = value;
            }
        }
        
        public Book()
        {
            this.title = "";
            this.authors = new List<string>();
            this.category = "";
            this.year = 0;
            this.price = 0;
        }

        /// <summary>
        /// Проверка на равенство
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Book other)
        {
            return (this.Title == other.Title)
                && (this.Price == other.Price)
                && (this.Category == other.Category)
                && (this.Year == other.Year)
                && EqualsAuthors(this.Authors, other.Authors);
        }

        /// <summary>
        /// Проверка на равенство авторов
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        private bool EqualsAuthors(List<string> t1, List<string> t2)
        {
            if (t1.Count != t2.Count)
                return false;

            for (int i = 0; i < t1.Count; i++)
            {
                if (t1[i] != t2[i])
                    return false;
            }
            return true;
        }
    }
}
