namespace PBL5BE.API.Data
{
    public class ReturnData
    {
        public bool isSuccess {get;set;}
        public List<object> Data {get;set;}
        public string errMessage {get;set;}
    }
}