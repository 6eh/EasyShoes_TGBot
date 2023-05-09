using System;
namespace EasyShoes_TGBot.Models
{
	public class OrderInfo
	{
		public long ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientTGUserName { get; set; }
        public string OrderText { get; set; }
    }

	public enum Gender
	{
		Male,
		Female
	}
}

