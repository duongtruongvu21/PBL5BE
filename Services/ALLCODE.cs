namespace PBL5BE.API.Services
{
    public class ALLCODE
    {
        public string Key { get; set; }
        public string Type { get; set; }
        public string ViValue { get; set; }
        public string EnValue { get; set; }

        public static ALLCODE Sex_Male = new ALLCODE() { Key = "S1", Type = "Sex", ViValue = "Nam", EnValue = "Male" };
        public static ALLCODE Sex_Female = new ALLCODE() { Key = "S2", Type = "Sex", ViValue = "Nữ", EnValue = "Female" };
        public static ALLCODE Sex_Other = new ALLCODE() { Key = "S3", Type = "Sex", ViValue = "Khác", EnValue = "Other" };

        public static ALLCODE Role_Admin = new ALLCODE() { Key = "R1", Type = "Role", ViValue = "Admin", EnValue = "Admin" };
        public static ALLCODE Role_User = new ALLCODE() { Key = "R2", Type = "Role", ViValue = "User", EnValue = "User" };
        public static List<ALLCODE> GetALLCODE()
        {
            List<ALLCODE> aLLCODEs = new List<ALLCODE>();
            aLLCODEs.Add(Role_Admin);
            aLLCODEs.Add(Role_User);
            aLLCODEs.Add(new ALLCODE() { Key = "US1", Type = "UserStatus", ViValue = "Bình Thường", EnValue = "Normal" });
            aLLCODEs.Add(new ALLCODE() { Key = "US2", Type = "UserStatus", ViValue = "Bị Khoá", EnValue = "Blocked" });
            aLLCODEs.Add(Sex_Male);
            aLLCODEs.Add(Sex_Female);
            aLLCODEs.Add(Sex_Other);
            aLLCODEs.Add(new ALLCODE() { Key = "PS1", Type = "ProductStatus", ViValue = "Đang Kinh Doanh", EnValue = "..." });
            aLLCODEs.Add(new ALLCODE() { Key = "PS2", Type = "ProductStatus", ViValue = "Hết Hàng", EnValue = "..." });
            aLLCODEs.Add(new ALLCODE() { Key = "PS3", Type = "ProductStatus", ViValue = "Ngừng Kinh Doanh", EnValue = "..." });

            return aLLCODEs;
        }
    }
}