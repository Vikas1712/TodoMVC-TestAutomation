/*
Instead of initializing a page in each class we created a base abstract class.
We are using Generic type parameter T which make it possible to design classes and method
that defer the specification of one or more types until the
classes and method is declared and instantiated by code
*/

namespace SeleniumSpecFlow.Base;

public abstract class BasePage : Base
{
    protected TPage GetInstance<TPage>() where TPage : new()
    {
        TPage pageInstance = new();

        return pageInstance;
    }

    public TPage As<TPage>() where TPage : BasePage
    {
        return (TPage)this;
    }
}