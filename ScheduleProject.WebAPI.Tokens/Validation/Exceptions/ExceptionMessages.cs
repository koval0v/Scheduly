namespace Services.Validation.Exceptions
{
    /// <summary>
    /// Privides basic exception messages.
    /// </summary>
    public static class ExceptionMessages
    {

        /// <summary>
        /// The email {0} is already used.
        /// </summary>
        public static string EmailUsed = "The email {0} is already used.";

        /// <summary>
        /// The nickname {0} is already taken.
        /// </summary>
        public static string NicknameTaken = "The nickname {0} is already taken.";

        /// <summary>
        /// The object of type {0} with property {1} = {2} is not found.
        /// </summary>
        public static string NotFound = "The object of type {0} with property {1} = {2} is not found. ";

        /// <summary>
        /// The object of type {0} with propery {1} = {2} is already exist.
        /// </summary>
        public static string AlreadyExists = "The object of type {0} with propery {1} = {2} is already exist.";

        /// <summary>
        /// The inputed password is incorrect.
        /// </summary>
        public static string WrongPassword = "The inputed password is incorrect.";

        /// <summary>
        /// Choose nickname different from current. 
        /// </summary>
        public static string NicknamesAreEqual = "Choose nickname different from current. ";

        /// <summary>
        /// The email {0} is different from the user's. User id: {1}.
        /// </summary>
        public static string DifferenceEmail = "The email {0} is different from the user's. User id: {1}. ";

        /// <summary>
        /// The operation {0} is prohibited with the object of type {1} with property {2} = {3}.
        /// </summary>

        public static string ProhibitedOperation = "The operation {0} is prohibited with the object of type {1} with property {2} = {3}. ";
    }
}
