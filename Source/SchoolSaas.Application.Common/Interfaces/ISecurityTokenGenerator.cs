namespace SchoolSaas.Application.Common.Interfaces
{
    public interface ISecurityTokenGenerator
    {
        string GetCode(string secret);
        bool ValidateCode(string secret, string code);
    }
}