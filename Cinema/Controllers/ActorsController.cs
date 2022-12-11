﻿using CinemaApp.Data;
using CinemaApp.Data.Services;
using CinemaApp.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync(m => m.Movies);
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureUrl,Bio")]Actor actor)
        {
            if(!ModelState.IsValid)
            {
                return View(actor);
            }
            else
            {
                await _service.AddAsync(actor);
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var actor = await _service.GetByIdAsync(id);
            if(actor == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(actor);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var actor = await _service.GetByIdAsync(id);

            if (actor == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(actor);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [ Bind("Id,FullName,ProfilePictureUrl,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            else
            {
                await _service.UpdateAsync(actor);
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var actor = await _service.GetByIdAsync(id);

            if (actor == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(actor);
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _service.GetByIdAsync(id);
            if (actor == null)
            {
                return View("NotFound");
            }

            await _service.DeleteAsync(id);
            return RedirectToAction("Index");           
        }
    }
}
