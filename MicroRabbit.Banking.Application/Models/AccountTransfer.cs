using System.Reflection.Metadata.Ecma335;

namespace MicroRabbit.Banking.Application.Models
{
    public class AccountTransfer
    {
        public  int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public decimal TransferAmount { get; set; }
    }
}