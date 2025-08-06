using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoolSaas.Domain.Backoffice.Academic;
using SchoolSaas.Domain.Backoffice.Resources;
using SchoolSaas.Domain.Backoffice.School;
using SchoolSaas.Domain.Common.Enums;
using SchoolSaas.Infrastructure.Backoffice.Context;

namespace SchoolSaas.Infrastructure.Backoffice.Helper
{
    public class SeedData
    {
        public static void EnsureSeedData(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BackofficeContext>();

            context.Database.EnsureCreated();

            SeedBranches(context);
            SeedClassrooms(context);
            SeedAcademicYears(context);
            SeedSemesters(context);
            SeedGradeLevels(context);
            SeedGradeSections(context);
            AssignGradeSectionsToClassrooms(context);
            SeedCourses(context);
            SeedCourseGradeMappings(context);
            SeedBooks(context);
            SeedSchoolSupplies(context);
            SeedGradeResources(context);
            SeedVacations(context);

            context.SaveChanges();
        }

        private static void SeedBranches(DbContext context)
        {
            if (!context.Set<Branch>().Any())
            {
                var now = DateTime.UtcNow;
                context.Set<Branch>().AddRange(
                    new Branch
                    {
                        Id = Guid.Parse("fe5cbd73-c197-4a12-89b1-030f73035a0c"),
                        BranchNameFr = "Casablanca",
                        BranchNameAr = "الدار البيضاء",
                        SchoolId = Guid.Parse("5e6e7d89-d45e-4f65-8ad2-78bd9e0201a4"),
                        CityId = Guid.Parse("6b1e521e-aff2-4e07-9221-88d2774a3a4e"),
                        AddressFr = "123 Bd Zerktouni",
                        AddressAr = "123 شارع الزرقطوني",
                        Phone = "+212522334455",
                        PrincipalNameFr = "Mr. Ahmed",
                        PrincipalNameAr = "السيد أحمد",
                        Created = now
                    },
                    new Branch
                    {
                        Id = Guid.Parse("7c4b9b93-1210-4b56-88b4-9449b8d84d66"),
                        BranchNameFr = "Rabat",
                        BranchNameAr = "الرباط",
                        SchoolId = Guid.Parse("5e6e7d89-d45e-4f65-8ad2-78bd9e0201a4"),
                        CityId = Guid.Parse("2e5eb92e-6d5c-4ef8-8b06-088bcf0c676d"),
                        AddressFr = "456 Rue Agdal",
                        AddressAr = "456 شارع أكدال",
                        Phone = "+212537778899",
                        PrincipalNameFr = "Mme. Fatima",
                        PrincipalNameAr = "السيدة فاطمة",
                        Created = now
                    },
                    new Branch
                    {
                        Id = Guid.Parse("d2cdb7a1-4804-463c-9a10-83889570d79c"),
                        BranchNameFr = "Marrakech",
                        BranchNameAr = "مراكش",
                        SchoolId = Guid.Parse("5e6e7d89-d45e-4f65-8ad2-78bd9e0201a4"),
                        CityId = Guid.Parse("1e061bb6-92a6-498a-a1ef-35ef46c5732e"),
                        AddressFr = "789 Bd Hassan II",
                        AddressAr = "789 شارع الحسن الثاني",
                        Phone = "+212524448866",
                        PrincipalNameFr = "Dr. Youssef",
                        PrincipalNameAr = "الدكتور يوسف",
                        Created = now
                    }
                );
            }
        }

        private static void SeedClassrooms(DbContext context)
        {
            var now = DateTime.UtcNow;
            var branches = context.Set<Branch>().ToList();
            var classrooms = new List<Classroom>();

            foreach (var (branch, index) in branches.Select((b, i) => (b, i)))
            {
                var roomTitles = new[] { "Salle A", "Salle B", "Salle C", "Salle D", "Salle E", "Salle F", "Salle G", "Salle H", "Salle I" };
                var roomTitlesAr = new[] { "قاعة أ", "قاعة ب", "قاعة ج", "قاعة د", "قاعة هـ", "قاعة ف", "قاعة ج", "قاعة ح", "قاعة ط" };

                for (int i = 0; i < roomTitles.Length; i++)
                {
                    classrooms.Add(new Classroom
                    {
                        Id = Guid.NewGuid(),
                        RoomTitleFr = roomTitles[i],
                        RoomTitleAr = roomTitlesAr[i],
                        RoomType = ClassroomTypeEnum.Standard,
                        Capacity = 30,
                        BuildingId = null,
                        Created = now
                    });
                }
            }

            if (classrooms.Any())
                context.Set<Classroom>().AddRange(classrooms);
        }



        private static void SeedSemesters(DbContext context)
        {
            if (!context.Set<Semester>().Any())
            {
                var now = DateTime.UtcNow;
                var years = context.Set<AcademicYear>().ToList();
                var semesters = new List<Semester>();

                foreach (var year in years)
                {
                    semesters.AddRange(new[]
                    {
                        new Semester
                        {
                            Id = Guid.NewGuid(),
                            AcademicYearId = year.Id,
                            Name = SemesterNameEnum.Fall,
                            StartDate = new DateTime(year.StartYear, 9, 1),
                            EndDate = new DateTime(year.StartYear + 1, 1, 15),
                            Created = now
                        },
                        new Semester
                        {
                            Id = Guid.NewGuid(),
                            AcademicYearId = year.Id,
                            Name = SemesterNameEnum.Spring,
                            StartDate = new DateTime(year.StartYear + 1, 2, 1),
                            EndDate = new DateTime(year.StartYear + 1, 6, 30),
                            Created = now
                        }
                    });
                }
                context.Set<Semester>().AddRange(semesters);
            }
        }

        private static void SeedAcademicYears(DbContext context)
        {
            if (!context.Set<AcademicYear>().Any())
            {
                var now = DateTime.UtcNow;
                for (int start = 2019; start <= 2031; start++)
                {
                    context.Set<AcademicYear>().Add(
                        new AcademicYear
                        {
                            Id = Guid.NewGuid(),
                            StartYear = start,
                            EndYear = start + 1,
                            Description = $"{start}/{start + 1}",
                            Created = now
                        }
                    );
                }
            }
        }



        private static void SeedGradeLevels(DbContext context)
        {
            if (!context.Set<GradeLevel>().Any())
            {
                var now = DateTime.UtcNow;
                var levels = new (string fr, string ar, string en, string desc, EducationalStageEnum stage)[]
                {
                    ("1ère Année", "السنة الأولى", "1st Year", "PreSchool year 1", EducationalStageEnum.PreSchool),
                    ("2ème Année", "السنة الثانية", "2nd Year", "PreSchool year 2", EducationalStageEnum.PreSchool),
                    ("1ère", "الأولى", "1st", "Primary 1", EducationalStageEnum.Primary),
                    ("2ème", "الثانية", "2nd", "Primary 2", EducationalStageEnum.Primary),
                    ("3ème", "الثالثة", "3rd", "Primary 3", EducationalStageEnum.Primary),
                    ("4ème", "الرابعة", "4th", "Primary 4", EducationalStageEnum.Primary),
                    ("5ème", "الخامسة", "5th", "Primary 5", EducationalStageEnum.Primary),
                    ("6ème", "السادسة", "6th", "Primary 6", EducationalStageEnum.Primary),
                    ("1ère Collège", "الأولى إعدادي", "7th", "Secondary 1", EducationalStageEnum.Secondary),
                    ("2ème Collège", "الثانية إعدادي", "8th", "Secondary 2", EducationalStageEnum.Secondary),
                    ("3ème Collège", "الثالثة إعدادي", "9th", "Secondary 3", EducationalStageEnum.Secondary),
                    ("1ère Bac", "الأولى بكالوريا", "10th", "HighSchool 1", EducationalStageEnum.HighSchool),
                    ("2ème Bac", "الثانية بكالوريا", "11th", "HighSchool 2", EducationalStageEnum.HighSchool)
                };

                foreach (var (fr, ar, en, desc, stage) in levels)
                {
                    context.Set<GradeLevel>().Add(new GradeLevel
                    {
                        Id = Guid.NewGuid(),
                        TitleFr = fr,
                        TitleAr = ar,
                        TitleEn = en,
                        Description = desc,
                        EducationalStage = stage,
                        Created = now
                    });
                }
            }
        }

        private static void SeedGradeSections(DbContext context)
        {
            var now = DateTime.UtcNow;
            var levels = context.Set<GradeLevel>().ToList();
            var existingSections = context.Set<GradeSection>().ToList();
            var sections = new List<GradeSection>();

            foreach (var level in levels)
            {
                foreach (var (nameFr, nameAr) in new[] { ("A", "ا"), ("B", "ب"), ("C", "ج") })
                {
                    var exists = existingSections.Any(s => s.GradeLevelId == level.Id && s.SectionNameFr == nameFr);
                    if (exists) continue;

                    sections.Add(new GradeSection
                    {
                        Id = Guid.NewGuid(),
                        GradeLevelId = level.Id,
                        SectionNameFr = nameFr,
                        SectionNameAr = nameAr,
                        MaxCapacity = 25,
                        Created = now
                    });
                }
            }

            if (sections.Any())
                context.Set<GradeSection>().AddRange(sections);
        }


        private static void SeedCourses(DbContext context)
        {
            if (!context.Set<Course>().Any())
            {
                var now = DateTime.UtcNow;
                var courses = new (string fr, string ar)[]
                {
                    ("Mathématiques", "رياضيات"),
                    ("Français", "فرنسية"),
                    ("Arabe", "عربية"),
                    ("Physique", "فيزياء"),
                    ("Chimie", "كيمياء"),
                    ("Biologie", "علوم الحياة"),
                    ("Informatique", "معلومات"),
                    ("Philosophie", "فلسفة"),
                    ("Histoire-Géographie", "تاريخ وجغرافيا")
                };

                foreach (var (fr, ar) in courses)
                {
                    context.Set<Course>().Add(new Course
                    {
                        Id = Guid.NewGuid(),
                        TitleFr = fr,
                        TitleAr = ar,
                        Created = now
                    });
                }
            }
        }

        private static void SeedCourseGradeMappings(DbContext context)
        {
            var now = DateTime.UtcNow;
            var levels = context.Set<GradeLevel>().ToList();
            var courses = context.Set<Course>().ToList();
            var existing = context.Set<CourseGradeMapping>().ToList();
            var mappings = new List<CourseGradeMapping>();

            foreach (var level in levels)
            {
                foreach (var course in courses)
                {
                    if (!existing.Any(e => e.GradeLevelId == level.Id && e.CourseId == course.Id))
                    {
                        mappings.Add(new CourseGradeMapping
                        {
                            Id = Guid.NewGuid(),
                            GradeLevelId = level.Id,
                            CourseId = course.Id,
                            IsElective = false,
                            Sequence = null,
                            Created = now
                        });
                    }
                }
            }

            if (mappings.Any())
                context.Set<CourseGradeMapping>().AddRange(mappings);
        }

        private static void AssignGradeSectionsToClassrooms(DbContext context)
        {
            var now = DateTime.UtcNow;
            var classrooms = context.Set<Classroom>().ToList();
            var gradeSections = context.Set<GradeSection>().ToList();
            var existingAssignments = context.Set<GradeClassroomAssignment>().ToList();
            var assignments = new List<GradeClassroomAssignment>();

            foreach (var (section, index) in gradeSections.Select((s, i) => (s, i)))
            {
                var classroom = classrooms.ElementAtOrDefault(index % classrooms.Count);
                if (classroom == null) continue;

                var exists = existingAssignments.Any(a => a.GradeSectionId == section.Id && a.ClassroomId == classroom.Id);
                if (!exists)
                {
                    assignments.Add(new GradeClassroomAssignment
                    {
                        Id = Guid.NewGuid(),
                        GradeSectionId = section.Id,
                        ClassroomId = classroom.Id,
                        Created = now
                    });
                }
            }

            if (assignments.Any())
                context.Set<GradeClassroomAssignment>().AddRange(assignments);
        }


        private static void SeedBooks(DbContext context)
        {
            if (!context.Set<Book>().Any())
            {
                var now = DateTime.UtcNow;
                var books = new (string fr, string ar, string subject, string authorFr, string authorAr, string isbn, string titleEn, string authorEn)[]
                {
                    ("Livre de Maths", "كتاب الرياضيات", "Mathématiques", "M. Karim", "السيد كريم", "9781234567890", "Math Book", "Mr. Karim"),
                    ("Livre de Science", "كتاب العلوم", "Science", "Mme. Nadia", "السيدة نادية", "9781234567891", "Science Book", "Ms. Nadia"),
                    ("Livre de Français", "كتاب الفرنسية", "Français", "M. Laurent", "السيد لوران", "9781234567892", "French Book", "Mr. Laurent"),
                    ("Livre d’Histoire", "كتاب التاريخ", "Histoire-Géographie", "Dr. Youssef", "الدكتور يوسف", "9781234567893", "History Book", "Dr. Youssef")
                };

                foreach (var (fr, ar, subject, authorFr, authorAr, isbn, titleEn, authorEn) in books)
                {
                    context.Set<Book>().Add(new Book
                    {
                        Id = Guid.NewGuid(),
                        TitleFr = fr,
                        TitleAr = ar,
                        TitleEn = titleEn,
                        Subject = subject,
                        AuthorNameFr = authorFr,
                        AuthorNameAr = authorAr,
                        AuthorNameEn = authorEn,
                        ISBN = isbn,
                        Created = now
                    });
                }
            }
        }


        private static void SeedSchoolSupplies(DbContext context)
        {
            if (!context.Set<SchoolSupply>().Any())
            {
                var now = DateTime.UtcNow;
                var supplies = new (string fr, string en, string ar)[]
                {
                    ("Stylo", "Pen", "قلم"),
                    ("Cahier", "Notebook", "دفتر"),
                    ("Règle", "Ruler", "مسطرة"),
                    ("Crayon", "Pencil", "قلم رصاص")
                };

                foreach (var (fr, en, ar) in supplies)
                {
                    context.Set<SchoolSupply>().Add(new SchoolSupply
                    {
                        Id = Guid.NewGuid(),
                        NameFr = fr,
                        NameEn = en,
                        NameAr = ar,
                        Created = now
                    });
                }
            }
        }

        private static void SeedGradeResources(DbContext context)
        {
            var now = DateTime.UtcNow;
            var levels = context.Set<GradeLevel>().ToList();
            var books = context.Set<Book>().ToList();
            var supplies = context.Set<SchoolSupply>().ToList();
            var existing = context.Set<GradeResource>().ToList();
            var resources = new List<GradeResource>();

            foreach (var level in levels)
            {
                foreach (var book in books)
                {
                    if (!existing.Any(r => r.BookId == book.Id && r.GradeLevelId == level.Id))
                    {
                        resources.Add(new GradeResource
                        {
                            Id = Guid.NewGuid(),
                            GradeLevelId = level.Id,
                            BookId = book.Id,
                            Created = now
                        });
                    }
                }

                foreach (var supply in supplies)
                {
                    var qty = supply.NameFr switch
                    {
                        "Stylo" => 3,
                        "Cahier" => 4,
                        "Crayon" => 2,
                        "Règle" => 1,
                        _ => 1
                    };

                    if (!existing.Any(r => r.SupplyId == supply.Id && r.GradeLevelId == level.Id))
                    {
                        resources.Add(new GradeResource
                        {
                            Id = Guid.NewGuid(),
                            GradeLevelId = level.Id,
                            SupplyId = supply.Id,
                            SupplyQuantity = qty,
                            Created = now
                        });
                    }
                }
            }

            if (resources.Any())
                context.Set<GradeResource>().AddRange(resources);
        }


        private static void SeedVacations(DbContext context)
        {
            if (!context.Set<Vacation>().Any())
            {
                var now = DateTime.UtcNow;
                var years = context.Set<AcademicYear>().ToList();
                var vacations = new List<Vacation>();

                foreach (var year in years)
                {
                    var start = new DateTime(year.StartYear, 12, 20);
                    var end = new DateTime(year.EndYear, 1, 3);

                    vacations.Add(new Vacation
                    {
                        Id = Guid.NewGuid(),
                        AcademicYearId = year.Id,
                        Name = VacationNameEnum.Eid1,
                        StartDate = start,
                        EndDate = end,
                        Duration = (end - start).Days,
                        Created = now
                    });
                }
                context.Set<Vacation>().AddRange(vacations);
            }
        }
    }
}






