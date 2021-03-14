namespace Contracts.Utility
{
    public interface IUserPasswordUtility
    {
        string HashPassword(string passedPassword, byte[] userSalt);
        bool IsPasswordCorrect(string passwordToBeCheck, byte[] userSalt, string storedUserPassword);
    }
}
