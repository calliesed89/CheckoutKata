namespace CheckoutKata
{
    public interface ICheckout
    {
        public int GetTotalPrice();
        public void Scan(string item);     
    }
}