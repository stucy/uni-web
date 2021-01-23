﻿using Microsoft.AspNetCore.Mvc;
using Schools.Services.Interfaces;
using Schools.ViewModels;
using System.Threading.Tasks;

namespace Schools.Controllers
{
  public class StudentController : Controller
  {
    public readonly IUserService userService;
    public readonly IClassService classService;
    public readonly IStudentService studentService;

    public StudentController(IUserService userService, IClassService classService, IStudentService studentService)
    {
      this.userService = userService;
      this.classService = classService;
      this.studentService = studentService;
    }

    [HttpGet]
    public IActionResult Edit()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(StudentEditViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }

      await this.userService.UpdatePersonalData(model.UserEditModel);

      await this.studentService.SaveStudentClass(model.UserEditModel.UserId, model.NewClassId.Value);

      return View();
    }
  }
}