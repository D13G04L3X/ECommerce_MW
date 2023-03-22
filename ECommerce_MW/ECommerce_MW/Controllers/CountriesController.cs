using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; 
using ECommerce_MW.DAL;
using ECommerce_MW.DAL.Entities;
using ECommerce_MW.Models;

namespace ECommerce_MW.Controllers
{
    public class CountriesController : Controller       //Hereda de controller
    {
        private readonly DatabaseContext _context;          //Prop privada, readonly, es un modificador parecido al constraint, siempre se debe dar valor a las variables que se inicializan

        public CountriesController(DatabaseContext context)     //Constructor
        {
            _context = context;
        }

        // GET: Countries
        public async Task<IActionResult> Index()                //Métodos o acciones
        {
            return View(await _context.Countries.Include(c => c.States).ToListAsync());
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.Id == id);          //Me permite traer un dato para hacer match
            if (country == null) return NotFound();
            
            return View(country);
        }

        public IActionResult Create()         //Tipo de retorno, es una interfaz que se usa para retornar cualquier tipo de cosa, por buenas prácticas de desarrollo se crea automáticamente
        {
            return View();                    //Puede retornar cualquier tipo de cosa
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country)             //[Bind("Name,Id,CreatedDate,ModifiedDate")] se borró, para que a futuro me permita ingresar nuevos valores, por ejemplo, la capital par ael country
        {
            if (ModelState.IsValid)                                 //El mensaje se muestra desde acá             //En notación diamante (< >) se devuelve el dato
            {
               _context.Add(country);
                //Aquí se edita y se coloca el código del teacher para controlar el error de la llave duplicada y se puede implementar este mismo código para el edit

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un país con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(country);
            //Primera forma del código
            //if (ModelState.IsValid)                                          //El mensaje se muestra desde acá             //En notación diamante (< >) se devuelve el dato
            //{
            //    
            //    country.Id = Guid.NewGuid();
            //    _context.Add(country);                                       //.Add es como un insert into, está bajo memoria
            //    await _context.SaveChangesAsync();                           //SaveChangesAsync(), es el método de entity framework que guarda la info
            //    return RedirectToAction(nameof(Index));                      //nameof, es la referencia a donde va a ir, evalua si es correcto o no
            //}
            //return View(country);
        }

        // GET: Countries/Edit/5            
        public async Task<IActionResult> Edit(Guid? id)             //Está nuleable
        {
            if (id == null || _context.Countries == null)           //El ya trae una data
            {
                return NotFound();
            }

            var country = await _context.Countries.FindAsync(id);       //Programación asíncrona, el (FindAsync(id)) busca el registro por la clave primaria
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]                                                          //Actualización en Base de datos
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Country country)     // [Bind("Name,Id,CreatedDate,ModifiedDate")] Esta línea se borró, por la misma razón de la de arriba
        {
            if (ModelState.IsValid)
            {
                //Aquí se edita y se coloca el código del teacher para controlar el error de la llave duplicada y se puede implementar este mismo código para el edit

                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un país con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(country);

        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Countries == null)
            {
                return Problem("Entity set 'DatabaseContext.Countries'  is null.");
            }
            var country = await _context.Countries.FindAsync(id);
            if (country != null)
            {
                _context.Countries.Remove(country);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AddState(Guid? countryId)
        {
            if (countryId == null)
            {
                return NotFound();
            }

            Country country = await _context.Countries.FindAsync(countryId);
            if (country == null)
            {
                return NotFound();
            }

            StateViewModel stateViewModel = new()
            {
                CountryId = country.Id,
            };

            return View(stateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddState(StateViewModel stateViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    State state = new State()
                    {
                        Cities = new List<City>(),
                        Country = await _context.Countries.FindAsync(stateViewModel.CountryId),
                        Name = stateViewModel.Name,
                        CreatedDate = stateViewModel.CreatedDate,
                        ModifiedDate = DateTime.Now,
                    };

                    _context.Add(state);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = stateViewModel.CountryId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un Dpto/Estado con el mismo nombre en este país.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(stateViewModel);
        }
    }
}
