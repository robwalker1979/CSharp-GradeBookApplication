using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            this.Type = Enums.GradeBookType.Ranked;
        }
        public override char GetLetterGrade(double averageGrade)
        {
            if (Students?.Count < 5)
            {
                throw new InvalidOperationException(
                    "There are less than 5 students. Ranked-grading requires a minimum of 5 students to work.");
            }

            var bandSize = this.CalculateBandSize();

            var aGradeMin = GetMinAverageGradePerBand(bandSize, 1);
            var bGradeMin = GetMinAverageGradePerBand(bandSize, 2);
            var cGradeMin = GetMinAverageGradePerBand(bandSize, 3);
            var dGradeMin = GetMinAverageGradePerBand(bandSize, 4);
            var eGradeMin = GetMinAverageGradePerBand(bandSize, 5);

            if (averageGrade >= aGradeMin)
            {
                return 'A';
            }
            else if (averageGrade >= bGradeMin)
            {
                return 'B';
            }
            else if (averageGrade >= cGradeMin)
            {
                return 'C';
            }
            else if (averageGrade >= dGradeMin)
            {
                return 'D';
            }
            else 
            {
                return 'F';
            }
        }

        private double GetMinAverageGradePerBand(int bandSize, int bandNumber) 
        {
            return Students.OrderByDescending(s => s.AverageGrade)
                                    .Skip((bandSize * bandNumber) - 1)
                                    .Take(1)
                                    .Single()
                                    .AverageGrade;
        }

        private int CalculateBandSize()
        {
            var numberOfStudents = this.Students?.Count ?? 0;
            return Convert.ToInt32(Math.Ceiling(numberOfStudents * 0.2));
        }

        public override void CalculateStatistics()
        {
            if (this.Students?.Count() < 5) 
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (this.Students?.Count() < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStudentStatistics(name);
        }
    }
}
