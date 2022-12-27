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
        NoAccess = -9,
        ProductBuyAmountExcessProductCount,
        NotEmail,
        NotAdmin,
    }
}