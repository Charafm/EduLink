namespace SchoolSaas.Domain.Common.DataObjects.Transfer
{
    public class TransferEligibilityResultDTO
    {
        public bool IsEligible { get; set; }
        public bool PendingFees { get; set; }
        public bool DisciplinaryIssues { get; set; }
    }
}
