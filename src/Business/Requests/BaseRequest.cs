namespace Business.Requests
{
    public abstract class BaseRequest
    {
        internal int UserId { get; private set; }

        public void SetUserId(int id)
            => UserId = id;
    }
}
