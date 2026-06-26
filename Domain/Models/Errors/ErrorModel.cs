namespace Domain.Models.Errors
{
    public static class ErrorModel
    {
        public const string SystemError = "System Error";

        #region Login Errors
        public const string UserNotFound = "User not found";
        public const string InvalidPassword = "Invalid Password";

        public const string UsernameRequired = "Username is required field";
        #endregion

        #region Register Errors
        public const string UserAlreadyExist = "The user with this email already exists";

        public const string InvalidEmail = "The Email is not valid";
        public const string EmailRequired = "The Email is required field";

        public const string FirstNameRequired = "The First Name is required field";
        public const string LastNameRequired = "The Last Name is required field";

        public const string PasswordRequired = "The Password is required field";
        public const string PasswordMinLength =
            "The Password must be greater than 6 characters";
        #endregion

        #region Generate Report Errors
        public const string IpAddressRequired = "The Ip Address is required field";
        public const string IpAddressInvalid = "The Ip Address is invalid";
        #endregion

    }
}
