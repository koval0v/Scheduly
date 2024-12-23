namespace Services.Validation.Exceptions
{
    /// <summary>
    /// Throw when an item is already exist.
    /// </summary>
    /// <seealso cref="CustomException" />
    public class AlreadyExistException : CustomException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlreadyExistException"/> class.
        /// </summary>
        /// <param name="mes">The message.</param>
        public AlreadyExistException(string mes) : base(mes) { }
    }
}
