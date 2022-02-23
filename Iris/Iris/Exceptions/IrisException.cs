using Iris.Api;
using Iris.Api.Results;

namespace Iris.Exceptions
{
    /// <summary>
    /// Класс Iris ошибок
    /// </summary>
    public abstract class IrisException: Exception
    {
        /// <summary>
        /// Сообщение ошибки (рус)
        /// </summary>
        public string RussianMessage { get; }

        /// <summary>
        /// .ctor
        /// </summary>
        protected IrisException(string russianMessage = null) : base()
        {
            RussianMessage = russianMessage;
        }

        /// <inheritdoc/>
        public override string Message => $"Iris process error ({RussianMessage})";

        /// <summary>
        /// Ответ об ошибке
        /// </summary>
        public virtual ErrorResult ErrorResponse => new(500, RussianMessage);
    }
}
