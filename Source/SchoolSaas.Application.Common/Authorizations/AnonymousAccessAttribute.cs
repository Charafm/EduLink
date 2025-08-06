namespace SchoolSaas.Application.Common.Authorizations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AnonymousAccessAttribute : Attribute
    {
    }
}