using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmExample
{
    public class ComputerScienceStudentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private Student _student;

        public string FristName => _student.FirstName;
        public string LastName => _student.LastName;
        public IEnumerable<CourseRecord> CourseRecords => _student.CourseRecords;
        public double GPA => _student.GPA;

        public double ComputerScienceGPA
        {
            get
            {
                var points = 0.0;
                var hours = 0.0;
                foreach (var cr in _student.CourseRecords)
                {
                    if (cr.CourseName.Contains("CIS"))
                    {
                        points += (double)cr.Grade * cr.CreditHours;
                        hours += cr.CreditHours;
                    }
                }
                return points / hours;
            }
        }

        private void HandleStudentPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Student.CourseRecords) || e.PropertyName == nameof(Student.GPA))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GPA)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ComputerScienceGPA)));
            }
        }

        public ComputerScienceStudentViewModel(Student student)
        {
            _student = student;
            student.PropertyChanged += HandleStudentPropertyChanged;
        }
    }
}
