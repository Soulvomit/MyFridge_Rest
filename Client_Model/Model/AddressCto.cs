using Client_Model.Model.Interface;

namespace Client_Model.Model
{
    public class AddressCto : ICto
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Extension { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int Country { get; set; }
    }
}
