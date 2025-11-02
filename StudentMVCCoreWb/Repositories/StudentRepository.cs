using Microsoft.EntityFrameworkCore;
using StudentMVCCoreWb.Data;
using StudentMVCCoreWb.Entities;

namespace StudentMVCCoreWb.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext _context;

        public StudentRepository(StudentDbContext context)
        {
            _context = context;
        }

        public async Task AddStudent(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetStudentById(int id)
        {
            var student = await _context.Students.FindAsync(id);
            return student!;
        }

        public async Task UpdateStudent(Student student)
        {
            await _context.Students.Where(s => s.Id == student.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(s => s.Name, student.Name)
                    .SetProperty(s => s.Email, student.Email)
                    .SetProperty(s => s.MobileNumber, student.MobileNumber)
                    .SetProperty(s => s.Address, student.Address)
                    .SetProperty(s => s.DateOfBirth, student.DateOfBirth)
                );

            await _context.SaveChangesAsync();
        }
    }
}
