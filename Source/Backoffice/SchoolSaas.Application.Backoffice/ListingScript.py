import os
from pathlib import Path


def list_specific_dirs(root_path, target_dirs):
    root = Path(root_path).resolve()
    print(f"Scanning root directory: {root}\n")

    for dir_name in target_dirs:
        target_path = root / dir_name
        if not target_path.exists():
            print(f"⚠️ Directory not found: {target_path}")
            continue

        print(f"📁 {target_path}")
        for root_dir, dirs, files in os.walk(target_path):
            # List directories first
            for d in dirs:
                print(f"  📂 {os.path.join(root_dir, d)}")

            # List files
            for f in files:
                file_path = os.path.join(root_dir, f)
                # Highlight .cs files with a different icon
                icon = "📄 .cs " if f.lower().endswith(".cs") else "📝"
                print(f"  {icon} {file_path}")


if __name__ == "__main__":
    # Add/remove directories from this list as needed
    TARGET_DIRS = [
        # "Academics",
        # "Attendance",
        # "Courses",
        # "Enrollment",
        # "Genders",
        # "Grades",
        # "Parents",
        # "Resources",
        # "Schedules",
        # "Staff",
        # "Students",
        # "Teachers",
        # "Transfer",
        "Academic",
        "Resources",
        "School",
        "Staff",
        "Students",
        "Traceability",
    ]

    import sys

    if len(sys.argv) > 1:
        base_path = sys.argv[1]
    else:
        base_path = os.getcwd()

    list_specific_dirs(base_path, TARGET_DIRS)
