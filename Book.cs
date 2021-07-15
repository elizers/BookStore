using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private string author;

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
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }

        /// <summary>
        /// Автор
        /// </summary>
        public string Author
        {
            get
            {
                return this.author;
            }
            set
            {
                this.author = value;
            }
        }
        
        /// <summary>
        /// Категория
        /// </summary>
        public string Category
        {
            get
            {
                return this.category;
            }
            set
            {
                this.category = value;
            }
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
                    throw new ArgumentOutOfRangeException("Ошибка! Некорректное значение {nameof(value)}.");
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
            this.author = "";
            this.category = "";
            this.year = 0;
            this.price = 0;
        }

        public Book(string title, string author, string category, int year, float price)
        {
            this.title = title;
            this.author = author;
            this.category = category;
            this.year = year;
            this.price = price;
        }

        /// <summary>
        /// Проверка на равенство
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Book other)
        {
            return (this.Author == other.Author)
                && (this.Title == other.Title)
                && (this.Price == other.Price)
                && (this.Category == other.Category)
                && (this.Year == other.Year);
        }
    }
}
