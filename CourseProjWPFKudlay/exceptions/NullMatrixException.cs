using System;

namespace CourseProjWPFKudlay.exceptions
{   //класс для обработки исключительных ситуаций, возникающих при отстутствии мтарицы
    class NullMatrixException : Exception
    {
        public NullMatrixException(string message) : base(message) //конструктор с заданием текста ошибки
        {
        }
    }
}
