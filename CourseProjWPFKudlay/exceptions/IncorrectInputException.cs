using System;

namespace CourseProjWPFKudlay.exceptions
{   //класс для обработки исключительных ситуаций, возникающих при некорректном вводе
    class IncorrectInputException : Exception 
    {
        public IncorrectInputException(string message) : base(message) //конструктор с заданием текста ошибки
        {
        }
    }
}
