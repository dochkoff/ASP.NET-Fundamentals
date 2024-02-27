using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data;
using SeminarHub.Data.Models;
using SeminarHub.Models;
using System.Globalization;
using System.Security.Claims;
using static SeminarHub.Data.DataConstants;

namespace SeminarHub.Controllers
{
    [Authorize]
    public class SeminarController : Controller
    {
        private readonly SeminarHubDbContext data;

        public SeminarController(SeminarHubDbContext context)
        {
            this.data = context;
        }

        
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new SeminarFormViewModel();
            model.Categories = await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SeminarFormViewModel model)
        {
            DateTime dateAndTime = DateTime.Now;

            if (!DateTime.TryParseExact(
                model.DateAndTime,
                DataConstants.DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out dateAndTime))
            {
                ModelState
                    .AddModelError(nameof(model.DateAndTime), $"Invalid date. Format must be {DataConstants.DateFormat}");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();

                return View(model);
            }

            var entity = new Seminar()
            {
                Topic = model.Topic,
                Lecturer = model.Lecturer,
                Details = model.Details,
                DateAndTime = dateAndTime,
                Duration = model.Duration,
                CategoryId = model.CategoryId,
                OrganizerId = GetUserId()
            };

            await data.Seminars.AddAsync(entity);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> All()
        {
            var seminars = await data.Seminars
                .Select(s => new SeminarInfoViewModel(
                    s.Id,
                    s.Topic,
                    s.Lecturer,
                    s.DateAndTime,
                    s.Category.Name,
                    s.Organizer.UserName
                    ))
                .ToListAsync();

            return View(seminars);
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = GetUserId();

            var model = await data.SeminarsParticipants
                .Where(sp => sp.ParticipantId == userId)
                .AsNoTracking()
                .Select(sp => new SeminarInfoViewModel(
                    sp.SeminarId,
                    sp.Seminar.Topic,
                    sp.Seminar.Lecturer,
                    sp.Seminar.DateAndTime,
                    sp.Seminar.Category.Name,
                    sp.Seminar.Organizer.UserName
                    ))
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var seminar = await data.Seminars
                .FindAsync(id);

            if (seminar == null)
            {
                return BadRequest();
            }

            if (seminar.OrganizerId != GetUserId())
            {
                return Unauthorized();
            }

            var model = new SeminarFormViewModel()
            {
                Topic = seminar.Topic,
                Lecturer = seminar.Lecturer,
                Details = seminar.Details,
                DateAndTime = seminar.DateAndTime.ToString(DataConstants.DateFormat),
                Duration = seminar.Duration,
                CategoryId = seminar.CategoryId,
                Categories = await GetCategories()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SeminarFormViewModel model, int id)
        {
            var seminar = await data.Seminars
                .FindAsync(id);

            if (seminar == null)
            {
                return BadRequest();
            }

            if (seminar.OrganizerId != GetUserId())
            {
                return Unauthorized();
            }

            DateTime dateAndTime = DateTime.Now;

            if (!DateTime.TryParseExact(
                model.DateAndTime,
                DataConstants.DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out dateAndTime))
            {
                ModelState
                    .AddModelError(nameof(model.DateAndTime), $"Invalid date. Format must be {DataConstants.DateFormat}");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();

                return View(model);
            }

            seminar.Topic = model.Topic;
            seminar.Lecturer = model.Lecturer;
            seminar.Details = model.Details;
            seminar.DateAndTime = dateAndTime;
            seminar.Duration = model.Duration;
            seminar.CategoryId = model.CategoryId;
            seminar.OrganizerId = GetUserId();

            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var s = await data.Seminars
                .Where(s=> s.Id == id)
                .Include(e => e.SeminarsParticipants)
                .FirstOrDefaultAsync();

            if (s == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (!s.SeminarsParticipants.Any(p => p.ParticipantId == userId))
            {
                s.SeminarsParticipants.Add(new SeminarParticipant()
                {
                    SeminarId = s.Id,
                    ParticipantId = userId
                });

                await data.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Leave(int id)
        {
            var e = await data.Seminars
                .Where(e => e.Id == id)
                .Include(e => e.SeminarsParticipants)
                .FirstOrDefaultAsync();

            if (e == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            var ep = e.SeminarsParticipants
                .FirstOrDefault(ep => ep.ParticipantId == userId);

            if (ep == null)
            {
                return BadRequest();
            }

            e.SeminarsParticipants.Remove(ep);

            await data.SaveChangesAsync();

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await data.Seminars
                .Where(s => s.Id == id)
                .AsNoTracking()
                .Select(s => new SeminarDetailsVewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    Lecturer = s.Lecturer,
                    Details = s.Details,
                    DateAndTime = s.DateAndTime.ToString(DateFormat),
                    Organizer = s.Organizer.UserName,
                    Duration = s.Duration,
                    Category = s.Category.Name
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var seminar = await data.Seminars
                .FindAsync(id);

            if (seminar == null)
            {
                return BadRequest();
            }

            if (seminar.OrganizerId != GetUserId())
            {
                return Unauthorized();
            }

            var model = await data.Seminars
                .Where(s => s.Id == id)
                .AsNoTracking()
                .Select(s => new SeminarDetailsVewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,    
                    DateAndTime = s.DateAndTime.ToString(DateFormat),
                    Organizer = s.Organizer.UserName,
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(SeminarDetailsVewModel model)
        {
            var seminar = await data.Seminars
                .FindAsync(model.Id);

            if (seminar == null)
            {
                return BadRequest();
            }

            if (seminar.OrganizerId != GetUserId())
            {
                return Unauthorized();
            }

            data.Seminars.Remove(seminar);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }


        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        private async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            return await data.Categories
                .AsNoTracking()
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }
    }
}
