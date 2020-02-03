using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using DatingApp.API.Helpers;

namespace DatingApp.API.Data
{
    public class ActividadRepository : IActividadRepository
    {
        private readonly DataContext _context;
        public ActividadRepository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

     
        public async Task<IList<Actividad>> GetActividad(int idUser)
        {
            List<Actividad> lActividad = await _context.Actividades.Where(x => x.User.Id ==idUser).ToListAsync();
            return  lActividad ;
        }

        public async Task<PagedList<Actividad>> GetActividadesPaginado(UserParams userParams)
        {
            var actividades = _context.Actividades.Where(x => x.User.Id == userParams.UserId);          

            return await PagedList<Actividad>.CreateAsync(actividades, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<Actividad> GetActivity(int id)
        {
            var activity = await _context.Actividades.FirstOrDefaultAsync(a => a.Id == id);
            return activity;
        }

        public async Task<Actividad> Register(Actividad actividad)
        {
            // Agregar el objeto al contexto
            await _context.AddAsync(actividad);
            // Realizar el guardado en nuestro repositorio(Base de Datos)
            await _context.SaveChangesAsync();
            return actividad;
        }

        public  async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async void   Update<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }



    }
}