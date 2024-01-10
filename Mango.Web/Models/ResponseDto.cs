namespace Mango.Web.Models
{
    public class ResponseDto
    {
        public bool IsSuccessful { get; set; } = true;
        public object Result { get; set; }
        public string DisplayMessage { get; set; } = string.Empty;
        public List<string> ErrorMessages { get; set; }
        public ResponseDto() { }

        //public ResponseDto(bool isSuccesful)
        //{
        //    IsSuccesful = isSuccesful;
        //}
    }
}