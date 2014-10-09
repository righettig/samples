namespace Mvc5DIFriendly.Services
{
    public interface IFoo
    {
        void DummyMethod();
    }

    public class Foo : IFoo
    {
        public void DummyMethod()
        {
            // do nothing
        }
    }
}