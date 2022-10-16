namespace PBL5BE.API.Data
{
    public class ReturnValue
    {
        private ReturnValue() {}
        private static ReturnValue _instance;
        public static ReturnValue instance {
            get {
                if (_instance == null) _instance = new ReturnValue();
                return _instance;
            }
        }
        public int ServerCodeException = 0;
        public int Success = 1;
        public int IDNotFound = -1;
        public int ForeignKeyIDNotFound = -2;
        public int Existed = -10;
    }
}