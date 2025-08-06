# Set the base path (adjust to your actual base folder)
$basePath = (Get-Location).Path
# Define the full directory structure (one path per line)
$structure = @"
Teachers/Commands/CreateTeacher
Teachers/Commands/UpdateTeacher
Teachers/Commands/DeleteTeacher
Teachers/Commands/UpdateEmploymentStatus
Teachers/Queries/GetTeacherById
Teachers/Queries/GetPaginatedTeachers
Teachers/Queries/GetAssignedCourses
Students/Commands/CreateStudent
Students/Commands/UpdateStudent
Students/Commands/BulkUpdateStudents
Students/Commands/TransitionStudentStatus
Students/Commands/UpdateStudentDetails
Students/Queries/GetPaginatedStudents
Students/Queries/GetStudentWithDetails
Students/Queries/GetStudentParents
Students/Queries/GetStudentHistory
Staff/Commands/CreateStaff
Staff/Commands/UpdateStaff
Staff/Commands/BulkCreateStaff
Staff/Commands/DeleteStaff
Staff/Commands/AssignRole
Staff/Queries/GetStaffById
Staff/Queries/GetPaginatedStaff
Staff/Queries/GetStaffAuditLogs
Parents/Commands/CreateParent
Parents/Commands/UpdateParent
Parents/Commands/BulkCreateParents
Parents/Commands/DeleteParent
Parents/Commands/VerifyParentIdentity
Parents/Queries/GetParentById
Parents/Queries/GetPaginatedParents
Parents/Queries/GetAssociatedStudents
Parents/Queries/GetParentAuditLogs
Courses/Commands/CreateCourse
Courses/Commands/UpdateCourse
Courses/Commands/DeleteCourse
Courses/Commands/AssignTeacherToCourse
Courses/Commands/BulkAssignTeachers
Courses/Commands/UnassignTeacher
Courses/Commands/MapCourseToGrade
Courses/Commands/UnmapCourseFromGrade
Courses/Queries/GetCourseById
Courses/Queries/GetPaginatedCourses
Courses/Queries/GetCoursesByGrade
Courses/Queries/GetCourseMappings
Courses/Queries/GetCoursesAssignedToTeacher
Grades/Commands/RecordGrade
Grades/Commands/UpdateGrade
Grades/Commands/BulkRecordGrades
Grades/Commands/BulkUpdateGrades
Grades/Commands/SubmitGradeAppeal
Grades/Commands/CalculateFinalGrades
Grades/Queries/GetPaginatedGrades
Grades/Queries/GetGradeStatistics
Grades/Queries/GetGradeAppeals
Grades/Queries/GetGradeHistory
Grades/Queries/CalculateGPA
Grades/Queries/GetGradesByTeacher
Attendance/Commands/RecordAttendance
Attendance/Commands/UpdateAttendance
Attendance/Commands/BulkRecordAttendance
Attendance/Commands/SubmitAttendanceExcuse
Attendance/Commands/DeleteAttendance
Attendance/Queries/GetAttendanceRecords
Attendance/Queries/GetAttendanceById
Attendance/Queries/GetPaginatedAttendance
Attendance/Queries/GetAttendanceHistory
Attendance/Queries/GetAttendanceSummary
Enrollment/Commands/SubmitEnrollment
Enrollment/Commands/UpdateEnrollmentStatus
Enrollment/Commands/BulkUpdateEnrollmentStatus
Enrollment/Commands/MoveEnrollmentToNextStep
Enrollment/Commands/UploadEnrollmentDocument
Enrollment/Commands/BulkUploadDocuments
Enrollment/Commands/VerifyEnrollmentDocument
Enrollment/Queries/GetEnrollment
Enrollment/Queries/GetPaginatedEnrollments
Enrollment/Queries/GetEnrollmentDashboard
Enrollment/Queries/GetEnrollmentHistory
Schedules/Commands/CreateSchedule
Schedules/Commands/UpdateSchedule
Schedules/Commands/DeleteSchedule
Schedules/Commands/AutoGenerateSchedules
Schedules/Queries/GetScheduleByGradeSection
Schedules/Queries/GetTeacherSchedule
Schedules/Queries/GetClassroomSchedule
Schedules/Queries/CheckConflict
Schedules/Queries/GetTeacherAssignments
Resources/Commands/CreateSchoolSupply
Resources/Commands/UpdateSchoolSupply
Resources/Commands/BulkCreateSupplies
Resources/Commands/CreateBook
Resources/Commands/UpdateBook
Resources/Commands/AssignResourceToGrade
Resources/Commands/BulkAssignResources
Resources/Queries/GetPaginatedSupplies
Resources/Queries/GetPaginatedResources
Communications/Commands/SendNotification
Communications/Commands/UpdateNotificationStatus
Communications/Commands/CreateAndSendNotification
Communications/Commands/MarkAllNotificationsRead
Communications/Commands/SendMessage
Communications/Commands/AddComment
Communications/Commands/UpdateComment
Communications/Queries/GetAllNotifications
Communications/Queries/GetMessagesForUser
Communications/Queries/GetCommentsForEntity
Academics/Commands/CreateAcademicYear
Academics/Commands/UpdateAcademicYear
Academics/Commands/CreateSemester
Academics/Queries/GetCurrentAcademicYear
Academics/Queries/GetSemesterById
Academics/Queries/GetPaginatedSemesters
"@

# Create directories and base files
$structure.Split("`n") | ForEach-Object {
    $relativePath = $_.Trim()
    if (-not [string]::IsNullOrWhiteSpace($relativePath)) {
        # Compute full directory path
        $dirPath = Join-Path $basePath $relativePath

        # Create directory structure if not exists
        New-Item -Path $dirPath -ItemType Directory -Force | Out-Null

        # Determine if this directory is for Commands or Queries
        if ($relativePath -match "Commands|Queries") {
            # Get the folder name (for example, "CreateTeacher" or "GetTeacherById")
            $folderName = ($relativePath -split '/')[ -1 ]
            # Append "Command" or "Query" to the filename if not already done
            if ($relativePath -match "Commands") {
                if (-not $folderName.EndsWith("Command", [System.StringComparison]::OrdinalIgnoreCase)) {
                    $fileName = "$folderName" + "Command.cs"
                } else {
                    $fileName = "$folderName.cs"
                }
            }
            elseif ($relativePath -match "Queries") {
                if (-not $folderName.EndsWith("Query", [System.StringComparison]::OrdinalIgnoreCase)) {
                    $fileName = "$folderName" + "Query.cs"
                } else {
                    $fileName = "$folderName.cs"
                }
            }
            else {
                $fileName = "$folderName.cs"
            }

            # Build file path (create file if doesn't exist)
            $filePath = Join-Path $dirPath $fileName
            # Determine the namespace based on the relative path (replace "/" with ".")
            $namespace = ($relativePath -replace '/', '.').Trim()
            # Remove any trailing "Commands" or "Queries" for the DTO name if desired
            $dtoType = $folderName
            if ($relativePath -match "Commands") {
                $dtoType = $folderName -replace "Command$", ""
            }
            elseif ($relativePath -match "Queries") {
                $dtoType = $folderName -replace "Query$", ""
            }

            # Create basic record class content using MediatR IRequest interface
            $content = @"
namespace $namespace;

public record ${folderName}$(if($relativePath -match "Commands"){"Command"}else{"Query"})( 
    $(if($relativePath -match "Commands"){ "$dtoType" + "DTO Dto" } else { "$dtoType" + "FilterDTO Filter" })
) : IRequest$(if($relativePath -match "Queries"){ "<PagedResult<$dtoType" + "DTO>>" } else { "<bool>" });
"@
            # Write the content to the file (overwrite if already exists)
            Set-Content -Path $filePath -Value $content
            Write-Host "Created: $filePath" -ForegroundColor Green
        }
    }
}

Write-Host "Structure created successfully!" -ForegroundColor Green

# --- Optional: Renaming existing files ---
# If you already have code files in the base path and you want to append Query or Command to file names in Queries or Commands directories respectively,
# you can run the following block:

Get-ChildItem -Path $basePath -Recurse -File | ForEach-Object {
    $parentFolder = $_.DirectoryName
    if ($parentFolder -match "Commands") {
        if (-not $_.Name.EndsWith("Command.cs", [System.StringComparison]::OrdinalIgnoreCase)) {
            $newName = ($_.BaseName + "Command" + $_.Extension)
            Rename-Item -Path $_.FullName -NewName $newName -Force
            Write-Host "Renamed to: $newName" -ForegroundColor Yellow
        }
    }
    elseif ($parentFolder -match "Queries") {
        if (-not $_.Name.EndsWith("Query.cs", [System.StringComparison]::OrdinalIgnoreCase)) {
            $newName = ($_.BaseName + "Query" + $_.Extension)
            Rename-Item -Path $_.FullName -NewName $newName -Force
            Write-Host "Renamed to: $newName" -ForegroundColor Yellow
        }
    }
}
