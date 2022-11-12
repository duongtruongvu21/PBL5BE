namespace PBL5BE.API.Data.Enums
{
    public enum STTCode : int
    {
            Existed = -10,
            UserExisted = -5,
            IncorrectPassword = -4,
            UserNotExist = -3,
            ForeignKeyIDNotFound = -2,
            IDNotFound = -1,
            ServerCodeException = 0,
            Success = 1,
            OrderIsPaid,
            
            E1 = 5,
            E2 = 6,
            E3 = 7,
            E4 = 8,
            E5 = 9,
            E6 = 10,
            E7 = 11,
            E8 = 12,
    }
}