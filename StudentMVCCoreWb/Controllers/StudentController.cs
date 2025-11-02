using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentMVCCoreWb.Repositories;

namespace StudentMVCCoreWb.Controllers
{
    public class StudentController(ILogger<StudentController> logger, IStudentRepository studentRepository, IMapper mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var students = await studentRepository.GetAllStudents();
            var viewModel = mapper.Map<IEnumerable<Models.StudentViewModel>>(students);
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await studentRepository.GetStudentById(id);
            if (student == null)
            {
                logger.LogWarning("Student with ID {StudentId} not found.", id);
                return NotFound();
            }
            var viewModel = mapper.Map<Models.StudentViewModel>(student);
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                var student = mapper.Map<Entities.Student>(studentViewModel);
                await studentRepository.AddStudent(student);
                logger.LogInformation("Created new student with ID {StudentId}.", student.Id);
                return RedirectToAction(nameof(Index));
            }
            return View(studentViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await studentRepository.GetStudentById(id);
            if (student == null)
            {
                logger.LogWarning("Student with ID {StudentId} not found for edit.", id);
                return NotFound();
            }
            var viewModel = mapper.Map<Models.StudentViewModel>(student);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Models.StudentViewModel studentViewModel)
        {
            if (id != studentViewModel.Id)
            {
                logger.LogWarning("Student ID mismatch for edit: {StudentId} != {ViewModelId}.", id, studentViewModel.Id);
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var student = mapper.Map<Entities.Student>(studentViewModel);
                await studentRepository.UpdateStudent(student);
                logger.LogInformation("Updated student with ID {StudentId}.", student.Id);
                return RedirectToAction(nameof(Index));
            }
            return View(studentViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var student = await studentRepository.GetStudentById(id);
            if (student == null)
            {
                logger.LogWarning("Student with ID {StudentId} not found for deletion.", id);
                return NotFound();
            }
            var viewModel = mapper.Map<Models.StudentViewModel>(student);
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await studentRepository.DeleteStudent(id);
            logger.LogInformation("Deleted student with ID {StudentId}.", id);
            return RedirectToAction(nameof(Index));
        }
    }
}