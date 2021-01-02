namespace MicroRabbit.Transfer.Domain.Models
{
    public class TransferLog
    {
        public int Id { get; set; }
        public int fromAccount { get; set; }
        public int ToAccount { get; set; }
        public decimal TrnasferAmount { get; set; }
            
    }
}