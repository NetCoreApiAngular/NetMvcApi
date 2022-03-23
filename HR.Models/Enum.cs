namespace HR.Models
{
    public enum LoginResult
    {
        Unknown = 0,
        Success = 1,
        IsLockedOut = 2,
        InvalidEmail = 3,
        InvalidPassword = 4,
        UnActive = 5,
    }
}
