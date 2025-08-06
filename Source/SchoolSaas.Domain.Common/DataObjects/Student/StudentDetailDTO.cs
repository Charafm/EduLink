namespace SchoolSaas.Domain.Common.DataObjects.Student
{
    public class StudentDetailDTO
    {
        public StudentDTO Student { get; set; }
        public string MedicalInfo { get; set; }
        public string EmergencyContact { get; set; }
        public string PreviousSchool { get; set; }
        public string AdditionalNotes { get; set; }
    }
}
